using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Application.Dtos;

namespace Team.Application.Features.ProjectResources.Queries.GetByProjectId
{
    public class GetByProjectIdQuery : IRequest<IEnumerable<ProjectResourceDto>>
    {
        public Guid ProjectId { get; set; }
    }
}
