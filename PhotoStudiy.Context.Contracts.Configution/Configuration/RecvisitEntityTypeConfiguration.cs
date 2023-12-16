using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PhotoStudiy.Context.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Context.Contracts.Configution.Configuration
{
    public class RecvisitEntityTypeConfiguration : IEntityTypeConfiguration<Recvisit>
    {/// <summary>
     /// Конфигурация для <see cref="Recvisit"/>
     /// </summary>
        void IEntityTypeConfiguration<Recvisit>.Configure(EntityTypeBuilder<Recvisit> builder)
        {

            builder.ToTable("Recvisits");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300).IsRequired();
            builder.HasIndex(x => x.Name)
                 .IsUnique()
                 .HasDatabaseName($"{nameof(Recvisit)}_{nameof(Recvisit.Name)}")
                 .HasFilter($"{nameof(Recvisit.DeletedAt)} is null");
            builder.HasMany(x => x.Dogovors).WithOne(x => x.Recvisit).HasForeignKey(x => x. RecvisitId);

        }
    }
}
