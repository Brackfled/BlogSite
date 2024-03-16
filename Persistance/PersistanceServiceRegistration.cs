using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NArchitecture.Core.Persistence.DependencyInjection;
using Application.Services.Repositories;
using Persistance.Repositories;
using Persistence.Repositories;

namespace Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("BlogDb")));
            //services.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFeedBackRepository, FeedBackRepository>();

            services.AddScoped<IPPFileRepository, PPFileRepository>();
            services.AddScoped<ISubjectImageFileRepository, SubjectImageFileRepository>();

            services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            return services;
           
        }
    }
}
