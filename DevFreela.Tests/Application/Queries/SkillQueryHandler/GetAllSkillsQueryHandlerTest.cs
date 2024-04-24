using DevFreela.Application.CQRS.Queries.SkillQueries;
using DevFreela.Application.Interfaces;
using DevFreela.Core.Entities;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Queries.SkillQueryHandler
{
    public class GetAllSkillsQueryHandlerTest
    {
        private readonly Mock<ISkillRepository> _skillRepositoryMock;

        public GetAllSkillsQueryHandlerTest()
        {
            _skillRepositoryMock = new Mock<ISkillRepository>();
        }

        [Fact]
        public async Task Handler_ShouldReturnSkillList_WhenSkillsExist()
        {
            //Arrange
            var request = new GetAllSkillsQuery();

            var handler = new GetAllSkillsQueryHandler(_skillRepositoryMock.Object);

            List<Skill> skillList = new()
            {
                (new Skill(1, "Test1", "Backend")),
                (new Skill(2, "Test2", "FrontEnd")),
             };

            _skillRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(skillList);

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.NotNull(result);
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handler_ShouldNotReturnSkillList_WhenSkillsNoExist()
        {
            //Arrange
            var request = new GetAllSkillsQuery();

            var handler = new GetAllSkillsQueryHandler(_skillRepositoryMock.Object);

            _skillRepositoryMock.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Skill>());

            //Act
            var result = await handler.Handle(request, default);

            //Assert
            Assert.NotNull(result);
            result.Should().HaveCount(0);
        }

    }
}
