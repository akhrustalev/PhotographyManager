using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Helpers;
using PhotographyManager.Services;
using PhotographyManager.Model;
using PhotographyManager.DataAccess.UnitOfWork;
using System.IO;

namespace PhotographyManager.Controllers
{
    public class LoadingPhotosController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        
        
        public class parameters
        {
            public int BlockNumber { get; set; }
            public string AlbumName { get; set; }
        }


        public LoadingPhotosController(IUnitOfWork uoW)
        {
           _unitOfWork = uoW;
        }

        [HttpPost]
        public string Get(int blockNumber, string albumName)
        {
            int BlockSize = 12;
            List<Photo> photos = MyServices.GetBlockOfPhotos(_unitOfWork.Albums.GetByName(album => album.Name.Equals(albumName)).Photo.ToList(), blockNumber, BlockSize);
            List<int> temps = new List<int>();
            foreach(Photo item in photos)
            {
                temps.Add(item.ID);
            }
            
            string result = System.Web.Helpers.Json.Encode(temps);

            return result;
        }

    }
}
