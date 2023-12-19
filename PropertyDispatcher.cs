namespace Monopoly
{
    public static class PropertyDispatcher
    {
        private static readonly Dictionary<Tile, List<Property>> PropertiesOfTiles = new();
        private static readonly Dictionary<Player, List<Property>> PropertiesOfPlayers = new();

        public static Property[] GetPropertiesByTile(Tile Tile)
        {
            return PropertiesOfTiles.GetValueOrDefault(Tile, new List<Property>()).ToArray();
        }

        public static Property[] GetPropertiesByPlayer(Player Player)
        {
            return PropertiesOfPlayers.GetValueOrDefault(Player, new List<Property>()).ToArray();
        }

        public static void AddProperty(Tile Tile, Player Player, Property Property)
        {
            if (PropertiesOfTiles.ContainsKey(Tile))
                PropertiesOfTiles[Tile].Add(Property);
            else
            {
                List<Property> Properties = new()
                {
                    Property
                };

                PropertiesOfTiles.Add(Tile, Properties);
            }

            if (PropertiesOfPlayers.ContainsKey(Player))
                PropertiesOfPlayers[Player].Add(Property);
            else
            {
                List<Property> Properties = new()
                {
                    Property
                };

                PropertiesOfPlayers.Add(Player, Properties);
            }
        }

        // TODO: test
        public static void OnPlayerLosed(Player Player)
        {
            foreach (Property? Property in PropertiesOfPlayers[Player])
                BoardDispatcher.IncrementBalance(Property.GetCost());

            foreach (var Entry in PropertiesOfTiles)
                if (Entry.Value[0].GetPlayer() == Player)
                    PropertiesOfTiles[Entry.Key] = new List<Property>();

            PropertiesOfPlayers.Remove(Player);
        }

        public static bool TileHasProperty(Tile Tile)
        {
            return PropertiesOfTiles.ContainsKey(Tile);
        }

        public static int GetNumberOfPropertyPlayerHas(Player Player, string PropertyName)
        {
            int count = 0;
            foreach (var Property in PropertiesOfPlayers[Player])
                if (Property.GetName() == PropertyName)
                    count++;

            return count;
        }

        public static int GetNumberOfPropertiesOnTile(Tile Tile)
        {
            return PropertiesOfTiles[Tile].Count;
        }

        // do not call this function without checking with TileHasProperty
        public static Player GetOwnerOfTile(Tile Tile)
        {
            return PropertiesOfTiles[Tile][0].GetPlayer();
        }

        public static string? GetNameOfPropertyAt(Tile Tile, int index)
        {
            if (index > PropertiesOfTiles[Tile].Count - 1)
                return null;

            return PropertiesOfTiles[Tile][index].GetName();
        }

        // for real estate tiles, the function should be called when became sure the tile has houses
        public static void ClearHousesOnTile(Tile Tile)
        {
            List<Property> Properties = PropertiesOfTiles[Tile];
            Properties.RemoveRange(1, Properties.Count - 1);
        }

        public static bool PlayerHasAllRealEstateTiles(Tile Tile, Player Player)
        {
            int TotalNumberOfThisKindOfTile = TileRepository.GetOccurenceOfTile(Tile.GetName()),
                Occurence = 0;

            foreach (var Entry in PropertiesOfTiles)
                if (Tile.GetName() == Entry.Key.GetName() && Entry.Value[0].GetPlayer() == Player)
                    Occurence++;

            return Occurence == TotalNumberOfThisKindOfTile;
        }
    }
}