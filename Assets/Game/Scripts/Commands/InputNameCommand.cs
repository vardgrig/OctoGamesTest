using Game.Scripts.UI;
using Naninovel;
using UnityEngine;

namespace Game.Scripts.Commands
{
    [CommandAlias("inputName")]
    public class InputNameCommand : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            var inputUI = uiManager.GetUI<InputNameUI>();
        
            if (inputUI)
            {
                await inputUI.ShowAndWaitForInput();
            }
        }
    }
}


