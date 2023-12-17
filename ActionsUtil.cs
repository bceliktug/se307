namespace Monopoly;

public static class ActionsUtil {
    public static void PayTax(Player Player, string TileName, int Amount) {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into the {TileName}, he placed {Amount}Ꝟ on the board.");
        Player.DecrementBalance(Amount);
        BoardDispatcher.IncrementBalance(Amount);
    }
}