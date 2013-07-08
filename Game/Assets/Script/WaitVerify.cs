using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class WaitVerify : Regulus.Game.IStage<Main>
    {
        string _StatusMessage;
        public string StatusMessage 
        {
            get 
            {
                return _StatusMessage;
            }
            set 
            {
                UnityEngine.Debug.Log(value);
                _StatusMessage = value;
            }
        }
        private string _Account = "1234567890";
        private string _Password = "1234567890";
        Regulus.Game.StageMachine<WaitVerify> _StageMachine;

        public WaitVerify(string account, string password)
        {
            _Account = account;
            _Password = password;
        }
        void Regulus.Game.IStage<Main>.Enter(Main obj)
        {
            obj.DrawEvent += obj_DrawEvent;
            _StageMachine = new Regulus.Game.StageMachine<WaitVerify>(this);
            _StageMachine.Push(new Regulus.Project.TurnBasedRPG.Unity.UserConnect(obj));
        }

        void obj_DrawEvent()
        {
            UnityEngine.GUILayout.BeginVertical();
            UnityEngine.GUILayout.Label(StatusMessage);
            UnityEngine.GUILayout.EndVertical();
        }

        void Regulus.Game.IStage<Main>.Leave(Main obj)
        {
            obj.DrawEvent -= obj_DrawEvent;
            _StageMachine.Termination();
        }

        void Regulus.Game.IStage<Main>.Update(Main obj)
        {
            _StageMachine.Update();
        }

        internal void ToVerify(Main obj)
        {
            _StageMachine.Push(new Regulus.Project.TurnBasedRPG.Unity.UserVerify(obj ,_Account, _Password));
        }

        internal void ToParking(Main obj)
        {
            obj.ToParking();
        }

        internal void ToExit(Main obj)
        {
            obj.ToFirst();
        }
    }
}
