using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int Id { get; set; }    
        public string Name { get; set; }

        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }

        [ForeignKey("Group")]
        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }


    }
}
