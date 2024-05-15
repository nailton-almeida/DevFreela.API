using DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand;
using DevFreela.Application.Interfaces;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.UserHandler
{
    public class EditUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        public EditUserCommandHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Handler_ShouldEditUser_WhenUserExist()
        {
            //Arrange
            var command = new EditUserCommand { Id = 1, Birthday = DateTime.Now.AddDays(-2), Email = "test@mock.com.br", Fullname = "Test Mock" };

            _userRepositoryMock.Setup(s => s.EditUserAsync(
                command)).ReturnsAsync(true);

            var handler = new EditUserCommandHandler(_userRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_ShouldNotEditUser_WhenUserNoExist()
        {
            //Arrange
            var command = new EditUserCommand { Id = 1, Birthday = DateTime.Now.AddDays(-2), Email = "test@mock.com.br", Fullname = "Test Mock" };

            _userRepositoryMock.Setup(s => s.EditUserAsync(
                command)).ReturnsAsync(false);

            var handler = new EditUserCommandHandler(_userRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeFalse();
        }
    }
}
