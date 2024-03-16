
using Infrastructure.Stroage;
using Infrastructure.Stroage.Azure;
using Insfrastructure.Stroage.AWS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStroage, AwsStroage>();

            return services;
        }
    }
}
