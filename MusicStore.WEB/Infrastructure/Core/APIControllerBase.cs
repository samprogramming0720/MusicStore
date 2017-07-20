using AutoMapper;
using MusicStore.DAL.Infrastructure;
using MusicStore.DAL.Repositories;
using MusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MusicStore.WEB.Infrastructure.Core
{
    public class APIControllerBase<Entity, VM> : ApiController where Entity:class where VM : class
    {
        protected readonly IGenericRepository<Error> _errorsRepository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGenericRepository<Entity> _entityRepository;

        protected APIControllerBase(IGenericRepository<Error> errorsRepository, IUnitOfWork unitOfWork, IGenericRepository<Entity> entityRepository)
        {
            _errorsRepository = errorsRepository;
            _unitOfWork = unitOfWork;
            _entityRepository = entityRepository;
        }

        protected HttpResponseMessage GetList(HttpRequestMessage request, Expression<Func<Entity, string>> orderByRule)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var entityList = _entityRepository.GetAll().OrderBy(orderByRule).ToList();
                if (entityList.Any())
                {
                    IEnumerable<VM> vmList = Mapper.Map<IEnumerable<Entity>, IEnumerable<VM>>(entityList);
                    response = request.CreateResponse(HttpStatusCode.OK, vmList);
                    return response;
                }
                response = request.CreateResponse(HttpStatusCode.NoContent);
                return response;
            });
        }

        protected HttpResponseMessage GetSingle(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var entity = _entityRepository.GetSingle(id);
                VM viewModel = Mapper.Map<Entity, VM>(entity);
                response = request.CreateResponse(HttpStatusCode.OK, viewModel);
                return response;
            });
        }

        protected HttpResponseMessage SearchBase(HttpRequestMessage request, Expression<Func<Entity, bool>> findByRule, Expression<Func<Entity, string>> orderByRule, 
            int? page, int? pageSize)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                IEnumerable<Entity> entityList = null;
                int total;
                if (findByRule!=null)
                {
                    var filtered = _entityRepository.FindBy(findByRule);
                    entityList = filtered.OrderBy(orderByRule).Skip(currentPage * currentPageSize).Take(currentPageSize).ToList();
                    total = filtered.Count();
                }
                else
                {
                    entityList = _entityRepository.GetAll().OrderBy(orderByRule).Skip(currentPage * currentPageSize).Take(currentPageSize).ToList();
                    total = _entityRepository.GetAll().Count();
                }
                IEnumerable<VM> vmList = Mapper.Map<IEnumerable<Entity>, IEnumerable<VM>>(entityList);

                PagingSet<VM> pagedSet = new PagingSet<VM>()
                {
                    Page = currentPage,
                    TotalCount = total,
                    TotalPages = (int)Math.Ceiling((decimal)total / currentPageSize),
                    Items = vmList,
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        


        #region Helper

        protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();
            }
            catch (DbUpdateException ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error _error = new Error()
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now
                };
                _errorsRepository.Add(_error);
                _unitOfWork.Save();
            }
            catch { }
        }
        #endregion
    }
}