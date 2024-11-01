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
                case "1"
                {
                    Console.WriteLine("Adding a new book!");
                    
                }
            }
        }
    }
}