using System;
using Game.Scripts.LocationSystem;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class GlobalMapUI : CustomUI
    {
        [SerializeField] private Button[] locationButtons;

        private UniTaskCompletionSource _completion;
        
        protected override void Start()
        {
            for (var i = 0; i < locationButtons.Length; i++)
            {
                var index = i + 1;
                locationButtons[i].onClick.AddListener(() => OnLocationSelected(index));
            }
        }

        public async UniTask ShowMapAsync()
        {
            _completion = new UniTaskCompletionSource();

            UpdateLocationStates();


            await _completion.Task;
        }
    
        private void UpdateLocationStates()
        {
            print("Updating Location Button States");
            for (var i = 0; i < LocationManager.Instance.Locations.Count; i++)
            {
                var location = LocationManager.Instance.Locations[i];
                locationButtons[i].interactable = location.isAccessible;
            }
        }
    
        private void OnLocationSelected(int locationIndex)
        {
            _ = OnLocationSelectedAsync(locationIndex);
        }

        private async UniTask OnLocationSelectedAsync(int locationIndex)
        {
            try
            {
                await LocationManager.Instance.TravelToLocation((LocationId)locationIndex);
                _completion.TrySetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}