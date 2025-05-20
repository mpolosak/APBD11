using APBD11.DTOs;

namespace APBD11.Services;

public interface IPrescriptionService
{
    public Task<int> CreatePrescriptionAsync(PostPrescriptionDto prescription);
    public Task<PatientDetailsDto> GetPatientAsync(int id);
}