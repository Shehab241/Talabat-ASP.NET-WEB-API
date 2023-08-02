using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Identity;

namespace Talabat.APIS.Extenstions
{
    public static class  IdentityServicesExtention
    {
        public static void AddIdentityServices( this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                //options.Password.RequireDigit = true;
                //options.Password.RequireUppercase = true;

            })
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddAuthentication();
        }
    }
}
