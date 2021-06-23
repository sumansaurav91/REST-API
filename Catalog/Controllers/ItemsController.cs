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
    }
}