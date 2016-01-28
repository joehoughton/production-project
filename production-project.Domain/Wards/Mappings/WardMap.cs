namespace production_project.Domain.Wards.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Wards.Models;

    public class WardMap : EntityTypeConfiguration<Ward>
    {
        public WardMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            ToTable("Ward");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.OrganisationId).HasColumnName("OrganisationId");
        }
    }
}
