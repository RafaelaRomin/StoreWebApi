using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreWebApi.Models;

namespace StoreWebApi.Data.Configurations
{
    public class ClientConfigurations : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasColumnType("VARCHAR(150)").IsRequired();
            builder.Property(c => c.Phone).HasColumnType("CHAR(11)").IsRequired();
            builder.Property(c => c.ZipCode).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(c => c.City).HasMaxLength(80).IsRequired();
            builder.Property(c => c.State).HasColumnType("CHAR(2)").IsRequired();

        }
    }
}
