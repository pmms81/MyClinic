
using MyClinic.Models;

namespace MyClinic.Interfaces
{
    public interface IClient
    {
        Task<IEnumerable<ClientModel>> GetAllClient();
        Task<ClientModel> GetClientByIDAsync(int id);
        Task<ClientModel> GetClient(ClientModel _client);
        //Task<IEnumerable<ClientRoleModel>> GetClientRolesByIDAsync(int id);
        Task<IEnumerable<RoleModel>> GetClientRolesByIDAsync(int id);
        bool Add(ClientModel client);
        bool Update(ClientModel client);
        bool Delete(ClientModel client);
        bool Save();
    }
}
