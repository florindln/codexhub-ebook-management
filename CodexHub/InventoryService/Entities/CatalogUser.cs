using CodexhubCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Entities
{
    public class CatalogUser : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<string> Interests { get; set; }
    }
}
