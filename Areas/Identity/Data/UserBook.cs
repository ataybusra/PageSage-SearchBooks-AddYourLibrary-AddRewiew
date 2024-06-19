using System.ComponentModel.DataAnnotations.Schema;

namespace PageSage.Areas.Identity.Data
{
	public class UserBook
	{
		public int UserBookId { get; set; } // Birincil anahtar
		public string? Title { get; set; } // Kitap başlığı
		public string? Authors { get; set; } // Kitap yazarları
		public string? Description { get; set; } // Kitap açıklaması
		public DateTime? BeginDate { get; set; } // Yayınlanma tarihi
		public DateTime? EndDate { get; set; } // Yayıncı
		public int? PageCount { get; set; } // Sayfa sayısı
		public string? Categories { get; set; } // Kategoriler
		public bool HasFinished { get; set; } = false;
        public string? Review { get; set; } // Kategoriler
        public int? Puan { get; set; } // Kategoriler

	}
}
