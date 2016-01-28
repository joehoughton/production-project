namespace production_project.Domain.Organisations.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Organisations.Models;

    public class OrganisationTypeMap : EntityTypeConfiguration<OrganisationType>
    {
        public OrganisationTypeMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("OrganisationType");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Type).HasColumnName("Type");
        }
    }
}
