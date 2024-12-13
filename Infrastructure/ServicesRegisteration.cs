using Application.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;


namespace Infrastructure
{
    public static class ServicesRegisteration
    {

        public static void RegisterEntitiesServices(this IServiceCollection services) {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFileStorageService, SaveToDeskFileStorage>();
            services.AddScoped<IUserService, UserService>();
            
        }

        public static void RegisterUtilServices(this IServiceCollection services) {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
        
        }

        public static void RegisterAuth(this IServiceCollection services) {
            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie();
        }

        public static void DbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            string DbConnectionString  = configuration.GetConnectionString("SqlServer")!;
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(DbConnectionString));

        }

    }
}
