using System;

namespace Game.Scripts.Minigames
{
    public static class PairedCardsEvents
    {
        public static event Action<bool> CardsMatched;
        public static event Action<Card> CardSelected;

        public static void OnCardsMatched(bool matched) => CardsMatched?.Invoke(matched);
        public static void OnCardSelected(Card a) => CardSelected?.Invoke(a);
    }
}