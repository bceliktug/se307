namespace Monopoly;

public class CommunityCardActions
{
    private readonly TheGame TheGame;
    private readonly TileDispatcher TileDispatcher;
    private readonly TileActionsUtil TileActionsUtil;
    public readonly Action<Player>[] Actions;

    public CommunityCardActions(TheGame TheGame, TileDispatcher TileDispatcher, TileActionsUtil TileActionsUtil)
    {
        this.TheGame = TheGame;
        this.TileDispatcher = TileDispatcher;
        this.TileActionsUtil = TileActionsUtil;

        Actions = new Action<Player>[] {
            Collect200,
            Collect100,
            PlaceOnTheBoard100,
            PlaceAccordingToOwnedHousesAndHotels,
            TravelToTheNearestUtility,
            AdvanceToTheBeginningTile,
            TravelToJail,
            Collect100FromEachPlayer
        };
    }

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
        TileActionsUtil.PlaceOnTheBoard(Player, 100);
    }

    public static void PlaceAccordingToOwnedHousesAndHotels(Player Player)
    {
        TileActionsUtil.PlaceAccordingToOwnedHousesAndHotels(Player, 25, 100);
    }

    public void TravelToTheNearestUtility(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to land to the nearest utility.");
        TileActionsUtil.GoToNearestTileAndCollectIfPassedThroughTheBeginningTile(
            Player,
            Player.GetTile()!,
            new string[] { TileNames.TILE_NAME_ELECTRIC_COMPANY, TileNames.TILE_NAME_WATER_WORKS },
            200
        );
    }

    public void AdvanceToTheBeginningTile(Player Player)
    {
        Tile BeginningTile = TileDispatcher.GetTiles()[0];
        Console.WriteLine($"Player#{Player.GetName()} is to land to the {BeginningTile.GetName()}");
        Player.SetTile(BeginningTile);
    }

    public void TravelToJail(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} is to land to the jail tile");
        Player.SetTile(TileDispatcher.GetTiles()[TileConstants.TILE_POSITION_JAIL]);
    }

    public void Collect100FromEachPlayer(Player Player)
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