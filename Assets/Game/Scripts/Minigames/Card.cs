using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Scripts.Minigames
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image image;
        private Button _cardButton;
        private Sprite _iconSprite;
        [SerializeField] private Sprite hiddenSprite;

        private int _spriteId;
        private bool _isSelected;


        private void OnEnable()
        {
            PairedCardsEvents.CardsMatched += OnCardsMatched;
        }

        private void OnDisable()
        {
            PairedCardsEvents.CardsMatched -= OnCardsMatched;
        }

        private void OnCardsMatched(bool matched)
        {
            if (!_isSelected) return;
            if (!matched)
                Hide();
            else
            {
                DOTween.Sequence()
                    .Append(transform.DOScale(Vector3.one * 1.2f, 0.3f)).SetEase(Ease.OutBack)
                    .Append(transform.DOScale(Vector3.one, 0.3f)).SetEase(Ease.OutBack);
            }

            _isSelected = false;
        }

        private void Start()
        {
            _cardButton = GetComponent<Button>();
            _cardButton.onClick.AddListener(OnCardSelected);

            image.sprite = hiddenSprite;
        }

        private void OnCardSelected()
        {
            Show();
            PairedCardsEvents.OnCardSelected(this);
        }

        public void SetSpriteAndId((Sprite, int) data)
        {
            _iconSprite = data.Item1;
            _spriteId = data.Item2;
        }

        private void Show()
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.3f);
            DOVirtual.DelayedCall(0.1f, () => { image.sprite = _iconSprite; });
            _cardButton.interactable = false;
            _isSelected = true;
        }

        private void Hide()
        {
            transform.DORotate(Vector3.zero, 0.3f);
            DOVirtual.DelayedCall(0.1f, () => { image.sprite = hiddenSprite; });
            _cardButton.interactable = true;
        }

        public int GetSpriteId => _spriteId;
    }
}