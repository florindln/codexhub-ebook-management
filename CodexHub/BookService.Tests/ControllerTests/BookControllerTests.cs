using BookService.AppServices;
using BookService.Controllers;
using BookService.Data;
using BookService.Entities;
using BookService.Tests.ConversionTests;
using CodexhubCommon;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookService.Tests.ControllerTests
{
    public class BookControllerTests
    {
        [Fact]
        public async void GetByIdAsync_NotFound()
        {
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((BookEntity)null);
            var controller = new BooksController(null, new BookApp(mockRepo.Object, null),
                null, null);

            var bookRes = await controller.GetByIdAsync(Guid.Empty);

            Assert.IsType<NotFoundResult>(bookRes.Result);
        }

        [Fact]
        public async void GetByIdAsync_Success()
        {
            var book = BookDataHelper.GetBook();
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(repo => repo.GetAsync(Guid.Empty)).ReturnsAsync(book);
            var controller = new BooksController(null, new BookApp(mockRepo.Object, null),
                null, null);

            var bookRes = await controller.GetByIdAsync(Guid.Empty);

            Assert.Equal(bookRes.Value.Title, book.Title);
            Assert.Equal(bookRes.Value.Category, book.Category);
            Assert.Equal(bookRes.Value.PageCount, book.PageCount);
        }
    }
}
