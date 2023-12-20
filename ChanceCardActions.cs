namespace Monopoly;

public static class ChanceCardActions
{
    public static readonly Action<Player>[] Actions = {
        Collect150,
        Collect50,
        PlaceOnTheBoard150,
        PlaceAccordingToOwnedHousesAndHotels,
        TravelToTheNearestTrainStation,
        GoBackThreeTiles,
        GetOutOfJail,
        PayEachPlayer50
    };

    public static void Collect150(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to collect 150Ꝟ.");
        Player.IncrementBalance(150);
    }

    public static void Collect50(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to collect 50Ꝟ.");
        Player.IncrementBalance(50);
    }

    public static void PlaceOnTheBoard150(Player Player)
    {
        ActionsUtil.PlaceOnTheBoard(Player, 150);
    }

    public static void PlaceAccordingToOwnedHousesAndHotels(Player Player)
    {
        ActionsUtil.PlaceAccordingToOwnedHousesAndHotels(Player, 25, 100);
    }

    // not completed
    public static void TravelToTheNearestTrainStation(Player Player)
    {
        // Console.WriteLine($"Player#{Player.GetName()} is to land to the nearest train station.");

        // Dictionary<int, Tile> Tiles = TileRepository.GetTiles();
        // IEnumerable<Tile> NextTiles = Tiles.Where(Entry => Entry.Key > Player.GetTile()!.GetPosition()).Select(Entry => Entry.Value);
        // foreach (Tile Tile in NextTiles)
        //     if (Tile.GetName() == TileNames.TILE_NAME_TRAIN_STATION)
        //     {
        //         Player.SetTile(Tile);

        //         return;
        //     }

        // IEnumerable<Tile> PreviousTiles = Tiles.Where(Entry => Entry.Key < Player.GetTile()!.GetPosition()).Select(Entry => Entry.Value);
        // foreach (Tile Tile in PreviousTiles)
        //     if (Tile.GetName() == TileNames.TILE_NAME_TRAIN_STATION)
        //     {
        //         Player.SetTile(Tile);

        //         return;
        //     }
    }

    public static void GoBackThreeTiles(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to go back 3 tiles.");
        int TotalTileCount = TileRepository.GetTiles().Count;
        Player.SetTile(TileRepository.GetTiles()[(Player.GetTile()!.GetPosition() - 3 + TotalTileCount) % TotalTileCount]);
    }

    public static void GetOutOfJail(Player Player)
    {
        if (Player.GetTile()!.GetName() == TileNames.TILE_NAME_JAIL)
        {
            Tile BeginningTile = TileRepository.GetTiles()[0];
            Console.WriteLine($"Player#{Player.GetName()} is to get out of the jail. He is to land to the {BeginningTile.GetName()}");
            Player.SetTile(BeginningTile);
        }
    }

    public static void PayEachPlayer50(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to pay 50Ꝟ to each player.");

        List<Player> Players = TheGame.GetPlayers();
        foreach (Player _Player in Players)
        {
            if (Player == _Player)
                continue;

            _Player.IncrementBalance(50);
            Player.DecrementBalance(50);
        }
    }
}