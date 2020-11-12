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
    public class PostsController : ControllerBase
    {
        private readonly AcmeContext _context;

        public PostsController(AcmeContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPosts(SortOrderPostEnum sortOrder, string currentFilter, string searchString, int? pageNumber, int? pageSize)
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

            var posts = from s in _context.Posts
                                           .Include(a => a.Author)
                                           .ThenInclude(r => r.Rates)
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case SortOrderPostEnum.Title_Desc:
                    posts = posts.OrderByDescending(s => s.Title);
                    break;
                case SortOrderPostEnum.Title_Asc:
                    posts = posts.OrderBy(s => s.Title);
                    break;
                case SortOrderPostEnum.PostDate_Asc:
                    posts = posts.OrderBy(s => s.Datepost);
                    break;
               case SortOrderPostEnum.PostDate_Desc:
                    posts = posts.OrderByDescending(s => s.Datepost);
                    break;
                default:
                    posts = posts.OrderBy(s => s.Title);
                    break;
            }


            return await PaginatedList<Posts>.CreateAsync(posts.AsNoTracking(), pageNumber ?? 1, pageSize.Value);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPosts(int id)
        {
            var posts = await _context.Posts.FindAsync(id);

            if (posts == null)
            {
                return NotFound();
            }

            return posts;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutPosts(int id, Posts posts)
        {
            if (id != posts.Id)
            {
                return BadRequest();
            }

            _context.Entry(posts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(id))
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

        // POST: api/Posts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Posts>> PostPosts(Posts posts)
        {
            _context.Posts.Add(posts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostsExists(posts.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPosts", new { id = posts.Id }, posts);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Posts>> DeletePosts(int id)
        {
            var posts = await _context.Posts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(posts);
            await _context.SaveChangesAsync();

            return posts;
        }

        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
