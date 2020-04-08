using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class GroupProfile
    {
        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        public RoleProfile Role { get; set; }
    }
}
