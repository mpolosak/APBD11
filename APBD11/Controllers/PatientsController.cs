using APBD11.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IPrescriptionService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        var patient = await service.GetPatientAsync(id);
        return Ok(patient);
    }
}