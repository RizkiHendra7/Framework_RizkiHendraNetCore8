using FrameworkRHP.Core.Models.EF;
using FrameworkRHP.Infrastructure.Context;
using FrameworkRHP.Infrastructure.UOW;
using FrameworkRHP.Services.Interfaces.GenericInterface;
using FrameworkRHP.Services.ServicesImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworkRHP.Services
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
            services.AddScoped<IGenericService<Mrole>, MRoleService>();   
            services.AddScoped<IGenericService<Muser>, MUserService>();   

            return services;
        }
    }
}
