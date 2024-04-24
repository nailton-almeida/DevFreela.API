using DevFreela.Application.CQRS.Commands.ProjectCommands.ProjectChangeStatusCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.ProjectHander
{
    public class ProjectChangeStatusCommandHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        public ProjectChangeStatusCommandHandlerTest()
        {
            _projectRepositoryMock = new();
        }

        [Fact]
        public async Task Handler_ShouldNotChangeStatus_WhenProjectNoExist()
        {
            //Arrange
            var command = new ProjectChangeStatusCommand(Guid.NewGuid(), 1);

            _projectRepositoryMock.Setup(p => p.ProjectExistAsync(
                It.IsAny<Guid>()))
                .ReturnsAsync((Project)null);

            var handler = new ProjectChangeStatusCommandHandler(_projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeFalse();

        }


        [Fact]
        public async Task Handler_ShouldChangeStatus_WhenProjectExist()
        {
            //Arrange

            Project project = new();

            var command = new ProjectChangeStatusCommand(Guid.NewGuid(), 1);

            _projectRepositoryMock.Setup(p => p.ProjectExistAsync(
                It.IsAny<Guid>()))
                .ReturnsAsync(project);

            _projectRepositoryMock.Setup(st => st.ProjectChangeStatusAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>())).ReturnsAsync(false);

            var handler = new ProjectChangeStatusCommandHandler(_projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Handler_ShouldNotChangeStatus_WhenStatusIsInvalid()
        {
            //Arrange

            Project project = new();

            var command = new ProjectChangeStatusCommand(Guid.NewGuid(), 1);

            _projectRepositoryMock.Setup(p => p.ProjectExistAsync(
                It.IsAny<Guid>()))
                .ReturnsAsync(project);

            _projectRepositoryMock.Setup(st => st.ProjectChangeStatusAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>())).ReturnsAsync(false);

            var handler = new ProjectChangeStatusCommandHandler(_projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Handler_ShouldChangeStatus_WhenStatusIsValid()
        {
            //Arrange

            Project project = new();

            var command = new ProjectChangeStatusCommand(Guid.NewGuid(), 1);

            _projectRepositoryMock.Setup(p => p.ProjectExistAsync(
                It.IsAny<Guid>()))
                .ReturnsAsync(project);

            _projectRepositoryMock.Setup(st => st.ProjectChangeStatusAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>())).ReturnsAsync(true);

            var handler = new ProjectChangeStatusCommandHandler(_projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeTrue();
        }
    }
}

