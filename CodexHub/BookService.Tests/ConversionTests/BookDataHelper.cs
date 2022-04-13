using BookService.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookService.Tests.ConversionTests
{
    public class BookDataHelper
    {
        public static BookEntity GetBook() =>
            new BookEntity
            {
                Authors = new List<Author>(),
                Category = "category",
                Description = "description",
                Id = Guid.NewGuid(),
                InitialPrice = 0,
                PageCount = 0,
                PublishedDate = DateTime.MinValue,
                ThumbnailURL = "url",
                Title = "title",
            };
        public static string GetGoogleApiContent()
        {
            string content = "{  \"kind\": \"books#volumes\",  \"totalItems\": 676,  \"items\": [    {      \"kind\": \"books#volume\",      \"id\": \"FEA6EAAAQBAJ\",      \"etag\": \"kjIo6CgrELU\",      \"selfLink\": \"https://www.googleapis.com/books/v1/volumes/FEA6EAAAQBAJ\",      \"volumeInfo\": {        \"title\": \"Conscious Crafts: Quilting\",        \"subtitle\": \"20 Mindful Makes to Reconnect Head, Heart & Hands\",        \"authors\": [          \"Elli Beaven\"        ],        \"publisher\": \"Conscious Crafts\",        \"publishedDate\": \"2021-08-10\",        \"description\": \"Conscious Crafts: Quilting is a contemporary craft book containing 20 simple projects for gorgeous quilted pieces.\",        \"industryIdentifiers\": [          {            \"type\": \"ISBN_13\",            \"identifier\": \"9780711257450\"          },          {            \"type\": \"ISBN_10\",            \"identifier\": \"0711257450\"          }        ],        \"readingModes\": {          \"text\": false,          \"image\": true        },        \"pageCount\": 144,        \"printType\": \"BOOK\",        \"categories\": [          \"Crafts & Hobbies\"        ],        \"maturityRating\": \"NOT_MATURE\",        \"allowAnonLogging\": false,        \"contentVersion\": \"preview-1.0.0\",        \"panelizationSummary\": {          \"containsEpubBubbles\": false,          \"containsImageBubbles\": false        },        \"imageLinks\": {          \"smallThumbnail\": \"http://books.google.com/books/content?id=FEA6EAAAQBAJ&printsec=frontcover&img=1&zoom=5&edge=curl&source=gbs_api\",          \"thumbnail\": \"http://books.google.com/books/content?id=FEA6EAAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api\"        },        \"language\": \"en\",        \"previewLink\": \"http://books.google.nl/books?id=FEA6EAAAQBAJ&printsec=frontcover&dq=quilting&hl=&cd=1&source=gbs_api\",        \"infoLink\": \"http://books.google.nl/books?id=FEA6EAAAQBAJ&dq=quilting&hl=&source=gbs_api\",        \"canonicalVolumeLink\": \"https://books.google.com/books/about/Conscious_Crafts_Quilting.html?hl=&id=FEA6EAAAQBAJ\"      },      \"saleInfo\": {        \"country\": \"NL\",        \"saleability\": \"NOT_FOR_SALE\",        \"isEbook\": false      },      \"accessInfo\": {        \"country\": \"NL\",        \"viewability\": \"PARTIAL\",        \"embeddable\": true,        \"publicDomain\": false,        \"textToSpeechPermission\": \"ALLOWED\",        \"epub\": {          \"isAvailable\": false        },        \"pdf\": {          \"isAvailable\": false        },        \"webReaderLink\": \"http://play.google.com/books/reader?id=FEA6EAAAQBAJ&hl=&printsec=frontcover&source=gbs_api\",        \"accessViewStatus\": \"SAMPLE\",        \"quoteSharingAllowed\": false      },      \"searchInfo\": {        \"textSnippet\": \"Conscious Crafts: Quilting is a contemporary craft book containing 20 simple projects for gorgeous quilted pieces.\"      }    }]}";

            return content;
        }
    }
}
