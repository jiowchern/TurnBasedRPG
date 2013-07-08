using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class CreateAccountStage : Regulus.Game.IStage<Main>
    {
        private string _Account;
        private string _Password;

        public CreateAccountStage(string account , string password)
        {
            // TODO: Complete member initialization
            _Account = account;
            _Password = password;
        }

        void Regulus.Game.IStage<Main>.Enter(Main obj)
        {
            _Message = "接收驗證元件...";
            obj.User.VerifyProvider.Supply += VerifyProvider_Supply;
            obj.User.VerifyProvider.Unsupply += VerifyProvider_Unsupply;
            obj.DrawEvent += obj_DrawEvent;
        }
        string _Message = "------------------------";
        private void obj_DrawEvent()
        {
            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.Label(_Message);
            UnityEngine.GUILayout.EndHorizontal();
        }

        

        void VerifyProvider_Unsupply(IVerify obj)
        {
            _Message = "驗證元件移除";
            if (CreateResult != null)
                CreateResult(false);
        }

        void VerifyProvider_Supply(IVerify obj)
        {
            _Message = "取得驗證元件";
            var val = obj.CreateAccount(_Account, _Password);
            val.OnValue += val_OnValue;
        }

        public event Action<bool> CreateResult;
        void val_OnValue(bool obj)
        {
            _Message = "驗證結束";
            if (CreateResult != null)
                CreateResult(obj);
        }

        void Regulus.Game.IStage<Main>.Leave(Main obj)
        {
            _Message = "結束驗證";
            obj.User.VerifyProvider.Supply -= VerifyProvider_Supply;
            obj.User.VerifyProvider.Unsupply -= VerifyProvider_Unsupply;
            obj.DrawEvent -= obj_DrawEvent;
        }

        void Regulus.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
