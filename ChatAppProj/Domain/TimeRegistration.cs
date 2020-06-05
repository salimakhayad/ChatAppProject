using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class TimeRegistration
    {
        public TimeRegistration()
        {

        }
        [Key]
        public int Id { get; set; }
        public DateTime  StartTime { get; set; }
        public DateTime EndTime { get; set; }

        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        [ForeignKey("Profile")]
        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

    }
}
