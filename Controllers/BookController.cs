using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PageSage.Areas.Identity.Data;

namespace PageSage.Controllers
{
    public class BookController : Controller
    {
        private readonly Context _context; // Veritabanı bağlamı

        public BookController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
        }

        // Tüm bitirilmemiş kitapları listeler
        public async Task<IActionResult> Index()
        {
            var books = await _context.UserBooks
                .Where(b => b.HasFinished == false)
                .ToListAsync();

            return View(books);
        }

        // Yeni bir kitap eklemek için form görüntüler
        public IActionResult Add()
        {
            return View();
        }

        // Yeni bir kitap ekler
        [HttpPost]
        public async Task<IActionResult> Add(GoogleBookItemDto googleBookItemDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return View(googleBookItemDto);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while adding the book: {ex.Message}");
                }
            }
            // Yeni bir kitap oluşturur
            UserBook book = new UserBook()
            {
                Title = googleBookItemDto.Title,
                Authors = googleBookItemDto.Authors,
                BeginDate = DateTime.Now
            };
            await _context.AddBookAsync(book);
            _context.SaveChanges();
            return RedirectToAction("Index", "Book");
        }

        // Kitabı düzenlemek için form görüntüler
        public async Task<IActionResult> Edit(int? id)
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

        // Kitabı düzenler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserBookId, Title, Authors,BeginDate,PageCount")] UserBook book)
        {
            if (id != book.UserBookId)
            {
                return NotFound();  //Kitap bulunamadıysa notfound dondürür
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book); 
                    await _context.SaveChangesAsync(); 
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.UserBookId))
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

        // Kitabı silmek için onaylama sayfasını görüntüler
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await _context.UserBooks.FirstOrDefaultAsync(m => m.UserBookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // Kitabı siler
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.UserBooks.FindAsync(id); //Idsine gore kitabi bulur
            _context.UserBooks.Remove(book); 
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }

        // Kitabın tamamlanmış olduğunu işaretler
        [HttpPost]
        public async Task<IActionResult> Incomplete(int id)
        {
            var book = await _context.UserBooks.FindAsync(id);
            if (book != null)
            {
                book.HasFinished = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "FinishedBook");
        }

        // Kitabın var olup olmadığını kontrol eder
        private bool BookExists(int id)
        {
            return _context.UserBooks.Any(e => e.UserBookId == id);
        }
    }

}
