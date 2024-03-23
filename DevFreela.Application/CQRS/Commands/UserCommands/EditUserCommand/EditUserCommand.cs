using MediatR;

namespace DevFreela.Application.CQRS.Commands.UserCommands.EditUserCommand
{
    public class EditUserCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}
