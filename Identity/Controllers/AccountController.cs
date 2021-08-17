using Identity.Models.DTOs;
using Identity.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return View(registerDto);

            var user = new AppUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,

            };
            var result = _userManager.CreateAsync(user, registerDto.Password).Result;

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            string messages="";
            foreach (var item in result.Errors.ToList())
            {
                messages += item.Description + Environment.NewLine;
            }
            TempData["Message"] = messages;

            return View(registerDto);
        }


        public IActionResult Login(string returnUrl="/")
        {
            return View(new LoginDto
            {
                ReturnUrl = returnUrl
            });
        }
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

           

            var result = _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password,
                loginDto.IsPersistent, false).Result;

            if(result.Succeeded)
            {
                return Redirect(loginDto.ReturnUrl);
            }
          
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View();
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
