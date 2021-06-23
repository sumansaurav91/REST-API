using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.DTOS;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{

    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IInMemItemsRepository repository;

        public ItemsController(IInMemItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDtos> GetItems()
        {
            var items = repository.GetItems().Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDtos> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item.AsDto());
        }

        [HttpPost]
        public ActionResult<ItemDtos> CreateItem(CreateItemDto createItemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = createItemDto.Name,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };


            repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new {  id = item.Id }, item.AsDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            var existingItem = repository.GetItem(id);

            if(existingItem != null)
            {
                Item item = existingItem with
                {
                    Name = updateItemDto.Name,
                    Price = updateItemDto.Price
                };

                repository.UpdateItem(item);
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if(existingItem == null)
            {
                return NotFound();
            }
            
            repository.DeleteItem(id);

            return NoContent();
        }
    }
}