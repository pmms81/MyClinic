using MyClinicWebAPI.Models;

namespace MyClinicWebAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ClientModel client);

    }
}
