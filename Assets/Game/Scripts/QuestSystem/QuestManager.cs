using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.LocationSystem;
using Game.Scripts.UI;
using Naninovel;
using UnityEngine;

namespace Game.Scripts.QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private QuestData currentQuest;
        [SerializeField] private List<QuestData> quests;

        public static QuestManager Instance { get; private set; }

        private ICustomVariableManager _manager;
        private IUIManager _uiManager;
        private QuestUI _questUI;

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

        private void Start()
        {
            _manager = Engine.GetService<ICustomVariableManager>();
            _uiManager = Engine.GetService<IUIManager>();
        }

        public void TriggerQuestUI(bool show)
        {
            _questUI.ShowQuest(currentQuest, !show);
        }

        public void StartQuest(string questId)
        {
            _questUI = _uiManager.GetUI<QuestUI>();
            if (!_questUI)
            {
                Debug.LogError("QuestUI not found");
            }
            
            var quest = quests.FirstOrDefault(q => q.questId == questId);
            if (quest == null)
            {
                Debug.LogError($"The quest with id {questId} not found");
                return;
            }

            currentQuest = quest;
            currentQuest.state = QuestState.InProgress;

            UpdateObjectiveVariables();

            _questUI.ShowQuest(currentQuest, false);
        }

        public void AdvanceQuestObjective()
        {
            if (currentQuest is { state: QuestState.InProgress })
            {
                currentQuest.currentObjectiveIndex++;
                UpdateObjectiveVariables();
                _questUI.UpdateQuest(currentQuest);
            }
        }

        private void UpdateObjectiveVariables()
        {
            var scriptName = currentQuest.CurrentObjective.GetObjectiveScriptName;
            var locId = Enum.GetName(typeof(LocationId), currentQuest.CurrentObjective.locationId);

            var a = _manager.TrySetVariableValue("G_CurrentObjectiveLocationId", locId);
            var b = _manager.TrySetVariableValue("G_CurrentObjectiveScriptName", scriptName);

            if (!a || !b)
            {
                Debug.LogError($"Error while trying to set variables");
            }
        }

        private void RestoreObjectiveVariables()
        {
            var a = _manager.TrySetVariableValue("G_CurrentObjectiveLocationId", "");
            var b = _manager.TrySetVariableValue("G_CurrentObjectiveScriptName", "");
            
            if (!a || !b)
            {
                Debug.LogError($"Error while trying to set variables");
            }
        }

        public void CompleteQuest()
        {
            if (currentQuest != null)
            {
                currentQuest.state = QuestState.Completed;
                RestoreObjectiveVariables();

                currentQuest = null;
                _questUI.ShowQuest(currentQuest, true);
            }
        }
    }
}