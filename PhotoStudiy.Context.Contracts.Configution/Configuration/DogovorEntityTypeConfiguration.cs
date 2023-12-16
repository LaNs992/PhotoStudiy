using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PhotoStudiy.Context.Contracts.Models;

namespace PhotoStudiy.Context.Contracts.Configution.Configuration
{
    public class DogovorEntityTypeConfiguration : IEntityTypeConfiguration<Dogovor>
    {
        /// <summary>
        /// Конфигурация для <see cref="Dogovor"/>
        /// </summary>
        void IEntityTypeConfiguration<Dogovor>.Configure(EntityTypeBuilder<Dogovor> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Date).IsRequired();
            builder.HasIndex(x => x.Date).HasDatabaseName($"{nameof(Dogovor)}_{nameof(Dogovor.Date)}")
                .HasFilter($"{nameof(Dogovor.DeletedAt)} is null");
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.PhotographId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.PhotosetId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.UslugiId).IsRequired();
            
        }
    }
}
