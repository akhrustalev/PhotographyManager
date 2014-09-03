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

namespace PhotographyManager.Controllers
{
    public class PhotoController : BaseController
    {
        public PhotoController(IUnitOfWork uoW)
        {
           _unitOfWork = uoW;
        }

        public ActionResult ManagePhotos()
        {
            return View(_unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }

        [HttpPost]
        public ActionResult Upload()
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

            //saving in big size
            byte[] image = new byte[length];
            file.InputStream.Read(image, 0, length);
            Photo photo = new Photo();
            photo.Image = image;

            Bitmap originalImage = new Bitmap(Image.FromStream(file.InputStream));
            //saving in middle size
            Bitmap middleImage = new Bitmap(400, 400);
            using (Graphics g = Graphics.FromImage((Image)middleImage))
                g.DrawImage(originalImage, 0, 0, 400, 400);
            ImageConverter converter = new ImageConverter();
            //saving in small size
            photo.MiddleImage = (byte[])converter.ConvertTo(middleImage, typeof(byte[]));
            Bitmap miniImage = new Bitmap(200, 200);
            using (Graphics g = Graphics.FromImage((Image)miniImage))
                g.DrawImage(originalImage, 0, 0, 200, 200);
            photo.MiniImage = (byte[])converter.ConvertTo(miniImage, typeof(byte[]));


            _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).Photo.Add(photo);
            _unitOfWork.Commit();
            return View("ManagePhotos", currentUser);
        }

        [HttpGet]
        public ActionResult ShowPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).Image, "jpeg");
            return result;
        }

        [HttpGet]
        public ActionResult ShowMiddlePhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).MiddleImage, "jpeg");
            return result;
        }
        [HttpGet]
        public ActionResult ShowMiniPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.Photos.GetById(id).MiniImage, "jpeg");
            return result;
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
