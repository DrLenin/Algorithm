var numbers = Array.Empty<int>();

try
{
    Console.WriteLine("Please input numbers fort sort");
    
    numbers = Console.ReadLine()?.Split(' ').Select(x => Convert.ToInt32(x)).ToArray(); 
}
catch
{
    Console.WriteLine("Error input numbers");
}

Console.WriteLine("Sort by desc");

Array.ForEach(SortDesc((int[])numbers.Clone()), Console.WriteLine);

Console.WriteLine("Sort by asce");

Array.ForEach(Sort((int[])numbers.Clone()), Console.WriteLine);


int[] SortDesc(int[] items) => SortArray(indexOfCheck => items[indexOfCheck] < items[0], ref items);

int[] Sort(int[] items) => SortArray(indexOfCheck => items[indexOfCheck] > items[0], ref items);

int[] SortArray(Predicate<int> predicate, ref int[] items)
{
    var length = numbers.Length;
    var sortArray = new int[items.Length];
    
    for (var indexOfStep = 0; indexOfStep < length; indexOfStep++)
    {
        var indexOfDelete = int.MinValue;
        
        for (var indexOfCheck = 0; indexOfCheck < items.Length; indexOfCheck++)
        {
            if (predicate(indexOfCheck)) continue;
        
            var variable = items[indexOfCheck];
            sortArray[indexOfStep] = variable;

            indexOfDelete = indexOfCheck;
        }
        
        items = items.Where((_ , ind) => ind != indexOfDelete).ToArray();
    }

    return sortArray;
}