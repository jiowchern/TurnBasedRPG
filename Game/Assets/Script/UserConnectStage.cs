using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class UserConnectStage : Samebest.Game.IStage<Regulus.Project.TurnBasedRPG.User>
    {
        void Samebest.Game.IStage<User>.Enter(User obj)
        {
            var fm = obj;
            fm.Launch();
        }

        void Samebest.Game.IStage<User>.Leave(User obj)
        {
            throw new NotImplementedException();
        }

        void Samebest.Game.IStage<User>.Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
