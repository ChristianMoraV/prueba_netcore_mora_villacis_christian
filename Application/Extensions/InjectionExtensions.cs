using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Commons.Ordering;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Infraestructure.Persistences.Contexts;
using System.Reflection;

namespace POS.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(POSCineContext).Assembly.FullName;
            services.AddSingleton(configuration);

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IOrderingQuery, OrderingQuery>();

            services.AddScoped<IPeliculaApplication, PeliculaApplication>();
            services.AddScoped<ISalaCineApplication, SalaCineApplication>();
            services.AddScoped<IPeliculaSalaApplication, PeliculaSalaApplication>();
            services.AddDbContext<POSCineContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("POSConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient
                    );

            services.AddScoped<IStoredProcedureRepository, StoredProcedureRepository>();
            services.AddScoped<IStoredProcedureService, StoredProcedureService>();







            return services;
        }
    }
}
