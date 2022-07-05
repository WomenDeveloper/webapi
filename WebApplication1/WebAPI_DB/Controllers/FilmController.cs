using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_DB.Models;

namespace WebAPI_DB.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly FilmDBContext _context;
        private readonly IJwt _jwt;

        public FilmController(FilmDBContext context, IJwt jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        // GET: api/Film
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filmler>>> GetFilmlers()
        {
          if (_context.Filmlers == null)
          {
              return NotFound();
          }
            return await _context.Filmlers.ToListAsync();
        }

        // GET: api/Film/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filmler>> GetFilmler(int id)
        {
          if (_context.Filmlers == null)
          {
              return NotFound();
          }
            var filmler = await _context.Filmlers.FindAsync(id);

            if (filmler == null)
            {
                return NotFound();
            }

            return filmler;
        }

        // PUT: api/Film/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmler(int id, Filmler filmler)
        {
            if (id != filmler.FilmId)
            {
                return BadRequest();
            }

            _context.Entry(filmler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmlerExists(id))
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

        // POST: api/Film
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filmler>> PostFilmler(Filmler filmler)
        {
          if (_context.Filmlers == null)
          {
              return Problem("Entity set 'FilmDBContext.Filmlers'  is null.");
          }
            _context.Filmlers.Add(filmler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmler", new { id = filmler.FilmId }, filmler);
        }

        // DELETE: api/Film/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilmler(int id)
        {
            if (_context.Filmlers == null)
            {
                return NotFound();
            }
            var filmler = await _context.Filmlers.FindAsync(id);
            if (filmler == null)
            {
                return NotFound();
            }

            _context.Filmlers.Remove(filmler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmlerExists(int id)
        {
            return (_context.Filmlers?.Any(e => e.FilmId == id)).GetValueOrDefault();
        }


        [AllowAnonymous]
        [HttpPost("user")]
        public IActionResult Login(UserVM user) 
        {
           string token = _jwt.KontrolEt(user.Username, user.Password);
           if (token == null) return Unauthorized();
           return Ok(token);
        }



    }
}
