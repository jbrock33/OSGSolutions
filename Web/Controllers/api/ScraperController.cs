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
    [RoutePrefix("api/scraper"), AllowAnonymous]
    public class ScraperController : ApiController
    {
        [Route]
        [HttpGet]
        public HttpResponseMessage GetAll()
        {
            try
            {
                ItemResponse<ScraperList> resp = new ItemResponse<ScraperList>();
                ScraperService svc = new ScraperService();

                resp.Item = svc.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}