namespace PageSage.Areas.Identity.Data
{
	public class GoogleBookItemDto
	{
		public string GoogleBookItemId { get; set; } // Kitap kimliği
		public string? Title { get; set; } // Kitap başlığı
		public string? Authors { get; set; } // Yazar(lar)
		public string? PublishedDate { get; set; } // Yayınlanma tarihi
		public string? Publisher { get; set; } // Yayıncı
		public string? Description { get; set; } // Açıklama
		public int? PageCount { get; set; } // Sayfa sayısı
		public string? Categories { get; set; } // Kategoriler
		public string? ImageLinks { get; set; } // Resim bağlantıları
		public string? Review { get; set; } // Resim bağlantıları
		public int? Puan { get; set; } // Resim bağlantıları
    }
}
