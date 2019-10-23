using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string ProfielNaam;
        public string Text;
        public Message()
        {

        }
        public Message(string naam,string text)
        {
            ProfielNaam = naam;
            Text = text;
        }
    }
}
