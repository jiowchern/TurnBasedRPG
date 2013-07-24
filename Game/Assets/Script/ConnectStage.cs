using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class ConnectStage : Regulus.Game.IStage<Main>
    {
        

        public ConnectStage()
        {        

        }

        Regulus.Game.StageLock Regulus.Game.IStage<Main>.Enter(Main obj)
        {
            obj.User.LinkSuccess += _UserLinkSuccess;
            obj.User.LinkFail += _UserLinkFail;
            obj.DrawEvent += obj_DrawEvent;
            obj.StartConnect();
            string _StatusMessage = "StartConnect...";

            return null;
        }

        string _StatusMessage = "開始連線...";
        void obj_DrawEvent()
        {
            UnityEngine.GUILayout.BeginVertical();
            UnityEngine.GUILayout.Label(_StatusMessage);
            UnityEngine.GUILayout.EndVertical();
        }

        private void _UserLinkFail(string obj)
        {
            _StatusMessage = "連線失敗";
            if (ConnectResultEvent != null)
                ConnectResultEvent(false);
        }

        public event Action<bool> ConnectResultEvent;
        private void _UserLinkSuccess()
        {
            _StatusMessage = "連線成功";
            if (ConnectResultEvent != null)
                ConnectResultEvent(true);

        }

        void Regulus.Game.IStage<Main>.Leave(Main obj)
        {
            obj.User.LinkSuccess -= _UserLinkSuccess;
            obj.User.LinkFail -= _UserLinkFail;
            obj.DrawEvent -= obj_DrawEvent;
        }

        void Regulus.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
