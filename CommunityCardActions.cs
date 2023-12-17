namespace Monopoly;

public static class CommunityCardActions
{
    public static readonly Action<Player>[] Actions = { 
        Collect200, 
        Collect100,
        PlaceOnTheBoard100,
        PlaceAccordingToOwnedHousesAndHotels,
        TravelToTheNearestUtility,
        AdvanceToTheBeginningTile,
        TravelToJail,
        Collect100FromEachPlayer
    };

    public static void Collect200(Player Player)
    {
        Player.IncrementBalance(200);
    }

    public static void Collect100(Player Player)
    {
        Player.IncrementBalance(100);
    }

    public static void PlaceOnTheBoard100(Player player)
    {
        player.DecrementBalance(100);
        BoardDispatcher.IncrementBalance(100);
        Console.WriteLine($"Player#{player.GetName} has placed 100Ꝟ on board");
    }

    public static void PlaceAccordingToOwnedHousesAndHotels(Player player)
    {

    }

    public static void TravelToTheNearestUtility(Player player)
    {

    }

    public static void AdvanceToTheBeginningTile(Player player)
    {

    }

    public static void TravelToJail(Player player)
    {

    }

    public static void Collect100FromEachPlayer(Player player)
    {
        
    }
}