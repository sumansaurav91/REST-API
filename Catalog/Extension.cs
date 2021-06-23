using Catalog.DTOS;
using Catalog.Entities;

namespace Catalog
{
    public static class Extensions{
        public static ItemDtos AsDto(this Item item)
        {
            return new ItemDtos{
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}