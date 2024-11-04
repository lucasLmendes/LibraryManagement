using System;
using LibraryManagement.Data;
using LibraryManagement.Models;



public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to LibCage System!\n");
        Console.WriteLine("Please select an option: ");
        Console.WriteLine("\n1. Add a book to the library.");
        Console.WriteLine("\n2. List all books.");
        Console.WriteLine("\n3. Change information about a book.");
        Console.WriteLine("\n4. Delete a book from the library.");
        Console.WriteLine("\n5. Close the application.");


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
                    Console.WriteLine("Please insert the name of the book you want to change or mark as borrowed: ");
                    // logic for changing information of a book in the table of books. TODO: add a row in the database to mark if a book is borrowed or not (boolean?)
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

    static void AddBook(Book newBook) // so this takes the parameter "Book" which is automatically an instantiation to the Book model in the "Model" folder
    {
        using (var context = new LibraryContext()) // the "using" statement is something you use when you want this resource to be disposed of when it finishes, meaning it's something that exists only during use
        {
            context.Books.Add(newBook);
            context.SaveChanges();
            Console.WriteLine("Book added successfully!");
        }
    }

    static void ListBooks()
    {
        using (var context = new LibraryContext())
        {
            var books = context.Books.ToList();
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Borrowed? {book.isBookBorrowed}");
            }
        }
    }

    static void ChangeOrBorrowBook()
    {
        Console.WriteLine("Coming soon!");
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
            else {
                Console.WriteLine("Book not found.");
            }
        
        }
    }
}