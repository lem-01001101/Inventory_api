using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory1_API.Models.DTO;
using Inventory1_API.Models.Domain;
using Inventory1_API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Inventory1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository itemRepository;
        private readonly ISupplierRepository supplierRepository;

        public ItemController(IItemRepository itemRepository, ISupplierRepository supplierRepository)
        {
            this.itemRepository = itemRepository;
            this.supplierRepository = supplierRepository;
        }

        // post; {apibaseurl}/api/Item
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemRequestDto request)
        {
            // convert dto to domain
            var item = new Item
            {
                Name = request.Name,
                Amount = request.Amount,
                Category = request.Category,
                Note = request.Note,
                LastInventory = request.LastInventory,
                LastManager = request.LastManager,
                Supplier = new List<Supplier>()
            };

            foreach (var supplierGuid in request.Supplier)
            {
                var existingSupplier = await supplierRepository.GetById(supplierGuid);
                if (existingSupplier is not null)
                {
                    item.Supplier.Add(existingSupplier);
                }
            }

            item = await itemRepository.CreateAsync(item);

            // convert domain model back to dto
            var response = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Amount = item.Amount,
                Category = item.Category,
                Note = item.Note,
                LastInventory = item.LastInventory,
                LastManager = item.LastManager,
                Supplier = item.Supplier.Select(x => new SupplierDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok();
        }


        // get:{apibaseurl}/api/Item
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await itemRepository.GetAllAsync();

            // convert dom model to dto
            var response = new List<ItemDto>();
            foreach (var item in items)
            {
                response.Add(new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.Amount,
                    Category = item.Category,
                    Note = item.Note,
                    LastInventory = item.LastInventory,
                    LastManager = item.LastManager,
                    Supplier = item.Supplier.Select(x => new SupplierDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
            }

            return Ok(response);
        }

        // get:{apibaseurl}/api/Item/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetItemById([FromRoute] Guid id)
        {
            // get item from repo
            var item = await itemRepository.GetByIdAsync(id);

            if(item is null)
            {
                return NotFound();
            }

            // convert domain model to DTO
            var response = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Amount = item.Amount,
                Category = item.Category,
                Note = item.Note,
                LastInventory = item.LastInventory,
                LastManager = item.LastManager,
                Supplier = item.Supplier.Select(x => new SupplierDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }


        // put: {apibaseurl}/api/Item/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateItemById([FromRoute] Guid id, UpdateItemRequestDto request)
        {
            // convert dto to domain model
            var item = new Item
            {
                Id = id,
                Name = request.Name,
                Amount = request.Amount,
                Category = request.Category,
                Note = request.Note,
                LastInventory = request.LastInventory,
                LastManager = request.LastManager,
                Supplier = new List<Supplier>()
            };

            foreach(var supplierGuid in request.Supplier)
            {
                var existingSupplier = await supplierRepository.GetById(supplierGuid);

                if(existingSupplier != null)
                {
                    item.Supplier.Add(existingSupplier);
                }
            }

            var updatedItem = await itemRepository.UpdateAsync(item);

            if(updatedItem == null) 
            {
                return NotFound(); 
            }

            var response = new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Amount = item.Amount,
                Category = item.Category,
                Note = item.Note,
                LastInventory = item.LastInventory,
                LastManager = item.LastManager,
                Supplier = item.Supplier.Select(x => new SupplierDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        // delete: {apibaseurl}/api/Item/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var deletedItem = await itemRepository.DeleteAsync(id);

            if(deletedItem == null)
            {
                return NotFound();
            }

            //convert dom to dto
            var response = new ItemDto
            {
                Id = deletedItem.Id,
                Name = deletedItem.Name,
                Amount = deletedItem.Amount,
                Category = deletedItem.Category,
                Note = deletedItem.Note,
                LastInventory = deletedItem.LastInventory,
                LastManager = deletedItem.LastManager
            };

            return Ok(response);
        }
    }
}
