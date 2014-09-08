using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;


namespace PhotographyManager.Controllers
{
    public class AlbumController : BaseController
    {
        public AlbumController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public ActionResult ManageAlbums()
        {
            return View("ManageAlbums", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }
     
        
        [HttpGet]
        public ActionResult ObservePhotos(string albumName)
        {
            return View(_unitOfWork.Albums.GetByName(album => album.Name.Equals(albumName)));
        }
        
        public ActionResult AddAlbum()
        {
            if (_unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).GetType().BaseType.Equals(typeof(FreeUser)))
            {
                if (_unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).Album.Count == 5)
                {
                    ModelState.AddModelError("", "You can't create more than 5  albums because you are a free user");

                    return View("ManageAlbums", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
                }
            }
            return View("AddAlbum");
        }

        public ActionResult AddAlbumToUser()
        {
            Album album;
            album = new Album();
            album.Name = String.Format(Request.Form["Name"]);
            album.Discription = String.Format(Request.Form["Discription"]);

            if (_unitOfWork.Albums.GetByName(a => a.Name.Equals(album.Name)) != null)
            {
                ModelState.AddModelError("", "Album's name already exists. Please enter another name");
                return View("AddAlbum");
            }

            _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).Album.Add(album);
            _unitOfWork.Commit();

            return View("ManageAlbums", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult ManagePhotosInAlbum(string albumName)
        {
            ViewBag.AlbumName = albumName;
            return View("ManagePhotosInAlbum", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult DeletePhotoFromAlbum(string albumName, int photoId)
        {
            ViewBag.AlbumName = albumName;
            _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).Album.Where(album => album.Name.Equals(albumName)).First().Photo.Remove(_unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).Photo.Where(photo => photo.ID == photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }

        public ActionResult AddPhotoToAlbum(string albumName, int photoId)
        {
            ViewBag.AlbumName = albumName;
            _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).Album.Where(album => album.Name.Equals(albumName)).First().Photo.Add(_unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey).Photo.Where(photo => photo.ID == photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum", _unitOfWork.Users.GetById((int)Membership.GetUser().ProviderUserKey));
        }
    }
}
