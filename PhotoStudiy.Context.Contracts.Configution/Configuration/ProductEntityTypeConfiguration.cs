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
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {/// <summary>
     /// Конфигурация для <see cref="Product"/>
     /// </summary>
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {

            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.HasIndex(x => x.Name)
                 .IsUnique()
                 .HasDatabaseName($"{nameof(Product)}_{nameof(Product.Name)}")
                 .HasFilter($"{nameof(Product.DeletedAt)} is null");
            builder.HasMany(x => x.Dogovors).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

        }
    }
}