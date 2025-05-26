using Game.Scripts.UI;
using Naninovel;

namespace Game.Scripts.Commands
{
	[CommandAlias("globalMap")]
    public class GlobalMapCommands : Command
    {
	    public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
	    {
		    var manager = Engine.GetService<IUIManager>();
		    var globalUI = manager.GetUI<GlobalMapUI>();

		    if (globalUI)
		    {
			    await globalUI.ShowMapAsync();
		    }
	    }
    }
}