using CodexhubCommon;
using InventoryService.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Contracts.Contracts;

namespace InventoryService.Consumers
{
    public class CatalogBookUpdatedConsumer : IConsumer<CatalogBookUpdated>
    {
        private readonly IRepository<CatalogBook> repository;
        public CatalogBookUpdatedConsumer(IRepository<CatalogBook> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogBookUpdated> context)
        {
            var message = context.Message;
            var book = await repository.GetAsync(message.Id);

            if (book == null)
            {
                book = new CatalogBook
                {
                    Id = message.Id,
                    InitialPrice = message.Price,
                };

                await repository.CreateAsync(book);
            }
            else
            {
                book.InitialPrice = message.Price;

                await repository.UpdateAsync(book);
            }
        }
    }
}
