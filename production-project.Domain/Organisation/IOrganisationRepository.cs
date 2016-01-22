namespace production_project.Domain.Organisation
{
    using production_project.Domain.Organisation.Dto;

    public interface IOrganisationRepository
    {
        OrganisationDto GetByUserId(string userId);
    }
}
