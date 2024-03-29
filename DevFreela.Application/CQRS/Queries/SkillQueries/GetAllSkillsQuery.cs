using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Queries.SkillQueries;

public class GetAllSkillsQuery : IRequest<List<SkillsViewModel>>
{

}
