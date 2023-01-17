using MediatR;

namespace Team.Application.Features.Resources.Commands.CreateResource
{
    public class CreateResourceCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
