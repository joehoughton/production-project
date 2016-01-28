namespace production_project.Domain.Beds.Models
{
    using production_project.Domain.Beds.Models.Enums;
    using production_project.Domain.Wards.Models;

    public class Bed
    {
        public int Id { get; set; }
        public int WardId { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public Gender Gender { get; set; }
        public Tier Tier { get; set; }
        public virtual Ward Ward { get; set; }
    }
}


