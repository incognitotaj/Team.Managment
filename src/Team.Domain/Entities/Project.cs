using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class Project : EntityBase
    {
        public Project(string name, DateTime startDate, DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;

            ProjectDocuments= new HashSet<ProjectDocument>();
            ProjectResources = new HashSet<ProjectResource>();
            ProjectClients = new HashSet<ProjectClient>();
            ProjectMilestones = new HashSet<ProjectMilestone>();
        }

        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
        public virtual ICollection<ProjectResource> ProjectResources { get; set; }
        public virtual ICollection<ProjectClient> ProjectClients { get; set; }
        public virtual ICollection<ProjectMilestone> ProjectMilestones { get; set; }
    }
}
