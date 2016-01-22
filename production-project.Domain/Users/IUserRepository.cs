namespace production_project.Domain.Users
{
    using production_project.Domain.Users.Dto;

    public interface IUserRepository
    {
        UserDetailDto GetUserDetails(string currentUserId);
        void UpdateUserDetails(UserDetailDto userDetailDto);
    }
}