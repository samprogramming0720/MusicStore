using AutoMapper;
using MusicStore.DAL.Infrastructure;
using MusicStore.DAL.Repositories;
using MusicStore.Entities;
using MusicStore.WEB.Infrastructure.Core;
using MusicStore.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MusicStore.WEB.Controllers
{
    [Authorize]
    [RoutePrefix("api/MyPlayList")]
    public class MyPlayListController:APIControllerBase<PlayList, PlayListViewModel>
    {

        public MyPlayListController(IGenericRepository<PlayList> playListRepository,
            IGenericRepository<Error> errorRepository,
            IUnitOfWork unitOfWork):base(errorRepository, unitOfWork, playListRepository)
        {
        }

        [Route("List")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return GetList(request, pl=>pl.Name);
        }
    }
}