namespace Monopoly
{
    public static class PropertyDispatcher
    {
        public static Dictionary<string, List<Property>> PropertiesOfTiles = new Dictionary<string, List<Property>>();
        public static Dictionary<string, List<Property>> PropertiesOfPlayers = new Dictionary<string, List<Property>>();

        // functions:
        // tileHas
        // playerHas
        // getPropertyByTile
        // getPropertyByPlayer
        // playerHasBoth
    }
}