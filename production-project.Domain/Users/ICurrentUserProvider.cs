namespace production_project.Domain.Users
{
    using production_project.Domain.Users.Models;

    public interface ICurrentUserProvider
    {
        CurrentUserDetail CurrentUserDetail { get; }
    }
}
