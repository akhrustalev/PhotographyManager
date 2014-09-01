using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;
using System.Xml.Linq;
using PhotographyManager.DataAccess.Repositories;

namespace PhotographyManager.Services
{
    public class MyServices
    {
        public static List<Photo> GetBlockOfPhotos(List<Photo> list, int blockNumber, int blockSize)
        {
            int startIndex = (blockNumber - 1) * blockSize;
            List<Photo> photos;

            if (list.Count - startIndex < blockSize) photos = list.GetRange(startIndex, list.Count - startIndex);
            else photos = list.GetRange(startIndex, blockSize);

            return photos;
        }

    }
}
