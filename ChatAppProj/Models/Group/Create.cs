﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ChatApp.Models.Group
{
    public class CreateGroupModel
    {
        public string Name { get; set; }
        public IFormFile Photo {get;set;}
        public string Content {get;set;}
        

    }

}
