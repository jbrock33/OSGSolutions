using OSGSolutions.Models.Domain;
using OSGSolutions.Models.Responses;
using OSGSolutions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Web.Controllers.api
{
    [RoutePrefix("api/statuses"), AllowAnonymous]
    public class StatusApiController : ApiController
    {
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemsResponse<StatusEntry> resp = new ItemsResponse<StatusEntry>();
                StatusService svc = new StatusService();

                resp.Items = svc.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                StatusService svc = new StatusService();
                svc.Delete(id);
                SuccessResponse resp = new SuccessResponse();
                
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                StatusService svc = new StatusService();
                ItemResponse<StatusEntry> resp = new ItemResponse<StatusEntry>();
                resp.Item = svc.GetById(id);

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("{id:int}")]
        [HttpPut]
        public HttpResponseMessage Put(int id, StatusEntry model)
        {
            try
            {
                StatusService svc = new StatusService();
                svc.Update(model);
                SuccessResponse resp = new SuccessResponse();

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route]
        [HttpPost]
        public HttpResponseMessage Post(StatusEntry model)
        {
            try
            {
                StatusService svc = new StatusService();
                int id = svc.Insert(model);

                ItemResponse<int> resp = new ItemResponse<int>();
                resp.Item = id;

                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}