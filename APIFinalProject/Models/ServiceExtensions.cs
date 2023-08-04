using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace APIFinalProject.Models
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services) {

            var builder = services.AddIdentity<User,IdentityRole>(q=>q.User.RequireUniqueEmail=true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
           // builder.Services.AddAutoMapper(typeof(Startup));

        }
    }
}
