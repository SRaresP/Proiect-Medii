﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext dbCtxt = new UserDbContext();
        
        public ActionResult Index()
        {
            return View(dbCtxt.Users.ToList());
        }
        public ActionResult Logare()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogareResult()
        {
            string username = Request["txtUtilizator"].ToString();
            string password = Request["txtPassword"].ToString();
            if (password == "1234")
            {
                string mesaj = "Succes";
                return Content(mesaj);
            }
            else
            {
                string mesaj = "Eroare";
                return Content(mesaj);
            }
        }
    }
}