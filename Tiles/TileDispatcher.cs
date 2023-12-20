namespace Monopoly;

public class TileDispatcher
{
    private readonly Dictionary<int, Tile> Tiles;
    private static readonly Dictionary<string, int> PropertyCountDictionary = new();

    public TileDispatcher(TileActions TileActions)
    {
        Tiles = new() {
            { 0, new Tile(TileNames.TILE_NAME_BEGINNING_TILE, TileActions.OnBeginningTile, 0) },
            { 1, new Tile(TileNames.TILE_NAME_BROWN_REAL_ESTATE, TileActions.OnBrownRealEstateTile, 1) },
            { 2, new Tile(TileNames.TILE_NAME_COMMUNITY_CHEST, TileActions.OnCommunityChestTile, 2) },
            { 3, new Tile(TileNames.TILE_NAME_BROWN_REAL_ESTATE, TileActions.OnBrownRealEstateTile, 3) },
            { 4, new Tile(TileNames.TILE_NAME_INCOME_TAX, TileActions.OnIncomeTaxTile, 4) },
            { 5, new Tile(TileNames.TILE_NAME_TRAIN_STATION, TileActions.OnTrainStationTile, 5) },
            { 6, new Tile(TileNames.TILE_NAME_LIGHT_BLUE_REAL_ESTATE, TileActions.OnLigthBlueRealEstateTile, 6) },
            { 7, new Tile(TileNames.TILE_NAME_CHANCE, TileActions.OnChanceTile, 7) },
            { 8, new Tile(TileNames.TILE_NAME_LIGHT_BLUE_REAL_ESTATE, TileActions.OnLigthBlueRealEstateTile, 8) },
            { 9, new Tile(TileNames.TILE_NAME_LIGHT_BLUE_REAL_ESTATE, TileActions.OnLigthBlueRealEstateTile, 9) },
            { 10, new Tile(TileNames.TILE_NAME_JAIL, TileActions.OnJailTile, TileConstants.TILE_POSITION_JAIL) },
            { 11, new Tile(TileNames.TILE_NAME_PURPLE_REAL_ESTATE, TileActions.OnPurpleRealEstateTile, 11) },
            { 12, new Tile(TileNames.TILE_NAME_ELECTRIC_COMPANY, TileActions.OnElectricCompanyTile, 12) },
            { 13, new Tile(TileNames.TILE_NAME_PURPLE_REAL_ESTATE, TileActions.OnPurpleRealEstateTile, 13) },
            { 14, new Tile(TileNames.TILE_NAME_PURPLE_REAL_ESTATE, TileActions.OnPurpleRealEstateTile, 14) },
            { 15, new Tile(TileNames.TILE_NAME_TRAIN_STATION, TileActions.OnTrainStationTile, 15) },
            { 16, new Tile(TileNames.TILE_NAME_ORANGE_REAL_ESTATE, TileActions.OnOrangeRealEstateTile, 16) },
            { 17, new Tile(TileNames.TILE_NAME_COMMUNITY_CHEST, TileActions.OnCommunityChestTile, 17) },
            { 18, new Tile(TileNames.TILE_NAME_ORANGE_REAL_ESTATE, TileActions.OnOrangeRealEstateTile, 18) },
            { 19, new Tile(TileNames.TILE_NAME_ORANGE_REAL_ESTATE, TileActions.OnOrangeRealEstateTile, 19) },
            { 20, new Tile(TileNames.TILE_NAME_FREE_PARKING, TileActions.OnFreeParkingTile, 20) },
            { 21, new Tile(TileNames.TILE_NAME_RED_REAL_ESTATE, TileActions.OnRedRealEstateTile, 21) },
            { 22, new Tile(TileNames.TILE_NAME_CHANCE, TileActions.OnChanceTile, 22) },
            { 23, new Tile(TileNames.TILE_NAME_RED_REAL_ESTATE, TileActions.OnRedRealEstateTile, 23) },
            { 24, new Tile(TileNames.TILE_NAME_RED_REAL_ESTATE, TileActions.OnRedRealEstateTile, 24) },
            { 25, new Tile(TileNames.TILE_NAME_TRAIN_STATION, TileActions.OnTrainStationTile, 25) },
            { 26, new Tile(TileNames.TILE_NAME_YELLOW_REAL_ESTATE, TileActions.OnYellowRealEstateTile, 26) },
            { 27, new Tile(TileNames.TILE_NAME_YELLOW_REAL_ESTATE, TileActions.OnYellowRealEstateTile, 27) },
            { 28, new Tile(TileNames.TILE_NAME_WATER_WORKS, TileActions.OnWaterWorksTile, 28) },
            { 29, new Tile(TileNames.TILE_NAME_YELLOW_REAL_ESTATE, TileActions.OnYellowRealEstateTile, 29) },
            { 30, new Tile(TileNames.TILE_NAME_GO_TO_JAIL, TileActions.OnGoToJailTile, 30) },
            { 31, new Tile(TileNames.TILE_NAME_GREEN_REAL_ESTATE, TileActions.OnGreenRealEstateTile, 31) },
            { 32, new Tile(TileNames.TILE_NAME_GREEN_REAL_ESTATE, TileActions.OnGreenRealEstateTile, 32) },
            { 33, new Tile(TileNames.TILE_NAME_COMMUNITY_CHEST, TileActions.OnCommunityChestTile, 33) },
            { 34, new Tile(TileNames.TILE_NAME_GREEN_REAL_ESTATE, TileActions.OnGreenRealEstateTile, 34) },
            { 35, new Tile(TileNames.TILE_NAME_TRAIN_STATION, TileActions.OnTrainStationTile, 35) },
            { 36, new Tile(TileNames.TILE_NAME_CHANCE, TileActions.OnChanceTile, 36) },
            { 37, new Tile(TileNames.TILE_NAME_BLUE_REAL_ESTATE, TileActions.OnBlueRealEstateTile, 37) },
            { 38, new Tile(TileNames.TILE_NAME_LUXURY_TAX, TileActions.OnLuxuryTaxTile, 38) },
            { 39, new Tile(TileNames.TILE_NAME_BLUE_REAL_ESTATE, TileActions.OnBlueRealEstateTile, 39) },
        };

        InitiliazePropertyCountDictionary();
    }
    
    public Dictionary<int, Tile> GetTiles()
    {
        return Tiles;
    }

    private void InitiliazePropertyCountDictionary()
    {
        if(PropertyCountDictionary.Count > 0)
            return;

        foreach (var Tile in Tiles.Values)
        {
            string PropertyName = Tile.GetName();

            if (PropertyCountDictionary.ContainsKey(PropertyName))
                PropertyCountDictionary[PropertyName]++;
            else
                PropertyCountDictionary[PropertyName] = 1;
        }
    }

    public static int GetOccurenceOfTile(string TileName)
    {
        return PropertyCountDictionary[TileName];
    }
}