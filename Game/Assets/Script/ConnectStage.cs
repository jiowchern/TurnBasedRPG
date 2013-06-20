using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class ConnectStage : Samebest.Game.IStage<Main>
    {
        

        public ConnectStage()
        {        
        }

        void Samebest.Game.IStage<Main>.Enter(Main obj)
        {
            obj.User.LinkSuccess += _UserLinkSuccess;
            obj.User.LinkFail += _UserLinkFail;
            obj.StartConnect();

            obj.DrawEvent += obj_DrawEvent;
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

        void Samebest.Game.IStage<Main>.Leave(Main obj)
        {
            obj.User.LinkSuccess -= _UserLinkSuccess;
            obj.User.LinkFail -= _UserLinkFail;
            obj.DrawEvent -= obj_DrawEvent;
        }

        void Samebest.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
