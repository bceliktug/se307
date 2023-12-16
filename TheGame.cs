namespace Monopoly;

public static class TheGame
{
    // List'e çevir
    private static Player[] Players = Array.Empty<Player>();

    public static Player[] GetPlayers()
    {
        return Players;
    }

    public static void GetOut(Player Player) {
        Console.WriteLine($"{Player.GetName()} LOSED THE GAME.");
        // PLayers Lİst'inden sil
    }

    public static void StartTheGame()
    {
        Players = CreatePlayers(GetPlayerCount());

        Console.WriteLine("Rolling a dice to determine the starting player.");
        Console.WriteLine($"\nThe Starting player is: {Players[Util.RollDie(Players.Length - 1)].GetName()}\n");

        InitializeCards();

        Console.WriteLine("The game has started.");

        int turn = 0;
        while (true)
        {
            Player Player = Players[turn];
            if (Player.GetTile() == null)
                Player.SetTile(TileRepository.Tiles[0]);
            else if (PunishmentDispatcher.HasPunishment(Player))
            {
                Console.WriteLine($"Becuase Player#{Player.GetName()} has punishment, his turn has passed.\n");

                PunishmentDispatcher.DecrementPunishment(Player);
                turn = (turn + 1) % Players.Length;

                continue;
            }
            else
            {
                PrintView();
                Console.WriteLine($"It is turn of Player#{Player.GetName()}.");

                int result = Util.RollTwoDice();
                Console.WriteLine($"Player#{Player.GetName()} rolled {result}");

                Player.SetTile(TileRepository.Tiles[(Player.GetTile()!.GetPosition() + result) % TileRepository.Tiles.Count]);
            }

            Console.WriteLine("If you want to exit, enter 'EXIT'.");
            Console.WriteLine("If you want to view properties and then continue, enter 'VIEW'.");
            Console.WriteLine("Enter any value to continue.");

            string? Input = Console.ReadLine();
            if (Input == "EXIT")
                return;
            else if (Input == "VIEW")
                PrintProperties();

            turn = (turn + 1) % Players.Length;
        }
    }

    private static Player[] CreatePlayers(int PlayerCount)
    {
        Player[] Players = new Player[PlayerCount];
        for (int i = 0; i < PlayerCount; i++)
            Players[i] = new Player(i.ToString(), null);

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

    private static void PrintView()
    {
        // Before a player rolls the dice, the system must display a view of the board. In this view, we should be able to see each tile(names and housing) and the position of each player.
    }

    private static void PrintProperties()
    {
        // The system must be able to display a view of properties (a complete list containing all lands and housing) and balances of each player. This functionality should be available on demand: Players should be able to use it before rolling the dice
    }
}