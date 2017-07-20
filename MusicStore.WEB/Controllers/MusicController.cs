using AutoMapper;
using MusicStore.DAL;
using MusicStore.DAL.Infrastructure;
using MusicStore.DAL.Repositories;
using MusicStore.Entities;
using MusicStore.WEB.Infrastructure.Core;
using MusicStore.WEB.Infrastructure.Extensions;
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
    [RoutePrefix("api/Music")]
    public class MusicController : APIControllerBase<Music, MusicViewModel>
    {
        private readonly IGenericRepository<Artist> _artistRepository;

        public MusicController(IGenericRepository<Music> musicRepository,
            IGenericRepository<Artist> artistRepository,
            IGenericRepository<Error> errorRepository,
            IUnitOfWork unitOfWork)
            : base(errorRepository, unitOfWork, musicRepository)
        {
            _artistRepository = artistRepository;
        }

        [AllowAnonymous]
        [Route("List")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return GetList(request, t => t.Title);
        }

        [AllowAnonymous]
        [Route("Single/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return GetSingle(request, id);
        }

        [AllowAnonymous]
        [Route("searchByArtist/{page:int=0}/{pageSize=4}/{filter:int=0}")]
        public HttpResponseMessage GetForArtist(HttpRequestMessage request, int? page, int? pageSize, int? filter)
        {
            return SearchBase(request, m => m.ArtistID == filter.Value, m => m.Title, page, pageSize);
        }

        [AllowAnonymous]
        [Route("searchByAlbum/{page:int=0}/{pageSize=4}/{filter:int=0}")]
        public HttpResponseMessage GetForAlbum(HttpRequestMessage request, int? page, int? pageSize, int? filter)
        {
            return SearchBase(request, m => m.AlbumID == filter.Value, m => m.Title, page, pageSize);
        }

        [AllowAnonymous]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return SearchBase(request, m => m.Title.ToLower().Contains(filter.ToLower().Trim()), m => m.Title, page, pageSize);
            }
            return SearchBase(request, null, m => m.Title, page, pageSize);
        }
        
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, MusicViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                Music newMusic = new Music();
                newMusic.UpdateMusic(model);
                _entityRepository.Add(newMusic);
                _unitOfWork.Save();

                model = Mapper.Map<Music, MusicViewModel>(newMusic);
                response = request.CreateResponse(HttpStatusCode.Created, model);

                return response;
            });
        }


    }
}