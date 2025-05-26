using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.InteractableItemSystem
{
    public class EmptyInteractableItem : MonoBehaviour
    {
        [SerializeField] private string id;
        public Button itemButton;
        public string Id => id;

        private void Start()
        {
            itemButton = GetComponent<Button>();
        }
    }
}