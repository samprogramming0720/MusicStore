using AutoMapper;
using MusicStore.DAL.Infrastructure;
using MusicStore.DAL.Repositories;
using MusicStore.Entities;
using MusicStore.WEB.Infrastructure.ActionFilter;
using MusicStore.WEB.Infrastructure.Core;
using MusicStore.WEB.Infrastructure.Extensions;
using MusicStore.WEB.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MusicStore.WEB.Controllers
{
    [Authorize]
    [RoutePrefix("api/Album")]
    public class AlbumController : APIControllerBase<Album, AlbumViewModel>
    {

        public AlbumController(IGenericRepository<Album> albumRepository,
            IGenericRepository<Error> errorRepository, 
            IUnitOfWork unitOfWork)
            :base(errorRepository, unitOfWork, albumRepository)
        {
        }

        [AllowAnonymous]
        [Route("List")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return GetList(request, a=>a.AlbumTitle);
        }

        [AllowAnonymous]
        [Route("List/{ArtistID:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int ArtistID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var Album = _entityRepository.FindBy(a => a.ArtistID == ArtistID).OrderBy(a => a.AlbumTitle).ToList();

                IEnumerable<AlbumViewModel> vmList = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumViewModel>>(Album);
                response = request.CreateResponse(HttpStatusCode.OK, vmList);
                return response;
            });
        }
        [AllowAnonymous]
        [Route("searchByArtist/{page:int=0}/{pageSize=4}/{filter:int=0}")]
        public HttpResponseMessage GetByArtist(HttpRequestMessage request, int? page, int? pageSize, int? filter)
        {
            return SearchBase(request, m => m.ArtistID == filter.Value, m => m.AlbumTitle, page, pageSize);
        }


        [AllowAnonymous]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return SearchBase(request, m => m.AlbumTitle.ToLower().Contains(filter.ToLower().Trim()), m => m.AlbumTitle, page, pageSize);
            }
            return SearchBase(request, null, m => m.AlbumTitle, page, pageSize);
        }

        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, AlbumViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                Album newAlbum = new Album();
                newAlbum.UpdateAlbum(model);
                _entityRepository.Add(newAlbum);
                _unitOfWork.Save();
                model = Mapper.Map<Album, AlbumViewModel>(newAlbum);
                response = request.CreateResponse(HttpStatusCode.Created, model);

                return response;
            });
        }

        [MimeMultipart]
        [HttpPost]
        [Route("upload/{albumId}")]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request, int albumId)
        {
            HttpResponseMessage response = null;

            var album = _entityRepository.GetSingle(albumId);
            if(album == null)
            {
                response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid");
            }

            var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/AlbumCover");

            var streamProvider = new UploadMultipartFormProvider(uploadPath);

            await Request.Content.ReadAsMultipartAsync(streamProvider);

            string _localFileName = streamProvider.FileData.Select(multipartData => multipartData.LocalFileName).FirstOrDefault();

            FileUploadResult fileUploadResult = new FileUploadResult
            {
                LocalFilePath = _localFileName,
                FileName = Path.GetFileName(_localFileName),
                FileLength = new FileInfo(_localFileName).Length
            };

            album.AlbumCover = fileUploadResult.FileName;
            _entityRepository.Edit(album);
            _unitOfWork.Save();

            response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
            return response;
        }
    }
}