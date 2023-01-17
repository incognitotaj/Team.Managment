using AutoMapper;
using MediatR;
using Team.Application.Contracts.Persistence;
using Team.Application.Dtos;

namespace Team.Application.Features.Projects.Queries.GetProjectsList
{
    public class GetProjectsListQueryHandler : IRequestHandler<GetProjectsListQuery, List<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectsListQueryHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            var resultList = await _projectRepository.GetAllAsync();
            return _mapper.Map<List<ProjectDto>>(resultList);
        }
    }
}
