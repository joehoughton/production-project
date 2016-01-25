namespace production_project.Web.Providers
{
    using System.Web;
    using Microsoft.AspNet.Identity;
    using production_project.Domain.Users;
    using production_project.Domain.Users.Models;

    public class CurrentUserProvider : ICurrentUserProvider
    {
        public CurrentUserProvider(HttpContextBase context)
        {
            CurrentUserDetail = new CurrentUserDetail
            {
                UserId = context.User.Identity.GetUserId(),
                Username = context.User.Identity.GetUserName(),
                Role = "Bed Manager"
            };
        }

        public CurrentUserDetail CurrentUserDetail { get; private set; }
    }
}
