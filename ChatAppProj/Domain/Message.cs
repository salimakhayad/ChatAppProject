using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Message
    {
        public Message()
        {
            Timestamp = DateTime.Now;
        }
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; }
        [ForeignKey("Channel")]
        public int ChannelId { get; set; }
        public Channel Channel { get; set; }


    }
}
