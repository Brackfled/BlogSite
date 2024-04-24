
using Core.Persistance.Repositories;
using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Contexts
{
    public class BaseDbContext : DbContext
    {

        protected IConfiguration Configuration;

        public DbSet<Subject> Subjects {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }

        public DbSet<BlogFile> BlogFiles { get; set; }
        public DbSet<PPFile> PPFiles { get; set; }
        public DbSet<SubjectImageFile> SubjectImageFiles { get; set; }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserOperationClaim> UserClaims { get; set; }
        public DbSet<User> Users { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
       : base(dbContextOptions)
        {
            Configuration = configuration;
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
