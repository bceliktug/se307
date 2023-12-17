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

        public static void AddProperty(int TileHash, int PlayerHash, Property Property)
        {
            if (PropertiesOfTiles.ContainsKey(TileHash))
            {
                PropertiesOfTiles[TileHash].Add(Property);
                PropertiesOfPlayers[PlayerHash].Add(Property);
            }
            else
            {
                List<Property> Properties = new()
                {
                    Property
                };

                PropertiesOfTiles.Add(TileHash, Properties);
                PropertiesOfPlayers.Add(TileHash, Properties);
            }
        }

        // TODO: test
        public static void OnPlayerLosed(int PlayerHash)
        {
            foreach (Property Property in PropertiesOfPlayers[PlayerHash])
                BoardDispatcher.IncrementBalance(Property.GetCost());

            foreach (var Entry in PropertiesOfTiles)
                if (Entry.Value[0].GetPlayer().GetHashCode() == PlayerHash)
                    PropertiesOfTiles[Entry.Key] = new List<Property>();

            PropertiesOfPlayers.Remove(PlayerHash);
        }

        public static bool TileHasProperty(int TileHash)
        {
            return PropertiesOfTiles.ContainsKey(TileHash);
        }

        public static int GetNumberOfPropertyPlayerHas(Player Player, string PropertyName)
        {
            int count = 0;
            foreach (var Property in PropertiesOfPlayers[Player.GetHashCode()])
                if (Property.GetName() == PropertyName)
                    count++;

            return count;
        }

        public static int GetNumberOfPropertiesOnTile(int TileHash)
        {
            return PropertiesOfTiles[TileHash].Count;
        }

        // do not call this function without checking with TileHasProperty
        public static Player GetOwnerOfTile(int TileHash)
        {
            return PropertiesOfTiles[TileHash][0].GetPlayer();
        }

        public static string GetNameOfPropertyAt(int TileHash, int index)
        {
            return PropertiesOfTiles[TileHash][index].GetName();
        }

        // for real estate tiles, the function should be called when became sure the tile has houses
        public static void ClearHousesOnTile(int TileHash)
        {
            List<Property> Properties = PropertiesOfTiles[TileHash];
            Properties.RemoveRange(1, Properties.Count - 1);
        }
    }
}