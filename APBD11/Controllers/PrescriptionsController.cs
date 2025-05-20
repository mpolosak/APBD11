using APBD11.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostPrescription([FromBody] PostPrescriptionDto prescription)
    {
        throw new System.NotImplementedException();
    }
}