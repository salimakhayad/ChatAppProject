using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Group 
    {
        public Group()
        {
            Channels = new List<Channel>();
            GroupProfiles = new List<GroupProfile>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Profile")]
        public string ProfileId { get; set; }
        public Profile Profile { get; set; }
        public string Content { get; set; }
        public byte[] Photo { get; set; }
        public virtual ICollection<Channel> Channels { get; set; }
        public virtual ICollection<GroupProfile> GroupProfiles { get; set; }





    }
}
