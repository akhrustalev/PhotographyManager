using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Security;

namespace PhotographyManager.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork uoW)
        {
           _unitOfWork = uoW;
        }
        
        public ActionResult Index()
        {
            return View("Index",_unitOfWork);
        }

        public ActionResult ObservePhotos(string albumName)
        {
            
            return View(_unitOfWork.GetAlbums().GetByName(album =>album.Name.Equals(albumName)));
        }

        public ActionResult MyHomePage()
        {
            
            return View(_unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult ManagePhotos()
        {

            return View(_unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult ManageAlbums()
        {
            return View("ManageAlbums",_unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
        }

        [HttpPost]
        public ActionResult Upload()
        {
           //(int)Membership.GetUser().ProviderUserKey) 
            HttpPostedFileBase file = Request.Files["OriginalLocation"];
            Int32 length = file.ContentLength;
            byte[] image = new byte[length];
            file.InputStream.Read(image, 0, length);
            Photo photo = new Photo();
            photo.Image = image;
            _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Photo.Add(photo);
            _unitOfWork.Commit();
            return View("ManagePhotos", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
        }

        [HttpGet]
        public ActionResult ShowPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.GetPhotos().GetById(id).Image, "jpeg");
            return result;
        }

        

        public ActionResult AddAlbum()
        {

            return View("AddAlbum", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
            
        }

        public ActionResult AddAlbumToUser()
        {
            Album album;
            album = new Album();
            album.Name = String.Format(Request.Form["Name"]);
            album.Discription = String.Format(Request.Form["Discription"]);

            _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Album.Add(album);
            _unitOfWork.Commit();

            return View("ManageAlbums", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult ManagePhotosInAlbum(string albumName)
        {
            ViewBag.AlbumName = albumName;
            return View("ManagePhotosInAlbum", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult DeletePhotoFromAlbum(string albumName, int photoId)
        {
            ViewBag.AlbumName = albumName;
            _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Album.Where(album => album.Name.Equals(albumName)).First().Photo.Remove(_unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Photo.Where(photo => photo.ID == photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));

        }

        public ActionResult AddPhotoToAlbum(string albumName, int photoId)
        {
            ViewBag.AlbumName = albumName;
            _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Album.Where(album => album.Name.Equals(albumName)).First().Photo.Add(_unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Photo.Where(photo => photo.ID == photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));

        }

    }
}
