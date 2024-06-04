using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TopUp.Domain;
using TopUp.Domain.Entities;
using TopUp.Infrastructure.EntityConfigurations;

namespace TopUp.Infrastructure
{
    public class TopUpContext : DbContext, IUnitOfWork
    {
        public TopUpContext() { }
        public TopUpContext(DbContextOptions<TopUpContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BeneficiaryEntityTypeConfiguration());
            
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public class BankingContextDesignFactory : IDesignTimeDbContextFactory<TopUpContext>
        {
            public TopUpContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<TopUpContext>()
                    .UseSqlServer("Server=localhost,1433;Initial Catalog=topup;User Id=sa;Password=TopUpDb202#;Encrypt=False;TrustServerCertificate=True");

                return new TopUpContext(optionsBuilder.Options);
            }
        }
    }
}
