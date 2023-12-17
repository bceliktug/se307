namespace Monopoly;

public static class ActionsUtil {
    public static void PayTax(Player Player, int Amount) {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into the Income Tax Tile, he placed {Amount}Ꝟ on the board.");
        Player.DecrementBalance(Amount);
        BoardDispatcher.IncrementBalance(Amount);
    }
}