using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class CreateAccountStage : Samebest.Game.IStage<Main>
    {
        private string _Account;
        private string _Password;

        public CreateAccountStage(string account , string password)
        {
            // TODO: Complete member initialization
            _Account = account;
            _Password = password;
        }

        void Samebest.Game.IStage<Main>.Enter(Main obj)
        {
            obj.User.VerifyProvider.Supply += VerifyProvider_Supply;
            obj.User.VerifyProvider.Unsupply += VerifyProvider_Unsupply;
        }

        void VerifyProvider_Unsupply(IVerify obj)
        {
            if (CreateResult != null)
                CreateResult(false);
        }

        void VerifyProvider_Supply(IVerify obj)
        {
            var val = obj.CreateAccount(_Account, _Password);
            val.OnValue += val_OnValue;
        }

        public event Action<bool> CreateResult;
        void val_OnValue(bool obj)
        {
            if (CreateResult != null)
                CreateResult(obj);
        }

        void Samebest.Game.IStage<Main>.Leave(Main obj)
        {
            obj.User.VerifyProvider.Supply -= VerifyProvider_Supply;
            obj.User.VerifyProvider.Unsupply -= VerifyProvider_Unsupply;            
        }

        void Samebest.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
