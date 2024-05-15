using DevFreela.Application.CQRS.Queries.UserQueries.GetAllUsersQuery;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.UserQueryHandler
{
    public class GetAllUsersQueryHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        public GetAllUsersQueryHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Handler_SholdReturnListUser_WhenUsersExist()
        {
            //Arrange
            List<User> userList = new()
            {
                new User("John Doe", "john.doe@example.com", "password123", new DateTime(1990, 1, 1), 1),
                new User("Jane Smith", "jane.smith@example.com", "password456", new DateTime(1995, 5, 10), 2),
                new User("Michael Johnson", "michael.johnson@example.com", "password789", new DateTime(1985, 12, 15), 1),
                new User("Emily Davis", "emily.davis@example.com", "passwordabc", new DateTime(1992, 8, 20), 2),
                new User("David Wilson", "david.wilson@example.com", "passwordxyz", new DateTime(1988, 4, 5), 1)
            };

            _userRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(userList);
                 
            var request = new GetAllUsersQuery();

            var handler = new GetAllUsersQueryHandler(_userRepositoryMock.Object);

            //Act
            var result = await handler.Handle(request, default);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<UsersViewModel>>(result);
            Assert.True(result.Count == 5);
        }

        [Fact]
        public async Task Handler_SholdNotReturnListUser_WhenUsersNoExist()
        {
            //Arrange
            _userRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<User>());

            var request = new GetAllUsersQuery();

            var handler = new GetAllUsersQueryHandler(_userRepositoryMock.Object);

            //Act
            var result = await handler.Handle(request, default);


            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<UsersViewModel>>(result);
            Assert.True(result.Count == 0);
        }

    }
}
