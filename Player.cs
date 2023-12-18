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
        if (this.Tile != null && Tile.GetPosition() != 0)
        {
            if (this.Tile.GetPosition() > Tile.GetPosition())
            {
                Console.WriteLine($"Player#{Name} passed through the beginning tile and collected 200Ꝟ.");
                IncrementBalance(200);
            }
        }
        
        this.Tile = Tile;
        Console.WriteLine($"\nPlayer#{Name} has landed to {Tile.GetName()}");
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

    public int CalculateDistance(Tile tile)
    {
        //Monopoly has a circular shape in order to find distance We have to think about the circular shape
        int distance = (tile.GetPosition() - Tile.GetPosition() + TileRepository.Tiles.Count) % TileRepository.Tiles.Count;

        return distance;
    }

    private void GetOutIfHasNegativeBalance()
    {
        if (Balance < 0)
            TheGame.OnPlayerLosed(this);
    }
}