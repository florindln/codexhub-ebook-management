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
    public class CatalogBookCreatedConsumer : IConsumer<CatalogBookCreated>
    {
        private readonly IRepository<CatalogBook> repository;
        public CatalogBookCreatedConsumer(IRepository<CatalogBook> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogBookCreated> context)
        {
            var message = context.Message;
            var book = await repository.GetAsync(message.Id);

            if (book != null)
                return;

            book = new CatalogBook
            {
                Id = message.Id,
                InitialPrice = message.InitialPrice,
                Title = message.Title,
            };

            await repository.CreateAsync(book);
        }
    }
}
