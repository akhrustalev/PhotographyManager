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
using PhotographyManager.Web.Filters;

namespace PhotographyManager.Web.Controllers
{
    public class PhotoController : BaseController
    {
        public PhotoController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        [PhotographyManagerAuthorize]
        public ActionResult ManagePhotos()
        {            
            return View(_unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name),user => user.Photo.Select(photo => photo.PhotoImage)));
        }

        [PhotographyManagerAuthorize]
        [HttpPost]
        public async Task<ActionResult> UploadAsync()
        {
            HttpPostedFileBase file = Request.Files[0];
            Int32 length = file.ContentLength;
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name), u => u.Photo.Select(p => p.PhotoImage));

            if (PhotosService.TooManyPhoto(currentUser)){
                    ModelState.AddModelError("", "You can't upload more than 30 photos because you are a free user");

                    return View("ManagePhotos", currentUser);
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
            if (!file.ContentType.Equals("image/jpeg"))
            {
                ModelState.AddModelError("", "Image must be JPEG");

                return View("ManagePhotos", currentUser);
            }
            Photo photo = await PhotosService.UploadAsync(file.InputStream, length,_unitOfWork,currentUser.ID);
            return View("ManagePhotos", _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name), u => u.Photo.Select(p => p.PhotoImage)));
        }


        [HttpGet]
        public ActionResult ShowPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id, p => p.PhotoImage).PhotoImage.BigImage, "jpeg");
            return result;
        }

        [HttpGet]
        public ActionResult ShowMiddlePhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id, p => p.PhotoImage).PhotoImage.MiddleImage, "jpeg");
            return result;
        }
        [HttpGet]
        public ActionResult ShowMiniPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id,p => p.PhotoImage).PhotoImage.MiniImage, "jpeg");
            return result;
        }

        public ActionResult ShowMiniPhotoOnIndex(int id,int albumId)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Albums.GetById(albumId,
                album => album.Photo.Select(photo => photo.PhotoImage)).Photo.ElementAt(id).PhotoImage.MiniImage, "jpeg");
            return result;
        }

        public ActionResult ShowCurrentPhoto(int id, int ind)
        {
            ViewBag.Ind = ind;
            return PartialView("CurrentPhoto", _unitOfWork.Photos.GetById(id, p => p.PhotoImage));
        }

        public ActionResult ShowCurrentPhotoOnIndex(int id, int albumId)
        {
            ViewBag.Ind = id;
            return PartialView("CurrentPhoto",_unitOfWork.Albums.GetById(albumId,album => album.Photo.Select(photo => photo.PhotoImage)).Photo.ElementAt(id));
        }

        public ActionResult ShowBigCurrentPhoto(int id)
        {
            return View("CurrentPhotoBigSize",_unitOfWork.Photos.GetById(id,photo => photo.PhotoImage));
        }

        [PhotographyManagerAuthorize]
        public ActionResult EditPhotosProperties(int id)
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            if (_unitOfWork.Photos.GetById(id).UserID != currentUser.ID)
                return View("Error");
            return View(_unitOfWork.Photos.GetById(id));
        }
        [PhotographyManagerAuthorize]
        [HttpPost]
        public ActionResult AddEditedProperties(Photo photo,int id)
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            if (_unitOfWork.Photos.GetById(id).UserID != currentUser.ID)
                return View("Error");
            Photo editedPhoto = _unitOfWork.Photos.GetById(id);
            editedPhoto.Name = photo.Name;
            editedPhoto.ShootingPlace = photo.ShootingPlace;
            editedPhoto.CameraModel = photo.CameraModel;
            editedPhoto.ShootingTime = photo.ShootingTime;
            editedPhoto.FocalDistance = photo.FocalDistance;
            editedPhoto.Diaphragm = photo.Diaphragm;
            editedPhoto.ISO = photo.ISO;
            editedPhoto.ShutterSpeed = photo.ShutterSpeed;
            editedPhoto.Flash = photo.Flash;
            _unitOfWork.Commit();
            return RedirectToAction("ManagePhotos","Photo");
        }

        [PhotographyManagerAuthorize]
        [HttpPost]
        public ActionResult DeletePhoto(int id)
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            if (_unitOfWork.Photos.GetById(id).UserID != currentUser.ID)
                return View("Error");
            Photo photo = _unitOfWork.Photos.GetById(id);
            for (int i = 0; i < photo.Album.Count; i++)
            {
                _unitOfWork.Albums.GetById(photo.Album.ElementAt(i).ID).Photo.Remove(photo);
            }
            currentUser.Photo.Remove(photo);
            _unitOfWork.PhotoImages.Remove(_unitOfWork.PhotoImages.GetOne(photoImage => photoImage.ID == id));
            photo.User = null;
            photo.Album = null;
            photo.PhotoImage = null;
            _unitOfWork.Photos.Remove(photo);
            _unitOfWork.Commit();
            return null;
            //return View("ManagePhotos", _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name), user => user.Photo.Select(p => p.PhotoImage)));
        }
    }
}
