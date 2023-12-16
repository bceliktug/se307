namespace Monopoly;

public static class CardDispatcher
{
    private static Action<Player>[] ArrChanceCardActions = Array.Empty<Action<Player>>();
    private static Action<Player>[] ArrCommunityCardActions = Array.Empty<Action<Player>>();
    private static readonly List<Action<Player>> CurrentChanceCardActions = new();
    private static readonly List<Action<Player>> CurrentCommunityCardActions = new();
    private readonly static Dictionary<string, List<CardType>> CardsOfPlayers = new();

    public static void InitializeChanceCards(Action<Player>[] Actions)
    {
        ArrChanceCardActions = Actions;
        ShuffleChanceCards();
    }

    public static void InitializeCommunityCards(Action<Player>[] Actions)
    {
        ArrCommunityCardActions = Actions;
        ShuffleCommunityCards();
    }

    public static void ShuffleChanceCards()
    {
        ShuffleCards(ArrChanceCardActions, CurrentChanceCardActions);
    }

    public static void ShuffleCommunityCards()
    {
        ShuffleCards(ArrCommunityCardActions, CurrentCommunityCardActions);
    }

    public static void ShuffleCards(Action<Player>[] Array, List<Action<Player>> List)
    {
        Util.ShuffleArray(Array);
        List.Clear();
        List.InsertRange(0, Array);
    }

    public static void UseChance(Player player, CardType ChangeType)
    {
        // remove from list according to the type
        // Whenever a card pool is depleted, they are re - shuffled.
    }
}