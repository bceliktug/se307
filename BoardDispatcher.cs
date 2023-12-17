namespace Monopoly;

public static class BoardDispatcher
{
    private static int Balance = 0;

    public static int GetBalance()
    {
        return Balance;
    }

    public static void SetBalance(int NewBalance)
    {
        Balance = NewBalance;
        Console.WriteLine($"New balance on the board is {Balance}");
    }

    public static void IncrementBalance(int Amount) {
        Balance += Amount;
        Console.WriteLine($"New balance on the board is {Balance}");
    }

    public static void Clear()
    {
        Balance = 0;
        Console.WriteLine("New balance on the board is 0");
    }
}