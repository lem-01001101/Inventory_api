using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory1_API.Models.DTO;
using Inventory1_API.Data;
using System.ComponentModel;
using Inventory1_API.Models.Domain;
using Inventory1_API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;

namespace Inventory1_API.Controllers
{
    // 
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository) {
            this.supplierRepository = supplierRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierRequestDto request)
        {
            var supplier = new Supplier
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            await supplierRepository.CreateAsync(supplier);

            var response = new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                UrlHandle = supplier.UrlHandle
            };

            return Ok(response);
        }


        // get: https://localhost:7151/api/Suppliers
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var suppliers = await supplierRepository.GetAllAsync();

            // map domain model to dto
            var response = new List<SupplierDto>();
            foreach (var supplier in suppliers)
            {
                response.Add(new SupplierDto
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    UrlHandle = supplier.UrlHandle
                });

            }

            return Ok(response);
        }


        // get: https://localhost:7151/api/suppliers/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetSupplierById([FromRoute] Guid id)
        {
            var existingSupplier = await supplierRepository.GetById(id);

            if (existingSupplier is null)
            {
                return NotFound();
            }

            var response = new SupplierDto
            {
                Id = existingSupplier.Id,
                Name = existingSupplier.Name,
                UrlHandle = existingSupplier.UrlHandle
            };

            return Ok(response);
        }


        
        // put: https://localhost:7151/api/suppliers/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> EditSupplier([FromRoute] Guid id, UpdateSupplierRequestDto request)
        {
            // convert dto to dom dmodel
            var supplier = new Supplier
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle
            };

            supplier = await supplierRepository.UpdateAsync(supplier);

            if(supplier == null)
            {
                return NotFound();
            }

            //convert dom model to dto
            var response = new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                UrlHandle = supplier.UrlHandle
            };
            
            return Ok(response);
        }


        // delete: https://localhost:7151/api/suppliers/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var supplier = await supplierRepository.DeleteAsync(id);

            if (supplier is null)
            {
                return NotFound();
            };

            // convert domain model to dto
            var response = new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                UrlHandle = supplier.UrlHandle
            };

            return Ok(response);
        }

    }
}
