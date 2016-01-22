namespace production_project.Domain.Users.Models
{
    using System.Collections.Generic;

    public class AspNetRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
