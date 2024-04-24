using DevFreela.Application.CQRS.Commands.UserCommands.InactiveUserCommand;
using DevFreela.Application.Interfaces;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.UserHandler
{
    public class InactiveUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        public InactiveUserCommandHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Handler_ShouldInactiveAccount_IfUserExist()
        {
            //Arrange

            _userRepositoryMock.Setup(s => s.InactiveUserAsync(It.IsAny<int>())).ReturnsAsync(true);

            var command = new InactiveUserCommand { Id = 1 };

            var handler = new InactiveUserCommandHandler(_userRepositoryMock.Object);

            //Act

            var result = await handler.Handle(command, default);

            //Arrange
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Handler_ShouldNotInactiveAccount_IfUserNoExist()
        {
            //Arrange
            _userRepositoryMock.Setup(s => s.InactiveUserAsync(It.IsAny<int>())).ReturnsAsync(false);

            var command = new InactiveUserCommand { Id = 1 };

            var handler = new InactiveUserCommandHandler(_userRepositoryMock.Object);

            //Act

            var result = await handler.Handle(command, default);

            //Arrange
            result.Should().BeFalse();
        }
    }
}
