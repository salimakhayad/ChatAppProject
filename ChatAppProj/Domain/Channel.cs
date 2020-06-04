using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Channel
    {
        public Channel()
        {
           
        }
        public string Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Chat")]
        public string ChatId { get; set; }
        public Chat Chat { get; set; }

        [ForeignKey("Group")]
        public string GroupId { get; set; }
        public Group Group { get; set; }

    }
}
