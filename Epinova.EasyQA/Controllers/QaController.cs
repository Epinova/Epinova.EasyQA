using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Epinova.EasyQA.Common.Utilities;
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
        IQaService _qaService;
        JsonResult _jsonResult;
        UserManager _userNamanger;

        public QaController()
        {
            _qaService = new QaService();
            _jsonResult = new JsonResult()
                              {
                                  ContentEncoding = System.Text.Encoding.UTF8,
                                  ContentType = "application/json",
                                  JsonRequestBehavior = JsonRequestBehavior.DenyGet
                              };
            _userNamanger = new UserManager();
        }

        public QaController(IQaTypeService qaTypeService, IQaService qaService)
        {
            _qaService = qaService;
        }

        public ActionResult Edit(int id)
        {
            QaInstance qaInstance = _qaService.Get(HttpContext.User.Identity.Name, id);
            if (qaInstance.User != HttpContext.User.Identity.Name)
                throw new Exception("No access!");

            return View(qaInstance);
        }

        public ActionResult View(int id)
        {
            var model = _qaService.Get(HttpContext.User.Identity.Name, id);

            ViewBag.CanMarkAsFixed = model.ProjectMembers.Contains(HttpContext.User.Identity.Name);

            return View(model);
        }


        public ActionResult New(int id)
        {
            QaInstance newQa = _qaService.CreateQaInstance(id, HttpContext.User.Identity.Name, App_GlobalResources.Qa.DefaultName);
            return RedirectToAction("Edit", new { id = newQa.Id});
        }

        public ActionResult UpdateTitle(int id, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                    title = App_GlobalResources.Qa.DefaultName;

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
                if (status == "f" || status == "wf") 
                {
                    QaInstanceCriteria criteria = _qaService.ResolveCriteria(criteriaId, qaId, status == "f" ? ResolvedType.Fixed : ResolvedType.WontFix);
                    _jsonResult.Data = new NewEntityModel() { Id = criteria.Id, Text = criteria.Status.ToString() };
                }
                else { 
                    InstanceCriteriaStatus newStatus;
                    if (status == "y")
                        newStatus = InstanceCriteriaStatus.Ok;
                    else if (status == "n")
                        newStatus = InstanceCriteriaStatus.NotOk;
                    else
                        newStatus = InstanceCriteriaStatus.NeedsExplanation;
                
                    QaInstanceCriteria criteria = _qaService.UpdateCriteriaStatus(criteriaId, qaId, newStatus);
                    _jsonResult.Data = new NewEntityModel() { Id = criteria.Id, Text = criteria.Status.ToString() };
                }
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

        public ActionResult AddProjectMember(int qaId, string projectMember)
        {
            if (string.IsNullOrEmpty(projectMember.Trim()))
                return null;

            try
            {
                QaInstance qa = _qaService.AddQaProjectMember(qaId, projectMember);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }

        public ActionResult RemoveProjectMember(int qaId, string projectMember)
        {
            if (string.IsNullOrEmpty(projectMember.Trim()))
                return null;

            try
            {
                QaInstance qa = _qaService.RemoveQaProjectMember(qaId, projectMember);
                _jsonResult.Data = new NewEntityModel() { Id = qa.Id, Text = qa.Name };
            } catch (Exception ex)
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
                jsonResult.Data = new { Users = new List<string>(), Id = "0" };
            else
                jsonResult.Data = new { Users = _userNamanger.Usernames.Where(x => x.StartsWith(id)).ToList(), Id = id };
            return jsonResult; 
        }
    }
}