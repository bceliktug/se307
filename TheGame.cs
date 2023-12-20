namespace Monopoly;

public class TheGame
{
    private List<Player> Players = new();
    private readonly CardDispatcher CardDispatcher = new();
    private readonly TileDispatcher TileDispatcher;
    private readonly ChanceCardActions ChanceCardActions;
    private readonly CommunityCardActions CommunityCardActions;
    private bool GameWon = false;

    public TheGame()
    {
        TileDispatcher = new(new TileActions(CardDispatcher, TileDispatcher));
        TileActionsUtil TileActionsUtil = new(TileDispatcher);
        ChanceCardActions = new ChanceCardActions(this, TileDispatcher, TileActionsUtil);
        CommunityCardActions = new CommunityCardActions(this, TileDispatcher, TileActionsUtil);

        InitializeCards();
    }

    public List<Player> GetPlayers()
    {
        return Players;
    }

    public void StartTheGame()
    {
        Players = TheGameUtil.CreatePlayers(this);

        Console.WriteLine("\nRolling a dice to determine the starting player.");
        Console.WriteLine($"The Starting player is: Player#{Players[Util.RollDie(Players.Count - 1)].GetName()}");

        StartLoop();
    }

    private void InitializeCards()
    {
        CardDispatcher.InitializeChanceCards(ChanceCardActions.Actions);
        CardDispatcher.InitializeCommunityCards(CommunityCardActions.Actions);
    }

    private void StartLoop()
    {
        Console.WriteLine("\nThe game has started.");

        int turn = 0;
        while (true && !GameWon)
        {
            Player Player = Players[turn];

            if (Player.GetTile() == null)
                Player.SetTile(TileDispatcher.GetTiles()[0]);

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

    private void Proceed(Player Player)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n------------ START TURN ------------");
        Console.ResetColor();

        Console.WriteLine($"\nIt is turn of Player#{Player.GetName()}.");

        if (CardDispatcher.HasChanceCard(Player))
        {
            Console.WriteLine($"\nYou have {CardDispatcher.GetChanceCardCountOf(Player)} chance cards. Do you want to use one? Enter Y to use.");
            if (Console.ReadLine() == "Y")
                CardDispatcher.UseChanceOf(Player);
            else
                Console.WriteLine($"Player#{Player.GetName()} declined to use his chance card, proceeding");
        }

        if (GameWon)
            return;

        int result = Util.RollTwoDice();
        Console.WriteLine($"Player#{Player.GetName()} rolled {result}");

        Player.SetTile(TileDispatcher.GetTiles()[(Player.GetTile()!.GetPosition() + result) % TileDispatcher.GetTiles().Count]);
        PrintView();

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n--------- END TURN ---------");
        Console.ResetColor();
    }

    private void PrintView()
    {
        Console.WriteLine("\n--------- START INFORMATION ---------");

        foreach (var Entry in TileDispatcher.GetTiles())
        {
            Tile Tile = Entry.Value;
            Console.Write("\n" + Tile.GetName());

            Property[] Properties = PropertyDispatcher.GetPropertiesByTile(Tile);
            int PropertiesLength = Properties.Length;

            if (PropertiesLength > 0)
                Console.Write(" [");

            for (int i = 0; i < Properties.Length; i++)
            {
                Property Property = Properties[i];
                Console.Write(" " + Property.GetName() + $" owned by Player#{Property.GetPlayer().GetName()}" + (i != PropertiesLength - 1 ? ", " : ""));
            }

            if (PropertiesLength > 0)
                Console.Write(" ]");


            foreach (Player Player in Players)
                if (Player.GetTile() == Tile)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" -> Player#{Player.GetName()}");
                    Console.ResetColor();
                }
        }

        Console.WriteLine("");

        foreach (Player Player in Players)
            Console.Write($"\nBalance of Player#{Player.GetName()} is {Player.GetBalance()}");

        Console.WriteLine($"\n\nThe price on the board: {BoardDispatcher.GetBalance()}");

        Console.WriteLine("\n----- END INFORMATION ------");
    }

    private bool GetTheChoice()
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

    private void PrintProperties()
    {
        Console.WriteLine("");

        foreach (Player Player in Players)
        {
            Console.WriteLine($"Player#{Player.GetName()}:");
            Console.WriteLine($"    Balance: {Player.GetBalance()}");

            Property[] Properties = PropertyDispatcher.GetPropertiesByPlayer(Player);
            int PropertiesLength = Properties.Length;

            if (PropertiesLength > 0)
                Console.Write("    Properties: ");

            for (int i = 0; i < PropertiesLength; i++)
                Console.Write($"{Properties[i].GetName()}" + (i != PropertiesLength - 1 ? ", " : "\n"));
        }
    }

    public void OnPlayerLosed(Player Player)
    {
        Console.WriteLine($"Player#{Player.GetName()} LOSED THE GAME.");
        PropertyDispatcher.OnPlayerLosed(Player);
        Players.Remove(Player);

        if (Players.Count == 1)
        {
            Console.WriteLine($"Player#{Players[0].GetName()} HAS WON THE GAME!");
            GameWon = true;
        }
    }
}