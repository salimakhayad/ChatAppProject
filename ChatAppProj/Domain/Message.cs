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
        }
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set;}
        public byte[] File { get; set; }

        public MessageType Type { get; set; }

        [ForeignKey("Chat")]
        public string ChatId { get; set; }
        public Chat Chat { get; set; }
        [ForeignKey("Profile")]
        public string ProfileId { get; set; }
        public Profile Profile { get; set; }
      

    }
}
