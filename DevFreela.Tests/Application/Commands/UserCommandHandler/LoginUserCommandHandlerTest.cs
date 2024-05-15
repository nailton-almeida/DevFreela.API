using DevFreela.Application.CQRS.Commands.UserCommands.LoginUserCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Services;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.UserHandler
{
    public class LoginUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAuthService> _authServiceMock;

        public LoginUserCommandHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task Handler_ShouldLogin_WhenUserAndPasswordCorrect()
        {
            //Arrange
            var user = new User();
            _authServiceMock.Setup(s => s.GeneratePasswordHash256(
                It.IsAny<string>())).ReturnsAsync(string.Empty);

            _userRepositoryMock.Setup(us => us.LoginUserAsync
            (It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(user);

            var request = new LoginUserCommand { Email = "test@mock.com.br", Password = "1232344234" };

            var handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _authServiceMock.Object);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<LoginUserViewModel>(result);

        }

        [Fact]
        public async Task Handler_ShouldNotLogin_WhenUserAndPasswordIncorret()
        {
            //Arrange
            var user = new User();
            _authServiceMock.Setup(s => s.GeneratePasswordHash256(
                It.IsAny<string>())).ReturnsAsync(string.Empty);

            _userRepositoryMock.Setup(us => us.LoginUserAsync
            (It.IsAny<string>(),
            It.IsAny<string>())).ReturnsAsync(() => null);

            var request = new LoginUserCommand { Email = "test@mock.com.br", Password = "1232344234" };

            var handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _authServiceMock.Object);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            result.Should().BeNull();


        }
    }
}
