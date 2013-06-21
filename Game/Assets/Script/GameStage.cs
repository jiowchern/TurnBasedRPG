using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class GameStage : Samebest.Game.IStage<Main>
    {
        void Samebest.Game.IStage<Main>.Enter(Main obj)
        {
            _Main = obj;


            _Bind(obj.User.PlayerProvider);
            _Bind(obj.User.ObservedAbilityProvider);
            _Bind(obj.User.MapInfomationProvider);
            _Bind(obj.User.TimeProvider);

            obj.DrawEvent += obj_DrawEvent;
        }

        private void _Bind(Samebest.Remoting.Ghost.IProviderNotice<Samebest.Remoting.ITime> providerNotice)
        {
            providerNotice.Supply += _TimeSupply;
            providerNotice.Unsupply += _TimeUnsupply;
        }

        private void _TimeUnsupply(Samebest.Remoting.ITime obj)
        {
            _Main.ResetTime();
        }

        private void _TimeSupply(Samebest.Remoting.ITime obj)
        {
            _Main.SetTime(obj);
        }

        private void _Bind(Samebest.Remoting.Ghost.IProviderNotice<IMapInfomation> providerNotice)
        {
            providerNotice.Supply += _MapSupply;
            providerNotice.Unsupply += _MapUnsupply;
        }

        private void _MapUnsupply(IMapInfomation obj)
        {
            _Main.SetMap(null);
        }
        
        private void _MapSupply(IMapInfomation obj)
        {
            _Main.SetMap(obj);
        }

        private void _Bind(Samebest.Remoting.Ghost.IProviderNotice<IPlayer> noti)
        {
            if (noti.Ghosts.Length > 0)
            {
                _Initial(noti.Ghosts[0]);
            }
            else
            {
                noti.Supply += _Initial;                
            }
        }

        private void _Bind(Samebest.Remoting.Ghost.IProviderNotice<IObservedAbility> noti)
        {
            noti.Supply += _Main.OnEntityInto;
            noti.Unsupply += _Main.OnEntityLeft;
        }

        

        string _PositionX = "-----";
        string _PositionY = "-----";
        string _Vision = "-----";
        string _Speed = "-----"; 
        void obj_DrawEvent()
        {
            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.TextField("視野");
            _Vision = UnityEngine.GUILayout.TextField(_Vision);
            if (UnityEngine.GUILayout.Button("設定"))
            { 
                int v;
                if (int.TryParse(_Vision, out v))
                {
                    if (_Player != null)
                    {
                        _Player.SetVision(v);
                    }
                }
            }
            UnityEngine.GUILayout.EndHorizontal();


            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.TextField("位置");
            _PositionX = UnityEngine.GUILayout.TextField(_PositionX);
            _PositionY = UnityEngine.GUILayout.TextField(_PositionY);
            if (UnityEngine.GUILayout.Button("設定"))
            { 
                if(_Player != null)
                {
                    int x,y;
                    if( int.TryParse(_PositionX , out x) && int.TryParse(_PositionY , out y))
                    {
                        _Player.SetPosition((float)x,(float)y);
                    }                    
                }
            }
            UnityEngine.GUILayout.EndHorizontal();

            UnityEngine.GUILayout.BeginHorizontal();
            UnityEngine.GUILayout.TextField("速度");
            _Speed = UnityEngine.GUILayout.TextField(_Speed);
            if (UnityEngine.GUILayout.Button("設定"))
            {
                int val;
                if (int.TryParse(_Speed, out val))
                    _Player.SetSpeed(val);
            }
            UnityEngine.GUILayout.EndHorizontal();

            UnityEngine.GUILayout.BeginVertical();

            foreach (ActionStatue actionStatue in Enum.GetValues(typeof(ActionStatue)))
            {
                if (UnityEngine.GUILayout.Button(actionStatue.ToString()))
                {
                    _Player.BodyMovements(actionStatue);
                }
            }

            UnityEngine.GUILayout.EndVertical();
        }
        IPlayer _Player;
        Main _Main;
        void _Initial(IPlayer obj)
        {
            _Player = obj;
            var id = _Player.Id;
            _Main.SetPlayer(_Player);
            _Main.SetPlayerId(id);
            _Player.Ready();            
        }

        void _Release(Main main)
        {
            if (_Player != null)
            {            
                _Player.Logout();
            }
        }

        

        void Samebest.Game.IStage<Main>.Leave(Main obj)
        {

            _Unbind(obj.User.PlayerProvider);
            _Unbind(obj.User.ObservedAbilityProvider);


            
            obj.DrawEvent -= obj_DrawEvent;
            _Release(obj);
        }

        private void _Unbind(Samebest.Remoting.Ghost.IProviderNotice<IObservedAbility> noti)
        {
            noti.Supply -= _Main.OnEntityInto;
            noti.Unsupply -= _Main.OnEntityLeft;
        }

        private void _Unbind(Samebest.Remoting.Ghost.IProviderNotice<IPlayer> noti)
        {
            noti.Supply -= _Initial;
        }

        void Samebest.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
