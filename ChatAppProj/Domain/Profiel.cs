using ChatAppProj.Domain;
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
            
        }
        public Profiel(string naam)
        {
            Naam = naam;
        }
        public string Naam { get; set; }
    
    }
}
