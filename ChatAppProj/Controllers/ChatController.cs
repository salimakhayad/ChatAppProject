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

namespace ChatApp.Controllers
{
    public class ChatController:Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserStore<Profiel> _userStore;
        private readonly IUserClaimsPrincipalFactory<Profiel> _claimsPrincipalFactory;
        private readonly SignInManager<Profiel> _signInManager;
        private readonly UserManager<Profiel> _userManager;

        public ChatController(IChatService service, IUserStore<Profiel> userStore, UserManager<Profiel> userManager, IUserClaimsPrincipalFactory<Profiel> claimsPrincipalFactory, SignInManager<Profiel> signInManager)
        {
            this._chatService = service;
            this._userManager = userManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
            this._userStore = userStore;
            this._signInManager = signInManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            IndexChatModel model = new IndexChatModel();
            model.Chats = _chatService.GetAllChats().ToList();
            return View(model);
        }
    }
}
