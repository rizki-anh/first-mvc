using MyApp.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;
using Microsoft.AspNetCore;

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

            // === Tambahin ResendClient ===
            services.AddOptions();
            services.AddHttpClient<ResendClient>(); // HttpClient untuk Resend
            services.Configure<ResendClientOptions>(o =>
            {
                // Ambil API key dari appsettings.json
                o.ApiToken = config["Resend:ApiKey"]!;
            });
            services.AddTransient<IResend, ResendClient>();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10 * 1024 * 1024; // 10 MB
});
            return services;
        }
    }
}
