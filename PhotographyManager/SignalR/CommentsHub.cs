using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PhotographyManager.DataAccess.UnitOfWork;
using PhotographyManager.Model;

namespace PhotographyManager.SignalR
{
    public class CommentsHub : Hub
    {
        //IUnitOfWork _unitOfWork;
        //public CommentsHub(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}
        
        public void PushComment(string comment, int photoId)
        {
            IUnitOfWork _unitOfWork = new UnitOfWork();
            Comment comm = new Comment{Text = comment};
            _unitOfWork.Photos.GetById(photoId).Comment.Add(comm);
            _unitOfWork.Users.GetOne(user => user.Name.Equals(HttpContext.Current.User.Identity.Name),user => user.Comment).Comment.Add(comm);
            _unitOfWork.Commit();
            Clients.All.acceptComment(comment,photoId);
        }
    }
}