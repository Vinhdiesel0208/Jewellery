using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lib;
using Group3.Db;
using Group3.Reponsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Group3.Controllers
{
    public class CreateAccountController : Controller
    {


        private ICreateAcountService cService;
        private DatabaseContext db;

        public CreateAccountController(ICreateAcountService cService, DatabaseContext db)
        {
            this.cService = cService;
            this.db = db;

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //public bool CheckPass(string pass)
        //{
        //    if (pass == null || pass.Length < 8)
        //    {
        //        return false;
        //    }
        //    return true;

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users newUser)
        {
            try
            {
                bool userExists = await db.User.AnyAsync(u => u.Email == newUser.Email);

                if (userExists)
                {
                    ViewBag.errormsg = "Email already exists, try again";
                    return View();
                }

                //if (!CheckPass(newUser.Password))
                //{
                //    ViewBag.errormsg = "Password must be at least 8 characters";
                //    return View();
                //}
                HttpContext.Session.SetString("accNo", newUser.Email);

                if(ModelState.IsValid)
                {
                bool added = await cService.addUser(newUser);
                return RedirectToAction("Index", "Frontend");

                }

            }
        catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            string accNo = HttpContext.Session.GetString("accNo");
            var user = await cService.GetUserByAccNo(accNo);
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> EditByUser()
        {
            string accNo = HttpContext.Session.GetString("accNo");
            var user = await cService.GetUserByAccNo(accNo);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditByUser(Users editUser)
        {
            try
            {

                await cService.edit(editUser);
                return RedirectToAction("Details", "CreateAccount" /*new { id = editUser.Id }*/);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditByAdmin(int id)
        {
            var user = await cService.GetUserById(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditByAdmin(Users editUser)
        {
            try
            {

                await cService.edit(editUser);
                return RedirectToAction("Index", "CreateAccount");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var model = await cService.GetUser();
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword(ForgotPassword model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await db.User.FirstOrDefaultAsync(u => u.Email == model.Email);
        //        if (user == null)
        //        {
        //            var token = await db;
        //        }
        //    }
        //}
    }
}

