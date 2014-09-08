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

        public LoadingPhotosController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public List<int> GetLoadingPhotos(int blockNumber, string albumName)
        {
            int BlockSize = 4;
            List<Photo> photos = PhotosService.GetBlockOfPhotos(_unitOfWork.Albums.GetByName(album => album.Name.Equals(albumName)).Photo.ToList(), blockNumber, BlockSize);
            List<int> list = new List<int>();
            foreach(Photo item in photos)
            {
                list.Add(item.ID);
            }
            return list ;
        }
    }
}
