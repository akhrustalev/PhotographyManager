﻿using System;
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
using PhotographyManager.Web.Filters;

namespace PhotographyManager.Web.Controllers
{
    [ExceptionHandler]
    public class LoadingPhotosController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public LoadingPhotosController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public List<int> GetLoadingPhotos(int blockNumber, string albumName, int blockSize)
        {
            List<Photo> photos = PhotosService.GetBlockOfPhotos(_unitOfWork.Albums.GetOne(album => album.Name.Equals(albumName),a => a.Photo).Photo.ToList(), blockNumber, blockSize);
            List<int> list = new List<int>();
            foreach(Photo item in photos)
            {
                list.Add(item.ID);
            }
            return list ;
        }
    }
}
