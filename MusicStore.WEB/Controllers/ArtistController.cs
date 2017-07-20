using AutoMapper;
using MusicStore.DAL.Infrastructure;
using MusicStore.DAL.Repositories;
using MusicStore.Entities;
using MusicStore.WEB.Infrastructure.ActionFilter;
using MusicStore.WEB.Infrastructure.Core;
using MusicStore.WEB.Infrastructure.Extensions;
using MusicStore.WEB.Models;
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
    [RoutePrefix("api/Artist")]
    public class ArtistController:APIControllerBase<Artist, ArtistViewModel>
    {

        public ArtistController(IGenericRepository<Artist> artistRepository,
            IGenericRepository<Error> errorRepository, IUnitOfWork unitOfWork)
            :base(errorRepository, unitOfWork, artistRepository)
        {
        }

        [AllowAnonymous]
        [Route("List")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return GetList(request, a=>a.Name);
        }

        [AllowAnonymous]
        [Route("search/{page:int=0}/{pageSize=4}/{filter?}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int? page, int? pageSize, string filter = null)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                return SearchBase(request, m => m.Name.ToLower().Contains(filter.ToLower().Trim()), m => m.Name, page, pageSize);
            }
            return SearchBase(request, null, m => m.Name, page, pageSize);
        }

        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ArtistViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                Artist newArtist = new Artist();
                newArtist.UpdateArtist(model);

                _entityRepository.Add(newArtist);

                _unitOfWork.Save();

                model = Mapper.Map<Artist, ArtistViewModel>(newArtist);
                response = request.CreateResponse(HttpStatusCode.Created, model);

                return response;
            });
        }

        [MimeMultipart]
        [HttpPost]
        [Route("upload/{artistId}")]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request, int artistId)
        {
            HttpResponseMessage response = null;

            var artist = _entityRepository.GetSingle(artistId);
            if (artist == null)
            {
                response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid");
            }

            var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/Artist");

            var streamProvider = new UploadMultipartFormProvider(uploadPath);

            await Request.Content.ReadAsMultipartAsync(streamProvider);

            string _localFileName = streamProvider.FileData.Select(multipartData => multipartData.LocalFileName).FirstOrDefault();

            FileUploadResult fileUploadResult = new FileUploadResult
            {
                LocalFilePath = _localFileName,
                FileName = Path.GetFileName(_localFileName),
                FileLength = new FileInfo(_localFileName).Length
            };

            artist.Image = fileUploadResult.FileName;
            _entityRepository.Edit(artist);
            _unitOfWork.Save();
            response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
            return response;
        }
    }
}