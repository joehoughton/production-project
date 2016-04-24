namespace production_project.Domain.Tests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using Moq;
    using NUnit.Framework;
    using production_project.Domain.Data;
    using production_project.Domain.Users;
    using production_project.Domain.Users.Models;

    [TestFixture]
    public class UserRepositoryTests
    {
        private readonly SparebedsContext _context;
        private readonly UserRepository _userRepository;
        private readonly List<UserDetail> _userDetailList;

        /// <summary>
        /// Constructor creates a mock context class with a mocked DbSet, which is passed into the repository
        /// </summary>
        public UserRepositoryTests()
        {
            // create connection string so we're not reliant on Web.config connection strings
            var connectionString = new SqlConnectionStringBuilder() { DataSource = ".", IntegratedSecurity = true, InitialCatalog = "RandomDbName" }.ConnectionString;

            // pass connection string to context class
            _context = new SparebedsContext(connectionString);

            _userDetailList = new List<UserDetail>
            {
                new UserDetail(){Id = 1, EmailNotification = false, SmsNotification = true,  Name = "Joe", AspNetUser = new AspNetUser(){Id = "dcc2f01a-0a9b-4ac6-b9fc-e03f46cc3101"}},
                new UserDetail(){Id = 1, EmailNotification = true, SmsNotification = false,  Name = "Lucy", AspNetUser = new AspNetUser(){Id = "dcc2f01a-0a9b-4ac6-b9fc-e03f46cc3102"}}
            };

            // convert the IEnumerable lists to an IQueryable list
            IQueryable<UserDetail> queryableListUserDetail = _userDetailList.AsQueryable();

            // force DbSet to return the IQueryable members of converted list object as its data source
            var mockSetUserDetail = new Mock<DbSet<UserDetail>>();
            mockSetUserDetail.As<IQueryable<UserDetail>>().Setup(m => m.Provider).Returns(queryableListUserDetail.Provider);
            mockSetUserDetail.As<IQueryable<UserDetail>>().Setup(m => m.Expression).Returns(queryableListUserDetail.Expression);
            mockSetUserDetail.As<IQueryable<UserDetail>>().Setup(m => m.ElementType).Returns(queryableListUserDetail.ElementType);
            mockSetUserDetail.As<IQueryable<UserDetail>>().Setup(m => m.GetEnumerator()).Returns(queryableListUserDetail.GetEnumerator());

            // context class will return mocked DbSet
            _context.UserDetails = mockSetUserDetail.Object;

            // pass context to repository
            _userRepository = new UserRepository(_context);
        }

        /// <summary>
        /// Correct user detail should be returned when an AspNetUserId is passed
        /// </summary>
        [Test]
        public void GetUserDetailsShouldReturnCorrectUserDetail()
        {
            var user = _userRepository.GetUserDetails("dcc2f01a-0a9b-4ac6-b9fc-e03f46cc3102");

            Assert.AreEqual("Lucy", user.Name);
            Assert.AreEqual(false, user.SmsNotification);
            Assert.AreEqual(true, user.EmailNotification);
        }
    }
}
