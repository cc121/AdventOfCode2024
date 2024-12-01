using AdventOfCode2024.Utilities;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3 || !args[0].StartsWith("-d") || !args[1].StartsWith("-p"))
        {
            Console.WriteLine("Usage: -d<#> -p<#> <input file path>");
            return;
        }

        if (!int.TryParse(args[0].Substring(2), out int dayNumber))
        {
            Console.WriteLine("Invalid day number format.");
            return;
        }

        if (!int.TryParse(args[1].Substring(2), out int partNumber) || (partNumber != 1 && partNumber != 2))
        {
            Console.WriteLine("Invalid part number format. It should be either 1 or 2.");
            return;
        }

        string filePath = args[2];
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string inputString = File.ReadAllText(filePath);
        Console.WriteLine($"Day: {dayNumber}, Part: {partNumber}, Input File: {filePath}");

        var result = DaySelector.SelectDay(dayNumber).Run(inputString, partNumber);
        Console.WriteLine($"Result for Day{dayNumber} Part{partNumber}: {result}");

        Console.WriteLine("Type 'x' to exit.");
        while (Console.ReadKey(true).KeyChar != 'x')
        {
            // Wait for the user to type 'x'
        }
    }
}
