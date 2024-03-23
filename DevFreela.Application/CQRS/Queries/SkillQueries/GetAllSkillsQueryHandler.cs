using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.SkillQueries;

public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillsViewModel>>
{
    private readonly ISkillRepository _skillRepository;
    public GetAllSkillsQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }
    public async Task<List<SkillsViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skillList = await _skillRepository.GetAllAsync();

        var skillViewModel = skillList.Select(skill => new SkillsViewModel(skill.Id, skill.Name, skill.TypeSkill)).ToList();

        return skillViewModel;

    }
}
   
