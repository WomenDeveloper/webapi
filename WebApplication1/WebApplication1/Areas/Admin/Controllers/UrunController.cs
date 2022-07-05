using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UrunController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrunController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Urun
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Urunler.Include(u => u.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Urun/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Admin/Urun/Create
        public IActionResult Create()
        {
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: Admin/Urun/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrunID,UrunAdi,Fiyat,StokAdedi,KategoriID")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriID", urun.KategoriID);
            return View(urun);
        }

        // GET: Admin/Urun/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriID", urun.KategoriID);
            return View(urun);
        }

        // POST: Admin/Urun/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UrunID,UrunAdi,Fiyat,StokAdedi,KategoriID")] Urun urun)
        {
            if (id != urun.UrunID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.UrunID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriID"] = new SelectList(_context.Kategoriler, "KategoriID", "KategoriID", urun.KategoriID);
            return View(urun);
        }

        // GET: Admin/Urun/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Urunler == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.UrunID == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: Admin/Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Urunler == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Urunler'  is null.");
            }
            var urun = await _context.Urunler.FindAsync(id);
            if (urun != null)
            {
                _context.Urunler.Remove(urun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
          return (_context.Urunler?.Any(e => e.UrunID == id)).GetValueOrDefault();
        }
    }
}
