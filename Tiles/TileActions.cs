namespace Monopoly;

public class TileActions
{
    private readonly CardDispatcher CardDispatcher;
    private readonly TileDispatcher TileDispatcher;

    public TileActions(CardDispatcher CardDispatcher, TileDispatcher TileDispatcher)
    {
        this.CardDispatcher = CardDispatcher;
        this.TileDispatcher = TileDispatcher;
    }

    public static void OnBeginningTile(Player Player, Tile Tile)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into {Tile.GetName()}, he collected 200Ꝟ.");
        Player.IncrementBalance(200);
    }

    public static void OnIncomeTaxTile(Player Player, Tile Tile)
    {
        TileActionsUtil.PayTax(Player, Tile.GetName(), 200);
    }

    public static void OnLuxuryTaxTile(Player Player, Tile Tile)
    {
        TileActionsUtil.PayTax(Player, Tile.GetName(), 150);
    }

    public void OnChanceTile(Player Player, Tile Tile)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into {Tile.GetName()}, he gained a chance card.");
        CardDispatcher.OnPlayerGetChance(Player, CardType.CHANCE_CARD);

        Console.WriteLine($"Do you want to use your chance card immediately? Enter Y to use.");
        if (Console.ReadLine() == "Y")
            CardDispatcher.UseChance(Player, CardType.CHANCE_CARD);
    }

    public void OnCommunityChestTile(Player Player, Tile Tile)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into {Tile.GetName()}, he gained a community chance card.");
        CardDispatcher.OnPlayerGetChance(Player, CardType.COMMUNITY_CHEST_CARD);

        Console.WriteLine($"Do you want to use your community chance card immediately? Enter Y to use.");
        if (Console.ReadLine() == "Y")
            CardDispatcher.UseChance(Player, CardType.COMMUNITY_CHEST_CARD);
    }

    public static void OnFreeParkingTile(Player Player, Tile Tile)
    {
        int PriceOnTheBoard = BoardDispatcher.GetBalance();
        Console.WriteLine($"Because Player {Player.GetName()} has landed into {Tile.GetName()}, he collected price on the board ({PriceOnTheBoard}Ꝟ).");

        Player.IncrementBalance(PriceOnTheBoard);
        BoardDispatcher.Clear();
    }

    public static void OnTrainStationTile(Player Player, Tile Tile)
    {
        if (PropertyDispatcher.TileHasProperty(Tile))
        {
            Player Owner = PropertyDispatcher.GetOwnerOfTile(Tile);

            if (Owner == Player)
            {
                Console.WriteLine($"Player#{Player.GetName()} has this tile.");

                return;
            }

            int NumberOwnerHasTrainStations = PropertyDispatcher.GetNumberOfPropertyPlayerHas(Owner, PropertyNames.PROPERTY_NAME_TRAIN_STATION),
                PriceForRent = NumberOwnerHasTrainStations * 50;

            Console.WriteLine($"Player#{Player.GetName()} has made payment of {PriceForRent} to Player#{Owner.GetName()} for rent because the owner has {NumberOwnerHasTrainStations} train stations.");

            Player.DecrementBalance(PriceForRent);
            Owner.IncrementBalance(PriceForRent);
        }
        else
        {
            if (Player.GetBalance() >= 100)
            {
                Console.WriteLine("Do you want to buy the train station? Enter Y to buy");

                if (Console.ReadLine() == "Y")
                {
                    Console.WriteLine($"Player#{Player.GetName()} has bought the train station.");
                    PropertyDispatcher.AddProperty(Tile, Player, new Property(PropertyNames.PROPERTY_NAME_TRAIN_STATION, Player, 100));
                    Player.DecrementBalance(100);
                }
                else Console.WriteLine($"Player#{Player.GetName()} has declined to buy the train station");
            }
            else
                Console.WriteLine(MessageConstants.MESSAGE_HAS_NO_ENOUGH_PRICE_TO_BUY_TRAIN_STATION);
        }
    }

    public static void OnElectricCompanyTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnUtilityTile(Player, Tile);
    }

    public static void OnWaterWorksTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnUtilityTile(Player, Tile);
    }

    public static void OnBrownRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 60, 50, 450, new int[] { 4, 20, 60, 180, 320 });
    }

    public static void OnLigthBlueRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 120, 50, 600, new int[] { 8, 40, 100, 300, 450 });
    }

    public static void OnPurpleRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 160, 100, 900, new int[] { 12, 60, 180, 500, 700 });

    }

    public static void OnOrangeRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 200, 100, 1000, new int[] { 16, 80, 220, 600, 800 });

    }

    public static void OnRedRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 240, 150, 1100, new int[] { 20, 100, 300, 750, 925 });

    }

    public static void OnYellowRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 280, 150, 1200, new int[] { 24, 120, 360, 850, 1200 });
    }

    public static void OnGreenRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 320, 200, 1400, new int[] { 28, 150, 450, 1000, 1200 });
    }

    public static void OnBlueRealEstateTile(Player Player, Tile Tile)
    {
        TileActionsUtil.OnRealEstateTile(Tile, Player, 400, 200, 2000, new int[] { 50, 200, 600, 1400, 1700 });
    }

    public void OnGoToJailTile(Player Player, Tile Tile)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into {Tile.GetName()}, he is going to the jail.");
        Player.SetTile(TileDispatcher.GetTiles()[TileConstants.TILE_POSITION_JAIL]);

    }

    public static void OnJailTile(Player Player, Tile Tile)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into {Tile.GetName()}, he got punishment for two turns.");
        PunishmentDispatcher.Punish(Player);
    }
}