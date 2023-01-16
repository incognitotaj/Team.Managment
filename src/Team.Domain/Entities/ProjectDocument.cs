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
        public ProjectDocument(string title, string url, string detail, int projectId)
        {
            Title = title;
            Url = url;
            Detail = detail;
            ProjectId = projectId;
        }

        public string Title { get; set; }
        public string Url { get; set; }
        public string Detail { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
