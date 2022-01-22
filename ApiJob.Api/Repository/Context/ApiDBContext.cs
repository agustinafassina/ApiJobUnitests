using System.Reflection;
using ApiJobUnitests.ApiJob.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiJobUnitests.ApiJob.Api.Repository.Context
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<JobOpportunity> JobOpportunities { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}