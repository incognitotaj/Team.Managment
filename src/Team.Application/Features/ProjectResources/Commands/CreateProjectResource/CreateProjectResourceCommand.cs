using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Application.Features.ProjectResources.Commands.CreateProjectResource
{
    public class CreateProjectResourceCommand : IRequest<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
