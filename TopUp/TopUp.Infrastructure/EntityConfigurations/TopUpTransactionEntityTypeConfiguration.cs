using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopUp.Domain.Entities;

namespace TopUp.Infrastructure.EntityConfigurations
{
    internal class TopUpTransactionEntityTypeConfiguration : IEntityTypeConfiguration<TopUpTransaction>
    {
        public void Configure(EntityTypeBuilder<TopUpTransaction> builder)
        {
            builder.ToTable("TopUpTransactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.BeneficiaryId)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.HasOne(x => x.Beneficiary)
                .WithMany(x => x.TopUpTransactions)
                .HasForeignKey(x => x.BeneficiaryId);
        }
    }
}
