using System.ComponentModel.DataAnnotations;

namespace APBD11.DTOs;

public class PostPrescriptionDto
{
    [Required]
    public PatientDTO Patient { get; set; }
    [Required]
    public ICollection<PrescriptionMedicamentDto> Medicaments { get; set; }
    [Required]
    public DateTime? Date { get; set; }
    [Required]
    public DateTime? DueDate { get; set; }
}