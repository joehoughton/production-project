namespace production_project.Domain.Users
{
    using System.Linq;
    using production_project.Domain.Data;
    using production_project.Domain.Users.Dto;

    public class UserRepository : IUserRepository
    {
        private readonly SparebedsContext _context;

        public UserRepository(SparebedsContext context)
        {
            _context = context;
        }

        public UserDetailDto GetUserDetails(string currentUserId)
        {
            var userDetails = _context.UserDetails.Single(x => x.AspNetUser.Id == currentUserId);

            var userDetailsDto = new UserDetailDto(
                currentUserId,
                userDetails.Name,
                userDetails.Telephone,
                userDetails.Mobile,
                userDetails.AspNetUser.Email,
                userDetails.EmailNotification,
                userDetails.SmsNotification
            );

            return userDetailsDto;
        }

        public void UpdateUserDetails(UserDetailDto userDetailDto)
        {
            var currentUser = _context.AspNetUsers.Find(userDetailDto.Id);
            var userDetails = _context.UserDetails.Single(x => x.AspNetUser.Id == userDetailDto.Id);

            currentUser.Email = userDetailDto.Email;
            userDetails.Name = userDetailDto.Name;
            userDetails.Telephone = userDetailDto.Telephone;
            userDetails.Mobile = userDetailDto.Mobile;
            userDetails.EmailNotification = userDetailDto.EmailNotification;
            userDetails.SmsNotification = userDetailDto.SmsNotification;

            _context.SaveChanges();
        }
    }
}
