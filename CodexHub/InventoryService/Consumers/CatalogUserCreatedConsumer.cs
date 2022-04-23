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
    public class CatalogUserCreatedConsumer : IConsumer<CatalogUserCreated>
    {
        private readonly IRepository<CatalogUser> repository;
        public CatalogUserCreatedConsumer(IRepository<CatalogUser> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogUserCreated> context)
        {
            var message = context.Message;
            var user = await repository.GetAsync(message.Id);

            if (user != null)
                return;

            user = new CatalogUser
            {
                Id = message.Id,
                Email = message.Email,
                Interests = message.Interests
            };

            await repository.CreateAsync(user);
        }
    }
}
