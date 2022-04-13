using BookService.Conversions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BookService.Tests.ConversionTests
{
    public class BookConversionTests
    {
        [Fact]
        public void GoogleAPI_Parsed_Correctly()
        {
            string content = BookDataHelper.GetGoogleApiContent();

            var book = Conversion.GoogleAPIContentToBooks(content).First();

            Assert.Equal("Conscious Crafts: Quilting", book.Title);
            Assert.Equal("Crafts & Hobbies", book.Category);
        }

        [Fact]
        public void BookEntity_To_BookDto()
        {
            var bookEntity = BookDataHelper.GetBook();

            var bookDto = bookEntity.AsDto();

            Assert.Equal(bookEntity.Category, bookDto.Category);
            Assert.Equal(bookEntity.Title, bookDto.Title);
            Assert.Equal(bookEntity.ThumbnailURL, bookDto.ThumbnailURL);
            Assert.Equal(bookEntity.Description, bookDto.Description);
            Assert.Equal(bookEntity.PublishedDate, bookDto.PublishedDate);
            Assert.Equal(bookEntity.PageCount, bookDto.PageCount);
        }
    }
}
