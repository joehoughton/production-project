namespace production_project.Domain.Organisations.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Organisations.Models;

    public class OrganisationMap : EntityTypeConfiguration<Organisation>
    {
        public OrganisationMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Organisation");
            Property(t => t.Id).HasColumnName("Id");

            // Relationships
            HasRequired(t => t.OrganisationType)
               .WithMany()
               .Map(m => m.MapKey("TypeId"));
        }
    }
}
