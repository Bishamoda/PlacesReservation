﻿using CoWorking.Models;
using CoWorking.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoWorking.Controllers
{
    public class AccountController : Controller
    {
        public List<Worker> worker;

        private PlaceDBContext db;
        public AccountController(PlaceDBContext context)
        {
            db = context;
        }

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
                var worker = await db.Worker.FirstOrDefaultAsync(worker => worker.Login == model.Login && worker.Password == model.Password);
                
                if (worker != null)
                {
                    if (worker.Role == 1)
                    {
                        await Authenticate(model.Login);
                        return RedirectToAction("Index", "AdminPanel");

                        
                    }
                    else if(worker.Role == 2)
                    {
                        await Authenticate(model.Login);
                        return RedirectToAction("Index", "UserPanel");

                        
                    }

                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");

                }              

            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
