using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAcme.Models;
using ApiAcme.Enumerador;

namespace ApiAcme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly AcmeContext _context;

        public RatesController(AcmeContext context)
        {
            _context = context;
        }

        // GET: api/Rates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rates>>> GetRates(SortOrderRateEnum sortOrder, string currentFilter, string searchString, int? pageNumber, int? pageSize)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!pageSize.HasValue)
            {
                pageSize = 5;
            }

            var rates = from s in _context.Rates
                                          .Include(a => a.Author)
                                          .Include(a => a.Post)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                rates = rates.Where(s => s.Author.FullName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case SortOrderRateEnum.FirstNameAuthor_Desc:
                    rates = rates.OrderByDescending(s => s.Author.FirstName);
                    break;
                case SortOrderRateEnum.FirstNameAuthor_Asc:
                    rates = rates.OrderBy(s => s.Author.FirstName);
                    break;
                case SortOrderRateEnum.Ratedate_Asc:
                    rates = rates.OrderBy(s => s.Daterate);
                    break;
                case SortOrderRateEnum.Ratedate_Desc:
                    rates = rates.OrderByDescending(s => s.Daterate);
                    break;
                default:
                    rates = rates.OrderBy(s => s.Author.FirstName);
                    break;
            }

            return await PaginatedList<Rates>.CreateAsync(rates.AsNoTracking(), pageNumber ?? 1, pageSize.Value);

            //  return await _context.Rates.ToListAsync();
        }

        // GET: api/Rates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rates>> GetRates(int id)
        {
            var rates = await _context.Rates.FindAsync(id);

            if (rates == null)
            {
                return NotFound();
            }

            return rates;
        }

        // PUT: api/Rates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutRates(int id, Rates rates)
        {
            if (id != rates.Id)
            {
                return BadRequest();
            }

            _context.Entry(rates).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatesExists(id))
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

        // POST: api/Rates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Rates>> PostRates(Rates rates)
        {
            _context.Rates.Add(rates);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RatesExists(rates.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRates", new { id = rates.Id }, rates);
        }

        // DELETE: api/Rates/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Rates>> DeleteRates(int id)
        {
            var rates = await _context.Rates.FindAsync(id);
            if (rates == null)
            {
                return NotFound();
            }

            _context.Rates.Remove(rates);
            await _context.SaveChangesAsync();

            return rates;
        }

        private bool RatesExists(int id)
        {
            return _context.Rates.Any(e => e.Id == id);
        }
    }
}
