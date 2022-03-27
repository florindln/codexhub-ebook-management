using System;

namespace Contracts
{
    public class Contracts
    {
        public record CatalogBookCreated(Guid Id, string Title, double InitialPrice);
        public record CatalogBookUpdated(Guid Id, double Price);
        public record CatalogBookDeleted(Guid Id);

    }
}
