using ChatApp.Domain;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Models.Group;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChannelController
    {
        private readonly IChatService _chatService;
        private readonly IUserStore<Profile> _userStore;
        private readonly IUserClaimsPrincipalFactory<Profile> _claimsPrincipalFactory;
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        private Profile _currentProfile;

        public ChannelController(IChatService service, IUserStore<Profile> userStore, UserManager<Profile> userManager, IUserClaimsPrincipalFactory<Profile> claimsPrincipalFactory, SignInManager<Profile> signInManager)
        {
            this._chatService = service;
            this._userManager = userManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
            this._userStore = userStore;
            this._signInManager = signInManager;
        }


    }
}
