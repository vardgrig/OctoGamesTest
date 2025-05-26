using Game.Scripts.InteractableItemSystem;
using Naninovel;

namespace Game.Scripts.Commands
{
    public class InteractableItemCommands
    {
        [CommandAlias("spawnSpecialItem")]
        public class SpawnInteractableItem : Command
        {
            [ParameterAlias("id")] public StringParameter Id;
            public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
            {
                var manager = Engine.GetService<IUIManager>();
                var itemUI = manager.GetUI<InteractableItemUI>();

                if (itemUI)
                {
                    await itemUI.LookForInteractable(Id);
                }

            }
        }
    }
}