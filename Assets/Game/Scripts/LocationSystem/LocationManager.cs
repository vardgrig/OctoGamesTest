using System;
using System.Collections.Generic;
using Naninovel;
using UnityEngine;

namespace Game.Scripts.LocationSystem
{
    public class LocationManager : MonoBehaviour
    {
        [SerializeField] private List<LocationData> locations;
        [SerializeField] private LocationId currentLocationId;

        public List<LocationData> Locations => locations;
    
        public static LocationManager Instance { get; private set; }
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
        public async UniTask TravelToLocation(LocationId locationId)
        {
            var location = locations.Find(l => l.locationId == locationId);
            if (location is { isAccessible: true })
            {
                var currentScript = location.sceneName;
                try
                {
                    await Engine.GetService<IScriptPlayer>().PreloadAndPlayAsync(currentScript);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    
        public void SetLocationAccessible(string locationName, bool accessible)
        {
            var locId = Enum.Parse<LocationId>(locationName);
            var location = locations.Find(l => l.locationId == locId);
            if (location != null)
            {
                location.isAccessible = accessible;
            }
        }
    }
}