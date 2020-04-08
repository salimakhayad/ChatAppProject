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
        public int Id { get; set; }
        public string Name { get; set; }
        public Chat Chat { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public Group Group { get; set; }

    }
}
