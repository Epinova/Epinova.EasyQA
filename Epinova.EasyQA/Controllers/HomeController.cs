using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;
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
            IEnumerable<QaInstance> qaInstances = _qaService.GetAll(HttpContext.User.Identity.Name);
            if(qaInstances.Count() > 0)
            {
                return View(qaInstances.OrderByDescending(x => x.PublishedDate).ToList());
            }

            return View(new List<QaInstance>());
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
