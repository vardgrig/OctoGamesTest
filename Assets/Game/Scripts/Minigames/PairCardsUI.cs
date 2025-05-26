using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Naninovel;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Minigames
{
    public class PairCardsUI : CustomUI
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Card card;
        [SerializeField] private Transform gridLayout;
        [SerializeField] private TMP_Text matchText;
        
        private List<(Sprite, int)> _spritePairs;

        private Card _firstSelection;
        private Card _secondSelection;

        private int _matchCount;
        private int _failCount;
        
        private UniTaskCompletionSource _gameCompletionTask;

        public async UniTask StartMinigameAsync()
        {
            PrepareSprites();
            CreateCards();
            _gameCompletionTask = new UniTaskCompletionSource();

            await _gameCompletionTask.Task;
        }
        
        private void PrepareSprites()
        {
            if (sprites.Length == 0)
            {
                Debug.LogError("The sprites are not added");
                return;
            }
            _spritePairs = new List<(Sprite, int)>();
            for (var i = 0; i < sprites.Length; i++)
            {
                var t = sprites[i];
                _spritePairs.Add((t,i));
                _spritePairs.Add((t,i));
            }

            ShuffleList(_spritePairs);
            UpdateMatchText();
        }

        private void OnCardSelected(Card selectedCard)
        {
            if (!_firstSelection)
            {
                _firstSelection = selectedCard;
                return;
            }

            if (!_secondSelection)
            {
                _secondSelection = selectedCard;
                StartCoroutine(CheckMatchingCard());
                _firstSelection = null;
                _secondSelection = null;
            }
        }

        private IEnumerator CheckMatchingCard()
        {
            var a = _firstSelection;
            var b = _secondSelection;
            yield return new WaitForSeconds(0.3f);
            if (a.GetSpriteId == b.GetSpriteId)
            {
                _matchCount++;
                PairedCardsEvents.OnCardsMatched(true);
                if (_matchCount == _spritePairs.Count / 2)
                {
                    _gameCompletionTask.TrySetResult();
                }
            }
            else
            {
                _failCount++;
                PairedCardsEvents.OnCardsMatched(false);
                UpdateMatchText();
                if (_failCount == _spritePairs.Count)
                {
                    _gameCompletionTask.TrySetResult();
                }
            }
        }

        private void CreateCards()
        {
            if (!gridLayout)
            {
                Debug.LogError("The Grid layout Transform is not assigned");
                return;
            }
            foreach (var sprite in _spritePairs)
            {
                var cardObject = Instantiate(card, gridLayout);
                cardObject.SetSpriteAndId(sprite);
            }
        }
        
        private void ShuffleList<T>(List<T> list) 
        {
            var count = list.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) 
            {
                var r = Random.Range(i, count);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }

        private void UpdateMatchText()
        {
            matchText.text = $"Attempts: {_failCount} / {_spritePairs.Count}";
            matchText.transform.DOPunchScale(Vector3.one * 1.1f, 0.5f);
        }

        protected override void OnEnable()
        {
            PairedCardsEvents.CardSelected += OnCardSelected;
        }

        protected override void OnDisable()
        {
            PairedCardsEvents.CardSelected -= OnCardSelected;
        }
    }
}
