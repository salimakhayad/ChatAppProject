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
        public int Id { get; set; }
        public string ZenderId { get; set; }
        public string ChatId { get; set; }
        public string Bericht;
        public DateTime TijdStip { get; }
        public virtual Chat Chat { get; set; }


    }
}
