using InventoryService.Dtos;
using InventoryService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Conversion
{
    public static class Conversion
    {
        public static BookUserDto AsDto(this BookUserEntity entity) =>
            new BookUserDto(entity.BookId, entity.BoughtAt);
    }
}
