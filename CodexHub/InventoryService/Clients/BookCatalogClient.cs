using InventoryService.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InventoryService.Clients
{
    public class BookCatalogClient
    {
        private readonly HttpClient httpClient;
        public BookCatalogClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IReadOnlyCollection<CatalogBookDto>> GetCatalogBooksAsync()
        {
            var items = await httpClient.GetAsync("v1/books");
            var content = await items.Content.ReadAsStringAsync();
            var bookList = JsonConvert.DeserializeObject<IReadOnlyCollection<CatalogBookDto>>(content);

            return bookList;
        }
    }
}
