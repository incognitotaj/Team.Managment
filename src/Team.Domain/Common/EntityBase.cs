using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team.Domain.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
    }
}
