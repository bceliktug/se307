namespace Monopoly;

public static class PunishmentDispatcher
{
    private static readonly Dictionary<Player, int> Punishments = new();
    private static readonly int PUNISHMENT_TURN_COUNT = 2;

    public static bool HasPunishment(Player Player)
    {
        return Punishments.ContainsKey(Player);
    }

    public static void Punish(Player Player)
    {
        Punishments.Add(Player, PUNISHMENT_TURN_COUNT);
    }

    public static void DecrementPunishment(Player Player)
    {
        int Remaining = Punishments[Player] - 1;
        Punishments[Player] = Remaining;

        if (Remaining == 0)
            Punishments.Remove(Player);
    }
}