using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Application.Features.ProjectClients.Commands.DeleteProjectClient
{
    public class DeleteProjectClientCommand : IRequest
    {
        public Guid ProjectId { get; set; }
        public Guid ProjectClientId { get; set; }
    }
}
