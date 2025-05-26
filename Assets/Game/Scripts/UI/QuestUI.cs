using DG.Tweening;
using Game.Scripts.QuestSystem;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class QuestUI : CustomUI
    {
        [SerializeField] private GameObject questPanel;
        [SerializeField] private TMP_Text currentObjective;
    
        public void ShowQuest(QuestData quest, bool forceHide)
        {
            if (quest == null || forceHide)
            {
                QuestPanelActivity(false);
                return;
            }
            
            QuestPanelActivity(true);
            UpdateObjective(quest);
        }
    
        public void UpdateQuest(QuestData quest)
        {
            UpdateObjective(quest);
        }
    
        private void UpdateObjective(QuestData quest)
        {
            currentObjective.text = quest.CurrentObjectiveDescription;
        }

        public void QuestPanelActivity(bool isEnabled)
        {
            questPanel.SetActive(isEnabled);
        }
    }
}