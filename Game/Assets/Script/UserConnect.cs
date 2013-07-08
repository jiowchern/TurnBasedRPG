using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class UserConnect : Regulus.Game.IStage<WaitVerify>
    {
        
        private Main _Main;

        public UserConnect(Main obj)
        {
        
            _Main = obj;
        }
        
        void Regulus.Game.IStage<WaitVerify>.Enter(WaitVerify obj)
        {
            _UserLinkSuccess = () =>
            {                
                obj.StatusMessage = "連線成功.";
                obj.ToVerify(_Main);
            };

            _UserLinkFail = (msg) =>
            {
                obj.StatusMessage = "連線失敗." + msg;
                obj.ToExit(_Main);
            };

            obj.StatusMessage = "連線到伺服器...";
            _Main.User.LinkSuccess += _UserLinkSuccess;
            _Main.User.LinkFail += _UserLinkFail;
            _Main.StartConnect();
            
            
        }

        Action _UserLinkSuccess;
        Action<string> _UserLinkFail;

        void Regulus.Game.IStage<WaitVerify>.Leave(WaitVerify obj)
        {
            _Main.User.LinkSuccess -= _UserLinkSuccess;
            _Main.User.LinkFail -= _UserLinkFail;
        }

        void Regulus.Game.IStage<WaitVerify>.Update(WaitVerify obj)
        {
            
        }
    }
}
