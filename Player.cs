namespace Monopoly;

public class Player
{
    private readonly string Name;
    private Tile? Tile;
    private int Balance = 0;

    private readonly TheGame TheGame;

    public Player(string Name, Tile? Tile, TheGame TheGame)
    {
        this.Name = Name;
        this.Tile = Tile;
        this.TheGame = TheGame;
    }

    public string GetName()
    {
        return Name;
    }

    public Tile? GetTile()
    {
        return Tile;
    }

    public void SetTile(Tile Tile)
    {
        this.Tile = Tile;
        Console.WriteLine($"\nPlayer#{Name} has landed to {Tile.GetName()}.");
        Tile.OnLand(this);
    }

    public int GetBalance()
    {
        return Balance;
    }

    public void SetBalance(int Balance)
    {
        this.Balance = Balance;
        Console.WriteLine($"Balance of Player#{Name} has changed to ${Balance}.");
        GetOutIfHasNegativeBalance();
    }

    public void IncrementBalance(int Amount)
    {
        Balance += Amount;
        Console.WriteLine($"Balance of Player#{Name} has changed to ${Balance}.");
    }

    public void DecrementBalance(int Amount)
    {
        Balance -= Amount;
        Console.WriteLine($"Balance of Player#{Name} has changed to ${Balance}.");
        GetOutIfHasNegativeBalance();
    }

    private void GetOutIfHasNegativeBalance()
    {
        if (Balance < 0)
            TheGame.OnPlayerLosed(this);
    }
}