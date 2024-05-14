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
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public CreateProjectComentsCommandHandlerTest()
        {
            _projectRepositoryMock = new();
            _userRepositoryMock = new();
        }

        [Fact]
        public async Task Handler_ShouldFail_WhenProjectNoExist()
        {
            //Arrange
            var command = new CreateProjectCommentCommand { Comment = "Test Comment", IdProject = Guid.NewGuid(), IdUser = 1 };


            _projectRepositoryMock.Setup(ps => ps.ProjectExistAsync(
                It.IsAny<Guid>()
                )).ReturnsAsync((Project)null);

            _userRepositoryMock.Setup(us => us.UsersHasProjectAsync(
                It.IsAny<int>()
                )).ReturnsAsync(true);

            var handler = new CreateProjectCommentCommandHandler(_projectRepositoryMock.Object, _userRepositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handler_ShouldPostComment_WhenProjectExistAndUserAssociate()
        {
            //Arrange
            Project project = new();

            var command = new CreateProjectCommentCommand { Comment = "Test Comment", IdProject = Guid.NewGuid(), IdUser = 1 };

            _projectRepositoryMock.Setup(s => s.PostComentsAsync(
                It.IsAny<ProjectComment>()
                )).ReturnsAsync(Guid.NewGuid());


            _projectRepositoryMock.Setup(ps => ps.ProjectExistAsync(
                It.IsAny<Guid>()
                )).ReturnsAsync(project);

            _userRepositoryMock.Setup(us => us.UsersHasProjectAsync(
                It.IsAny<int>()
                )).ReturnsAsync(true);

            var handler = new CreateProjectCommentCommandHandler(_projectRepositoryMock.Object, _userRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            //Assert
            result.Should().NotBeNull();
            Assert.IsType<Guid>(result);

        }

        [Fact]
        public async Task Handler_ShouldFailComment_WhenUserNoAssociateToProject()
        {
            //Arrange
            var command = new CreateProjectCommentCommand { Comment = "Test Comment", IdProject = Guid.NewGuid(), IdUser = 1 };

            Project project = new();

            _projectRepositoryMock.Setup(ps => ps.ProjectExistAsync(
                It.IsAny<Guid>()
                )).ReturnsAsync(project);

            _userRepositoryMock.Setup(us => us.UsersHasProjectAsync(
                It.IsAny<int>()
                )).ReturnsAsync(false);

            var handler = new CreateProjectCommentCommandHandler(_projectRepositoryMock.Object, _userRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Handler_ShouldFailComment_WhenUserNoAssociateAndProjectNoExist()
        {
            //Arrange
            var command = new CreateProjectCommentCommand { Comment = "Test Comment", IdProject = Guid.NewGuid(), IdUser = 1 };

            _projectRepositoryMock.Setup(ps => ps.ProjectExistAsync(
                It.IsAny<Guid>()
                )).ReturnsAsync((Project)null);

            _userRepositoryMock.Setup(us => us.UsersHasProjectAsync(
                It.IsAny<int>()
                )).ReturnsAsync(false);

            var handler = new CreateProjectCommentCommandHandler(_projectRepositoryMock.Object, _userRepositoryMock.Object);

            var result = await handler.Handle(command, default);

            //Assert
            result.Should().BeNull();
                   }
    }

}
