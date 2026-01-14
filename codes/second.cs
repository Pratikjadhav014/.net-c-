using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int n = int.Parse(Console.ReadLine());

        if (n % 2 == 0)
            Console.WriteLine("Even number");
        else
            Console.WriteLine("Odd number");

        Console.ReadLine();
    }
}
