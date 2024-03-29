using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.SkillCommand;

public class CreateSkillCommand : IRequest<SkillsViewModel>
{
    public string Name { get; set; }
    public string TypeSkills { get; set; }
}
