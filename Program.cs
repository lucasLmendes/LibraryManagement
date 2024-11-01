using System;


public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to LibCage System!\n");
        Console.WriteLine("Please select an option: ");
        Console.WriteLine("\n1. Add a book to the library.");
        Console.writeLine("\n2. Find a book.");
        Console.WriteLine("\n3. Change information about a book.");
        Console.WriteLine("\n4. Delete a book from the library.");
        Console.WriteLine("\n5. Close the application.");
    }

    public static void menuSelect()
    {
        string? choiceInput = Console.ReadLine();

        while (choiceInput != null)
        {
            switch (choiceInput.Value)
            {
                case "1":
                {
                    Console.WriteLine("Adding a new book!");
                    // logic for adding the book in the DB
                    break;
                }
                case "2":
                {
                    Console.WriteLine("Please, write the name of the book you want to find: ");
                    // logic for the search function inside the database
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
                }
            }
        }
    }
}