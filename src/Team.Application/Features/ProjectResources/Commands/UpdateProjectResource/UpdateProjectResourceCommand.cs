using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Application.Features.ProjectResources.Commands.UpdateProjectResource
{
    public class UpdateProjectResourceCommand : IRequest
    {
        public Guid ProjectResourceId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
