namespace production_project.Domain.Beds
{
    using production_project.Domain.Beds.Dto;

    public interface IBedRepository
    {
        BedRequestDto GetBed(int id);
    }
}
