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
        // TODO: display information on to console
    }

    public static void IncrementBalance(int Amount) {
        Balance += Amount;
        // TODO: display information on to console
    }

    public static void Clear()
    {
        Balance = 0;
        // TODO: display information on to console
    }
}