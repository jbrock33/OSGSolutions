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
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                UsersService svc = new UsersService();
                ItemsResponse<Users> resp = new ItemsResponse<Users>();
                resp.Items = svc.SelectAll();
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("{email}")]                                                                  // need to include '/' after email when making api call
        public HttpResponseMessage GetByEmail(string email)
        {
            try
            {
                UsersService svc = new UsersService();
                ItemResponse<Users> resp = new ItemResponse<Users>();
                resp.Item = svc.SelectByEmail(email);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route]
        public HttpResponseMessage Post(RegisterUser model)
        {
            try
            {
                UsersService svc = new UsersService();
                model.Email = model.Email.ToLower();
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
        

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login(LoginUser model)
        {
            UsersService svc = new UsersService();

            bool res = false;

            try
            {
                res = svc.Login(model);
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            

        }
    }
}