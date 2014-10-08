using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Web.Filters;
using PhotographyManager.Services;


namespace PhotographyManager.Web.Controllers
{
    public class AlbumController : BaseController
    {
        public AlbumController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }
        [PhotographyManagerAuthorize]
        public ActionResult ManageAlbums()
        {
            return View("ManageAlbums", _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name),user=>user.Album.Select(album=>album.Photo.Select(photo=>photo.PhotoImage))));
        }    
        
        [HttpGet]
        public ActionResult ObservePhotos(string albumName)
        {
            return View(_unitOfWork.Albums.GetOne(a => a.Name.Equals(albumName),album=>album.Photo.Select(photo=>photo.PhotoImage),album=>album.User));
        }
        [PhotographyManagerAuthorize]
        public ActionResult AddAlbum()
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            if (PhotosService.TooManyAlbums(currentUser))
            {
                    ModelState.AddModelError("", "You can't create more than 5  albums because you are a free user");

                    return View("ManageAlbums",currentUser);
            }
            return View("AddAlbum");
        }
        [PhotographyManagerAuthorize]
        public ActionResult AddAlbumToUser()
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            Album album;
            album = new Album();
            album.Name = String.Format(Request.Form["Name"]);
            album.Discription = String.Format(Request.Form["Discription"]);

            if (_unitOfWork.Albums.GetOne(a => a.Name.Equals(album.Name)) != null)
            {
                ModelState.AddModelError("", "Album's name already exists. Please enter another name");
                return View("AddAlbum");
            }

            _unitOfWork.Users.GetById(currentUser.ID).Album.Add(album);
            _unitOfWork.Commit();

            return View("ManageAlbums", _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name), u => u.Album.Select(a => a.Photo.Select(p => p.PhotoImage))));
        }
        [PhotographyManagerAuthorize]
        public ActionResult ManagePhotosInAlbum(string albumName)
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name),u=>u.Photo);
            if (_unitOfWork.Albums.GetOne(album => album.Name.Equals(albumName)).UserID != currentUser.ID)
                return View("Error");
            ViewBag.AlbumName = albumName;
            return View("ManagePhotosInAlbum", _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name), u => u.Photo, u => u.Album.Select(a => a.Photo.Select(p => p.PhotoImage))));
        }
        [PhotographyManagerAuthorize]
        [HttpPost]
        public ActionResult DeletePhotoFromAlbum(string albumName, int photoId)
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            if (_unitOfWork.Albums.GetOne(album => album.Name.Equals(albumName)).UserID != currentUser.ID)
                return View("Error");
            ViewBag.AlbumName = albumName;
            _unitOfWork.Users.GetById(currentUser.ID,u=>u.Album.Select(a=>a.Photo.Select(p=>p.PhotoImage))).Album.Where(album => album.Name.Equals(albumName)).First().Photo.Remove(_unitOfWork.Users.GetById(currentUser.ID,u=>u.Photo).Photo.Where(photo => photo.ID == photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum", _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name), u => u.Photo,u=>u.Album.Select(a=>a.Photo.Select(p=>p.PhotoImage))));
        }
        [PhotographyManagerAuthorize]
        public ActionResult AddPhotoToAlbum(string albumName, int photoId)
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            if (_unitOfWork.Albums.GetOne(album => album.Name.Equals(albumName)).UserID != currentUser.ID)
                return View("Error");
            ViewBag.AlbumName = albumName;
            _unitOfWork.Users.GetById(currentUser.ID,u=>u.Album.Select(a=>a.Photo.Select(p=>p.PhotoImage))).Album.Where(album => album.Name.Equals(albumName)).First().Photo.Add(_unitOfWork.Users.GetById(currentUser.ID,u=>u.Photo).Photo.Where(photo => photo.ID == photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum", _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name), u => u.Photo, u => u.Album.Select(a => a.Photo.Select(p => p.PhotoImage))));
        }

        public ActionResult GetLink()
        {
           string result = Request.Url.AbsoluteUri;
           ViewBag.Url = result;
           return PartialView("UrlView");
        }
    }
}
