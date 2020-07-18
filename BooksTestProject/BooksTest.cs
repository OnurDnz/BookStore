using BookStore.Helpers;
using BookStore.Models;
using NUnit.Framework;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace BooksTestProject
{
    [TestFixture]
    public class BooksTest
    {
        BookStore.Controllers.BooksController _controller;
        public BooksTest()
        {
            _controller = new BookStore.Controllers.BooksController();
            _controller.Request = new HttpRequestMessage();
            _controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey,
                                              new HttpConfiguration());
        }

        JsonHelpers helpers = new JsonHelpers();
        public static string ProjectPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string SolutionPath = ProjectPath.Substring(0, ProjectPath.Length - 27);
        string jsonFilePath = Path.Combine(SolutionPath + @"BookStore\JsonData\json.json");
        //string jsonFilePath = @"C:\Users\onurd\source\repos\OnurDnz\BookStore\BookStore\JsonData\json.json";
        
        [SetUp]
        public void SetUp()
        {
            helpers.ClearData(jsonFilePath);
        }

        [Test]
        public void isDataCountZero_dataCountShouldBeZero_True()
        {
            var dataCount = helpers.ReadAllBook(jsonFilePath);

            Assert.AreEqual(dataCount.Count == 0, true);
        }

        [Test]
        public void requiredFieldsCheck_ShouldBeReturnBadRequest_ReturnBadRequest()
        {
            var response = _controller.Put(new Book { });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void requiredFieldsMessageCheck_ShouldBeReturnMessage_ReturnMessage()
        {
            var response = _controller.Put(new Book { });
            var data = response.Content.ReadAsStringAsync();
            var message = data.Result;
            Assert.AreEqual(message, "{\"Message\":\"error : Fields is required\"}");
        }

        [Test]
        public void putNewBook_ShouldBeAddNewBook_RetrunOk()
        {
            var response = _controller.Put(new Book
            {
                Author = "Robert Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public void putDuplicateBook_ShouldntBeAddDuplicateBook_ReturnBadRequest()
        {
            _controller.Put(new Book
            {
                Author = "Robert Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
            });
            var response = _controller.Put(new Book
            {
                Author = "Robert Martin",
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
            });
            Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
        }

    }
}
