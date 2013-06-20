using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class VerifyStage : Samebest.Game.IStage<Main>
    {
        private string _Account;
        private string _Password;

        public VerifyStage(string _Account, string _Password)
        {
            // TODO: Complete member initialization
            this._Account = _Account;
            this._Password = _Password;
        }

        void Samebest.Game.IStage<Main>.Enter(Main obj)
        {
            obj.User.VerifyProvider.Supply += VerifyProvider_Supply;
        }

        void VerifyProvider_Supply(IVerify obj)
        {
            var val = obj.Login(_Account, _Password);
            val.OnValue += val_OnValue;
        }
        public event Action<bool> VerifyResultEvent;
        void val_OnValue(LoginResult obj)
        {
            if (VerifyResultEvent != null)
                if (obj == LoginResult.Error)
                {
                    VerifyResultEvent(false);                       
                }
                else if (obj == LoginResult.RepeatLogin)
                {
                    VerifyResultEvent(false);                       
                }
                else if (obj == LoginResult.Success)
                {
                    VerifyResultEvent(true);                       
                }
        }

        void Samebest.Game.IStage<Main>.Leave(Main obj)
        {
            obj.User.VerifyProvider.Supply -= VerifyProvider_Supply;
        }

        void Samebest.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
