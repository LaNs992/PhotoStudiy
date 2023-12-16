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
    public class PhotographEntityTypeConfiguration : IEntityTypeConfiguration<Photogragh>
    {/// <summary>
     /// Конфигурация для <see cref="Photogragh"/>
     /// </summary>
        void IEntityTypeConfiguration<Photogragh>.Configure(EntityTypeBuilder<Photogragh> builder)
        {

            builder.ToTable("Photograghs");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Number).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Number)
                 .IsUnique()
                 .HasDatabaseName($"{nameof(Photogragh)}_{nameof(Photogragh.Number)}")
                 .HasFilter($"{nameof(Photogragh.DeletedAt)} is null");
            builder.HasMany(x => x.Dogovors).WithOne(x => x.Photogragh).HasForeignKey(x => x.PhotographId);


        }

    }
}
