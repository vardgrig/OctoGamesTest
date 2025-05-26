using System;
using Game.Scripts.LocationSystem;
using Naninovel;

namespace Game.Scripts.Commands
{
    public class LocationCommands
    {
        [CommandAlias("lockLocation")]
        public class LockLocationCommand : Command
        {
            [ParameterAlias("name")] public StringParameter LocationName;
    
            public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
            {
                LocationManager.Instance.SetLocationAccessible(LocationName, false);
                return UniTask.CompletedTask;
            }
        }
        
        [CommandAlias("unlockLocation")]
        public class UnLockLocationCommand : Command
        {
            [ParameterAlias("name")] public StringParameter LocationName;
    
            public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
            {
                LocationManager.Instance.SetLocationAccessible(LocationName, true);
                return UniTask.CompletedTask;
            }
        }
    }
}