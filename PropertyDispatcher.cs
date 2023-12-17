namespace Monopoly
{
    public static class PropertyDispatcher
    {
        private static readonly Dictionary<int, List<Property>> PropertiesOfTiles = new();
        private static readonly Dictionary<int, List<Property>> PropertiesOfPlayers = new();

        public static Property[] GetPropertiesByTile(int TileHash)
        {
            return PropertiesOfTiles.GetValueOrDefault(TileHash, new List<Property>()).ToArray();
        }

        public static Property[] GetPropertiesByPlayer(int PlayerHash)
        {
            return PropertiesOfPlayers.GetValueOrDefault(PlayerHash, new List<Property>()).ToArray();
        }

        // TODO: test
        public static void OnPlayerLosed(int PlayerHash)
        {
            foreach (var Entry in PropertiesOfTiles)
                if (Entry.Value[0].GetPlayer().GetHashCode() == PlayerHash)
                    PropertiesOfTiles[Entry.Key] = new List<Property>();

            foreach (Property Property in PropertiesOfPlayers[PlayerHash])
                BoardDispatcher.IncrementBalance(Property.GetCost());

            PropertiesOfPlayers.Remove(PlayerHash);
        }
    }
}