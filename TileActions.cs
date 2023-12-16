namespace Monopoly;

public static class TileActions
{
    public static void OnBeginningTile(Player Player)
    {
        Player.SetBalance(200);
    }

    public static void OnIncomeTaxTile(Player Player)
    {
        BoardDispatcher.IncrementBalance(200);
        Console.WriteLine($"Because Player {Player.GetName} has landed into the Income Tax Tile, he placed 200Ꝟ on the board.");
        Player.DecrementBalance(200);
    }

    public static void OnLuxuryTaxTile(Player Player)
    {

    }

    public static void OnChanceTile(Player Player)
    {

    }

    public static void OnCommunityChestTile(Player Player)
    {

    }

    public static void OnFreeParkingTile(Player Player)
    {

    }

    public static void OnTrainStationTile(Player Player)
    {

    }

    public static void OnElectricCompanyTile(Player Player)
    {

    }

    public static void OnWaterWorksTile(Player Player)
    {

    }

    public static void OnBrownRealEstateTile(Player Player)
    {

    }

    public static void OnLigthBlueRealEstateTile(Player Player)
    {

    }

    public static void OnPurpleRealEstateTile(Player Player)
    {

    }

    public static void OnOrangeRealEstateTile(Player Player)
    {

    }

    public static void OnRedRealEstateTile(Player Player)
    {

    }

    public static void OnYellowRealEstateTile(Player Player)
    {

    }

    public static void OnGreenRealEstateTile(Player Player)
    {

    }

    public static void OnBlueRealEstateTile(Player Player)
    {

    }

    public static void OnGoToJailTile(Player Player)
    {

    }

    public static void OnJailTile(Player Player)
    {

    }
}