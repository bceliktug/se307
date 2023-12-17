namespace Monopoly;

public static class TileActions
{
    public static void OnBeginningTile(Player Player)
    {
        Console.WriteLine($"Because Player {Player.GetName()} has landed into Beginning Tile, he collected 200Ꝟ.");
        Player.IncrementBalance(200);
    }

    public static void OnIncomeTaxTile(Player Player)
    {
        ActionsUtil.PayTax(Player, 200);
    }

    public static void OnLuxuryTaxTile(Player Player)
    {
        ActionsUtil.PayTax(Player, 150);
    }

    //
    public static void OnChanceTile(Player Player)
    {

    }

    //
    public static void OnCommunityChestTile(Player Player)
    {
        
    }

    public static void OnFreeParkingTile(Player Player)
    {
        int PriceOnTheBoard = BoardDispatcher.GetBalance();
        Console.WriteLine($"Because Player {Player.GetName()} has landed into Free Parking Tile, he collected price on the board ({PriceOnTheBoard}Ꝟ).");
        
        Player.IncrementBalance(PriceOnTheBoard);
        BoardDispatcher.Clear();
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