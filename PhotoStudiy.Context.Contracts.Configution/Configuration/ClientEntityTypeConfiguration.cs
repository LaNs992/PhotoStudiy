using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoStudiy.Context.Contracts.Models;


namespace PhotoStudiy.Context.Contracts.Configution.Configuration
{
    public class ClientEntityTypeConfiguration:IEntityTypeConfiguration<Client>
    { /// <summary>
      /// Конфигурация для <see cref="Client"/>
      /// </summary>
        void IEntityTypeConfiguration<Client>.Configure(EntityTypeBuilder<Client> builder)
        {

            builder.ToTable("Clients");
            builder.HasKey(x => x.Id);
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Number).HasMaxLength(100).IsRequired();
            builder.HasIndex(x => x.Number)
                 .IsUnique()
                 .HasDatabaseName($"{nameof(Client)}_{nameof(Client.Number)}")
                 .HasFilter($"{nameof(Client.DeletedAt)} is null");
            builder.HasMany(x => x.Dogovors).WithOne(x => x.Client).HasForeignKey(x => x.ClientId);


        }

    }
}
