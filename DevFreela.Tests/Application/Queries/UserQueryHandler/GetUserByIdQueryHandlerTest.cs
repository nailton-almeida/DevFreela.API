using DevFreela.Application.CQRS.Queries.UserQueries.GetUserByIdQuery;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.UserQueryHandler
{
    public class GetUserByIdQueryHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        public GetUserByIdQueryHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Handler_ShouldReturnUser_WhenUserExist()
        {
            //Arrange
            int id = 1;

            User user = new();

            var request = new GetUserByIdQuery(id);

            var handler = new GetUserByIdQueryHandler(_userRepositoryMock.Object);

            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<UsersViewModel>(result);
        }

        [Fact]
        public async Task Handler_ShouldNotReturnUser_WhenUserNoExist()
        {
            int id = 1;
            //Arrange
            var request = new GetUserByIdQuery(id);


            var handler = new GetUserByIdQueryHandler(_userRepositoryMock.Object);

            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.Null(result);

        }


    }
}
