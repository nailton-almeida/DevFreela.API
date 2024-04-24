using DevFreela.Application.CQRS.Queries.ProjectQueries.GetProjectByIdQuery;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.ProjectQueryHandler
{
    public class GetProjectByIdQueryHandlerTest
    {

        private readonly Mock<IProjectRepository> _projectRepository;

        public GetProjectByIdQueryHandlerTest()
        {
            _projectRepository = new Mock<IProjectRepository>();
        }

        [Fact]
        public async Task Handler_ShouldReturnProject_WhenProjectExit()
        {
            //Arrange
            var id = Guid.NewGuid();
            User freelancer = new();
            User client = new();

            var project = new Project(id, "Sample Project", "This is a sample project", 1, 2, 1000.00m, DateTime.Now, DateTime.Now.AddDays(10), freelancer, client);

            var projectDetailsViewModel = new ProjectDetailsViewModel(id, "Sample Project", "This is a sample project", 1000.00m, DateTime.Now, DateTime.Now.AddDays(10), "John Doe", "Jane Smith");

            var request = new GetProjectByIdQuery(id);

            var handler = new GetProjectByIdQueryhandler(_projectRepository.Object);

            _projectRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(project);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ProjectDetailsViewModel>(result);
        }

        [Fact]
        public async Task Handler_ShouldNotReturnProject_WhenProjectNoExit()
        {
            //Arrange
            var id = Guid.NewGuid();

            _projectRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);

            var projectDetailsViewModel = new ProjectDetailsViewModel(id, "Sample Project", "This is a sample project", 1000.00m, DateTime.Now, DateTime.Now.AddDays(10), "John Doe", "Jane Smith");

            var request = new GetProjectByIdQuery(id);

            var handler = new GetProjectByIdQueryhandler(_projectRepository.Object);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.Null(result);
        }
    }
}
