using DevFreela.Application.CQRS.Commands.SkillCommand;
using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application.Commands.SkillCommandHandler
{ 
    public class CreateSkillCommandHandlerTest
    {
        private readonly Mock<ISkillRepository> _skillRepository;

        public CreateSkillCommandHandlerTest()
        {
            _skillRepository = new();
        }

        [Fact]
        public async Task Handler_ShouldCreateSkill_WhenSkillNoExist()
        {
            Skill skill = new();
            //Assert
            var command = new CreateSkillCommand { Name = "Teste", TypeSkills = "Backend" };

            _skillRepository.Setup(ss => ss.CreateAsync(
                It.IsAny<Skill>()
                )).ReturnsAsync(skill);

            var handler = new CreateSkillCommandHandler(_skillRepository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Arrange
            result.Should().NotBeNull();
            Assert.IsType<SkillsViewModel>(result);
        }

        [Fact]
        public async Task Handler_ShouldNoCreateSkill_WhenSkillExist()
        {
            //Assert
            var command = new CreateSkillCommand { Name = "Teste", TypeSkills = "Backend" };

            _skillRepository.Setup(ss => ss.CreateAsync(
                It.IsAny<Skill>()
                )).ReturnsAsync((Skill)null);

            var handler = new CreateSkillCommandHandler(_skillRepository.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Arrange
            result.Should().BeNull();
        }
    }
}
