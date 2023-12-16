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
    public class UslugiEntityTypeConfiguration : IEntityTypeConfiguration<Uslugi>
    {/// <summary>
     /// Конфигурация для <see cref="Uslugi"/>
     /// </summary>
        void IEntityTypeConfiguration<Uslugi>.Configure(EntityTypeBuilder<Uslugi> builder)
        {

            builder.ToTable("Uslugs");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.HasIndex(x => x.Name)
                 .IsUnique()
                 .HasDatabaseName($"{nameof(Uslugi)}_{nameof(Uslugi.Name)}")
                 .HasFilter($"{nameof(Uslugi.DeletedAt)} is null");
            builder.HasMany(x => x.Dogovors).WithOne(x => x.Uslugi).HasForeignKey(x => x.UslugiId);

        }
    }
}
