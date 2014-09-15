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
            return View(currentUser);
        }

        [HttpPost]
        public async Task<ActionResult> UploadAsync()
        {
            HttpPostedFileBase file = Request.Files[0];
            Int32 length = file.ContentLength;

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
            Photo photo = await PhotosService.UploadAsync(file.InputStream, length,_unitOfWork,currentUser.ID);
            return View("ManagePhotos", currentUser);
        }

        [HttpGet]
        public ActionResult ShowPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).PhotoImage.BigImage, "jpeg");
            return result;
        }

        [HttpGet]
        public ActionResult ShowMiddlePhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).PhotoImage.MiddleImage, "jpeg");
            return result;
        }
        [HttpGet]
        public ActionResult ShowMiniPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).PhotoImage.MiniImage, "jpeg");
            return result;
        }

        public ActionResult ShowMiniPhotoOnIndex(int id,int albumId)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Albums.GetById(albumId).Photo.ElementAt(id).PhotoImage.MiniImage, "jpeg");
            return result;
        }

        public ActionResult ShowCurrentPhoto(int id, int ind)
        {
            ViewBag.Ind = ind;
            return PartialView("CurrentPhoto",_unitOfWork.Photos.GetById(id));
        }

        public ActionResult ShowCurrentPhotoOnIndex(int id, int albumId)
        {
            ViewBag.Ind = id;
            return PartialView("CurrentPhoto",_unitOfWork.Albums.GetById(albumId).Photo.ElementAt(id));
        }

        public ActionResult EditPhotosProperties(int id)
        {
            if (_unitOfWork.Photos.GetById(id).UserID != currentUser.ID)
                return View("Error");
            return View(_unitOfWork.Photos.GetById(id));
        }
        [HttpPost]
        public ActionResult AddEditedProperties(Photo photo,int id)
        {
            if (_unitOfWork.Photos.GetById(id).UserID != currentUser.ID)
                return View("Error");
            _unitOfWork.Photos.GetById(id).Name = photo.Name;
            _unitOfWork.Photos.GetById(id).ShootingPlace = photo.ShootingPlace;
            _unitOfWork.Photos.GetById(id).CameraModel =photo.CameraModel;
            _unitOfWork.Photos.GetById(id).FocalDistance = photo.FocalDistance;
            _unitOfWork.Photos.GetById(id).Diaphragm = photo.Diaphragm;
            _unitOfWork.Photos.GetById(id).ISO = photo.ISO;
            _unitOfWork.Photos.GetById(id).ShutterSpeed = photo.ShutterSpeed;
            _unitOfWork.Photos.GetById(id).Flash = photo.Flash;
            _unitOfWork.Commit();
            return View("Error");
        }

        public ActionResult DeletePhoto(int id)
        {
            if (_unitOfWork.Photos.GetById(id).UserID != currentUser.ID)
                return View("Error");
            Photo photo = _unitOfWork.Photos.GetById(id);
            for (int i = 0; i < photo.Album.Count; i++ )
            {
                _unitOfWork.Albums.GetById(photo.Album.ElementAt(i).ID).Photo.Remove(photo);
            }
            currentUser.Photo.Remove(photo);
            _unitOfWork.PhotoImages.Remove(_unitOfWork.PhotoImages.GetOne(photoImage => photoImage.ID == id));
            _unitOfWork.Photos.GetById(id).User = null;
            _unitOfWork.Photos.GetById(id).Album = null;
            _unitOfWork.Photos.GetById(id).PhotoImage = null;
            _unitOfWork.Photos.Remove(photo);
            _unitOfWork.Commit();
            return View("ManagePhotos",currentUser);
        }
    }
}
