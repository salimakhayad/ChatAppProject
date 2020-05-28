using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class GroupProfile
    {
        [Key]
        public int Id { get; set; }
        public string ProfileId { get; set; }
        public int GroupId { get; set; }
        public RoleProfile Role { get; set; }
    }
}
