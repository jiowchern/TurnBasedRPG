using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class UserConnectStage : Regulus.Game.IStage<Regulus.Project.TurnBasedRPG.User>
    {
        Regulus.Game.StageLock Regulus.Game.IStage<User>.Enter(User obj)
        {
            var fm = obj;
            fm.Launch();

            return null;
        }

        void Regulus.Game.IStage<User>.Leave(User obj)
        {
            throw new NotImplementedException();
        }

        void Regulus.Game.IStage<User>.Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
