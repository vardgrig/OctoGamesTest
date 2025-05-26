using System;
using Naninovel;
namespace Game.Scripts.Commands
{
    [CommandAlias("startQuestScript")]
    public class ScriptCommands : Command
    {
        public override async UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            try
            {
                var scriptToPlay = Engine.GetService<ICustomVariableManager>()
                    .GetVariableValue("G_CurrentObjectiveScriptName");
            
                await Engine.GetService<IScriptPlayer>().PreloadAndPlayAsync(scriptToPlay);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}