using DevFreela.Application.CQRS.Commands.UserCommands.CreateUserCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Core.Services;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.UserHandler
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAuthService> _authServiceMock;
        public CreateUserCommandHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task Handler_ShouldCreateUser_WhenUserNoExist()
        {
            //Arrange
            _authServiceMock.Setup(p => p.GeneratePasswordHash256
            (It.IsAny<string>())).ReturnsAsync(string.Empty);

            _userRepositoryMock.Setup(s => s.CreateUserAsync(
                It.IsAny<User>())).ReturnsAsync(1);

            var command = new CreateUserCommand { Fullname = "Fullname", Email = "test@mock.com", Password = "testpassword", Birthday = DateTime.Now.AddDays(-1), Role = 1 };
            var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _authServiceMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().NotBeNull();
            result.Should().Be(1);
        }

        [Fact]
        public async Task Handler_ShouldNotCreateUser_WhenUserExist()
        {
            //Arrange
            _authServiceMock.Setup(p => p.GeneratePasswordHash256
            (It.IsAny<string>())).ReturnsAsync(string.Empty);

            _userRepositoryMock.Setup(s => s.CreateUserAsync(
                It.IsAny<User>())).ReturnsAsync(() => null);

            var command = new CreateUserCommand { Fullname = "Fullname", Email = "test@mock.com", Password = "testpassword", Birthday = DateTime.Now.AddDays(-1), Role = 1 };
            var handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _authServiceMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeNull();
        }
    }
}
