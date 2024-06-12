using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Interfaces
{
    public interface IClient
    {
        Task<IEnumerable<ClientModel>> GetAllClient();
        Task<ClientModel> GetClientByIDAsync(int id);
        Task<bool> ClientExists(int id);
        Task<ClientModel> GetClient(ClientModel _client);
    }
}
