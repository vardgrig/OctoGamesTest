using Game.Scripts.Minigames;
using Naninovel;
using UnityEngine;

namespace Game.Scripts.Commands
{
    [CommandAlias("minigame")]
    public class MinigameCommands : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            var minigameUI = uiManager.GetUI<PairCardsUI>();
        
            if (minigameUI)
            {
                await minigameUI.StartMinigameAsync();
            }
        }
    }
}