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
        ActionsUtil.PlaceAccordingToOwnedHousesAndHotels(Player, 25, 100);
    }

    public static void TravelToTheNearestUtility(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to land to the nearest utility.");
        ActionsUtil.GoToNearestTileAndCollectIfPassedThroughTheBeginningTile(
            Player, 
            Player.GetTile()!, 
            new string[] { TileNames.TILE_NAME_ELECTRIC_COMPANY, TileNames.TILE_NAME_WATER_WORKS }, 
            200
        );
    }

    public static void AdvanceToTheBeginningTile(Player Player)
    {
        Tile BeginningTile = TileRepository.GetTiles()[0];
        Console.WriteLine($"Player#{Player.GetName()} is to land to the {BeginningTile.GetName()}");
        Player.SetTile(BeginningTile);
    }

    public static void TravelToJail(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to land to the jail tile");
        Player.SetTile(TileRepository.GetTiles()[TileConstants.TILE_POSITION_JAIL]);
    }

    public static void Collect100FromEachPlayer(Player Player)
    {
        foreach (Player _Player in TheGame.GetPlayers())
        {
            if (Player == _Player)
                continue;

            _Player.DecrementBalance(100);
            Player.IncrementBalance(100);
        }
    }
}