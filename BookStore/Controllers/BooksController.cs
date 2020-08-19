using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore.Helpers;
using System;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class BooksController : ApiController
    {
        JsonHelpers jsonHelper = new JsonHelpers();


        [HttpGet]
        [Route("api/books/")]
        public List<Book> Get()
        {
            return jsonHelper.ReadAllBook(jsonHelper.jsonFilePath);
        }

        [HttpGet]
        [Route("api/books/")]
        public HttpResponseMessage Get(int id)
        {
            var data = jsonHelper.ReadAllBook(jsonHelper.jsonFilePath).Where(x => x.Id == id).FirstOrDefault();
            if (null == data)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Id doesn't exist");
            }
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPut]
        [Route("api/books/")]
        public HttpResponseMessage Put([FromBody] Book newBook)
        {
            try
            {
                if ((newBook.Author == null) || (newBook.Title == null))
                {
                    var mes = string.Format("error : Fields is required");
                    HttpError error = new HttpError(mes);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                    return response;
                }
                else if (null != jsonHelper.ReadAllBook(jsonHelper.jsonFilePath).Where(x => x.Author == newBook.Author).FirstOrDefault() || null != jsonHelper.ReadAllBook(jsonHelper.jsonFilePath).Where(x => x.Title == newBook.Title).FirstOrDefault())
                {
                    var mes = string.Format("You cannot add books with the same author and title");
                    HttpError error = new HttpError(mes);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, error);
                    return response;
                }
                var addedBook = jsonHelper.AddBook(newBook);
                return Request.CreateResponse(HttpStatusCode.Created, addedBook);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
