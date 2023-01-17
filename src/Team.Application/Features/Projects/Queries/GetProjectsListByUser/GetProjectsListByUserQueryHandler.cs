using AutoMapper;
using MediatR;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;

namespace Team.Application.Features.Projects.Queries.GetProjectsListByUser
{
    public class GetProjectsListByUserQueryHandler : IRequestHandler<GetProjectsListByUserQuery, List<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectsListByUserQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsListByUserQuery request, CancellationToken cancellationToken)
        {
            var resultList = await _projectRepository.GetProjectsByUserAsync(request.UserId);
            return _mapper.Map<List<ProjectDto>>(resultList);
        }
    }
}
