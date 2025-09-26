using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MyApp.Repository;

namespace user.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMyDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Daftarkan UserRepository
            services.AddScoped<UserRepository>(provider =>
                new UserRepository(config.GetConnectionString("MySqlConnection")!)
            );
            // Daftarkan RegisterService
            services.AddScoped<service.registerservice.RegisterService>();
            services.AddScoped<service.loginservices.LoginService>();
            return services;
        }
    }
}
