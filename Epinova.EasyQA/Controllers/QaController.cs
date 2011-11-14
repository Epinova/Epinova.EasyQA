using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epinova.EasyQA.Controllers
{
    [Authorize]
    public class QaController : Controller
    {
        public ActionResult Edit(int id)
        {
            return View();
        }

        public ActionResult New(int id)
        {
            return RedirectToAction("Edit", new { id = 1 });
        }
    }
}
