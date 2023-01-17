using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Application.Dtos;

namespace Team.Application.Features.Projects.Queries.GetProjectsListByUser
{
    public class GetProjectsListByUserQuery : IRequest<List<ProjectDto>>
    {
        public GetProjectsListByUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }
    }
}
