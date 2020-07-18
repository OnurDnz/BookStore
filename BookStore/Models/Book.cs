using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace BookStore.Models
{
    public class Book
    {
        public Book() { }
        public Book(int bookListCount, Book book)
        {
            book.Id = bookListCount;
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public int Id { get; private set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
    }
}