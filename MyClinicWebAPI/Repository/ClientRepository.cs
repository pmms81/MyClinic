using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> ClientExists(int id)
        {
            return await _dbContext.Client.AnyAsync(c => c.ID == id);
        }

        public async Task<ClientModel> GetClient(ClientModel _client)
        {
            PasswordHasher<object> pwd = new PasswordHasher<Object?>();
            ClientModel ?c = await _dbContext.Client.FirstOrDefaultAsync(i => i.Email == _client.Email);
            if (c != null)
            {
                PasswordVerificationResult ver = pwd.VerifyHashedPassword(null, c.Password, _client.Password);
                if (ver == PasswordVerificationResult.Success)
                    return c;
            }

            return null;
        }
    }
}
