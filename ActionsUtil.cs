namespace Monopoly;

public static class ActionsUtil
{
    public static void PayTax(Player Player, string TileName, int Amount)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into the {TileName}, he placed {Amount}Ꝟ on the board.");
        Player.DecrementBalance(Amount);
        BoardDispatcher.IncrementBalance(Amount);
    }

    public static void OnRealEstateTile(Tile Tile, Player Player, int LandCost, int BuildingHouseCost, int HotelRentCost, int[] RentCosts)
    {
        if (PropertyDispatcher.TileHasProperty(Tile))
        {
            Player Owner = PropertyDispatcher.GetOwnerOfTile(Tile);

            if (Owner == Player)
            {
                int NumberOwnerHasProperties = PropertyDispatcher.GetNumberOfPropertiesOnTile(Tile);

                if (PropertyDispatcher.GetNameOfPropertyAt(Tile, 1) == PropertyNames.PROPERTY_NAME_HOTEL)
                {
                    Console.WriteLine($"Player#{Player.GetName()} has an hotel on the land. There is nothing to do.");

                    return;
                }

                // can upgrade to hotel if the player has enough balance
                if (NumberOwnerHasProperties == TileConstants.MAX_NUMBER_OF_HOUSES_IN_REAL_ESTATE_TILE + 1)
                    if (Player.GetBalance() >= BuildingHouseCost)
                    {
                        Console.WriteLine("Do you want to upgrade to hotel? Enter Y to upgrade.");

                        if (Console.ReadLine() == "Y")
                        {
                            Console.WriteLine($"Player#{Player.GetName()} has upgraded to hotel.");
                            PropertyDispatcher.ClearHousesOnTile(Tile);
                            PropertyDispatcher.AddProperty(Tile, Player, new Property(PropertyNames.PROPERTY_NAME_HOTEL, Player, BuildingHouseCost));
                            Player.DecrementBalance(BuildingHouseCost);
                        }
                        else Console.WriteLine($"Player#{Player.GetName()} has declined to upgrade to hotel.");
                    }
                    else Console.WriteLine(MessageConstants.MESSAGE_HAS_NO_ENOUGH_PRICE_TO_UPGRADE_TO_HOTEL);
                else
                {
                    if (Player.GetBalance() >= BuildingHouseCost)
                    {
                        if (!PropertyDispatcher.PlayerHasAllRealEstateTiles(Tile, Player))
                        {
                            Console.WriteLine("You did not buy all this kind of real estate tiles, so you cannot build any house.");

                            return;
                        }

                        Console.WriteLine("Do you want to build an house or hotel? If you dont want, enter 0.");
                        int MaxNumberOfHousesToBuild = TileConstants.MAX_NUMBER_OF_HOUSES_IN_REAL_ESTATE_TILE - (PropertyDispatcher.GetNumberOfPropertiesOnTile(Tile) - 1);

                        for (int i = 0; i < MaxNumberOfHousesToBuild; i++)
                            Console.WriteLine($"Enter {i + 1} to build {i + 1} houses ({(i + 1) * BuildingHouseCost}Ꝟ)");

                        Console.WriteLine($"Enter {MaxNumberOfHousesToBuild + 1} to build an hotel ({(MaxNumberOfHousesToBuild + 1) * BuildingHouseCost}Ꝟ)");

                        while (true)
                            if (int.TryParse(Console.ReadLine(), out int SelectedNumberOfHouses))
                            {
                                if (SelectedNumberOfHouses == 0)
                                {
                                    Console.WriteLine($"Player#{Player.GetName()} has declined to build houses.");

                                    return;
                                }

                                if (SelectedNumberOfHouses > MaxNumberOfHousesToBuild + 1 || SelectedNumberOfHouses < 0)
                                    Console.WriteLine($"Invalid input. House count must be between 1 - {MaxNumberOfHousesToBuild}");

                                else // valid input
                                {
                                    int TotalCost = SelectedNumberOfHouses * BuildingHouseCost;

                                    if (Player.GetBalance() < TotalCost)
                                    {
                                        Console.WriteLine("You do not have enough price to buy this number of houses");

                                        continue;
                                    }

                                    if (SelectedNumberOfHouses == MaxNumberOfHousesToBuild + 1)
                                    {
                                        PropertyDispatcher.ClearHousesOnTile(Tile);

                                        PropertyDispatcher.AddProperty(
                                            Tile,
                                            Player,
                                            new Property(
                                                PropertyNames.PROPERTY_NAME_HOTEL,
                                                Player,
                                                TotalCost
                                            )
                                        );
                                    }
                                    else
                                        for (int i = 0; i < SelectedNumberOfHouses; i++)
                                            PropertyDispatcher.AddProperty(
                                                Tile,
                                                Player,
                                                new Property(
                                                    PropertyNames.PROPERTY_NAME_HOUSE,
                                                    Player,
                                                    BuildingHouseCost
                                                )
                                            );

                                    break;
                                }
                            }
                            else Console.WriteLine("Invalid input. Please enter a valid integer.");
                    }
                    else Console.WriteLine(MessageConstants.MESSAGE_HAS_NO_ENOUGH_PRICE_TO_BUILD_HOUSE);
                }
            }
            else
            {
                int NumberOwnerHasProperties = PropertyDispatcher.GetNumberOfPropertiesOnTile(Tile);

                // if have 2 properties, he may have 1 house or 1 hotel
                if (NumberOwnerHasProperties == 2 && PropertyDispatcher.GetNameOfPropertyAt(Tile, 1) == PropertyNames.PROPERTY_NAME_HOTEL)
                {
                    Console.WriteLine($"Player#{Player.GetName()} has made payment of {HotelRentCost} to Player#{Owner.GetName()} for rent because the owner has the land and an hotel.");
                    Player.DecrementBalance(HotelRentCost);
                }
                else
                {
                    int PriceForRent = RentCosts[NumberOwnerHasProperties - 1];
                    Console.WriteLine($"Player#{Player.GetName()} has made payment of {PriceForRent} to Player#{Owner.GetName()} for rent because the owner has the land" + (NumberOwnerHasProperties > 1 ? $" and {NumberOwnerHasProperties - 1} houses." : "."));
                    Player.DecrementBalance(PriceForRent);
                    Owner.IncrementBalance(PriceForRent);
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
                    PropertyDispatcher.AddProperty(Tile, Player, new Property(PropertyNames.PROPERTY_NAME_LAND, Player, LandCost));
                    Console.WriteLine($"Player#{Player.GetName()} has bought the land.");

                    Player.DecrementBalance(LandCost);
                }
                else Console.WriteLine($"Player#{Player.GetName()} has declined to buy the land.");
            }
            else Console.WriteLine(MessageConstants.MESSAGE_HAS_NO_ENOUGH_PRICE);
        }
    }
}