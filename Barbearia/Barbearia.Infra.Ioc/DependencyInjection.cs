using Barbearia.Domain.Interfaces;
using Barbearia.Infra.Data.Data;
using Barbearia.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Barbearia.Application.Interfaces;
using Barbearia.Application.Services;

namespace Barbearia.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();
            services.AddScoped<IServicoRepository, ServicoRepository>();
            services.AddScoped<ITelefoneRepository, TelefoneRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IAgendaServicoRepository, AgendaServicoRepository>();            
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IBarbeiroService, BarbeiroService>();
            services.AddScoped<IServicoService, ServicoService>();
            services.AddScoped<IAgendaService, AgendaService>();

            return services;
        }
    }
}