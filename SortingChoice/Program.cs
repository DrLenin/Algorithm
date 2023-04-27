var numbers = Array.Empty<int>();

try
{
    Console.WriteLine("Please input numbers fort sort");
    
    numbers = Console.ReadLine()?.Split(' ').Select(x => Convert.ToInt32(x)).ToArray(); 
}
catch
{
    Console.WriteLine("Error input numbers");
    return;
}

Console.WriteLine("Sort by desc");

Array.ForEach(SortDesc((int[])numbers.Clone()), Console.WriteLine);

Console.WriteLine("Sort by asce");

Array.ForEach(Sort((int[])numbers.Clone()), Console.WriteLine);


int[] SortDesc(int[] items) => SortArray((c, d) => c < d, items);

int[] Sort(int[] items) => SortArray((c, d) => c > d, items);

int[] SortArray(Func<int, int, bool> predicate, int[] items)
{
    var length = numbers.Length;
    var sortArray = new int[items.Length];
    
    for (var indexOfStep = 0; indexOfStep < length; indexOfStep++)
    {
        var indexOfDelete = int.MinValue;

        var variable = items[0];
        
        for (var indexOfCheck = 0; indexOfCheck < items.Length; indexOfCheck++)
        {
            if (predicate(items[indexOfCheck], variable)) continue;
        
            variable = items[indexOfCheck];
            sortArray[indexOfStep] = variable;

            indexOfDelete = indexOfCheck;
        }
        
        items = items.Where((_ , ind) => ind != indexOfDelete).ToArray();
    }

    return sortArray;
}