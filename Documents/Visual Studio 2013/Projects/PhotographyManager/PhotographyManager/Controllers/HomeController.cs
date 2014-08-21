using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.DataAccess.Repositories.Users;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
namespace PhotographyManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IQueryableUnitOfWork _unitOfWork;
        Album album;
        public HomeController(IQueryableUnitOfWork uoW)
        {
            //var context = new PhotographyManagerContext();
            _unitOfWork = uoW;
            
            _userRepository = new UserRepository(_unitOfWork);

            
        }
        
        public ActionResult Index()
        {
            //User user = new FreeUser();
            //user.Name = "Arthur";
            //Album a1 = new Album();
            //Photo p1 = new Photo();
            //p1.Name = "Ship";
            //Photo p2 = new Photo();
            //p2.Name = "Sun";
            //a1.Photo.Add(p1);
            //a1.Photo.Add(p2);
            //a1.Name = "Ocean";
            //Album a2 = new Album();
            //a2.Name = "Seaside";
            //user.Album.Add(a1);
            //user.Album.Add(a2);
            //_userRepository.AddUser(user);
            //_unitOfWork.Commit();


            return View("Index",_userRepository);
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }

        public ActionResult ObservePhotos(int ind1, int ind2)
        {

            return View(_userRepository.GetAll()[ind1].Album.ElementAt(ind2));
        }

        public ActionResult MyHomePage(int id)
        {
            User user = _userRepository.GetById(id);
            return View(user);
        }

        public ActionResult ManagePhotos(int id)
        {

            
            return View(_userRepository.GetById(id));
        }

        public ActionResult ManageAlbums(int id)
        {
            return View("ManageAlbums",_userRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Upload(int Id)
        {
            
            HttpPostedFileBase file = Request.Files["OriginalLocation"];
            Int32 length = file.ContentLength;
            byte[] image = new byte[length];
            file.InputStream.Read(image, 0, length);
            Photo photo = new Photo();
            photo.Image = image;
            _userRepository.GetById(Id).Photo.Add(photo);
            _unitOfWork.Commit();
            return View("ManagePhotos", _userRepository.GetById(Id));
        }

        [HttpGet]
        public ActionResult ShowPhoto(int id, string photoNumber)
        {
             //Photo photo = _userRepository.GetById(id).Photo.ElementAt(int.Parse(photoNumber));
             ////MemoryStream ms = new MemoryStream(photo.Image);
             
             ////Image returnImage = Image.FromStream(ms);
             ////ms.Close();
             ImageResult imageResult = new ImageResult(_userRepository.GetById(id).Photo.ElementAt(int.Parse(photoNumber)).Image, "");
             return imageResult;
         }

        public ActionResult AddAlbum(int id)
        {

            return View("AddAlbum",_userRepository.GetById(id));
            
        }

        public ActionResult AddAlbumToUser(int id)
        {
            album = new Album();
            album.Name = String.Format(Request.Form["Name"]);
            album.Discription = String.Format(Request.Form["Discription"]);

            _userRepository.GetById(id).Album.Add(album);
            _unitOfWork.Commit();

            return View("ManageAlbums", _userRepository.GetById(id));
        }

        public ActionResult ManagePhotosInAlbum(int id, string albumName)
        {
            ViewBag.AlbumName = albumName;
            return View("ManagePhotosInAlbum",_userRepository.GetById(id));
        }

        public ActionResult DeletePhotoFromAlbum(int id, string albumName, int photoId)
        {
            ViewBag.AlbumName = albumName;
            _userRepository.GetById(id).Album.Where(album =>album.Name.Equals(albumName)).First().Photo.Remove(_userRepository.GetById(id).Photo.Where(photo =>photo.ID==photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum",_userRepository.GetById(id));

        }

        public ActionResult AddPhotoToAlbum(int id, string albumName, int photoId)
        {
            ViewBag.AlbumName = albumName;
            _userRepository.GetById(id).Album.Where(album => album.Name.Equals(albumName)).First().Photo.Add(_userRepository.GetById(id).Photo.Where(photo => photo.ID == photoId).First());
            _unitOfWork.Commit();
            return View("ManagePhotosInAlbum", _userRepository.GetById(id));

        }




    }
}
