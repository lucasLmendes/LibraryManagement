namespace LibraryManagement.Models
{
    public class Book //  so this is why there was the "DbSet<Book> Books", because the class is named Book, even if the table is named Books inside the DB
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public int isBookBorrowed { get; set; }
    }    
}