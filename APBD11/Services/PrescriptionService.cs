using APBD11.Data;
using APBD11.DTOs;
using APBD11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD11.Services;

public class PrescriptionService(DatabaseContext context) : IPrescriptionService
{
    public async Task<int> CreatePrescriptionAsync(PostPrescriptionDto prescription)
    {
        throw new NotImplementedException();
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