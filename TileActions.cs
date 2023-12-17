namespace Monopoly;

public static class TileActions
{
    public static void OnBeginningTile(Player Player, Tile Tile)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into {Tile.GetName()}, he collected 200Ꝟ.");
        Player.IncrementBalance(200);
    }

    public static void OnIncomeTaxTile(Player Player, Tile Tile)
    {
        ActionsUtil.PayTax(Player, Tile.GetName(), 200);
    }

    public static void OnLuxuryTaxTile(Player Player, Tile Tile)
    {
        ActionsUtil.PayTax(Player, Tile.GetName(), 150);
    }

    //
    public static void OnChanceTile(Player Player, Tile Tile)
    {

    }

    //
    public static void OnCommunityChestTile(Player Player, Tile Tile)
    {

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
        int TileHash = Tile.GetHashCode();

        if (PropertyDispatcher.TileHasProperty(TileHash))
        {
            Player Owner = PropertyDispatcher.GetOwnerOfTile(TileHash);

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
                    PropertyDispatcher.AddProperty(TileHash, Player.GetHashCode(), new Property(PropertyNames.PROPERTY_NAME_TRAIN_STATION, Player, 100));
                    Player.DecrementBalance(100);
                }
                else Console.WriteLine($"Player#{Player.GetName()} declined to buy the train station");
            }
            else
                Console.WriteLine(MessageConstants.MESSAGE_HAS_NO_ENOUGH_PRICE_TO_BUY_TRAIN_STATION);
        }
    }

    public static void OnElectricCompanyTile(Player Player, Tile Tile)
    {

    }

    public static void OnWaterWorksTile(Player Player, Tile Tile)
    {

    }

    public static void OnBrownRealEstateTile(Player Player, Tile Tile)
    {
        int LandCost = 60,
            TileHash = Tile.GetHashCode(),
            HotelCost = 450;

        int[] RentCosts = { 4, 20, 60, 180, 320 };

        if (PropertyDispatcher.TileHasProperty(TileHash))
        {
            Player Owner = PropertyDispatcher.GetOwnerOfTile(Tile.GetHashCode());

            if (Owner == Player)
            {
                // ask to build house(how many (has -4), or hotel), if has enough price
                int NumberOwnerHasProperties = PropertyDispatcher.GetNumberOfPropertiesOnTile(TileHash);

                // can upgrade to hotel if have enough balance
                if (NumberOwnerHasProperties == 5)
                    if (Player.GetBalance() >= HotelCost)
                    {
                        Console.WriteLine("Do you want to upgrade to hotel? Enter Y to upgrade.");

                        if (Console.ReadLine() == "Y")
                        {
                            Console.WriteLine($"Player#{Player.GetName()} has upgraded to hotel.");
                            PropertyDispatcher.ClearHousesOnTile(TileHash);
                            PropertyDispatcher.AddProperty(TileHash, Player.GetHashCode(), new Property(PropertyNames.PROPERTY_NAME_HOTEL, Player, HotelCost));
                            Player.DecrementBalance(HotelCost);
                        }
                        else Console.WriteLine($"Player#{Player.GetName()} declined to upgrade to hotel.");
                    }
                    else Console.WriteLine(MessageConstants.MESSAGE_HAS_NO_ENOUGH_PRICE_TO_UPGRADE_TO_HOTEL);
                else
                {

                }
            }
            else
            {
                int NumberOwnerHasProperties = PropertyDispatcher.GetNumberOfPropertiesOnTile(TileHash);

                // if have 2 properties, he may have 1 house or 1 hotel
                if (NumberOwnerHasProperties == 2)
                {
                    if (PropertyDispatcher.GetNameOfPropertyAt(TileHash, 1) == PropertyNames.PROPERTY_NAME_HOTEL)
                    {
                        Console.WriteLine($"Player#{Player.GetName()} has made payment of {HotelCost} to Player#{Owner.GetName()} for rent because the owner has the land and an hotel.");
                        Player.DecrementBalance(HotelCost);
                    }
                    else
                    {
                        int PriceForRent = RentCosts[NumberOwnerHasProperties - 1];
                        Console.WriteLine($"Player#{Player.GetName()} has made payment of {PriceForRent} to Player#{Owner.GetName()} for rent because the owner has the land" + (NumberOwnerHasProperties > 1 ? $" and {NumberOwnerHasProperties - 1} houses." : "."));
                        Player.DecrementBalance(PriceForRent);
                    }
                }
            }
        }
        else
        {
            if (Player.GetBalance() >= LandCost)
            {
                Console.WriteLine("Do you want to buy the land? Enter Y to buy.");
                if (Console.ReadLine() == "Y")
                {
                    PropertyDispatcher.AddProperty(TileHash, Player.GetHashCode(), new Property(PropertyNames.PROPERTY_NAME_LAND, Player, LandCost));
                    Player.DecrementBalance(LandCost);
                }
            }
            else Console.WriteLine(MessageConstants.MESSAGE_HAS_NO_ENOUGH_PRICE);

        }
    }

    public static void OnLigthBlueRealEstateTile(Player Player, Tile Tile)
    {

    }

    public static void OnPurpleRealEstateTile(Player Player, Tile Tile)
    {

    }

    public static void OnOrangeRealEstateTile(Player Player, Tile Tile)
    {

    }

    public static void OnRedRealEstateTile(Player Player, Tile Tile)
    {

    }

    public static void OnYellowRealEstateTile(Player Player, Tile Tile)
    {

    }

    public static void OnGreenRealEstateTile(Player Player, Tile Tile)
    {

    }

    public static void OnBlueRealEstateTile(Player Player, Tile Tile)
    {

    }

    public static void OnGoToJailTile(Player Player, Tile Tile)
    {

    }

    public static void OnJailTile(Player Player, Tile Tile)
    {

    }
}