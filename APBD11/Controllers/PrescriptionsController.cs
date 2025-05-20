using APBD11.DTOs;
using APBD11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController(IPrescriptionService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostPrescription([FromBody] PostPrescriptionDto prescription)
    {
        var id =  await service.CreatePrescriptionAsync(prescription);
        return Created($"api/Prescriptions/{id}", id);
    }
}