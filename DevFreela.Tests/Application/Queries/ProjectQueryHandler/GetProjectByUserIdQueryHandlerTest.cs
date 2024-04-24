using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByUserIdQuery;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.ProjectQueryHandler
{


    public class GetProjectByUserIdQueryHandlerTest
    {
        private readonly Mock<IProjectRepository> _projectRepository;
        public GetProjectByUserIdQueryHandlerTest()
        {
            _projectRepository = new Mock<IProjectRepository>();
        }

        [Fact]
        public async Task Handler_ShouldReturnProjects_WhenUserHasProject()
        {
            //Arrange
            User freelancer = new();
            User client = new();

            var project = new Project(Guid.NewGuid(), "Sample Project", "This is a sample project", 1, 2, 1000.00m, DateTime.Now, DateTime.Now.AddDays(10), freelancer, client);

            var projectDetailsViewModel = new ProjectDetailsViewModel(project.Id, project.Title, project.Description, project.TotalCost, project.StartedAt, project.FinishedAt, "John Doe", "Jane Smith");

            var request = new GetProjectByUserIdQuery(1);

            var handler = new GetProjectByUserIdQueryHandler(_projectRepository.Object);

            _projectRepository.Setup(s => s.GetByUserIdAsync(It.IsAny<int>())).ReturnsAsync(new List<Project>());

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProjectViewModel>>(result);
        }

        [Fact]
        public async Task Handler_ShouldNotReturnProjects_WhenUserHasNotProject()
        {
            //Arrange
            var id = Guid.NewGuid();

            _projectRepository.Setup(s => s.GetByUserIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            var projectDetailsViewModel = new ProjectDetailsViewModel(id, "Sample Project", "This is a sample project", 1000.00m, DateTime.Now, DateTime.Now.AddDays(10), "John Doe", "Jane Smith");

            var request = new GetProjectByUserIdQuery(2);

            var handler = new GetProjectByUserIdQueryHandler(_projectRepository.Object);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.Null(result);
        }
    }

}
