using DevFreela.Application.CQRS.Commands.ProjectCommands.CreateProjectCommand;
using DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using FluentAssertions;
using MediatR;
using Moq;

namespace DevFreela.Tests.Application.Commands.ProjectCommandHander
{
    public class UpdateProjectCommandHandlerTest
    {   
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
       

        public UpdateProjectCommandHandlerTest()
        {
            _projectRepositoryMock = new();
        }

        [Fact]
        public async Task Handler_ShouldUpdateProject_WhenProjectExist()
        {
            //Arrange

            Project project = new Project();

            var command = new UpdateProjectCommand
            {
                Title = "Title Project UnitTest",
                Description = "Description Project Unit Test",
                TotalCost = 50.0m,
                StartedAt = DateTime.Now.AddDays(1),
                FinishedAt = DateTime.Now.AddDays(15)
            };

            _projectRepositoryMock.Setup(ps => ps.ProjectExistAsync(It.IsAny<Guid>())).ReturnsAsync(project);

            _projectRepositoryMock.Setup(p => p.UpdateProjectAsync(
                It.IsAny<UpdateProjectCommand>())).ReturnsAsync(Guid.NewGuid());

            var handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);


            //Assert
            result.Should().NotBeNull();
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async Task Handler_ShouldNotUpdateProject_WhenProjectNoExist()
        {
            //Arrange

            Project project = new Project();

            var command = new UpdateProjectCommand
            {
                Title = "Title Project UnitTest",
                Description = "Description Project Unit Test",
                TotalCost = 50.0m,
                StartedAt = DateTime.Now.AddDays(1),
                FinishedAt = DateTime.Now.AddDays(15)
            };

            _projectRepositoryMock.Setup(ps => ps.ProjectExistAsync(It.IsAny<Guid>())).ReturnsAsync((Project)null);

            var handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);


            //Assert
            result.Should().BeNull();
 
        }
    }
}
