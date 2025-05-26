using Game.Scripts.QuestSystem;
using Naninovel;

namespace Game.Scripts.Commands
{
    [CommandAlias("startQuest")]
    public class StartQuestCommand : Command
    {
        [ParameterAlias("id")] public StringParameter QuestId;
    
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            QuestManager.Instance.StartQuest(QuestId);
            return UniTask.CompletedTask;
        }
    }

    [CommandAlias("updateQuest")]
    public class UpdateQuestCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            QuestManager.Instance.AdvanceQuestObjective();
            return UniTask.CompletedTask;
        }
    }

    [CommandAlias("completeQuest")]
    public class CompleteQuestCommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            QuestManager.Instance.CompleteQuest();
            return UniTask.CompletedTask;
        }
    }

    [CommandAlias("showQuestUI")]
    public class ShowQuestUICommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            QuestManager.Instance.TriggerQuestUI(true);
            return UniTask.CompletedTask;
        }
    }
    [CommandAlias("hideQuestUI")]
    public class HideQuestUICommand : Command
    {
        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            QuestManager.Instance.TriggerQuestUI(false);
            return UniTask.CompletedTask;
        }
    }
}