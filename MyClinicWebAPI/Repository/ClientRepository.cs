using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyClinicWebAPI.DataLayer;
using MyClinicWebAPI.Interfaces;
using MyClinicWebAPI.Models;
using System.Data;
namespace MyClinicWebAPI.Repository
{
    public class ClientRepository : IClient
    {
        private readonly ApplicationDBContext _dbContext;

        public ClientRepository(ApplicationDBContext _context)
        {
            _dbContext = _context;

        }
        public async Task<IEnumerable<ClientModel>> GetAllClient()
        {
            return await _dbContext.Client.ToListAsync();
        }

        public async Task<ClientModel> GetClientByIDAsync(int id)
        {
            return await _dbContext.Client.Where(c => c.ID == id).FirstOrDefaultAsync();
        }
    }
}
