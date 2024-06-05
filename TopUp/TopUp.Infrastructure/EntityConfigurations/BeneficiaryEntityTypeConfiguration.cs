using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopUp.Domain.Entities;

namespace TopUp.Infrastructure.EntityConfigurations
{
    public class BeneficiaryEntityTypeConfiguration : IEntityTypeConfiguration<Beneficiary>
    {
        public void Configure(EntityTypeBuilder<Beneficiary> builder)
        {
            builder.ToTable("Beneficiaries");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nickname)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.TopUpBalance)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(d => d.UserId)
               .IsRequired();
        }
    }
}
