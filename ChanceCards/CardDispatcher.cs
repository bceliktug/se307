namespace Monopoly;

public class CardDispatcher
{
    private Action<Player>[] ArrChanceCardActions = Array.Empty<Action<Player>>();
    private Action<Player>[] ArrCommunityCardActions = Array.Empty<Action<Player>>();
    private readonly List<Action<Player>> CurrentChanceCardActions = new();
    private readonly List<Action<Player>> CurrentCommunityCardActions = new();
    private readonly Dictionary<Player, List<CardType>> CardsOfPlayers = new();

    public void InitializeChanceCards(Action<Player>[] Actions)
    {
        ArrChanceCardActions = Actions;
        ShuffleChanceCards();
    }

    public void InitializeCommunityCards(Action<Player>[] Actions)
    {
        ArrCommunityCardActions = Actions;
        ShuffleCommunityCards();
    }

    private void ShuffleChanceCards()
    {
        ShuffleCards(ArrChanceCardActions, CurrentChanceCardActions);
    }

    private void ShuffleCommunityCards()
    {
        ShuffleCards(ArrCommunityCardActions, CurrentCommunityCardActions);
    }

    private void ShuffleCards(Action<Player>[] Array, List<Action<Player>> List)
    {
        Util.ShuffleArray(Array);
        List.Clear();
        List.InsertRange(0, Array);
    }

    public void OnPlayerGetChance(Player Player, CardType CardType)
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

    public void UseChance(Player Player, CardType CardType)
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

    private void ConsumeChance(List<Action<Player>> List, Player Player)
    {
        List[0](Player);
        List.RemoveAt(0);

    }

    public bool HasChanceCard(Player Player)
    {
        return CardsOfPlayers.ContainsKey(Player);
    }

    public int GetChanceCardCountOf(Player Player)
    {
        return CardsOfPlayers[Player].Count;
    }

    public void UseChanceOf(Player Player)
    {
        List<CardType> List = CardsOfPlayers[Player];
        UseChance(Player, List[0]);
        List.RemoveAt(0);

        if (List.Count == 0)
            CardsOfPlayers.Remove(Player);
    }
}