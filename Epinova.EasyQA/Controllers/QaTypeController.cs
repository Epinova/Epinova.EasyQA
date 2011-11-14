using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Web.Mvc;
using Epinova.EasyQA.Core;
using Epinova.EasyQA.Core.DataInterfaces;
using Epinova.EasyQA.Core.Entities;
using Epinova.EasyQA.Core.ServiceInterfaces;
using Epinova.EasyQA.Data;
using Epinova.EasyQA.Data.Repositories;
using Epinova.EasyQA.Models;
using Services;

namespace Epinova.EasyQA.Controllers
{
    [Authorize]
    public class QaTypeController : Controller
    {
        private IQaTypeService _qaTypeService;
        private JsonResult _jsonResult;

        public QaTypeController()
        {
            _qaTypeService = new QaTypeService();
            _jsonResult = new JsonResult()
                              {
                                  ContentEncoding = System.Text.Encoding.UTF8,
                                  ContentType = "application/json",
                                  JsonRequestBehavior = JsonRequestBehavior.DenyGet
                              };
        }

        public QaTypeController(IQaTypeService qaTypeService)
        {
            _qaTypeService = qaTypeService;
        }

        public ActionResult Index()
        {
            return View(_qaTypeService.GetQaTypes().OrderBy(x => x.Name));
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View(_qaTypeService.GetQaType(id));
        }

        public ActionResult New()
        {
            QaType newQaType = _qaTypeService.CreateQaType();
            return RedirectToAction("Edit", new { id = newQaType.Id });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateQaTypeTitle(int id, string text)
        {
            try
            {
                QaType qaType = _qaTypeService.UpdateQaType(id, text);
                _jsonResult.Data = new NewEntityModel() { Id = qaType.Id, Title = qaType.Name };
            }
            catch(Exception ex)
            {
               // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }
            return _jsonResult;
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult CreateCategory(int qaType)
        {
            JsonResult result = new JsonResult() { };
            try
            {
                CriteriaCategory newCategory = _qaTypeService.CreateCriteriaCategory(qaType);

                _jsonResult.Data = new NewEntityModel() { Id = newCategory.Id, Title = newCategory.Text };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }

            return _jsonResult;
        }

        public JsonResult UpdateCategoryTitle(int qaTypeId, int categoryId, string text)
        {
            try
            {
                CriteriaCategory category = _qaTypeService.UpdateCriteriaCategory(qaTypeId, categoryId, text);

                _jsonResult.Data = new NewEntityModel() { Id = category.Id, Title = category.Text };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Id = -1, Error = ex.Message };
            }

            return _jsonResult;
        }

        public JsonResult CreateCriteria(int qaType, int criteriaCategory)
        {
            JsonResult result = new JsonResult() { };
            try
            {
                QaCriteria newCriteria = _qaTypeService.CreateQaCriteria(qaType, criteriaCategory);
                _jsonResult.Data = new NewEntityModel() { Id = newCriteria.Id, Title = newCriteria.Text };
            }
            catch (Exception ex)
            {
                // logging   
                _jsonResult.Data = new NewEntityModel() { Error = ex.Message };
            }

            return _jsonResult;
        }

        public JsonResult UpdateCriteriaText(int qaType, int criteriaCategory, string text)
        {
            JsonResult result = new JsonResult() { };
            try
            {
                QaCriteria newCriteria = _qaTypeService.UpdateQaCriteria(qaType, criteriaCategory, text);
                _jsonResult.Data = new NewEntityModel() { Id = newCriteria.Id, Title = newCriteria.Text };
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