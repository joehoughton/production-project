namespace production_project.Domain.Beds.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Beds.Models;

    public class BedMap : EntityTypeConfiguration<Bed>
    {
        public BedMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Bed");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.WardId).HasColumnName("WardId");
            Property(t => t.Price).HasColumnName("Price");
            Property(t => t.Availability).HasColumnName("Availability");
            Property(t => t.Name).HasColumnName("Name");

            // Relationships
            HasRequired(t => t.Ward)
                .WithMany(t => t.Beds)
                .HasForeignKey(d => d.WardId);
        }
    }
}
