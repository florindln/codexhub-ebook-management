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
    public class CatalogUserDeletedConsumer : IConsumer<CatalogUserDeleted>
    {
        private readonly IRepository<CatalogUser> repository;
        public CatalogUserDeletedConsumer(IRepository<CatalogUser> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogUserDeleted> context)
        {
            var message = context.Message;
            var user = await repository.GetAsync(message.Id);

            if (user == null)
                return;

            await repository.DeleteAsync(message.Id);
        }
    }
}
