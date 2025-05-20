using Microsoft.AspNetCore.Mvc;

namespace APBD11.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPatient(int id)
    {
        throw new System.NotImplementedException();
    }
}