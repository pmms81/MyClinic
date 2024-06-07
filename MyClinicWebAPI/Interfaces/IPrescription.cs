using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Interfaces
{
    public interface IPrescription
    {
        Task<IEnumerable<PrescriptionModel>> GetAllPrescription();
        Task<bool> CreateNewPrescription(PrescriptionModel _prescription);
        bool UpdatePrescription(PrescriptionModel _prescription);
        Task<bool> PrescriptionExist(int idPresciption);
        bool Save();
    }
}
