
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Chat
    {
        public int Id { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
