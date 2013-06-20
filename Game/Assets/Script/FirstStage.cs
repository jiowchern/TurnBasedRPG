using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class FirstStage :  Samebest.Game.IStage<Main>
    {
        private string _Account = "1234567890";
        private string _Password = "1234567890";

        void Samebest.Game.IStage<Main>.Enter(Main obj)
        {
            _InputAccount = () =>
            {
                UnityEngine.GUILayout.BeginHorizontal();
                UnityEngine.GUILayout.Label("請輸入帳號密碼");
                _Account = UnityEngine.GUILayout.TextField(_Account);
                _Password = UnityEngine.GUILayout.TextField(_Password);
                if (UnityEngine.GUILayout.Button("登入"))
                {                    
                    obj.DrawEvent -= _InputAccount;
                    obj.ToWaitVerify(_Account, _Password);
                }
                UnityEngine.GUILayout.EndHorizontal();

                UnityEngine.GUILayout.BeginHorizontal();
                UnityEngine.GUILayout.Label("創造帳號");
                _Account = UnityEngine.GUILayout.TextField(_Account);
                _Password = UnityEngine.GUILayout.TextField(_Password);

                if (UnityEngine.GUILayout.Button("創造"))
                {
                    obj.DrawEvent -= _InputAccount;
                    obj.ToCreateAccount(_Account, _Password);
                }

                UnityEngine.GUILayout.EndHorizontal();
            };
            obj.DrawEvent += _InputAccount;
        }
        
        Action _InputAccount;
        void Samebest.Game.IStage<Main>.Leave(Main obj)
        {
            obj.DrawEvent -= _InputAccount;
        }

        void Samebest.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
