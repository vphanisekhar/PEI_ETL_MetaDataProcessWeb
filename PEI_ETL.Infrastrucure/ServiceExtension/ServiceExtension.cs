using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PEI_ETL.Core.Interfaces;
using PEI_ETL.Infrastrucure.Repository;
using UOW = PEI_ETL.Infrastrucure.UnitOfWork;


namespace PEI_ETL.Infrastrucure.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ETLDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUnitOfWork, UOW.UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IETLBatchSrcRepository, ETLBatchSRCRepository>();

            return services;
        }
    }
}
