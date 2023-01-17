using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class ProjectResource : EntityBase
    {
        public ProjectResource(Guid projectId, Guid resourceId, DateTime fromDate, DateTime? toDate)
        {
            ProjectId = projectId;
            ResourceId = resourceId;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public Guid ResourceId { get; set; } 
        public virtual Resource Resource { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
