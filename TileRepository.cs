namespace Monopoly;

public static class TileRepository
{
    public static readonly Dictionary<int, Tile> Tiles = new() {
        { 0, new Tile("Beginning Tile", TileActions.OnBeginningTile, 0) },
        { 1, new Tile("Income Tax Tile", TileActions.OnIncomeTaxTile, 1) },
        { 2, new Tile("Luxury Tax Tile", TileActions.OnLuxuryTaxTile, 2) },
        { 3, new Tile("Chance Tile", TileActions.OnChanceTile, 3) },
        { 4, new Tile("Community Chest Tile", TileActions.OnCommunityChestTile, 4) },
        { 5, new Tile("Free Parking Tile", TileActions.OnFreeParkingTile, 5) },
        { 6, new Tile(TileNames.TILE_NAME_TRAIN_STATION, TileActions.OnTrainStationTile, 6) },
        { 7, new Tile("Electric Company Tile", TileActions.OnElectricCompanyTile, 7) },
        { 8, new Tile("Water Works Tile", TileActions.OnWaterWorksTile, 8) },
        { 9, new Tile("Brown Real Estate Tile", TileActions.OnBrownRealEstateTile, 9) },
        { 10, new Tile("Light Blue Real Estate Tile", TileActions.OnLigthBlueRealEstateTile, 10) },
        { 11, new Tile("Purple Real Estate Tile", TileActions.OnPurpleRealEstateTile,11) },
        { 12, new Tile("Orange Real Estate Tile", TileActions.OnOrangeRealEstateTile, 12) },
        { 13, new Tile("Red Real Estate Tile", TileActions.OnRedRealEstateTile, 13) },
        { 14, new Tile("Yellow Real Estate Tile", TileActions.OnYellowRealEstateTile, 14) },
        { 15, new Tile("Green Real Estate Tile", TileActions.OnGreenRealEstateTile, 15) },
        { 16, new Tile("Blue Real Estate Tile", TileActions.OnBlueRealEstateTile, 16) },
        { 17, new Tile("Go To Jail Tile", TileActions.OnGoToJailTile, 17) },
        { 18, new Tile("Jail Tile", TileActions.OnJailTile, 18) },
    };
}