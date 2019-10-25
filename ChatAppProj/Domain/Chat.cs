using ChatAppProj.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Chat : UniqueId
    {
        public Chat()
        {
            Messages = new List<Message>();
        }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
