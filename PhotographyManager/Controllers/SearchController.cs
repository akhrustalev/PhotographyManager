using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;
using System.Globalization;
using System.Web.Security;

namespace PhotographyManager.Controllers
{
    public class SearchController : BaseController
    {
        public SearchController(IUnitOfWork uoW)
        {
           _unitOfWork = uoW;
        }

        public ActionResult Search()
        {
            string keyword = String.Format(Request.Form["SearchText"]);
            List<Photo> searchResult =(List<Photo>)_unitOfWork.SearchPhotos(keyword);


            return View("SearchResult", searchResult);
        }

        public ActionResult AdvancedSearch()
        {
            return View("AdvancedSearch");
        }

        public ActionResult AdvancedSearchResult(AdvancedSearchModel model)
        {

            
            List<Photo> searchResult = (List<Photo>)_unitOfWork.AdvancedSearchPhotos((model.Name == null) ? "" : model.Name, (model.ShootingPlace == null) ? "" : model.ShootingPlace, model.ShotAfter, model.ShotBefore, (model.CameraModel == null) ? "" : model.CameraModel, (model.Diaphragm == null) ? "" : model.Diaphragm, (model.ISO == null) ? "" : model.ISO, model.ShutterSpeed, model.FocalDistance, model.Flash);
            return View("SearchResult", searchResult);
        }

    }
}
