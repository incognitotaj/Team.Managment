using AutoMapper;
using Team.Application.Dtos;
using Team.Application.Features.ProjectClients.Commands.CreateProjectClient;
using Team.Application.Features.ProjectClients.Commands.UpdateProjectClient;
using Team.Application.Features.Projects.Commands.CreateProject;
using Team.Application.Features.Projects.Commands.UpdateProject;
using Team.Application.Features.Resources.Commands.CreateResource;
using Team.Application.Features.Resources.Commands.UpdateResource;
using Team.Domain.Entities;

namespace Team.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();

            CreateMap<Project, CreateProjectCommand>().ReverseMap();
            CreateMap<Project, UpdateProjectCommand>().ReverseMap();


            CreateMap<ProjectClient, ProjectClientDto>().ReverseMap();
            CreateMap<ProjectClient, CreateProjectClientCommand>().ReverseMap();
            CreateMap<ProjectClient, UpdateProjectClientCommand>().ReverseMap();

            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<Resource, CreateResourceCommand>().ReverseMap();
            CreateMap<Resource, UpdateResourceCommand>().ReverseMap();
        }
    }
}
