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
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set;}
        public byte[] File { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public MessageType Type { get; set; }

    }
}
