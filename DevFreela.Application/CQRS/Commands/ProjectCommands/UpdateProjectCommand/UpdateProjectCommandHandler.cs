using DevFreela.Application.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.UpdateProjectCommand
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Guid?>
    {
        private readonly IProjectRepository _repository;

        public UpdateProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        public async Task<Guid?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Guid? updateProject = await _repository.UpdateProjectAsync(request);

            if (updateProject is not null) return updateProject;

            return null;
        }
    }
}
