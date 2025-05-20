using System.ComponentModel.DataAnnotations;

namespace APBD11.DTOs;

public class PrescriptionMedicamentDto
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public int Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
}