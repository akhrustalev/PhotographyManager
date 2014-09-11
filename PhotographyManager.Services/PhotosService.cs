using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotographyManager.Model;
using System.Xml.Linq;
using PhotographyManager.DataAccess.Repositories;
using System.Net.Http;
using System.Drawing;
using PhotographyManager.DataAccess.UnitOfWork;

namespace PhotographyManager.Services
{
    public class PhotosService
    {
        public static List<Photo> GetBlockOfPhotos(List<Photo> list, int blockNumber, int blockSize)
        {
            int startIndex = (blockNumber - 1) * blockSize;
            List<Photo> photos = new List<Photo>();

            if (startIndex > list.Count) return photos;

            if (list.Count - startIndex < blockSize) photos = list.GetRange(startIndex, list.Count - startIndex);
            else photos = list.GetRange(startIndex, blockSize);

            return photos;
        }

        public async static Task<Photo> UploadAsync(System.IO.Stream inputStream, int length, IUnitOfWork unitOfWork, int userId)
        {
            //saving in big size
            byte[] image = new byte[length];
            inputStream.Read(image, 0, length);
            Photo photo = new Photo();
            photo.Image = new PhotoImage();
            photo.Image.BigImage = image;

            Bitmap originalImage = new Bitmap(Image.FromStream(inputStream));
            //saving in middle size
            Bitmap middleImage = new Bitmap(originalImage.Width/2, originalImage.Height/2);
            using (Graphics g = Graphics.FromImage((Image)middleImage))
                g.DrawImage(originalImage, 0, 0, originalImage.Width/2, originalImage.Height/2);
            ImageConverter converter = new ImageConverter();
            //saving in small size
            photo.Image.MiddleImage = (byte[])converter.ConvertTo(middleImage, typeof(byte[]));
            Bitmap miniImage = new Bitmap(200, 200);
            using (Graphics g = Graphics.FromImage((Image)miniImage))
                g.DrawImage(originalImage, 0, 0, 200, 200);
            photo.Image.MiniImage = (byte[])converter.ConvertTo(miniImage, typeof(byte[]));
            unitOfWork.Users.GetById(userId).Photo.Add(photo);
            unitOfWork.Commit();
            return photo;
        }
    }
}
