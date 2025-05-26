using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Scripts.UI
{
    public class ScalableButton : Button
    {
        [SerializeField] private float scaleFactor = 1.2f;
        [SerializeField] private float scaleDuration = 0.3f;
        [SerializeField] private Ease easeType = Ease.OutBack;
        
        private void ScaleUp()
        {
            transform.DOScale(Vector3.one * scaleFactor, scaleDuration).SetEase(easeType);
        }

        private void ResetScale()
        {
            transform.DOScale(Vector3.one, scaleDuration).SetEase(easeType);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (!IsInteractable()) return;
            ScaleUp();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (!IsInteractable()) return;
            ResetScale();
        }
    }
}
