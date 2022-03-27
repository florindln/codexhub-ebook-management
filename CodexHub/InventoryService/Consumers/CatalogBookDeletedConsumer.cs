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
    public class CatalogBookDeletedConsumer : IConsumer<CatalogBookDeleted>
    {
        private readonly IRepository<CatalogBook> repository;
        public CatalogBookDeletedConsumer(IRepository<CatalogBook> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogBookDeleted> context)
        {
            var message = context.Message;
            var book = await repository.GetAsync(message.Id);

            if (book == null)
                return;

            await repository.DeleteAsync(message.Id);
        }
    }
}
