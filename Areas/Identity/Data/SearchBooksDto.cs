namespace PageSage.Areas.Identity.Data
{
	public class SearchBooksDto
	{
		public string Query { get; set; }
		public string Category { get; set; }
        public List<GoogleBookItemDto> Books { get; set; }

	}
}
