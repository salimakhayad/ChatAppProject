using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApp.Models;
using ChatApp.Data;
using ChatApp.Domain;
using Microsoft.AspNetCore.Identity;
using ChatApp.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.IO;
using ChatApp.Models.Home;

namespace ChatApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserClaimsPrincipalFactory<Profile> _claimsPrincipalFactory;
        private readonly SignInManager<Profile> _signInManager;
        private readonly UserManager<Profile> _userManager;
        private Profile _currentProfile;

        public HomeController(IChatService chatService, UserManager<Profile> userManager, IUserClaimsPrincipalFactory<Profile> claimsPrincipalFactory, SignInManager<Profile> signInManager)
        {
            this._userManager = userManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
            this._chatService = chatService;
            this._signInManager = signInManager;
        }
        [Route("")]
        [Authorize]
        public IActionResult Index()
        {
            HomeModel model = new HomeModel()
            {
                Profiles = new List<Profile>(),
                ChatGroups = new List<Group>()
            };

            var profilesFromDb = _chatService.GetAllProfiles().ToList();
            if (profilesFromDb != null)
            {
                model.Profiles = profilesFromDb;
            }
            var groupsFromDb = _chatService.GetAllGroups()
                                           
                                            .ToList();
            if (groupsFromDb != null)
            {
                model.ChatGroups = groupsFromDb;
            }

            return View(model);  
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new Profile
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                        EmailAddress = model.EmailAddress,
                        FavouriteColor = "Dark Orange"                       
                    };
                    var identityResult = await _userManager.CreateAsync(user, model.Password);
                    if (identityResult.Succeeded)
                    {
                        var userFromDb = _chatService.GetAllProfiles().FirstOrDefault(x => x.Id == user.Id);
                        
                        using var memoryStream = new MemoryStream();
                        model.ProfilePicture.CopyTo(memoryStream);
                        userFromDb.ProfilePicture = memoryStream.ToArray();

                        _chatService.SaveChanges();
                        return View("Success");
                    }
                    return View();
                }
            }
            return View();

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                // var user = await _userManager.FindByNameAsync(model.UserName);
                

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var principal = await _claimsPrincipalFactory.CreateAsync(user);

                    await HttpContext.SignInAsync("Identity.Application", principal);
                    _currentProfile = _chatService.GetAllProfiles().FirstOrDefault(usr => usr.Id == user.Id);
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Invalid Username or Password");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
