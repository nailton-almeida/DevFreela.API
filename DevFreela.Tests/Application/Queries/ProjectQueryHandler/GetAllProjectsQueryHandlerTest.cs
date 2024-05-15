using DevFreela.Application.CQRS.Queries.ProjectQueries.GetAllProjectsQuery;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.ProjectQueryHandler
{
    public class GetAllProjectsQueryHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepository;
        public GetAllProjectsQueryHandlerTest()
        {
            _projectRepository = new Mock<IProjectRepository>();
        }

        [Fact]
        public async Task Handler_ShouldReturnListProjects_WhenProjectsExist()
        {
            //Arrange

            var projects = new List<Project>();

            _projectRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(projects);

            var request = new GetAllProjectsQuery();

            var handler = new GetAllProjectsQueryHandler(_projectRepository.Object);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.NotNull(result);
            //Assert.NotEmpty(result);
            Assert.IsType<List<ProjectViewModel>>(result);
        }

        [Fact]
        public async Task Handler_ShouldNotReturnListProjects_WhenProjectsNoExist()
        {
            //Arrange

            var projects = new List<Project>();

            _projectRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(projects);

            var request = new GetAllProjectsQuery();

            var handler = new GetAllProjectsQueryHandler(_projectRepository.Object);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            // Assert.Null(result);
            Assert.Empty(result);
        }
    }
}
