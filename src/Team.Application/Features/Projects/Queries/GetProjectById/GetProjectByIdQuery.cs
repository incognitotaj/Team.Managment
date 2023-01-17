using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Application.Dtos;

namespace Team.Application.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public Guid ProjectId { get; set; }
    }
}
