using BookStore.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Helpers;

namespace BookStore.Helpers
{
    public class JsonHelpers
    {
        //public string jsonFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"JsonData\json.json"));
        public string jsonFilePath = @"C:\Users\onurd\source\repos\OnurDnz\BookStore\BookStore\JsonData\json.json";
       
        public Book AddBook(Book book)
        {
            var jsonData = File.ReadAllText(jsonFilePath);
            var bookList = JsonConvert.DeserializeObject<List<Book>>(jsonData);
            new Book(bookList.Count + 1, book);
            bookList.Add(book);
            jsonData = JsonConvert.SerializeObject(bookList);
            File.WriteAllText(jsonFilePath, jsonData);
            return book;
        }

        public List<Book> ReadAllBook(string path)
        {
            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(path));
            return books;
        }

        public void ClearData(string filePath)
        {
            var jsonData = File.ReadAllText(filePath);
            var newData = jsonData.Remove(1, jsonData.Length - 2);
            File.WriteAllText(filePath, newData);
        }
    }
}