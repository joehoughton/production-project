namespace production_project.Domain.Organisations
{
    using System.Collections.Generic;
    using production_project.Domain.Organisations.Dto;

    public interface IOrganisationRepository
    {
        OrganisationDto GetByUserId(string userId);
        ICollection<OrganisationLocationDto> Search(OrganisationSearchDto searchDto, string userId);
    }
}
