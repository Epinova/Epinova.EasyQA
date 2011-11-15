﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;
using Epinova.EasyQA.Data;
using Epinova.EasyQA.Data.Repositories;
using Epinova.EasyQA.Services;

namespace Epinova.EasyQA.Controllers
{
    public class HomeController : Controller
    {
        private IQaTypeService _qaTypeService;
        private IQaService _qaService;

        public HomeController()
        {
            _qaTypeService = new QaTypeService();
            _qaService = new QaService();
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            List<QaInstance> qaInstances = _qaService.GetAll().ToList();
            return View(qaInstances);
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult QaTypesListPartial()
        {
            return View(_qaTypeService.GetQaTypes());
        }

        public ActionResult Login()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string username, string password)
        {
            if (Membership.ValidateUser(username, password))
            {
                FormsAuthentication.SetAuthCookie(username, true);
            }
            return RedirectToAction("Index");
        }
    }
}
