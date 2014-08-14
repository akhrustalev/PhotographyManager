using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotographyManager.DataAccess.Repositories.Users;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;

namespace PhotographyManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IQueryableUnitOfWork _unitOfWork;

        public HomeController(IQueryableUnitOfWork uoW)
        {
            //var context = new PhotographyManagerContext();
            _unitOfWork = uoW;
            
            _userRepository = new UserRepository(_unitOfWork);
        }
        
        public ActionResult Index()
        {
            //User USER = new FreeUser();
            //USER.Id = 1;
            //USER.Name = "Reff";
            //_userRepository.AddUser(USER);
            //_unitOfWork.Commit();

            // USER = new FreeUser();
            //USER.Id = 2;
            //USER.Name = "Jeff";
            //_userRepository.AddUser(USER);
            //_unitOfWork.Commit();

            // USER = new FreeUser();
            //USER.Id = 3;
            //USER.Name = "Andrew";
            //_userRepository.AddUser(USER);
            //_unitOfWork.Commit();

            User user =new User();
            user.Id = 5;
            user.Name = "Peter";
            Album album1 = new Album();
            album1.Name = "Рыбалка";

            Album album2 = new Album();
            album2.Name = "Море";


            user.Album.Add(album1);
            user.Album.Add(album2);

            _userRepository.AddUser(user);
            _unitOfWork.Commit();


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
    }
}
