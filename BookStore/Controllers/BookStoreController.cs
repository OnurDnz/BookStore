using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers
{
    public class BookStoreController : ApiController
    {
        static Book book = new Book();
        static List<Book> books = new List<Book>();

        [HttpGet]
        public List<Book> Get()
        {
            return books;
        }

        [HttpGet]
        public Book Get(int _id)
        {
            return books.Where(x => x.Id == _id).FirstOrDefault();
        }

        [HttpPost]
        public HttpResponseMessage Post(Book otherBook)
        {
            book.Books.Add(new Book { Author = otherBook.Author, Title = otherBook.Title });
            // books.Add(new Book { Author = book.Author, Title = book.Title });
            //books.Add(new Book { Author = book.Author, Title = book.Title, Id = 4 });

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public void Put(int id, Book book)
        {
        }

        // DELETE: api/BookStore/5
        public void Delete(int id)
        {
        }
    }
}
