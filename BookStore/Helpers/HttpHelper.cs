using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BookStore.Helpers
{
    public class HttpHelper : Controllers.BooksController
    {
        public HttpResponseMessage ReturnResponseWithMessage(HttpStatusCode code, string message)
        {
            var mes = string.Format(message);
            HttpError error = new HttpError(mes);
            HttpResponseMessage response = Request.CreateResponse(code, error);
            return response;
        }
        public HttpResponseMessage ReturnResponseWithData(HttpStatusCode code, object data)
        {
            HttpResponseMessage response = Request.CreateResponse(code, data);
            return response;
        }
    }
}