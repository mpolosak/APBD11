using APBD11.DTOs;

namespace APBD11.Services;

public class PrescriptionService : IPrescriptionService
{
    public async Task<int> CreatePrescriptionAsync(PostPrescriptionDto prescription)
    {
        throw new NotImplementedException();
    }

    public async Task<PatientDetailsDto> GetPatientAsync(int id)
    {
        throw new NotImplementedException();
    }
}