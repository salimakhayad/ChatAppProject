
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Chat
    {
        public Chat()
        {
            Messages = new List<Message>();
            Profiles = new List<Profile>();
        }
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Group")]
        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public ChatType ChatType { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Profile> Profiles { get; set; }
    }
}
