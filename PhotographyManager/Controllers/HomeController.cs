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
using PhotographyManager.Services;
using System.Data.SqlClient;
using System.Globalization;


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
            return View(_unitOfWork.GetAlbums().GetByName(album=>album.Name.Equals(albumName)));
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
            HttpPostedFileBase file = Request.Files["OriginalLocation"];
            Int32 length = file.ContentLength;
            User currentUser = _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey);

            
            if (currentUser.GetType().BaseType.Equals(typeof(FreeUser)))
            {
                if (currentUser.Photo.Count==30)
                {
                    ModelState.AddModelError("", "You can't upload more than 30 photos because you are a free user");

                    return View("ManagePhotos", currentUser);
                }
            }




            //Проверка, был ли выбран файл
            if (length == 0)
            {
                ModelState.AddModelError("", "No image was chosen");

                return View("ManagePhotos", currentUser);
            }
            //Проверка, не превышает ли объем файла 500 килобайт
            if (length > 500*1024)
            {
                ModelState.AddModelError("", "The image size is more than 500K. Please choose smaller image");

                return View("ManagePhotos", currentUser);
            }
            //Проверка,является ли файл JPEG
            if (System.IO.Path.GetExtension(file.FileName).ToLower() != ".jpg")
            {
                ModelState.AddModelError("", "Image must be JPEG");
                
                return View("ManagePhotos", currentUser);
            }

            //Сохранение в полном размере
            byte[] image = new byte[length];
            file.InputStream.Read(image, 0, length);
            Photo photo = new Photo();
            photo.Image = image;

            Bitmap originalImage = new Bitmap(Image.FromStream(file.InputStream));
            //Сохранение в среднем размере
            Bitmap middleImage = new Bitmap(400,400);
            using (Graphics g = Graphics.FromImage((Image)middleImage))
                g.DrawImage(originalImage, 0, 0, 400, 400);
            ImageConverter converter = new ImageConverter();
            photo.MiddleImage =  (byte[])converter.ConvertTo(middleImage, typeof(byte[]));
            //Сохранение в миниатюре
            Bitmap miniImage = new Bitmap(200,200);
            using (Graphics g = Graphics.FromImage((Image)miniImage))
                g.DrawImage(originalImage, 0, 0, 200, 200);
            photo.MiniImage = (byte[])converter.ConvertTo(miniImage, typeof(byte[]));


            _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Photo.Add(photo);
            _unitOfWork.Commit();
            return View("ManagePhotos", currentUser);
        }

        [HttpGet]
        public ActionResult ShowPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.GetPhotos().GetById(id).Image, "jpeg");
            return result;
        }
        [HttpGet]
        public ActionResult ShowMiddlePhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.GetPhotos().GetById(id).MiddleImage, "jpeg");
            return result;
        }
        [HttpGet]
        public ActionResult ShowMiniPhoto(int id)
        {
            FileContentResult result = new FileContentResult(_unitOfWork.GetPhotos().GetById(id).MiniImage, "jpeg");
            return result;
        }

        public ActionResult AddAlbum()
        {
            if (_unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).GetType().BaseType.Equals(typeof(FreeUser)))
            {
                if (_unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Album.Count == 5)
                {
                    ModelState.AddModelError("", "You can't create more than 5  albums because you are a free user");

                    return View("ManageAlbums", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
                }
            }
            return View("AddAlbum");            
        }

        public ActionResult AddAlbumToUser()
        {
            try
            {
                Album album;
                album = new Album();
                album.Name = String.Format(Request.Form["Name"]);
                album.Discription = String.Format(Request.Form["Discription"]);

                if (_unitOfWork.GetAlbums().GetByName(a => a.Name.Equals(album.Name)) != null) throw new DuplicateNameException();

                _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey).Album.Add(album);
                _unitOfWork.Commit();

                return View("ManageAlbums", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
            }
            catch (DuplicateNameException e)
            {
                ModelState.AddModelError("","Album's name already exists. Please enter another name");
            }
            return View("AddAlbum");
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

        public ActionResult Search()
        {
            string keyword = String.Format(Request.Form["SearchText"]);

            List<Photo> searchResult = _unitOfWork.GetPhotos().Search(keyword);

           
            return View("SearchResult",searchResult);
        }

        public ActionResult EditPhotosProperties(int id)
        {
            return View(_unitOfWork.GetPhotos().GetById(id));
        }

        public ActionResult AddEditedProperties(int id)
        {
            string name = String.Format(Request.Form["Name"]);
            string shootingPlace = String.Format(Request.Form["ShootingPlace"]);
            DateTime shootingTime = DateTime.Parse(String.Format(Request.Form["ShootingTime"]));
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
                flash=false;
            }
            _unitOfWork.GetPhotos().GetById(id).Name = name;
            _unitOfWork.GetPhotos().GetById(id).ShootingPlace = shootingPlace;
            _unitOfWork.GetPhotos().GetById(id).ShootingTime = shootingTime;
            _unitOfWork.GetPhotos().GetById(id).CameraModel = cameraModel;
            _unitOfWork.GetPhotos().GetById(id).FocalDistance = focalDistance;
            _unitOfWork.GetPhotos().GetById(id).Diaphragm = diaphragm;
            _unitOfWork.GetPhotos().GetById(id).ISO = ISO;
            _unitOfWork.GetPhotos().GetById(id).ShutterSpeed = shutterSpeed;
            _unitOfWork.GetPhotos().GetById(id).Flash = flash;
            _unitOfWork.Commit();
            return View("ManagePhotos", _unitOfWork.GetUsers().GetById((int)Membership.GetUser().ProviderUserKey));
        }
    }
}
