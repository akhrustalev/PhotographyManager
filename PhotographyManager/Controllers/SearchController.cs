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
            return View("AdvancedSearch", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult AdvancedSearchResult()
        {
            string name = String.Format(Request.Form["Name"]);
            string shootingPlace = String.Format(Request.Form["ShootingPlace"]);
            DateTime shootingTime = DateTime.Now;
            if (String.Format(Request.Form["ShootingTime"]) != "")
            {
                shootingTime = DateTime.Parse(String.Format(Request.Form["ShootingTime"]));
            }

            string cameraModel = String.Format(Request.Form["CameraModel"]);
            string diaphragm = String.Format(Request.Form["Diaphragm"]);
            string ISO = String.Format(Request.Form["ISO"]);
            double focalDistance = 0;
            if (!double.TryParse(Request.Form["FocalDistance"], NumberStyles.Float, CultureInfo.InvariantCulture, out focalDistance))
            {
                focalDistance = 0;
            }
            double shutterSpeed = 0;
            if (!double.TryParse(Request.Form["ShutterSpeed"], NumberStyles.Float, CultureInfo.InvariantCulture, out shutterSpeed))
            {
                shutterSpeed = 0;
            }
            bool flash;
            if (Request.Form["Flash"] != null && Request.Form["Flash"] == "on")
            {
                flash = true;
            }
            else
            {
                flash = false;
            }
            List<Photo> searchResult = new List<Photo>();
            //List<Photo> searchResult = _unitOfWork.Photos.AdvancedSearch(name,shootingPlace,shootingTime,cameraModel,diaphragm,ISO,shutterSpeed,focalDistance,flash);
            return View("SearchResult", searchResult);
        }

    }
}
