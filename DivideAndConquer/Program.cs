var numbers = Array.Empty<int>();

try
{
    Console.WriteLine("Please input numbers separated by commas for quick sort and summation of numbers");

    numbers = Console.ReadLine()?.Split(' ').Select(x => Convert.ToInt32(x)).OrderBy(x => x).ToArray();
}
catch
{
    Console.WriteLine("Error input numbers");
}

Console.WriteLine("Quick sort by desc");

Array.ForEach(SortDesc((int[])numbers!.Clone()), Console.WriteLine);

Console.WriteLine("Quick sort by asce");

Array.ForEach(Sort((int[])numbers.Clone()), Console.WriteLine);

Console.WriteLine("Quick sum = " + Sum(numbers!));

#region QuickSum

int Sum(int[] array)
{
    if (array.Length < 1)
        return 0;
    
    return array[0] + Sum(array.Skip(1).ToArray());
}

#endregion

#region QuickSort

int[] SortDesc(int[] items) => QuickSort((c, d) => c > d, (c, d) => c < d,items);

int[] Sort(int[] items) => QuickSort((c, d) => c < d,(c, d) => c > d, items);

int[] QuickSort(Func<int, int, bool> predicateLeft, Func<int, int, bool> predicateRight, int[] array)
{
    if (array.Length < 1)
        return array;
    //use Random for don't take worst case
    //use 0 index for standard variant
    var rnd = new Random();
    
    var mid = array[rnd.Next(0, array.Length)];

    var left  = array.Where(x => predicateLeft(x, mid)).ToArray();
    var right = array.Where(x => predicateRight(x, mid)).ToArray();
    
    return Concat(QuickSort(predicateLeft, predicateRight, left ), new []{mid}, QuickSort(predicateLeft, predicateRight, right));
}

T[] Concat<T>(params T[][] arrays)
{
    var result = new T[arrays.Sum(a => a.Length)];
    
    var offset = 0;
    
    foreach (var array in arrays) {
        array.CopyTo(result, offset);
        offset += array.Length;
    }
    
    return result;
}

#endregion
