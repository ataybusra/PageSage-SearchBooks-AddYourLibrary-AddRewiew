using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PageSage.Areas.Identity.Data;

namespace PageSage.Controllers
{
    public class ReviewController : Controller
    {
        private readonly Context _context;

        public ReviewController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Sadece puan verilmiş olan kitapları listeler
            var books = await _context.UserBooks
                .Where(b => b.Puan != null)
                .ToListAsync();

            return View(books); 
        }
    }
}
