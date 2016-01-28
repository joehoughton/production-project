namespace production_project.Domain.Wards.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Wards.Models;

    public class WardMap : EntityTypeConfiguration<Ward>
    {
        public WardMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            ToTable("Ward");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.OrganisationId).HasColumnName("OrganisationId");
        }
    }
}
