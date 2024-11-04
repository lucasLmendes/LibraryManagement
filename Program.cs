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
                        var title = Console.ReadLine();
                        Console.Write("Enter author: ");
                        var author = Console.ReadLine();
                        Console.Write("Enter genre: ");
                        var genre = Console.ReadLine();
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
                        int idNumber;
                        Console.WriteLine("Please insert the ID of the book you want to change or mark as borrowed: ");
                        int bookToUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine("Do you want to:");
                        Console.WriteLine("1. Change this book.");
                        Console.WriteLine("2. Mark this book as borrowed.");
                        ChangeOrBorrowBook(bookToUpdate);
                        break;
                    }
                case "4":
                    {
                        Console.WriteLine("Remove a book from the application.");
                        // logic for book removal from the database
                        break;
                    }
                case "5":
                    {
                        Console.WriteLine("Exiting the application.");
                        //logic for exiting the app (probably change the parameter for while loop)
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Please insert a number from 1 to 5!");
                        break;
                    }
            }
        } while (choiceInput != "5");
    }

    static void MenuShow()
    {
        Console.WriteLine("Welcome to LibCage System!\n");
        Console.WriteLine("Please select an option: ");
        Console.WriteLine("1. Add a book to the library.");
        Console.WriteLine("2. List all books.");
        Console.WriteLine("3. Change information about a book.");
        Console.WriteLine("4. Delete a book from the library.");
        Console.WriteLine("5. Close the application.");
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
                string? changeOrBorrow = Console.ReadLine();
                if (changeOrBorrow == "1")
                {
                    Console.WriteLine("Please, write the new title: ");
                    string? newTitle = Console.ReadLine();
                    Console.WriteLine("Please, write the new author: ");
                    string? newAuthor = Console.ReadLine();
                    Console.WriteLine("Please, write the new genre: ");
                    string? newGenre = Console.ReadLine();
                    ChangeBook(bookId, newTitle, newAuthor, newGenre);
                }
                else
                {
                    Console.WriteLine("Press 1 to borrow the book, or 2 to return it: ");
                    int? borrowOrReturn = int.Parse(Console.ReadLine());
                    if (borrowOrReturn != null)
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
            book.Title = newTitle;
            book.Author = newAuthor;
            book.Genre = newGenre;
            context.SaveChanges();
            Console.WriteLine("Book updtaed successfully!");
            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
            MenuShow();
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
                context.Books.Remove(book);
                context.SaveChanges();
                Console.WriteLine($"Book {book.Title} deleted");
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