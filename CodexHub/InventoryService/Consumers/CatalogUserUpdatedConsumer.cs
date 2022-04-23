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
    public class CatalogUserUpdatedConsumer : IConsumer<CatalogUserUpdated>
    {
        private readonly IRepository<CatalogUser> repository;
        public CatalogUserUpdatedConsumer(IRepository<CatalogUser> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<CatalogUserUpdated> context)
        {
            var message = context.Message;
            var user = await repository.GetAsync(message.Id);

            if (user == null)
            {
                user = new CatalogUser
                {
                    Id = message.Id,
                    Interests = message.Interests,
                };

                await repository.CreateAsync(user);
            }
            else
            {
                user.Interests = message.Interests;

                await repository.UpdateAsync(user);
            }
        }
    }
}
