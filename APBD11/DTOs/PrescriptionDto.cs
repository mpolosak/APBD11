namespace APBD11.DTOs;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<MedicamentDto> Medicaments { get; set; }
    public DoctorDto Doctor { get; set; }
}