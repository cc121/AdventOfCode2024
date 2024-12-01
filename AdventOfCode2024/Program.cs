using AdventOfCode2024.Utilities;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2 || !args[0].StartsWith("-d"))
        {
            Console.WriteLine("Usage: -d<#> <input string>");
            return;
        }

        if (!int.TryParse(args[0].Substring(2), out int number))
        {
            Console.WriteLine("Invalid number format.");
            return;
        }

        string inputString = args[1];
        Console.WriteLine($"Number: {number}, Input String: {inputString}");

        var result = DaySelector.SelectDay(number).Run(inputString);
        Console.WriteLine($"Result for Day{number}: {result}");

        Console.WriteLine("Type 'x' to exit.");
        while (Console.ReadKey(true).KeyChar != 'x')
        {
            // Wait for the user to type 'x'
        }
    }
}
