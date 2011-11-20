using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;
using Epinova.EasyQA.Models;
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
            _qaService = new QaService();
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
            return View(_qaService.Get(id));
        }

        public ActionResult View(int id)
        {
            return View(_qaService.Get(id));
        }


        public ActionResult New(int id)
        {
            QaInstance newQa = _qaService.CreateQaInstance(id);
            return RedirectToAction("Edit", new { id = newQa.Id});
        }

        public ActionResult UpdateTitle(int id, string title)
        {
            try
            {
                QaInstance qaInstance = _qaService.UpdateQaInstance(id, title);
                _jsonResult.Data = new NewEntityModel() { Id = qaInstance.Id, Title = qaInstance.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult UpdateCriteriaComment(int criteriaId, int qaId, string text)
        {
            try
            {
                QaInstanceCriteria criteria = _qaService.UpdateCriteriaInstance(criteriaId, qaId, text);
                _jsonResult.Data = new NewEntityModel() { Id = criteria.Id, Title = text };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult UpdateCriteriaStatus(int criteriaId, int qaId, string status)
        {
            try
            {
                InstanceCriteriaStatus newStatus;
                if(status == "y")
                    newStatus = InstanceCriteriaStatus.Ok;
                else if(status == "n")
                    newStatus = InstanceCriteriaStatus.NotOk;
                else 
                    newStatus = InstanceCriteriaStatus.NeedsExplanation;

                QaInstanceCriteria criteria = _qaService.UpdateCriteriaInstance(criteriaId, qaId, newStatus);
                _jsonResult.Data = new NewEntityModel() { Id = criteria.Id, Title = criteria.Status.ToString() };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }
    }
}
