namespace production_project.Domain.Patients.Mappings
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Patients.Models;

    public class PatientMap : EntityTypeConfiguration<Patient>
    {
        public PatientMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.PasId)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("Patient");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.PasId).HasColumnName("PasId");
            Property(t => t.Age).HasColumnName("Age");
            Property(t => t.Gender).HasColumnName("Gender");
        }
    }
}
