using MyClinic.Models;

namespace MyClinic.DataLayer
{
    public class Create
    {
        public static void CreateClient(IApplicationBuilder applicationBuilder) {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDBContext>();
                context.Database.EnsureCreated();

                //if(!context.Client.Any())
                //{
                    context.Client.AddRange(new List<ClientModel>()
                    {
                        new ClientModel()
                        {
                            Name = "Paulo",
                            Email = "tttt@gmail.com",
                            Password = "12345"
                        }
                    });

                    context.SaveChanges();
                //}
            }
        }
    }
}
