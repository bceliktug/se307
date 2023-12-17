namespace Monopoly;

public class Player
{
    private readonly string Name;
    private Tile? Tile;
    private int Balance = 0;

    public Player(string Name, Tile? Tile)
    {
        this.Name = Name;
        this.Tile = Tile;
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
        Console.WriteLine($"Player#{Name} has landed to {Tile.GetName()}");
        Tile.OnLand(this);
    }

    public int GetBalance()
    {
        return Balance;
    }
    
    public void IncreaseBalance(int Amount)
    {
        Balance += Amount;
    }

    public void SetBalance(int Balance)
    {
        this.Balance = Balance;
        Console.WriteLine($"Balance of Player#{Name} has changed to ${Balance}.");
        GetOutIfHasNegativeBalance();
    }

    public void DecrementBalance(int Amount)
    {
        Balance -= Amount;
        // TODO: display information on to console
        GetOutIfHasNegativeBalance();
    }

    private void GetOutIfHasNegativeBalance()
    {
        if (Balance < 0)
            TheGame.GetOut(this);
    }
}