using Game.Scripts.LocationSystem;
using Naninovel;
using UnityEngine;

namespace Game.Scripts.QuestSystem
{
    [CreateAssetMenu(fileName = "Objective Data", menuName = "Quest")]
    public class ObjectiveData : ScriptableObject
    {
        public int objectiveId;
        public LocationId locationId;
        public string objectiveDescription;
        public Script objectiveScript;

        public string GetObjectiveScriptName => objectiveScript.Name;
    }
}