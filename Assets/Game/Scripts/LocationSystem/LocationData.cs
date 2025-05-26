namespace Game.Scripts.LocationSystem
{
    [System.Serializable]
    public class LocationData
    {
        public LocationId locationId;
        public string locationName;
        public string sceneName;
        public bool isAccessible = true;
    }

    public enum LocationId
    {
        None, Location1, Location2, Location3
    }
}