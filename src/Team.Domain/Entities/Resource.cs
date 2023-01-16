using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class Resource : EntityBase
    {
        public Resource(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;

            ProjectResource = new HashSet<ProjectResource>();
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<ProjectResource> ProjectResource { get; set; }

    }
}
