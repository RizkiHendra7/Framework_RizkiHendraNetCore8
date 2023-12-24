using FrameworkRHP.Infrastructure.Context;
using FrameworkRHP.Infrastructure.Repository.Interface;
using FrameworkRHP.Infrastructure.UOW;
using FrameworkRHP.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkRHP.Infrastructure
{
    public static class ServiceExtension
    { 
        public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddDbContext<EProcurementDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("eProcurementDB")));


            //services.AddDbContext<DbContextClass>(options => 
            //    options.UseSqlServer(configuration.GetConnectionString("eProcurementDB")); 

            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            //services.AddScoped<IMUserRepository, MUserRepository>();

            return services;
        }
    }
}
