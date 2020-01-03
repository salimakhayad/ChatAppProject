using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Domain
{
    public class Profiel : IdentityUser
    {
        public Profiel()
        {
            Messages = new HashSet<Message>();
        }
        public Profiel(string naam)
        {
            Naam = naam;
            Messages = new HashSet<Message>();
        }
        public string Naam { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public string FavorieteKleur { get; set; }

    }
}
