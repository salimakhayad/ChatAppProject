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
using ChatApp.Models.Profiel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.IO;
using ChatApp.Models.Home;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChatService _chatService;
        private readonly IUserClaimsPrincipalFactory<Profiel> _claimsPrincipalFactory;
        private readonly SignInManager<Profiel> _signInManager;
        private readonly UserManager<Profiel> _userManager;
        private Profiel _currentProfiel;

        public HomeController(IChatService chatService, UserManager<Profiel> userManager, IUserClaimsPrincipalFactory<Profiel> claimsPrincipalFactory, SignInManager<Profiel> signInManager)
        {
            this._userManager = userManager;
            this._claimsPrincipalFactory = claimsPrincipalFactory;
            this._chatService = chatService;
            this._signInManager = signInManager;
        }
        [Route("")]
        public IActionResult Index()
        {
            HomeModel model = new HomeModel()
            {
                Profielen = new List<Profiel>()
            };

            var profilesFromDb = _chatService.GetAllProfielen().ToList();
            if (profilesFromDb != null)
            {
                model.Profielen = profilesFromDb;
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
                    user = new Profiel
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                        FavorieteKleur = "Dark Orange"
                    };
                    var identityResult = await _userManager.CreateAsync(user, model.Password);
                    if (identityResult.Succeeded)
                    {
                        var userFromDb = _chatService.GetAllProfielen().FirstOrDefault(x => x.Id == user.Id);
                        
                        using var memoryStream = new MemoryStream();
                        model.ProfielFoto.CopyTo(memoryStream);
                        userFromDb.ProfielFoto = memoryStream.ToArray();

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

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var principal = await _claimsPrincipalFactory.CreateAsync(user);

                    await HttpContext.SignInAsync("Identity.Application", principal);
                    _currentProfiel = _chatService.GetAllProfielen().FirstOrDefault(usr => usr.Id == user.Id);
                    return RedirectToAction("Index");
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
