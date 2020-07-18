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
        HttpHelper httpHelper = new HttpHelper();

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
            if (null == jsonHelper.ReadAllBook(jsonHelper.jsonFilePath).Where(x => x.Id == id).FirstOrDefault())
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Id doesn't exist");
            }
            var data = jsonHelper.ReadAllBook(jsonHelper.jsonFilePath).Where(x => x.Id == id).FirstOrDefault();
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
                    return httpHelper.ReturnResponseWithMessage(HttpStatusCode.BadRequest, "error : Fields is required");
                }
                else if ((null != jsonHelper.ReadAllBook(jsonHelper.jsonFilePath).Where(x => x.Author == newBook.Author).FirstOrDefault()) || null != jsonHelper.ReadAllBook(jsonHelper.jsonFilePath).Where(x => x.Title == newBook.Title).FirstOrDefault())
                {
                    return httpHelper.ReturnResponseWithMessage(HttpStatusCode.BadRequest, "You cannot add books with the same author and title");
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
