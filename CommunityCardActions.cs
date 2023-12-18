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

    public static void Collect200(Player player)
    {
        player.IncrementBalance(200);
    }

    public static void Collect100(Player player)
    {
        player.IncrementBalance(100);
    }

    public static void PlaceOnTheBoard100(Player player)
    {
        player.DecrementBalance(100);
        BoardDispatcher.IncrementBalance(100);
        Console.WriteLine($"Player#{player.GetName} has placed 100Ꝟ on board");
    }

    public static void PlaceAccordingToOwnedHousesAndHotels(Player player)
    { 
        Property[] properties = PropertyDispatcher.GetPropertiesByPlayer(player.GetHashCode());
        int houseCount = 0;
        int hotelCount = 0;
        int total = 0;
        
        foreach (Property property in properties)
        {
            if (property.GetName() == "House")
            {
                houseCount++;
            }
            else if (property.GetName() == "Hotel")
            {
                hotelCount++;
            }
        }

        total = (40 * houseCount) + (115 * hotelCount);
        
        Console.WriteLine($"Player#{player.GetName} has to place total of {total}Ꝟ for each owned house and hotel");
        
        player.DecrementBalance(total);
        BoardDispatcher.IncrementBalance(total);


    }

    public static void TravelToTheNearestUtility(Player player)
    {

    }

    public static void AdvanceToTheBeginningTile(Player player)
    {
        Console.WriteLine($"Player#{player.GetName} is advancing to the beginning tile");
        player.SetTile(TileRepository.Tiles[0]);
    }

    public static void TravelToJail(Player player)
    {
        Console.WriteLine($"Player#{player.GetName} has to travel to the jail tile");
        player.SetTile(TileRepository.Tiles[10]);

    }

    public static void Collect100FromEachPlayer(Player player)
    {
        Console.WriteLine($"Player#{player.GetName} is collecting 100Ꝟ from every player.");
        List<Player> playerList = TheGame.GetPlayers();
        playerList.Remove(player);

        int total = (playerList.Count * 100);

        foreach (Player otherPlayer in playerList)
        {
            otherPlayer.DecrementBalance(100);
        }
        
        player.IncrementBalance(total);
    }
}