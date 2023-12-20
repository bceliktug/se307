namespace Monopoly;

public class ChanceCardActions
{
    private readonly TheGame TheGame;
    private readonly TileDispatcher TileDispatcher;
    private readonly TileActionsUtil TileActionsUtil;
    public readonly Action<Player>[] Actions;

    public ChanceCardActions(TheGame TheGame, TileDispatcher TileDispatcher, TileActionsUtil TileActionsUtil)
    {
        this.TheGame = TheGame;
        this.TileDispatcher = TileDispatcher;
        this.TileActionsUtil = TileActionsUtil;

        Actions = new Action<Player>[] {
            Collect150,
            Collect50,
            PlaceOnTheBoard150,
            PlaceAccordingToOwnedHousesAndHotels,
            TravelToTheNearestTrainStation,
            GoBackThreeTiles,
            GetOutOfJail,
            PayEachPlayer50
        };
    }


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
        TileActionsUtil.PlaceOnTheBoard(Player, 150);
    }

    public static void PlaceAccordingToOwnedHousesAndHotels(Player Player)
    {
        TileActionsUtil.PlaceAccordingToOwnedHousesAndHotels(Player, 25, 100);
    }

    public void TravelToTheNearestTrainStation(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to land to the nearest train station.");
        TileActionsUtil.GoToNearestTileAndCollectIfPassedThroughTheBeginningTile(
            Player,
            Player.GetTile()!,
            new string[] { TileNames.TILE_NAME_TRAIN_STATION },
            200
        );
    }

    public void GoBackThreeTiles(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to go back 3 tiles.");
        int TotalTileCount = TileDispatcher.GetTiles().Count;
        Player.SetTile(TileDispatcher.GetTiles()[(Player.GetTile()!.GetPosition() - 3 + TotalTileCount) % TotalTileCount]);
    }

    public void GetOutOfJail(Player Player)
    {
        if (Player.GetTile()!.GetName() == TileNames.TILE_NAME_JAIL)
        {
            Tile BeginningTile = TileDispatcher.GetTiles()[0];
            Console.WriteLine($"Player#{Player.GetName()} is to get out of the jail. He is to land to the {BeginningTile.GetName()}");
            Player.SetTile(BeginningTile);
        }
    }

    public void PayEachPlayer50(Player Player)
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