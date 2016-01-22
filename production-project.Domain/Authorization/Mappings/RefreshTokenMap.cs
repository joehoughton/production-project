namespace production_project.Domain.Authorization.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Authorization.Models;

    public class RefreshTokenMap : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(128);

            Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ClientId)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ProtectedTicket)
                .IsRequired();

            // Table & Column Mappings
            ToTable("RefreshTokens");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Subject).HasColumnName("Subject");
            Property(t => t.ClientId).HasColumnName("ClientId");
            Property(t => t.IssuedUtc).HasColumnName("IssuedUtc");
            Property(t => t.ExpiresUtc).HasColumnName("ExpiresUtc");
            Property(t => t.ProtectedTicket).HasColumnName("ProtectedTicket");
        }
    }
}
