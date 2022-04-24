using System;
using System.Collections.Generic;

namespace Contracts
{
    public class Contracts
    {
        public record CatalogBookCreated(Guid Id, string Title, string Description, double InitialPrice, string Category, string ThumbnailURL);
        public record CatalogBookUpdated(Guid Id, string Description, double Price);
        public record CatalogBookDeleted(Guid Id);

        public record CatalogUserCreated(Guid Id, string Email, List<string> Interests);
        public record CatalogUserUpdated(Guid Id, List<string> Interests);
        public record CatalogUserDeleted(Guid Id);

    }
}
