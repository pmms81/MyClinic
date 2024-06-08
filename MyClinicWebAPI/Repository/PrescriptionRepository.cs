using Microsoft.EntityFrameworkCore;
using MyClinicWebAPI.DataLayer;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Repository
{
    public class PrescriptionRepository : IPrescription
    {
        private readonly ApplicationDBContext _dbContext;
        
        public PrescriptionRepository(ApplicationDBContext _context)
        {
            this._dbContext = _context;
        }

        public async Task<IEnumerable<PrescriptionModel>> GetAllPrescription()
        {
            return await _dbContext.Prescription.ToListAsync();
        }

        public async Task<bool> CreateNewPrescription(PrescriptionModel _prescription)
        {
            await _dbContext.Prescription.AddAsync(_prescription);
            return await Save();
        }

        public async Task<bool> UpdatePrescription(PrescriptionModel _prescription)
        {
            _dbContext.Prescription.Update(_prescription);
            return await Save();
        }

        public async Task<bool> PrescriptionExist(int idPrescription)
        {
            return await _dbContext.Prescription.AnyAsync(p => p.IDPrescription == idPrescription);
        }

        public async Task<bool> DeletePrescription(PrescriptionModel _prescription)
        {
            _dbContext.Prescription.Remove(_prescription);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

    }
}
