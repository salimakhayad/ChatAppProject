using ChatAppProj.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Message:UniqueId
    {
        public Message()
        {

        }
        public virtual Profiel Zender { get; set; }
        public virtual Profiel Ontvanger { get; set; }
        public string Bericht;
        public DateTime Datum { get; }

        public ICollection<Profiel> Ontvangers { get; set; }
        public Message(Profiel zender, Profiel ontvanger,string bericht)
        {
            Zender = zender;
            Ontvanger = ontvanger;
            Bericht = bericht;
            Datum = DateTime.Now;
            Ontvangers = new List<Profiel>() { Ontvanger };
        }
    }
}
