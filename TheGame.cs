namespace Monopoly;

public static class TheGame
{
    // List'e çevir
    private static List<Player> Players = new();

    public static List<Player> GetPlayers()
    {
        return Players;
    }

    public static void StartTheGame()
    {
        Players = CreatePlayers(GetPlayerCount());

        Console.WriteLine("\nRolling a dice to determine the starting player.");
        Console.WriteLine($"The Starting player is: Player#{Players[Util.RollDie(Players.Count - 1)].GetName()}");

        InitializeCards();

        StartLoop();
    }

    private static List<Player> CreatePlayers(int PlayerCount)
    {
        List<Player> Players = new(PlayerCount);
        for (int i = 0; i < PlayerCount; i++)
            Players.Add(new Player(i.ToString(), null));

        return Players;
    }

    private static int GetPlayerCount()
    {
        Console.WriteLine("Welcome to the Monopoly Game. Enter the player count.");

        while (true)
            if (int.TryParse(Console.ReadLine(), out int PlayerCount))
                if (PlayerCount < 2 || PlayerCount > 4)
                    Console.WriteLine("Invalid input. Player count must be between 2 - 4.");
                else return PlayerCount;
            else Console.WriteLine("Invalid input. Please enter a valid integer.");
    }

    private static void InitializeCards()
    {
        CardDispatcher.InitializeChanceCards(ChanceCardActions.Actions);
        CardDispatcher.InitializeCommunityCards(CommunityCardActions.Actions);
    }

    private static void StartLoop()
    {
        Console.WriteLine("\nThe game has started.");

        int turn = 0;
        while (true)
        {
            Player Player = Players[turn];

            if (Player.GetTile() == null)
                Player.SetTile(TileRepository.Tiles[0]);

            if (PunishmentDispatcher.HasPunishment(Player))
            {
                OnPunishment(Player);
                turn = (turn + 1) % Players.Count;

                continue;
            }
            else
                Proceed(Player);

            if (!GetTheChoice())
                return;

            turn = (turn + 1) % Players.Count;
        }
    }

    private static void OnPunishment(Player Player)
    {
        Console.WriteLine($"\nBecause Player#{Player.GetName()} has punishment, his turn has passed.");
        PunishmentDispatcher.DecrementPunishment(Player);

    }

    private static void Proceed(Player Player)
    {
        PrintView();
        Console.WriteLine($"\nIt is turn of Player#{Player.GetName()}.");

        int result = Util.RollTwoDice();
        Console.WriteLine($"Player#{Player.GetName()} rolled {result}");

        Player.SetTile(TileRepository.Tiles[(Player.GetTile()!.GetPosition() + result) % TileRepository.Tiles.Count]);
    }

    // TODO: test with properties
    private static void PrintView()
    {
        Console.WriteLine("\n------------ START INFORMATION ------------");

        foreach (var Entry in TileRepository.Tiles)
        {
            Tile Tile = Entry.Value;
            Console.Write("\n" + Tile.GetName());

            Property[] Properties = PropertyDispatcher.GetPropertiesByTile(Tile.GetHashCode());
            int PropertiesLength = Properties.Length;

            if (PropertiesLength > 0)
                Console.Write(" [");

            for (int i = 0; i < Properties.Length; i++)
            {
                Property Property = Properties[i];
                Console.Write(" " + Property.GetName() + $" owned by Player#{Property.GetPlayer().GetName()}");
            }

            if (PropertiesLength > 0)
                Console.Write(" ]");


            foreach (Player Player in Players)
                if (Player.GetTile() == Tile)
                    Console.Write($" -> Player#{Player.GetName()}");

        }

        Console.WriteLine("");

        foreach (Player Player in Players)
            Console.Write($"\nBalance of Player#{Player.GetName()} is {Player.GetBalance()}");

        Console.WriteLine($"\n\nThe price on the board: {BoardDispatcher.GetBalance()}" );

        Console.WriteLine("\n------------ END INFORMATION ------------");
    }

    private static bool GetTheChoice()
    {
        Console.WriteLine("\nIf you want to exit, enter 'EXIT'.");
        Console.WriteLine("If you want to view properties and then continue, enter 'VIEW'.");
        Console.WriteLine("Enter any value to continue.");

        string? Input = Console.ReadLine();
        if (Input == "EXIT")
            return false;
        else if (Input == "VIEW")
            PrintProperties();

        return true;
    }

    // TODO: test with properties
    private static void PrintProperties()
    {
        Console.WriteLine("");

        foreach (Player Player in Players)
        {
            Console.WriteLine($"Player#{Player.GetName()}:");
            Console.WriteLine($"    Balance: {Player.GetBalance()}");

            Property[] Properties = PropertyDispatcher.GetPropertiesByPlayer(Player.GetHashCode());
            int PropertiesLength = Properties.Length;

            if (PropertiesLength > 0)
                Console.Write("    Properties:");

            for (int i = 0; i < PropertiesLength; i++)
                Console.Write($"{Properties[i].GetName()}" + (i != PropertiesLength - 1 ? ", " : "\n"));
        }
    }

    // TODO: test
    public static void OnPlayerLosed(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} LOSED THE GAME.");
        Players.Remove(Player);
        PropertyDispatcher.OnPlayerLosed(Player.GetHashCode());
    }
}