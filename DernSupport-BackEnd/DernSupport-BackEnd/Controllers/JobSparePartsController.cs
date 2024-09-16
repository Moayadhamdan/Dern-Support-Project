using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DernSupport_BackEnd.Data;
using DernSupport_BackEnd.Models;

namespace DernSupport_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobSparePartsController : ControllerBase
    {
        private readonly DernSupportDbContext _context;

        public JobSparePartsController(DernSupportDbContext context)
        {
            _context = context;
        }

        [HttpGet("allStocks")]
        public async Task<IActionResult> GetAllStocks()
        {
            var spareParts = await _context.JobSparePartDb.ToListAsync();
            return Ok(spareParts);
        }


        [HttpGet("search")]
        public async Task<IActionResult> Search(string name = "", string category = "")
        {
            var spareParts = await _context.JobSparePartDb
                .Where(sp => (string.IsNullOrEmpty(name) || sp.Name.Contains(name)) &&
                             (string.IsNullOrEmpty(category) || sp.Category.Contains(category)))
                .ToListAsync();

            if (spareParts == null || !spareParts.Any())
            {
                return NotFound("No spare parts found.");
            }

            return Ok(spareParts);
        }

        // 2. edit laptops parts by id
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] JobSparePart updatedPart)
        {
            var sparePart = await _context.JobSparePartDb.FindAsync(id);
            if (sparePart == null)
            {
                return NotFound("Spare part not found.");
            }

            // Update part details
            sparePart.Name = updatedPart.Name;
            sparePart.Category = updatedPart.Category;
            sparePart.Description = updatedPart.Description;
            sparePart.QuantityInStock = updatedPart.QuantityInStock;

            await _context.SaveChangesAsync();

            return Ok(sparePart);
        }
    }
}
