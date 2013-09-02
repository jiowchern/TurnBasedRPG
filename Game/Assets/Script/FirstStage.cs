using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class FirstStage :  Regulus.Game.IStage<Main>
    {
        private string _Account = "輸入帳號";
        private string _Password = "輸入密碼";

        Regulus.Game.StageLock Regulus.Game.IStage<Main>.Enter(Main obj)
        {
            _InputAccount = () =>
            {
                UnityEngine.GUILayout.BeginHorizontal();
                UnityEngine.GUILayout.Label("請輸入帳號密碼");
                _Account = UnityEngine.GUILayout.TextField(_Account);
                _Password = UnityEngine.GUILayout.TextField(_Password);
                if (UnityEngine.GUILayout.Button("登入"))
                {                    
                    obj.ToWaitVerify(_Account, _Password);
                }
                if (UnityEngine.GUILayout.Button("創造"))
                {
                    obj.ToCreateAccount(_Account, _Password);
                }
                UnityEngine.GUILayout.EndHorizontal();
            };
            obj.DrawEvent += _InputAccount;
            obj.BuildMap("Login" , null);
            return null;
        }

        Action _InputAccount;
       
        void Regulus.Game.IStage<Main>.Leave(Main obj)
        {
            obj.DrawEvent -= _InputAccount;
        }

        void Regulus.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
