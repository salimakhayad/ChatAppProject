using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}
