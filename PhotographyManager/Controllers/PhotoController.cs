using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;
using System.Web.Security;
using System.Drawing;
using System.Globalization;
using PhotographyManager.Services;
using System.Threading.Tasks;

namespace PhotographyManager.Controllers
{
    public class PhotoController : BaseController
    {
        public PhotoController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public ActionResult ManagePhotos()
        {
            return View(_unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }

        [HttpPost]
        public async Task<ActionResult> UploadAsync()
        {
            HttpPostedFileBase file = Request.Files["OriginalLocation"];
            Int32 length = file.ContentLength;
            User currentUser = _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey);
            if (currentUser.GetType().BaseType.Equals(typeof(FreeUser)))
            {
                if (currentUser.Photo.Count == 30)
                {
                    ModelState.AddModelError("", "You can't upload more than 30 photos because you are a free user");

                    return View("ManagePhotos", currentUser);
                }
            }


            if (length == 0)
            {
                ModelState.AddModelError("", "No image was chosen");

                return View("ManagePhotos", currentUser);
            }
            if (length > 500 * 1024)
            {
                ModelState.AddModelError("", "The image size is more than 500K. Please choose smaller image");

                return View("ManagePhotos", currentUser);
            }
            if (System.IO.Path.GetExtension(file.FileName).ToLower() != ".jpg")
            {
                ModelState.AddModelError("", "Image must be JPEG");

                return View("ManagePhotos", currentUser);
            }
            await PhotosService.UploadAsync(file.InputStream, length,_unitOfWork,(int)Membership.GetUser().ProviderUserKey);
            return View("ManagePhotos", currentUser);
        }

        [HttpGet]
        public ActionResult ShowPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).Image.BigImage, "jpeg");
            return result;
        }

        [HttpGet]
        public ActionResult ShowMiddlePhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).Image.MiddleImage, "jpeg");
            return result;
        }
        [HttpGet]
        public ActionResult ShowMiniPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).Image.MiniImage, "jpeg");
            return result;
        }

        public ActionResult ShowCurrentPhoto(int id)
        {
            return PartialView("CurrentPhoto",_unitOfWork.Photos.GetById(id));
        }

        public ActionResult EditPhotosProperties(int id)
        {
            return View(_unitOfWork.Photos.GetById(id));
        }
        [HttpPost]
        public ActionResult AddEditedProperties(Photo photo,int id)
        {
            _unitOfWork.Photos.GetById(id).Name = photo.Name;
            _unitOfWork.Photos.GetById(id).ShootingPlace = photo.ShootingPlace;
            _unitOfWork.Photos.GetById(id).CameraModel =photo.CameraModel;
            _unitOfWork.Photos.GetById(id).FocalDistance = photo.FocalDistance;
            _unitOfWork.Photos.GetById(id).Diaphragm = photo.Diaphragm;
            _unitOfWork.Photos.GetById(id).ISO = photo.ISO;
            _unitOfWork.Photos.GetById(id).ShutterSpeed = photo.ShutterSpeed;
            _unitOfWork.Photos.GetById(id).Flash = photo.Flash;
            _unitOfWork.Commit();
            return View("ManagePhotos", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }
    }
}
