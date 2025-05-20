using System.Runtime.InteropServices.JavaScript;
using APBD11.Data;
using APBD11.DTOs;
using APBD11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Services;

public class PrescriptionService(DatabaseContext context) : IPrescriptionService
{
    public async Task<int> CreatePrescriptionAsync(PostPrescriptionDto prescription)
    {
        if(!await context.Doctors.AnyAsync(d => d.IdDoctor == prescription.IdDoctor))
            throw new NotFoundException("Doctor not found");
        
        foreach (var medicament in prescription.Medicaments)
            if(!await context.Medicaments.AnyAsync(m => m.IdMedicament == medicament.IdMedicament))
                throw new NotFoundException($"Medicament with id {medicament.IdMedicament} not found");
        var idPatient = prescription.Patient.IdPatient;
        var patientExist = await context.Patients.AnyAsync(p
            => p.IdPatient == idPatient);
        if (!patientExist)
        {
            var entry = await context.Patients.AddAsync(new Patient()
            {
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = DateOnly.FromDateTime(prescription.Patient.Birthdate ?? DateTime.Now),
            });
            await context.SaveChangesAsync();
            idPatient = entry.Entity.IdPatient;
        }
        var newEntry = await context.Prescriptions.AddAsync(new Prescription()
        {
            IdPatient = idPatient,
            PrescriptionMedicaments = prescription.Medicaments.Select(m => new PrescriptionMedicament()
            {
                IdMedicament = m.IdMedicament,
                Details = m.Details,
                Dose = m.Dose,
            }).ToList(),
            IdDoctor = prescription.IdDoctor??-1,
            Date = DateOnly.FromDateTime(prescription.Date ?? DateTime.Now),
            DueDate = DateOnly.FromDateTime(prescription.DueDate ?? DateTime.Now),
        });
        await context.SaveChangesAsync();
        return newEntry.Entity.IdPrescription;
    }

    public async Task<PatientDetailsDto> GetPatientAsync(int id)
    {
        var patient = await context.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => new PatientDetailsDto {
            IdPatient = p.IdPatient,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Birthdate = p.Birthdate.ToDateTime(TimeOnly.MinValue),
            Prescriptions = p.Prescriptions.Select(pr => new PrescriptionDto()
            {
                IdPrescription = pr.IdPrescription,
                Date = pr.Date.ToDateTime(TimeOnly.MinValue),
                DueDate = pr.DueDate.ToDateTime(TimeOnly.MinValue),
                Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentDto()
                {
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Type = pm.Medicament.Type,
                    Dose = pm.Dose,
                    Details = pm.Details,
                }).ToList(),
                Doctor = new DoctorDto()
                {
                    IdDoctor = pr.IdDoctor,
                    FirstName = pr.Doctor.FirstName,
                    LastName = pr.Doctor.LastName,
                    Email = pr.Doctor.Email,
                }
            }).ToList()
        }).FirstOrDefaultAsync();
        if (patient is null)
        {
            throw new NotFoundException("Patient not found");
        }
        return patient;
    }
}