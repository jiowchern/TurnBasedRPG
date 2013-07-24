using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class ParkingStage :   Regulus.Game.IStage<Main>
    {
        Serializable.EntityLookInfomation[] _Actors = new Serializable.EntityLookInfomation[0];
        Action<Regulus.Project.TurnBasedRPG.IParking> _QueryActors;
        Regulus.Project.TurnBasedRPG.IParking _Parking;
        Regulus.Game.StageLock Regulus.Game.IStage<Main>.Enter(Main obj)
        {
                        
            obj.DrawEvent += obj_DrawEvent;

            var notif = obj.User.ParkingProvider;

            _QueryActors = (parking) =>
            {
                var val = parking.QueryActors();
                val.OnValue += val_OnValue;

                _Parking = parking;
            };

            if (notif.Ghosts.Length > 0)
            {                
                _QueryActors(notif.Ghosts[0]);
            }
            else
            {
                notif.Supply += _QueryActors;
            }
            return null;
        }

        void val_OnValue(Serializable.EntityLookInfomation[] obj)
        {
            _Actors = obj;
            if (_Actors.Length > 1)
            {
                _Actor = _Actors[0].Name;
            }
        }

        string _Actor ="輸入名稱";
        void _InfoGame()
        {
            if (_Parking != null)
            {
                var res = _Parking.Select(_Actor);
                res.OnValue += _SelectResult;                
            }
        }

        bool _SelectActor = false;
        void _SelectResult(bool res)
        {
            _SelectActor = res;
        }
        void obj_DrawEvent()
        {
            
            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.Label("輸入角色登入(注意大小寫):");
            _Actor = UnityEngine.GUILayout.TextField(_Actor);
            if (UnityEngine.GUILayout.Button("進入遊戲"))
            {                
                _InfoGame();
            }
            if (UnityEngine.GUILayout.Button("創造角色"))
            {                
                _CreateActor();
            }
            UnityEngine.GUILayout.EndHorizontal();

            UnityEngine.GUILayout.BeginVertical();
            UnityEngine.GUILayout.TextField("角色列表");
            foreach (var actor in _Actors)
            {
                UnityEngine.GUILayout.TextField(actor.Name);
            }
            UnityEngine.GUILayout.EndVertical();
        }

        private void _CreateActor()
        {
            var val = _Parking.CreateActor(new Serializable.EntityLookInfomation() { Name = _Actor });
            val.OnValue += _OnCreateActorResult;
        }

        void _OnCreateActorResult(bool obj)
        {
            if (obj)
            {
                var val = _Parking.QueryActors();
                val.OnValue += val_OnValue;
            }
        }

        void Regulus.Game.IStage<Main>.Leave(Main obj)
        {
            

            var notif = obj.User.ParkingProvider;            
            obj.DrawEvent -= obj_DrawEvent;
            notif.Supply -= _QueryActors;
            
        }

        void Regulus.Game.IStage<Main>.Update(Main obj)
        {
            if (_SelectActor)
            {
                obj.ToGame();
            }
        }
    }
}
