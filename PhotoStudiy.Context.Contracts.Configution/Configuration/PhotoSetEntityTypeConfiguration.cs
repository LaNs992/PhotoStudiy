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
    public class PhotoSetEntityTypeConfiguration : IEntityTypeConfiguration<PhotoSet>
    {/// <summary>
     /// Конфигурация для <see cref="PhotoSet"/>
     /// </summary>
        void IEntityTypeConfiguration<PhotoSet>.Configure(EntityTypeBuilder<PhotoSet> builder)
        {

            builder.ToTable("PhotoSets");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.HasIndex(x => x.Name)
                 .IsUnique()
                 .HasDatabaseName($"{nameof(PhotoSet)}_{nameof(PhotoSet.Name)}")
                 .HasFilter($"{nameof(PhotoSet.DeletedAt)} is null");
            builder.HasMany(x => x.Dogovors).WithOne(x => x.PhotoSet).HasForeignKey(x => x.PhotosetId);


        }

    }
}
