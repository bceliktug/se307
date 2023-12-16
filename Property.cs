namespace Monopoly;

public class Property
{
    private readonly string Name;
    private readonly Player Player;
    private readonly int Cost;

    public Property(string Name, Player Player, int Cost)
    {
        this.Name = Name;
        this.Player = Player;
        this.Cost = Cost;
    }

    public string GetName()
    {
        return Name;
    }

    public Player GetPlayer() {
        return Player;
    }

    public int GetCost() {
        return Cost;
    }
}