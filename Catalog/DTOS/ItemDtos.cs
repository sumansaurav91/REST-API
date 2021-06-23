using System;

namespace Catalog.DTOS
{

    public record ItemDtos
    {
         public Guid Id { get; init; }

        public string Name { get; init; }

        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}