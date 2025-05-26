// Copyright 2023 ReWaffle LLC. All rights reserved.


namespace Naninovel.UI
{
    public class ControlPanelTitleButton : ScriptableButton
    {
        private IStateManager gameState;
        private IUIManager uiManager;

        protected override void Awake ()
        {
            base.Awake();

            gameState = Engine.GetService<IStateManager>();
            uiManager = Engine.GetService<IUIManager>();
        }
        

        protected override void OnButtonClick ()
        {
            ExitToTitleAsync();
        }

        private async void ExitToTitleAsync ()
        {
            await gameState.ResetStateAsync();
            uiManager.GetUI<ITitleUI>()?.Show();
        }
    } 
}
