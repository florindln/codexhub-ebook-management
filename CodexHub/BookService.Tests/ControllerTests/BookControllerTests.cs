using BookService.AppServices;
using BookService.Controllers;
using BookService.Conversions;
using BookService.Data;
using BookService.Dtos;
using BookService.Entities;
using BookService.Tests.ConversionTests;
using CodexhubCommon;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using StackExchange.Redis;
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
            var mockCache = new Mock<IDatabase>();

            mockCache.Setup(cache => cache.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync((string)null);

            mockRepo.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync((BookEntity)null);

            var controller = new BooksController(null, new BookApp(mockRepo.Object, mockCache.Object),
                null, null);

            var bookRes = await controller.GetByIdAsync(Guid.Empty);

            Assert.IsType<NotFoundResult>(bookRes.Result);
        }

        [Fact]
        public async void GetByIdAsync_Success()
        {
            var book = BookDataHelper.GetBook();
            var mockRepo = new Mock<IBookRepository>();
            var mockCache = new Mock<IDatabase>();

            mockCache.Setup(cache => cache.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>())).ReturnsAsync((string)null);

            mockRepo.Setup(repo => repo.GetAsync(Guid.Empty)).ReturnsAsync(book);

            var controller = new BooksController(null, new BookApp(mockRepo.Object, mockCache.Object),
                null, null);

            var bookRes = await controller.GetByIdAsync(Guid.Empty);

            Assert.Equal(book.Title, bookRes.Value.Title);
            Assert.Equal(book.Category, bookRes.Value.Category);
            Assert.Equal(book.PageCount, bookRes.Value.PageCount);
        }

        [Fact]
        public async void PostAsync_Success()
        {
            var book = BookDataHelper.GetBook();
            var mockRepo = new Mock<IBookRepository>();
            var mockPublishEndpoint = new Mock<IPublishEndpoint>();

            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<BookEntity>()));

            var controller = new BooksController(mockPublishEndpoint.Object, new BookApp(mockRepo.Object, null),
                null, null);

            var createBookDto = new CreateBookDto
            (
               "title",
               new List<string>(),
               "description",
               0,
               DateTime.MinValue,
               "category",
               "url",
               0
            );

            var bookRes = await controller.PostAsync(createBookDto);

            Assert.Equal(book.Title, bookRes.Value.Title);
            Assert.Equal(book.PageCount, bookRes.Value.PageCount);
            Assert.Equal(book.Category, bookRes.Value.Category);
        }

        [Fact]
        public async void PutAsync_Success()
        {
            var book = BookDataHelper.GetBook();
            var mockRepo = new Mock<IBookRepository>();
            var mockPublishEndpoint = new Mock<IPublishEndpoint>();
            var mockCache = new Mock<IDatabase>();

            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<BookEntity>()));
            mockRepo.Setup(repo => repo.GetAsync(It.IsAny<Guid>())).ReturnsAsync(book);

            var controller = new BooksController(mockPublishEndpoint.Object, new BookApp(mockRepo.Object, mockCache.Object),
                null, null);

            var updateBookDto = new UpdateBookDto(99, "newdescription");

            var bookRes = await controller.PutAsync(book.Id, updateBookDto);

            var okResult = bookRes as OkObjectResult;

            Assert.Equal(updateBookDto.InitialPrice, ((BookEntity)okResult.Value).InitialPrice);
            Assert.Equal(updateBookDto.Description, ((BookEntity)okResult.Value).Description);
        }

        [Fact]
        public async void DeleteAsync_Success()
        {
            var book = BookDataHelper.GetBook();
            var mockRepo = new Mock<IBookRepository>();
            var mockPublishEndpoint = new Mock<IPublishEndpoint>();
            var mockCache = new Mock<IDatabase>();

            var controller = new BooksController(mockPublishEndpoint.Object, new BookApp(mockRepo.Object, mockCache.Object),
                null, null);

            var bookRes = await controller.DeleteAsync(book.Id);

            Assert.IsType<NoContentResult>(bookRes);
        }
    }
}
