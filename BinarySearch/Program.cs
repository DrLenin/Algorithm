var numbers = Array.Empty<int>();
var numberForSearch = 0;

try
{
    Console.WriteLine("Please input numbers separated by commas");
    
    numbers = Console.ReadLine()?.Split(' ').Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToArray(); 
    
    Console.WriteLine("Please enter a number to search for the index");
    
    numberForSearch = Convert.ToInt32(Console.ReadLine());
}
catch
{
    Console.WriteLine("Error input numbers");
}

var range = new Range(0, numbers!.Length - 1);

while (range.Low <= range.High)
{
    if (numbers[range.Mid] == numberForSearch)
    {
        Console.WriteLine($@"Index = {range.Mid}");
        return;
    }

    if (numbers[range.Mid] < numberForSearch)
    {
        range.Low = range.Mid;
    }
    else if(numbers[range.Mid] > numberForSearch)
    {
        range.High = range.Mid;
    }
}

Console.WriteLine("Index = none");

internal struct Range
{
    public int Low { get; set; }
    
    public int High { get; set; }

    public int Mid => (Low + High) / 2;

    public Range(int low, int high)
    {
        Low = low;
        High = high;
    }
}
