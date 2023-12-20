namespace Monopoly;

public class Tile
{
    private readonly string Name;
    private readonly Action<Player, Tile> Action;
    private readonly int Position;

    public Tile(string Name, Action<Player, Tile> Action, int Position)
    {
        this.Name = Name;
        this.Action = Action;
        this.Position = Position;
    }

    public string GetName() {
        return Name;
    }

    public int GetPosition()
    {
        return Position;
    }

    public void OnLand(Player Player)
    {
        Action(Player, this);
    }
}