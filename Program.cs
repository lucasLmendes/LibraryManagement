using System;
using LibraryManagement.Data;
using LibraryManagement.Models;



public static class Program
{
    public static void Main(string[] args)
    {
        MenuShow();
        MenuSelect();
    }

    public static void MenuSelect()
    {
        string? choiceInput = "";
        do
        {
            choiceInput = Console.ReadLine();
            switch (choiceInput)
            {
                case "1":
                    {
                        Console.WriteLine("Adding a new book!");
                        Console.Write("Enter title: ");
                        var title = Console.ReadLine() ?? "Unknown Title";
                        Console.Write("Enter author: ");
                        var author = Console.ReadLine() ?? "Unknown Author";
                        Console.Write("Enter genre: ");
                        var genre = Console.ReadLine() ?? "Unknown Genre";
                        AddBook(new Book { Title = title, Author = author, Genre = genre, isBookBorrowed = 0 }); // this adds the book, and the book Id is now set in the database as Auto Increment
                        break;
                    }
                case "2":
                    {
                        Console.WriteLine("Listing all books.");
                        ListBooks();

                        break;
                    }
                case "3":
                    {
                        Console.WriteLine("Please, search for the book using the title: ");
                        string? searchTerm = Console.ReadLine();
                        SearchBook(searchTerm);
                        Console.WriteLine("Press ENTER to continue.");
                        Console.ReadLine();
                        break;
                    }
                case "4":
                    {                     
                        Console.WriteLine("Please insert the ID of the book you want to change or mark as borrowed: ");
                        if (int.TryParse(Console.ReadLine(), out int bookToUpdate))  // Check if input is a valid integer
                        {
                            ChangeOrBorrowBook(bookToUpdate);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID entered.");
                        }
                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("Please insert the ID of the book you want to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int bookToDelete))  // Check if input is a valid integer
                        {
                            DeleteBook(bookToDelete);
                        }
                        else
                        {
                            Console.WriteLine("Invalid ID entered.");
                        }
                        break;
                        
                    }
                case "6":
                    {
                        Console.WriteLine("Exiting the application.");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Please insert a number from 1 to 5!");
                        MenuShow();
                        break;
                    }
            }
        } while (choiceInput != "6");
    }

    static void MenuShow()
    {
        Console.WriteLine("Welcome to LibCage System!\n"); // To do: an option to search a book by title and return the Id, for when the library db is filled with hundreds or thousands of books.
        Console.WriteLine("Please select an option: ");
        Console.WriteLine("1. Add a book to the library.");
        Console.WriteLine("2. List all books.");
        Console.WriteLine("3. Search book by title.");
        Console.WriteLine("4. Change information about a book.");
        Console.WriteLine("5. Delete a book from the library.");
        Console.WriteLine("6. Close the application.");
    }

    static void AddBook(Book newBook) // so this takes the parameter "Book" which is automatically an instantiation to the Book model in the "Model" folder
    {
        using (var context = new LibraryContext()) // the "using" statement is something you use when you want this resource to be disposed of when it finishes, meaning it's something that exists only during use
        {
            context.Books.Add(newBook);
            context.SaveChanges();
            Console.WriteLine("Book added successfully!");
        }
        Console.WriteLine("Press ENTER to continue.");
        Console.ReadLine();
        MenuShow();
}

    static void ListBooks()
    {
        using (var context = new LibraryContext())
        {
            var books = context.Books.ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Borrowed? {(book.isBookBorrowed == 1 ? "Yes" : "No")}");
            }
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            MenuShow();
        }
    }

    static void ChangeOrBorrowBook(int bookId)
    {
        using (var context = new LibraryContext())
        {

            var book = context.Books.Find(bookId);
            if (book != null)
            {
                Console.WriteLine("Do you want to:\n1. Change information about the book\n2. Mark the book as borrowed or return it");
                string? changeOrBorrow = Console.ReadLine();
                if (changeOrBorrow == "1")
                {
                    Console.WriteLine("Please, write the new title: ");
                    string? newTitle = Console.ReadLine() ?? "Unknown Title";
                    Console.WriteLine("Please, write the new author: ");
                    string? newAuthor = Console.ReadLine() ?? "Unknown Author";
                    Console.WriteLine("Please, write the new genre: ");
                    string? newGenre = Console.ReadLine() ?? "Unknown Genre";
                    ChangeBook(bookId, newTitle, newAuthor, newGenre);
                }
                else
                {
                    Console.WriteLine("Press 1 to borrow the book, or 2 to return it: ");
                    if (int.TryParse(Console.ReadLine(), out int borrowOrReturn))
                    {
                        if (borrowOrReturn == 1)
                        {
                            BorrowBook(bookId);
                        }
                        else
                        {
                            ReturnBook(bookId);
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            MenuShow();
        }
    }

    static void ChangeBook(int bookId, string newTitle, string newAuthor, string newGenre)
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books.Find(bookId);
            if (book != null)
            {
                book.Title = newTitle;
                book.Author = newAuthor;
                book.Genre = newGenre;
                context.SaveChanges();
                Console.WriteLine("Book updated successfully!");
                Console.WriteLine("Press ENTER to continue.");
                Console.ReadLine();
            }
        }
    
    }

    static void SearchBook(string? partialMatchForSearch)
    {
        using (var context = new LibraryContext())
        {
            var books = context.Books
                                .Where(book => book.Title.Contains(partialMatchForSearch))
                                .ToList();
            if (partialMatchForSearch != null)
            {
                if (books.Any())
                {
                    foreach (var book in books)
                    {
                        Console.WriteLine($"ID = {book.Id}, Title = {book.Title}");
                    }
                }
                else 
                {
                    Console.WriteLine("No books found with that term on the title.");
                }
            }    
        }
    }

    static void BorrowBook(int bookId)
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books.Find(bookId);
            if (book != null)
            {
                book.isBookBorrowed = 1;
                context.SaveChanges();
                Console.WriteLine("Book is now marked as borrowed.");
            }
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            MenuShow();
        }
    }

    static void ReturnBook(int bookId)
    {
        using (var context = new LibraryContext())
        {
            var book = context.Books.Find(bookId);
            if (book != null)
            {
                book.isBookBorrowed = 0;
                context.SaveChanges();
                Console.WriteLine("Book is now marked as not borrowed.");
            }
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            MenuShow();
        }
    }

    static void DeleteBook(int bookId)
    {
        using (var context = new LibraryContext())
        {

            var book = context.Books.Find(bookId);
            if (book != null)
            {
                Console.WriteLine($"Are you sure you want to delete {book.Title}? Enter \"y\" for Yes and \"n\" for No");
                string? confirmDeletion = Console.ReadLine().Trim().ToLower();
                do
                {
                    if (confirmDeletion == "y")
                    {
                        context.Books.Remove(book);
                        context.SaveChanges();
                        Console.WriteLine($"Book {book.Title} deleted");
                    }
                    else if (confirmDeletion == "n")
                    {
                        Console.WriteLine("Cancelling operation.");
                    }
                    else 
                    {
                        Console.WriteLine("Please, enter \"y\" or \"n\". Returning to the main menu.");
                    }
                } while (confirmDeletion == null);   
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            MenuShow();
        }
    }
}