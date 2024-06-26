using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinhVienApi.Models;

namespace SinhVienApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SinhVienItemsController : ControllerBase
    {
        private readonly SinhVienContext _context;

        public SinhVienItemsController(SinhVienContext context)
        {
            _context = context;
        }

        // GET: api/SinhVienItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SinhVienItem>>> GetSinhVienItems()
        {
            return await _context.SinhVienItems.ToListAsync();
        }

        // GET: api/SinhVienItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SinhVienItem>> GetSinhVienItem(int id)
        {
            var sinhVienItem = await _context.SinhVienItems.FindAsync(id);

            if (sinhVienItem == null)
            {
                return NotFound();
            }

            return sinhVienItem;
        }

        // PUT: api/SinhVienItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSinhVienItem(int id, SinhVienItem sinhVienItem)
        {
            if (id != sinhVienItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(sinhVienItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhVienItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

		// POST: api/SinhVienItems
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<SinhVienItem>> PostSinhVienItem(SinhVienItem sinhVienItem)
		{
			try
			{
				// Ensure ID is not set by client
				sinhVienItem.Id = 0;

				_context.SinhVienItems.Add(sinhVienItem);
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetSinhVienItem), new { id = sinhVienItem.Id }, sinhVienItem);
			}
			catch (Exception ex)
			{
				// Log detailed error information
				Console.Error.WriteLine($"Error in PostSinhVienItem: {ex.Message}");
				if (ex.InnerException != null)
				{
					Console.Error.WriteLine($"Inner Exception: {ex.InnerException.Message}");
				}
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding student");
			}
		}




		// DELETE: api/SinhVienItems/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSinhVienItem(int id)
        {
            var sinhVienItem = await _context.SinhVienItems.FindAsync(id);
            if (sinhVienItem == null)
            {
                return NotFound();
            }

            _context.SinhVienItems.Remove(sinhVienItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SinhVienItemExists(int id)
        {
            return _context.SinhVienItems.Any(e => e.Id == id);
        }
    }
}
