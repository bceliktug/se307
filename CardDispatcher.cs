namespace Monopoly;

public static class CardDispatcher
{
    private static Action<Player>[] ArrChanceCardActions = Array.Empty<Action<Player>>();
    private static Action<Player>[] ArrCommunityCardActions = Array.Empty<Action<Player>>();
    private static readonly List<Action<Player>> CurrentChanceCardActions = new();
    private static readonly List<Action<Player>> CurrentCommunityCardActions = new();
    private readonly static Dictionary<Player, List<CardType>> CardsOfPlayers = new();

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

    private static void ShuffleChanceCards()
    {
        ShuffleCards(ArrChanceCardActions, CurrentChanceCardActions);
    }

    private static void ShuffleCommunityCards()
    {
        ShuffleCards(ArrCommunityCardActions, CurrentCommunityCardActions);
    }

    private static void ShuffleCards(Action<Player>[] Array, List<Action<Player>> List)
    {
        Util.ShuffleArray(Array);
        List.Clear();
        List.InsertRange(0, Array);
    }

    public static void OnPlayerGetChance(Player Player, CardType CardType)
    {
        if (CardsOfPlayers.ContainsKey(Player))
            CardsOfPlayers[Player].Add(CardType);
        else
        {
            List<CardType> Chances = new()
                {
                    CardType
                };

            CardsOfPlayers.Add(Player, Chances);
        }
    }

    public static void UseChance(Player Player, CardType CardType)
    {
        if (CardType == CardType.CHANCE_CARD)
        {
            ConsumeChance(CurrentChanceCardActions, Player);
            if (CurrentChanceCardActions.Count == 0)
                ShuffleCards(ArrChanceCardActions, CurrentChanceCardActions);

            return;
        }

        ConsumeChance(CurrentCommunityCardActions, Player);
        if (CurrentCommunityCardActions.Count == 0)
            ShuffleCards(ArrCommunityCardActions, CurrentCommunityCardActions);
    }

    private static void ConsumeChance(List<Action<Player>> List, Player Player)
    {
        List[0](Player);
        List.RemoveAt(0);

        if (List.Count == 0)
            CardsOfPlayers.Remove(Player);
    }

    public static bool HasChanceCard(Player Player)
    {
        return CardsOfPlayers.ContainsKey(Player);
    }

    public static int GetChanceCardCountOf(Player Player)
    {
        return CardsOfPlayers[Player].Count;
    }

    public static void UseChanceOf(Player Player)
    {
        UseChance(Player, CardsOfPlayers[Player][0]);
    }
}