using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epinova.EasyQA.Core.ServiceInterfaces;
using Epinova.EasyQA.Services;

namespace Epinova.EasyQA.Controllers
{
    [Authorize]
    public class QaController : Controller
    {
        IQaTypeService _qaTypeService;
        IQaService _qaService;
        JsonResult _jsonResult;

        public QaController()
        {
            _qaTypeService = new QaTypeService();
            _jsonResult = new JsonResult()
                              {
                                  ContentEncoding = System.Text.Encoding.UTF8,
                                  ContentType = "application/json",
                                  JsonRequestBehavior = JsonRequestBehavior.DenyGet
                              };
        }

        public QaController(IQaTypeService qaTypeService, IQaService qaService)
        {
            _qaTypeService = qaTypeService;
            _qaService = qaService;
        }

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
