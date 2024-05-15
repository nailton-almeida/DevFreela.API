using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.SkillCommand;

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, SkillsViewModel>
{
    private readonly ISkillRepository _skillRepository;
    public CreateSkillCommandHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }
    public async Task<SkillsViewModel?> Handle(CreateSkillCommand command, CancellationToken cancellationToken)
    {
        var newSkill = new Skill(command.Name, command.TypeSkills);

        var skillCreated = await _skillRepository.CreateAsync(newSkill);

        if (skillCreated is not null)
        {
            return new SkillsViewModel(skillCreated.Id, skillCreated.Name, skillCreated.TypeSkill);
        }

        return null;
    }
}
