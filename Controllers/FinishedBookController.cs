using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PageSage.Areas.Identity.Data;

namespace PageSage.Controllers
{
    public class FinishedBookController : Controller
    {
        public readonly Context _context; // Veritabanı bağlamı

        public FinishedBookController(Context context)
        {
            _context = context;
        }

        // Tamamlanmış kitapları listeler
        public async Task<IActionResult> Index()
        {
            var books = await _context.UserBooks.Where(b => b.HasFinished == true).ToListAsync(); 
            return View(books);
        }

        // İnceleme eklemek için form görüntüler
        public async Task<IActionResult> AddReview(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.UserBooks.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // İnceleme ekler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(int id, [Bind("UserBookId, Title, Authors, BeginDate, PageCount, Review, Puan")] UserBook book)
        {
            var books = await _context.UserBooks.FindAsync(id);
            if (books == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    books.Review = book.Review; 
                    books.Puan = book.Puan; 
                    _context.Update(books); 
                    await _context.SaveChangesAsync(); 
                    return RedirectToAction(nameof(Index)); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(book);
        }

        // Kitabın var olup olmadığını kontrol eder
        private bool BookExists(int id)
        {
            return _context.UserBooks.Any(e => e.UserBookId == id);
        }

        // Kitabı tamamlanmamış olarak işaretler
        [HttpPost]
        public async Task<IActionResult> Complete(int id)
        {
            var book = await _context.UserBooks.FindAsync(id);
            if (book != null)
            {
                book.HasFinished = false; 
                await _context.SaveChangesAsync(); 
            }
            return RedirectToAction("Index", "Book");
        }
    }

}

