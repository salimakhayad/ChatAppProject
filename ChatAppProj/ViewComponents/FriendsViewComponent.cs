using ChatApp.Data;
using ChatApp.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatApp.ViewComponents
{
    public class FriendsViewComponent:ViewComponent
    {
     // private readonly IChatService _chatService;
     // private readonly IUserStore<Profile> _userStore;
     // private readonly IUserClaimsPrincipalFactory<Profile> _claimsPrincipalFactory;
     // private readonly SignInManager<Profile> _signInManager;
     // private readonly UserManager<Profile> _userManager;
     // public FriendsViewComponent(IChatService service)
     // {
     //     _chatService = service;
     // }
     //
     // public IViewComponentResult Invoke()
     // {
     //     var profileId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
     //     var profile = _chatService.GetAllProfiles().Where(x => x.Id == profileId).FirstOrDefault();
     //
     //     var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
     //     var privateChats = _chatService.ChatUsers
     //     .Include(x => x.Chat)
     //     .Where(x => x.UserId == userId
     //         && x.Chat.Type != ChatType.Private)
     //     .Select(x => x.Chat)
     //     .ToList();
     //     return View(privateChats);
     // }
    }
}
