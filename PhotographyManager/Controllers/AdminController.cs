using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.Web.Filters;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;

namespace PhotographyManager.Web.Controllers
{
    [PhotographyManagerAuthorize(Roles="Admin")]
    public class AdminController :BaseController
    {
        public AdminController(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }
        public ActionResult ManageUsers()
        {
            User currentUser = _unitOfWork.Users.GetOne(user => user.Name.Equals(User.Identity.Name));
            List<User> users = new List<User>();
            users = _unitOfWork.Users.GetMany(user => user.ID != currentUser.ID).ToList();
            return View("ManageUsers", users);
        }

        public ActionResult AddChangedUsers()
        {
            List<User> users = _unitOfWork.Users.GetMany(u => !u.Name.Equals(User.Identity.Name), u => u.Membership).ToList();
            foreach(User user in users)
            {
                if (Request.Form[user.Name]!=null&&Request.Form[user.Name]=="Free")
                {
                    if (!user.GetType().BaseType.Equals(typeof(FreeUser)))
                    {
                        User changedUser = new FreeUser { Name = user.Name };
                        List<Photo> photos = _unitOfWork.Photos.GetMany(p => p.UserID == user.ID, p => p.PhotoImage,p=>p.Album).ToList();
                        List<PhotoImage> photoImages = new List<PhotoImage>();
                        for (int i = 0; i < photos.Count; i++)
                        {
                            int id = photos.ElementAt(i).ID;
                            photoImages.Add(_unitOfWork.PhotoImages.GetOne(p => p.ID == id));
                        }
                            foreach (Photo photo in photos)
                            {
                                for (int i = 0; i < photo.Album.Count; i++)
                                {
                                    _unitOfWork.Albums.GetById(photo.Album.ElementAt(i).ID).Photo.Remove(photo);
                                }
                                _unitOfWork.PhotoImages.Remove(_unitOfWork.PhotoImages.GetOne(photoImage => photoImage.ID == photo.ID));
                                _unitOfWork.Photos.GetById(photo.ID).User = null;
                                _unitOfWork.Photos.GetById(photo.ID).Album = null;
                                _unitOfWork.Photos.GetById(photo.ID).PhotoImage = null;
                                _unitOfWork.Photos.Remove(photo);
                            }
                        List<Album> albums = _unitOfWork.Albums.GetMany(a => a.UserID == user.ID,a=>a.Photo).ToList();
                        foreach (Album album in albums)
                        {
                            _unitOfWork.Albums.Remove(album);
                        }
                        string password = user.Membership.Password;
                        string passwordSalt = user.Membership.PasswordSalt;

                        _unitOfWork.UserMembership.Remove(user.Membership);

                        _unitOfWork.Users.Remove(_unitOfWork.Users.GetById(user.ID));
                        _unitOfWork.Commit();

                        changedUser.Roles.Add(_unitOfWork.UserRoles.GetOne(role => role.RoleName.Equals("User")));
                        _unitOfWork.UserRoles.GetOne(role => role.RoleName.Equals("User"), u => u.User).User.Add(changedUser);
                        _unitOfWork.Users.Add(changedUser);
                        _unitOfWork.Commit();
                        int userId = _unitOfWork.Users.GetOne(u => u.Name.Equals(changedUser.Name)).ID;
                        UserMembership membership = new UserMembership { ID = userId, Password = password, PasswordSalt = passwordSalt };
                        _unitOfWork.UserMembership.Add(membership);
                        _unitOfWork.Commit();
                        foreach (Photo photo in photos)
                        {
                            _unitOfWork.Users.GetById(userId).Photo.Add(photo);
                        }
                        foreach (PhotoImage photoImage in photoImages)
                        {
                            _unitOfWork.PhotoImages.Add(photoImage);
                        }
                        _unitOfWork.Commit();
                        foreach (Album album in albums)
                        {
                            _unitOfWork.Users.GetById(userId).Album.Add(album);
                        }
                        _unitOfWork.Commit();
                    }
                }
                else
                {
                    if (user.GetType().BaseType.Equals(typeof(FreeUser)))
                    {
                        User changedUser = new PaidUser { Name = user.Name };
                        List<Photo> photos = _unitOfWork.Photos.GetMany(p => p.UserID == user.ID, p => p.PhotoImage, p => p.Album).ToList();
                        List<PhotoImage> photoImages = new List<PhotoImage>();
                        for (int i = 0; i < photos.Count; i++)
                        {
                            int id = photos.ElementAt(i).ID;
                            photoImages.Add(_unitOfWork.PhotoImages.GetOne(p => p.ID == id));
                        }
                        foreach (Photo photo in photos)
                        {
                            for (int i = 0; i < photo.Album.Count; i++)
                            {
                                _unitOfWork.Albums.GetById(photo.Album.ElementAt(i).ID).Photo.Remove(photo);
                            }
                            _unitOfWork.PhotoImages.Remove(_unitOfWork.PhotoImages.GetOne(photoImage => photoImage.ID == photo.ID));
                            _unitOfWork.Photos.GetById(photo.ID).User = null;
                            _unitOfWork.Photos.GetById(photo.ID).Album = null;
                            _unitOfWork.Photos.GetById(photo.ID).PhotoImage = null;
                            _unitOfWork.Photos.Remove(photo);
                        }
                        List<Album> albums = _unitOfWork.Albums.GetMany(a => a.UserID == user.ID, a => a.Photo).ToList();
                        foreach (Album album in albums)
                        {
                            _unitOfWork.Albums.Remove(album);
                        }
                        string password = user.Membership.Password;
                        string passwordSalt = user.Membership.PasswordSalt;

                        _unitOfWork.UserMembership.Remove(user.Membership);

                        _unitOfWork.Users.Remove(_unitOfWork.Users.GetById(user.ID));
                        _unitOfWork.Commit();

                        changedUser.Roles.Add(_unitOfWork.UserRoles.GetOne(role => role.RoleName.Equals("User")));
                        _unitOfWork.UserRoles.GetOne(role => role.RoleName.Equals("User"), u => u.User).User.Add(changedUser);
                        _unitOfWork.Users.Add(changedUser);
                        _unitOfWork.Commit();
                        int userId = _unitOfWork.Users.GetOne(u => u.Name.Equals(changedUser.Name)).ID;
                        UserMembership membership = new UserMembership { ID = userId, Password = password, PasswordSalt = passwordSalt };
                        _unitOfWork.UserMembership.Add(membership);
                        _unitOfWork.Commit();
                        foreach (Photo photo in photos)
                        {
                            _unitOfWork.Users.GetById(userId).Photo.Add(photo);
                        }
                        foreach (PhotoImage photoImage in photoImages)
                        {
                            _unitOfWork.PhotoImages.Add(photoImage);
                        }
                        _unitOfWork.Commit();
                        foreach (Album album in albums)
                        {
                            _unitOfWork.Users.GetById(userId).Album.Add(album);
                        }
                        _unitOfWork.Commit();
                    }

                }
            }
            return RedirectToAction("MyHomePage","Home");
        }
        

    }
}
