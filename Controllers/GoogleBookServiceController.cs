using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PageSage.Areas.Identity.Data;
using System.Net.Http;
using System.Security.Policy;

namespace PageSage.Controllers
{
    public class GoogleBookServiceController : Controller
    {
        private readonly HttpClient _httpClient; // HttpClient nesnesi, dış API ile iletişim kurmak için kullanılır

        public GoogleBookServiceController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> SearchBooks(string query) // Kitap arama
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=${query}"); // Google Books API'ye GET isteği gönderilir
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync(); // Yanıt içeriği okunur.
                var responseObject = JsonConvert.DeserializeObject<GoogleBookResponse>(content) ?? new GoogleBookResponse(); //Http den gelen yanıt C# nesnesine dönüşürülür

                var books = responseObject.Items // API yanıtından alınan kitap öğelerini içeren koleksiyon
                .Where(item => item != null && item.Authors != null) // Null olmayan ve yazarları olan kitapları filtreler.
                .Select(item => new GoogleBookItemDto // Her kitap öğesini GoogleBookItemDto'ya dönüştürülür
                {
                    GoogleBookItemId = item.GoogleBookItemId,
                    Title = item.Title,
                    Authors = item.Authors,
                    PublishedDate = item.PublishedDate,
                    Publisher = item.Publisher,
                    Description = item.Description,
                    PageCount = item.PageCount,
                    Categories = item.Categories,
                    ImageLinks = item.ImageLinks
                })
                .ToList();

                var viewModel = new SearchBooksDto
                {
                    Query = query, // Arama sorgusu
                    Books = books.Select(book => new GoogleBookItemDto // Kitap koleksiyonu oluşturulur.
                    {
                        GoogleBookItemId = book.GoogleBookItemId,
                        Title = book.Title,
                        Authors = book.Authors,
                        PublishedDate = book.PublishedDate,
                        Publisher = book.Publisher,
                        Description = book.Description,
                        PageCount = book.PageCount,
                        Categories = book.Categories,
                        ImageLinks = book.ImageLinks
                    }).ToList()
                };

                return View();
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"API isteği başarısız: {e.Message}");
            }
        }

        public async Task<IActionResult> SearchCategoryBooks(string category)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=%20+subject:{category}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<GoogleBookResponse>(content) ?? new GoogleBookResponse();

                var books = responseObject.Items
                .Where(item => item != null && item.Authors != null)
                .Select(item => new GoogleBookItemDto
                {
                    GoogleBookItemId = item.GoogleBookItemId,
                    Title = item.Title,
                    Authors = item.Authors,
                    PublishedDate = item.PublishedDate,
                    Publisher = item.Publisher,
                    Description = item.Description,
                    PageCount = item.PageCount,
                    Categories = item.Categories,
                    ImageLinks = item.ImageLinks
                })
                .ToList();

                var viewModel = new SearchBooksDto
                {
                    Category = category,
                    Books = books.Select(book => new GoogleBookItemDto
                    {
                        GoogleBookItemId = book.GoogleBookItemId,
                        Title = book.Title,
                        Authors = book.Authors,
                        PublishedDate = book.PublishedDate,
                        Publisher = book.Publisher,
                        Description = book.Description,
                        PageCount = book.PageCount,
                        Categories = book.Categories,
                        ImageLinks = book.ImageLinks
                    }).ToList()
                };
                return View();
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"API isteği başarısız: {e.Message}");
            }
        }
    }
}
