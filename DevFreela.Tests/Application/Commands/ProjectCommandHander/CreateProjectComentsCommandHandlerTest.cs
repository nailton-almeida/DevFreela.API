using DevFreela.Application.CQRS.Commands.ProjectCommands.CreatePostComentsCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.ProjectHander
{
    public class CreateProjectComentsCommandHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepositoryMock;
        
        public CreateProjectComentsCommandHandlerTest()
        {
            _projectRepositoryMock = new();
            
        }

        [Fact]
        public async Task Handler_ShouldFail_WhenProjectNoExist()
        {
            //Arrange
            var command = new CreateProjectCommentCommand { Comment = "Test Comment", IdProject = Guid.NewGuid(), IdUser = 1 };

            _projectRepositoryMock.Setup(ps=> ps.ProjectExistAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>()
                )).ReturnsAsync(false);

            var handler = new CreateProjectCommentCommandHandler(_projectRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handler_ShouldSucess_WhenProjectExist()
        {
            //Arrange
            var command = new CreateProjectCommentCommand { Comment = "Test Comment", IdProject = Guid.NewGuid(), IdUser = 1 };


            _projectRepositoryMock.Setup(s => s.PostComentsAsync(
                It.IsAny<ProjectComment>()
                )).ReturnsAsync(Guid.NewGuid());


            _projectRepositoryMock.Setup(ps => ps.ProjectExistAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>()
                )).ReturnsAsync(true);

            var handler = new CreateProjectCommentCommandHandler(_projectRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<Guid>(result);

        }
    }

}
