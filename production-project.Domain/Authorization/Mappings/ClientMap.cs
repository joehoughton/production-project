namespace production_project.Domain.Authorization.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Authorization.Models;

    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.Secret)
                .IsRequired();

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.AllowedOrigin)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("Clients");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Secret).HasColumnName("Secret");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.ApplicationType).HasColumnName("ApplicationType");
            Property(t => t.Active).HasColumnName("Active");
            Property(t => t.RefreshTokenLifeTime).HasColumnName("RefreshTokenLifeTime");
            Property(t => t.AllowedOrigin).HasColumnName("AllowedOrigin");
        }
    }
}
