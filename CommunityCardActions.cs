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

    public static void PlaceOnTheBoard100(Player Player)
    {
        ActionsUtil.PlaceOnTheBoard(Player, 100);
    }

    public static void PlaceAccordingToOwnedHousesAndHotels(Player Player)
    {

    }

    public static void TravelToTheNearestUtility(Player Player)
    {

    }

    public static void AdvanceToTheBeginningTile(Player Player)
    {

    }

    public static void TravelToJail(Player Player)
    {

    }

    public static void Collect100FromEachPlayer(Player Player)
    {
        
    }
}