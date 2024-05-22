using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyClinic.DataLayer;
using MyClinic.Interfaces;
using MyClinic.Models;

namespace MyClinic.Repository
{
    public class ClientRepository : IClient
    {
        private readonly ApplicationDBContext _context;
        public ClientRepository(ApplicationDBContext context)
        {
            this._context = context;
        }

        public bool Add(ClientModel client)
        {
            _context.Add(client);
            return Save();
        }

        public bool Delete(ClientModel client)
        {
            _context.Remove(client);
            return Save();
        }

        public async Task<IEnumerable<ClientModel>> GetAllClient()
        {
            return await _context.Client.ToListAsync();
        }

        public async Task<ClientModel> GetClientByIDAsync(int id)
        {
            return await _context.Client.FirstOrDefaultAsync(i => i.ID == id);
        }

        
        public async Task<ClientModel> GetClient(ClientModel _client) 
        {
            PasswordHasher<object> pwd = new PasswordHasher<Object?>();
            ClientModel c = await _context.Client.FirstOrDefaultAsync(i =>  i.Email == _client.Email);
            if(c != null)
            {
                PasswordVerificationResult ver = pwd.VerifyHashedPassword(null, c.Password, _client.Password);
                if (ver == PasswordVerificationResult.Success)
                    return c;
            }
            
            return null;
        }

        public async Task<IEnumerable<RoleModel>> GetClientRolesByIDAsync(int id)
        {
            //return await _context.ClientRole.Where(cr => cr.IDClient == id).ToListAsync();
            return await _context.Role.Where(cr => cr.ClientRole.IDClient == id).ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool Update(ClientModel client)
        {
            _context.Update(client);
            return Save();
        }
    }
}
