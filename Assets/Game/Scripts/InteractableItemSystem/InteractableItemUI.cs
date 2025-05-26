using System.Collections.Generic;
using System.Linq;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace Game.Scripts.InteractableItemSystem
{
    public class InteractableItemUI : CustomUI
    {
        [SerializeField] private List<EmptyInteractableItem> interactableItems;

        private UniTaskCompletionSource _completion;

        protected override void Start()
        {
            AssignItemInteractions();
        }

        public async UniTask LookForInteractable(string id)
        {
            _completion = new UniTaskCompletionSource();

            ShowItem(id);

            await _completion.Task;
        }

        private void ShowItem(string id)
        {
            var item = interactableItems.FirstOrDefault(item => item.Id == id);
            
            if(item)
                item.gameObject.SetActive(true);
            else
            {
                Debug.LogError($"Item with id {id} not found");
            }
        }
        
        private void AssignItemInteractions()
        {
            foreach (var item in interactableItems)
            {
                item.itemButton.onClick.AddListener(() => OnItemCollected(item));
                item.gameObject.SetActive(false);
            }
        }

        private void OnItemCollected(EmptyInteractableItem item)
        {
            if (interactableItems.Remove(item))
            {
                item.gameObject.SetActive(false);
                print($"The item with id {item.Id} collected");
                _completion.TrySetResult();
            }
        }
    }
}