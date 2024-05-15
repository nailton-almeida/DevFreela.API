using DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.ProjectHander
{
    public class CreateProjectCommandHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        public CreateProjectCommandHandlerTest()
        {
            _projectRepositoryMock = new();
            _userRepositoryMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnNull_WhenClientDontExist()
        {
            User user = new();
            //Arrange            
            var command = new CreateProjectCommand
            {
                Title = "Title Project UnitTest",
                Description = "Description Project Unit Test",
                ClientID = 1,
                FreelancerID = 2,
                TotalCost = 50.0m,
                StartedAt = DateTime.Now.AddDays(1),
                FinishedAt = DateTime.Now.AddDays(15)
            };

            _userRepositoryMock.Setup(
                us => us.UsersExistAndActivateAsync(
                    It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _userRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_Should_ReturnIdProject_WhenClientExist()
        {
            //Arrange
            User user = new();
            var command = new CreateProjectCommand
            {
                Title = "Title Project UnitTest",
                Description = "Description Project Unit Test",
                ClientID = 1,
                FreelancerID = 2,
                TotalCost = 50.0m,
                StartedAt = DateTime.Now.AddDays(1),
                FinishedAt = DateTime.Now.AddDays(15)
            };

            _userRepositoryMock.Setup(
                us => us.UsersExistAndActivateAsync(
                    It.IsAny<int>()))
                .ReturnsAsync(user);

            _projectRepositoryMock.Setup(ps => ps.CreateProjectAsync(
                It.IsAny<Project>()))
                    .ReturnsAsync(Guid.NewGuid());

            var handler = new CreateProjectCommandHandler(_projectRepositoryMock.Object, _userRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<Guid>(result);
            _projectRepositoryMock.Verify(p => p.CreateProjectAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
