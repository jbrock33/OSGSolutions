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
    [RoutePrefix("api/upload"), AllowAnonymous]
    public class FileUploadController : ApiController
    {
            [HttpPost]
            [Route("image")]
            public HttpResponseMessage FileUpload(EncodedImage encodedImage)
            {
                try
                {
                    FileUploadService fSvc = new FileUploadService();


                    byte[] newBytes = Convert.FromBase64String(encodedImage.EncodedImageFile); ;
                    UploadFile model = new UploadFile();
                    model.FileUploadName = "img";
                    model.ByteArray = newBytes;
                    model.Extension = encodedImage.FileExtension;
                    model.Location = "TestImages";

                    int fileId = fSvc.Insert(model);

                    ItemResponse<int> resp = new ItemResponse<int>();
                    resp.Item = fileId;

                    return Request.CreateResponse(HttpStatusCode.OK, resp);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
    }
}