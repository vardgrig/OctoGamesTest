using System.Collections.Generic;

namespace Game.Scripts.QuestSystem
{
    [System.Serializable]
    public class QuestData
    {
        public string questId;
        public string title;
        public string description;
        public QuestState state;
        public List<ObjectiveData> objectives;
        public int currentObjectiveIndex;
        
        public ObjectiveData CurrentObjective => objectives[currentObjectiveIndex];
        public string CurrentObjectiveDescription => objectives[currentObjectiveIndex].objectiveDescription;
    }

    public enum QuestState
    {
        NotStarted, InProgress, Completed
    }
}