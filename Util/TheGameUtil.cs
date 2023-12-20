namespace Monopoly;

public static class TheGameUtil
{
    public static List<Player> CreatePlayers(TheGame TheGame)
    {
        int PlayerCount = GetPlayerCount();
        
        List<Player> Players = new(PlayerCount);
        for (int i = 0; i < PlayerCount; i++)
            Players.Add(new Player(i.ToString(), null, TheGame));

        return Players;
    }

    public static int GetPlayerCount()
    {
        Console.WriteLine("Welcome to the Monopoly Game. Enter the player count.");

        while (true)
            if (int.TryParse(Console.ReadLine(), out int PlayerCount))
                if (PlayerCount < 2 || PlayerCount > 4)
                    Console.WriteLine("Invalid input. Player count must be between 2 - 4.");
                else return PlayerCount;
            else Console.WriteLine("Invalid input. Please enter a valid integer.");
    }
}