using BookService.Dtos;
using BookService.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Conversions
{
    public static class Conversion
    {
        public static BookDto AsDto(this BookEntity bookEntity)
        {
            if (bookEntity is null)
                return null;

            return new BookDto(bookEntity.Id, bookEntity.Title, bookEntity.Authors.Select(author => author.Name).ToList(), bookEntity.Description, bookEntity.PageCount, bookEntity.PublishedDate, bookEntity.Category, bookEntity.ThumbnailURL, bookEntity.InitialPrice);
        }

        public static List<BookEntity> GoogleAPIContentToBooks(string content)
        {
            JObject obj = JObject.Parse(content);
            var items = obj["items"];

            var books = new List<BookEntity>();

            foreach (var item in items)
            {
                try //ignore parsing errors
                {
                    var info = item["volumeInfo"];
                    string title = info["title"].ToString();
                    var authors = info["authors"].ToObject<List<string>>();
                    var random = new Random();

                    string publisher = info["publisher"].ToString();
                    string publishDate = info["publishedDate"].ToString();
                    string description = info["description"].ToString();
                    int pageCount = Int32.Parse(info["pageCount"].ToString());
                    string category = info["categories"][0].ToString();
                    var imageUrls = info["imageLinks"];
                    string thumbnailUrl = imageUrls["thumbnail"].ToString();
                    DateTime dateValue;
                    books.Add(new BookEntity
                    {
                        Authors = authors.Select(name => new Author { Name = name, }).ToList(),
                        Category = category,
                        Description = description,
                        InitialPrice = random.Next(1, 50),
                        PageCount = pageCount,
                        PublishedDate = DateTime.TryParse(publishDate, out dateValue) ? dateValue : dateValue,
                        ThumbnailURL = thumbnailUrl,
                        Title = title,
                        Id = Guid.NewGuid(),
                    });
                }
                catch { }
            }

            return books;
        }
    }
}
