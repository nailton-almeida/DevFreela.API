using DevFreela.Application.Interfaces;
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
            var projectExist = await _repository.ProjectExistAsync(request.Id);

            if (projectExist != null)
            {
                return await _repository.UpdateProjectAsync(request);
            }
             return null;
        }
    }
}
