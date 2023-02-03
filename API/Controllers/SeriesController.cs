using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.EntityFramework;

namespace API.Controllers
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly SeriesDbContext _context;

        public SeriesController(SeriesDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all Series
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">All Series can be fetched</response>
        // GET: api/Series
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Serie>))]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            return await _context.Series.ToListAsync();
        }

        /// <summary>
        /// Get a single Serie with its id
        /// </summary>
        /// <param name="id">The id of the requested Serie</param>
        /// <returns>Http response</returns>
        /// <response code="200">The Serie with this id exists and is returned</response>
        /// <response code="404">The Serie with this id does not exist</response>
        /// GET: api/Series/
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Serie))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Serie>> GetSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);

            if (serie == null)
            {
                return NotFound();
            }

            return serie;
        }

        /// <summary>
        /// Update a Serie
        /// </summary>
        /// <param name="id">The id of the Serie</param>
        /// <param name="serie">The data of the Serie</param>
        /// <returns>Http Response</returns>
        /// <response code="204">The Serie is successfully updated</response>
        /// <response code="400">Mismatch between the given id and the id of the Serie</response>
        /// <response code="404">The serie with the given id does not exist</response>
        // PUT: api/Series/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSerie(int id, Serie serie)
        {
            if (id != serie.Serieid)
            {
                return BadRequest();
            }

            _context.Entry(serie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SerieExists(id))
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

        /// <summary>
        /// Create a new Serie entry
        /// </summary>
        /// <param name="serie">The Serie to be inserted</param>
        /// <returns>Https Response</returns>
        /// <response code="201">The serie is successfully stored</response>
        // POST: api/Series
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Serie))]
        [HttpPost]
        public async Task<ActionResult<Serie>> PostSerie(Serie serie)
        {
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSerie", new { id = serie.Serieid }, serie);
        }

        /// <summary>
        /// Delete a Serie entry
        /// </summary>
        /// <param name="id">The id of the Serie you want to delete</param>
        /// <returns>Http Response</returns>
        /// <response code="404">The Serie with the given id does not exist</response>
        /// <response code="204">The Serie entry is successufully deleted</response>
        // DELETE: api/Series/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSerie(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }

            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SerieExists(int id)
        {
            return _context.Series.Any(e => e.Serieid == id);
        }
    }
}
