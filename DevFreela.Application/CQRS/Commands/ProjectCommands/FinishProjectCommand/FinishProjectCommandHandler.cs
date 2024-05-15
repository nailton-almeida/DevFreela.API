using DevFreela.Application.Interfaces;
using DevFreela.Core.DTO;
using DevFreela.Core.Enums;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.CQRS.Commands.ProjectCommands.FinishProjectCommand
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IPaymentService _paymentService;
        private readonly IProjectRepository _projectRepository;

        public FinishProjectCommandHandler(IPaymentService paymentService, IProjectRepository projectRepository)
        {
            _paymentService = paymentService;
            _projectRepository = projectRepository;
        }
        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            PaymentProjectDTO finishProjectDTO = new(request.IdProject, request.CardName!, request.CardNumber!, request.CVV!, request.ValidateDate!);

            var projectChangeStatus = await _projectRepository.ProjectChangeStatusAsync(request.IdProject, (int)ProjectStatusEnum.Finish);

            if (projectChangeStatus)
            {
                await _paymentService.CheckPaymentAsync(finishProjectDTO);
                return true;
            }

            return false;
        }
    }
}
