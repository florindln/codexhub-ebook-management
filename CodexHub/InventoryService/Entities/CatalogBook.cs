using CodexhubCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Entities
{
    public class CatalogBook : IEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double InitialPrice { get; set; }
        public string Category { get; set; }
        public string ThumbnailURL { get; set; }
    }
}
