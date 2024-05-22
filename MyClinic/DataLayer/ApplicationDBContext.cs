
using Microsoft.EntityFrameworkCore;
using MyClinic.Models;
using System.Collections.Generic;

namespace MyClinic.DataLayer
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<ClientModel> Client { get; set; }
        public DbSet<PrescriptionModel> Prescription { get; set; }
        public DbSet<ClientRoleModel> ClientRole { get; set; }
        public DbSet<RoleModel> Role { get; set; }
    }
}
