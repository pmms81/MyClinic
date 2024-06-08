using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Interfaces
{
    public interface IPrescription
    {
        Task<IEnumerable<PrescriptionModel>> GetAllPrescription();
        Task<bool> CreateNewPrescription(PrescriptionModel _prescription);
        Task<bool> UpdatePrescription(PrescriptionModel _prescription);
        Task<bool> PrescriptionExist(int idPresciption);
        Task<bool> DeletePrescription(PrescriptionModel _prescription);
        Task<bool> Save();
    }
}
