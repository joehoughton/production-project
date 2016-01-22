namespace production_project.Domain.Users.Mapping
{
    using System.Data.Entity.ModelConfiguration;
    using production_project.Domain.Users.Models;

    public class UserDetailMap : EntityTypeConfiguration<UserDetail>
    {
        public UserDetailMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Telephone)
                .IsRequired()
                .HasMaxLength(11);

            Property(t => t.Mobile)
                .IsRequired()
                .HasMaxLength(11);

            Property(t => t.AspNetUserId)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            ToTable("UserDetails");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Telephone).HasColumnName("Telephone");
            Property(t => t.Mobile).HasColumnName("Mobile");
            Property(t => t.EmailNotification).HasColumnName("EmailNotification");
            Property(t => t.SmsNotification).HasColumnName("SmsNotification");
            Property(t => t.AspNetUserId).HasColumnName("AspNetUserId");
            Property(t => t.OrganisationId).HasColumnName("OrganisationId");

            // Relationships
            HasRequired(t => t.AspNetUser)
                .WithMany(t => t.UserDetails)
                .HasForeignKey(d => d.AspNetUserId);
            HasRequired(t => t.Organisation)
                .WithMany()
                .HasForeignKey(d => d.OrganisationId);
        }
    }
}
