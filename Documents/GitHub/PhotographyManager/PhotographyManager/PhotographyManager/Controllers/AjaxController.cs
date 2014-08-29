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
    public class AjaxController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public class temp
        {
            public int ID;
            public string Name;
        }

        public AjaxController(IUnitOfWork uoW)
        {
           _unitOfWork = uoW;
        }

        [HttpGet]
        public string Get(int blockNumber, string albumName)
        {
            int BlockSize = 12;
            List<Photo> photos = MyServices.GetBlockOfPhotos(_unitOfWork.GetAlbums().GetByName(album => album.Name.Equals(albumName)).Photo.ToList(), blockNumber, BlockSize);
            List<temp> temps = new List<temp>();
            foreach(Photo item in photos)
            {
                temp t = new temp();
                t.Name = item.Name;
                t.ID = item.ID;
            }
            
            string result = System.Web.Helpers.Json.Encode(temps);

            return result;
        }

    }
}
