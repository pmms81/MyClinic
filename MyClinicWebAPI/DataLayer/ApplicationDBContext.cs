using Microsoft.EntityFrameworkCore;
using MyClinicWebAPI.Models;
namespace MyClinicWebAPI.DataLayer
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<ClientModel> Client { get; set; }

    }
}
