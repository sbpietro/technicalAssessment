using BankApi.Domain;
using BankApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace BankApi.Infrastructure
{
    public class BankContext : DbContext, IUnitOfWork
    {
        public BankContext() { }
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: add model configs
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public class BankingContextDesignFactory : IDesignTimeDbContextFactory<BankContext>
        {
            public BankContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BankContext>()
                    .UseSqlServer("Server=localhost,1433;Initial Catalog=topup;User Id=sa;Password=TopUpDb202#;Encrypt=False;TrustServerCertificate=True");

                return new BankContext(optionsBuilder.Options);
            }
        }
    }
}
