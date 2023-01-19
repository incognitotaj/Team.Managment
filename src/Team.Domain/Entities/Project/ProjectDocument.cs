using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class ProjectDocument : EntityBase
    {
        public ProjectDocument(string title, string filePath, string detail, Guid projectId)
        {
            Title = title;
            FilePath = filePath;
            Detail = detail;
            ProjectId = projectId;
        }

        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Detail { get; set; }
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
