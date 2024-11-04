using Microsoft.EntityFrameworkCore;   //  so, this calls the classes and methods that are native to EF Core so I can use its tools 
using LibraryManagement.Models; // This references the items created in the models, that basically converts tables in the database to C# classes so it can be used in the code

namespace LibraryManagement.Data // Creating a namespace seems to be an important practice in larger codes, in order to keep them organized
{
    public class LibraryContext : DbContext // Here, we create a class that inherits from DbContext class, which is the native EF Core class that manages database connections and queries.
    {
        public DbSet<Book> Books { get; set; }  // this draws from my books table, so I can use the information contained there inside of my code

        protected override void OnConfiguring(DbContextOptionsBuilder options) // TODO: understand what is override and the "=>" In this case, override is changing an existing method inside of the Entity Framework, altering its functionality
            => options.UseSqlite("Data Source=C:/Users/LLM/DevFolder/LibraryManagement/BooksStock.db"); // Here I'm using the real path for the database I had created using SQLite
                                                    
    }
}