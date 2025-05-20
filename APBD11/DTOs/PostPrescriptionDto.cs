using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace APBD11.DTOs;

public class PostPrescriptionDto :  IValidatableObject
{
    [Required]
    public PatientDTO Patient { get; set; }
    [Required]
    [MaxLength(10)]
    public ICollection<PrescriptionMedicamentDto> Medicaments { get; set; }
    [Required]
    public DateTime? Date { get; set; }
    [Required]
    public DateTime? DueDate { get; set; }
    [Required]
    public int? IdDoctor { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DueDate < Date)
        {
            yield return new ValidationResult(
                $"DueDate must be greater than or equal to Date",
                [nameof(DueDate)]);
        }
    }
}