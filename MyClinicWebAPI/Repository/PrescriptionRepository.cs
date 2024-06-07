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
            return Save();
        }

        public bool UpdatePrescription(PrescriptionModel _prescription)
        {
            _dbContext.Prescription.Update(_prescription);
            return Save();
        }

        public async Task<bool> PrescriptionExist(int idPrescription)
        {
            return await _dbContext.Prescription.AnyAsync(p => p.IDPrescription == idPrescription);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0 ? true : false;
        }

    }
}
