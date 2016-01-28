namespace production_project.Domain.Users.Models
{
    using production_project.Domain.Organisations.Models;

    public class UserDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public bool EmailNotification { get; set; }
        public bool SmsNotification { get; set; }
        public string AspNetUserId { get; set; }
        public int OrganisationId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Organisation Organisation { get; set; }
    }
}
