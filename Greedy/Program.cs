double origAmount;

try
{
    Console.WriteLine("Enter the amount you want to change");
    
    origAmount = Convert.ToDouble(Console.ReadLine()); 
}
catch
{
    Console.WriteLine("Error input number");
    return;
}

MakeChange(origAmount);

static void MakeChange(double origAmount)
{
    var denominationsOfBanknotes = new[] { 50, 10, 5, 1 };
    
    Console.WriteLine($"The best way to exchange {origAmount}");

    foreach (var nominal in denominationsOfBanknotes.OrderByDescending(x => x))
    {
        if (!(origAmount % nominal < origAmount)) continue;
        
        Console.WriteLine($"Number of {nominal} ruble banknotes: {origAmount / nominal:0}");
        var remainAmount = origAmount % nominal;
        origAmount = remainAmount;
    }
}