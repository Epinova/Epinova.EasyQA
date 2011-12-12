using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;
using Epinova.EasyQA.Models;
using Epinova.EasyQA.Services;
using Epinova.EasyQA.Utilities;
using MarkdownSharp;

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
            return View(_qaService.Get(HttpContext.User.Identity.Name, id));
        }

        public ActionResult View(int id)
        {
            return View(_qaService.Get(HttpContext.User.Identity.Name, id));
        }


        public ActionResult New(int id)
        {
            QaInstance newQa = _qaService.CreateQaInstance(id, HttpContext.User.Identity.Name);
            return RedirectToAction("Edit", new { id = newQa.Id});
        }

        public ActionResult UpdateTitle(int id, string title)
        {
            try
            {
                QaInstance qaInstance = _qaService.UpdateQaName(id, title);
                _jsonResult.Data = new NewEntityModel() { Id = qaInstance.Id, Text = qaInstance.Name };
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
                QaInstanceCriteria criteria = _qaService.UpdateCriteriaComment(criteriaId, qaId, text);
                _jsonResult.Data = new NewEntityModel() { Id = criteria.Id, Text = text };
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

                QaInstanceCriteria criteria = _qaService.UpdateCriteriaStatus(criteriaId, qaId, newStatus);
                _jsonResult.Data = new NewEntityModel() { Id = criteria.Id, Text = criteria.Status.ToString() };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult Publish(int qaId)
        {           
            try
            {
                QaInstance qa = _qaService.UpdateQaPublished(qaId, true);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult UnPublish(int qaId)
        {
            try
            {
                QaInstance qa = _qaService.UpdateQaPublished(qaId, false);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult UpdateProjectMembers(int qaId, string projectMembers)
        {
            try
            {
                QaInstance qa = _qaService.UpdateQaProjectMembers(qaId, projectMembers);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult UpdatePresentAtReview(int qaId, string presentAtReview)
        {
            try
            {
                QaInstance qa = _qaService.UpdateQaPresentAtReview(qaId, presentAtReview);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult UpdateSummary(int qaId, string summary)
        {
            try
            {
                QaInstance qa = _qaService.UpdateQaSummary(qaId, summary);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult UpdateMisc(int qaId, string misc)
        {
            try
            {
                QaInstance qa = _qaService.UpdateQaMisc(qaId, misc);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public JsonResult FindUser(string id)
        {
            JsonResult jsonResult = new JsonResult()
            {
                ContentEncoding = System.Text.Encoding.UTF8,
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            if (string.IsNullOrEmpty(id))
                jsonResult.Data = new { Users = new List<string>() };
            else 
                jsonResult.Data = new { Users = CacheManager.Usernames.Where(x => x.StartsWith(id)).ToList() };
            return jsonResult; 
        }
    }
}