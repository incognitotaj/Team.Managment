using AutoMapper;
using Team.Application.Dtos;
using Team.Application.Features.ProjectClients.Commands.CreateProjectClient;
using Team.Application.Features.ProjectClients.Commands.UpdateProjectClient;
using Team.Application.Features.ProjectDocuments.Commands.CreateProjectDocument;
using Team.Application.Features.ProjectDocuments.Commands.UpdateProjectDocument;
using Team.Application.Features.ProjectMilestones.Commands.CreateProjectMilestone;
using Team.Application.Features.ProjectMilestones.Commands.UpdateProjectMilestone;
using Team.Application.Features.ProjectResources.Commands.CreateProjectResource;
using Team.Application.Features.ProjectResources.Commands.UpdateProjectResource;
using Team.Application.Features.Projects.Commands.CreateProject;
using Team.Application.Features.Projects.Commands.UpdateProject;
using Team.Application.Features.Resources.Commands.CreateResource;
using Team.Application.Features.Resources.Commands.UpdateResource;
using Team.Application.Helpers;
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

            CreateMap<Resource, ResourceDto>().ReverseMap();
            CreateMap<Resource, CreateResourceCommand>().ReverseMap();
            CreateMap<Resource, UpdateResourceCommand>().ReverseMap();

            CreateMap<ProjectClient, ProjectClientDto>().ReverseMap();
            CreateMap<ProjectClient, CreateProjectClientCommand>().ReverseMap();
            CreateMap<ProjectClient, UpdateProjectClientCommand>().ReverseMap();

            CreateMap<ProjectResource, ProjectResourceDto>().ReverseMap();
            CreateMap<ProjectResource, CreateProjectResourceCommand>().ReverseMap();
            CreateMap<ProjectResource, UpdateProjectResourceCommand>().ReverseMap();

            CreateMap<ProjectMilestone, ProjectMilestoneDto>().ReverseMap();
            CreateMap<ProjectMilestone, CreateProjectMilestoneCommand>().ReverseMap();
            CreateMap<ProjectMilestone, UpdateProjectMilestoneCommand>().ReverseMap();

            CreateMap<ProjectDocument, ProjectDocumentDto>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom<ProjectDocumentUrlResolver>())
                .ReverseMap();
            CreateMap<ProjectDocument, CreateProjectDocumentCommand>().ReverseMap();
            CreateMap<ProjectDocument, UpdateProjectDocumentCommand>().ReverseMap();
        }
    }
}
