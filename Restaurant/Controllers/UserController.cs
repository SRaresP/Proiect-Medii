﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using Restaurant.Helpers;
using System.Data.Entity;
namespace Restaurant.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext dbCtxt = new UserDbContext();
        
        public ActionResult Index()
        {
            return View(dbCtxt.Users.ToList());
        }
        public ActionResult Create(UserModel msg)
        {
            if (ModelState.IsValid)
            {
                dbCtxt.Users.Add(msg);
                dbCtxt.SaveChanges();

                return RedirectToAction("Index");
            }
            Task.Run(() => TraceWriter.WriteLineToTraceAsync("Model state was not valid in \"Create\" post method in \"User Controller\"."));
            return View(msg);
        }
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                Task.Run(() => TraceWriter.WriteLineToTraceAsync("Id had no value in \"Edit\" get method in \"User Controller\"."));
                return HttpNotFound();
            }
            UserModel user = dbCtxt.Users.Find(id);
            if (null == user)
            {
                Task.Run(() => TraceWriter.WriteLineToTraceAsync("\"user\" was null in \"Edit\" get method in \"User Controller\"."));
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {

                dbCtxt.Entry(user).State = System.Data.Entity.EntityState.Modified;
                dbCtxt.SaveChanges();
                return RedirectToAction("Index");

            }
            Task.Run(() => TraceWriter.WriteLineToTraceAsync("Model state was not valid in \"Edit\" post method in \"User Controller\"."));
            return View(user);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            UserModel user = dbCtxt.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserModel user = dbCtxt.Users.Find(id);
            dbCtxt.Users.Remove(user);
            dbCtxt.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
