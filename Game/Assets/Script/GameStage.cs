using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Regulus.Project.TurnBasedRPG.Unity
{
    class GameStage : Regulus.Game.IStage<Main>
    {
        Regulus.Game.StageLock Regulus.Game.IStage<Main>.Enter(Main obj)
        {
            _Main = obj;


            _Bind(obj.User.PlayerProvider);
            _Bind(obj.User.ObservedAbilityProvider);
            _Bind(obj.User.MapInfomationProvider);
            _Bind(obj.User.TimeProvider);

            obj.DrawEvent += obj_DrawEvent;

            return null;
        }

        private void _Bind(Regulus.Remoting.Ghost.IProviderNotice<Regulus.Remoting.ITime> providerNotice)
        {
            providerNotice.Supply += _TimeSupply;
            providerNotice.Unsupply += _TimeUnsupply;
        }

        private void _TimeUnsupply(Regulus.Remoting.ITime obj)
        {
            _Main.ResetTime();
        }

        private void _TimeSupply(Regulus.Remoting.ITime obj)
        {
            _Main.SetTime(obj);
        }

        private void _Bind(Regulus.Remoting.Ghost.IProviderNotice<IMapInfomation> providerNotice)
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

        private void _Bind(Regulus.Remoting.Ghost.IProviderNotice<IPlayer> noti)
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

        private void _Bind(Regulus.Remoting.Ghost.IProviderNotice<IObservedAbility> noti)
        {
            noti.Supply += _Main.OnEntityInto;
            noti.Unsupply += _Main.OnEntityLeft;

            noti.Supply += _ObservedAbility;
        }

        void _ObservedAbility(IObservedAbility obj)
        {
            obj.SayEvent += (message) => 
            {
                _Say(obj.Name, message);
            };
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

            _InputSayMessage();
            _ShowSayMessage();
            
        }

        string _SayText = "";
        private void _InputSayMessage()
        {
            UnityEngine.GUILayout.BeginHorizontal();

            _SayText = UnityEngine.GUILayout.TextField(_SayText);
            if (UnityEngine.GUILayout.Button("說"))
            {
                _SendSay(_SayText);
                _SayText = "";
            }

            UnityEngine.GUILayout.EndHorizontal();
        }

        private void _SendSay(string say_text)
        {
            _Player.Say(say_text);
        }

        private void _Say(string id , string message)
        {
            _Says.Enqueue("["+id+"]:"+ message );
            if (_Says.Count > 50)
                _Says.Dequeue();
        }

        Queue<string> _Says = new Queue<string>();
        UnityEngine.Vector2 _ScrollValue = new UnityEngine.Vector2();
        private void _ShowSayMessage()
        {
            _ScrollValue = UnityEngine.GUILayout.BeginScrollView(_ScrollValue );
            
            foreach (var say in _Says.Reverse())
            {
                UnityEngine.GUILayout.Label(say);
            }
            UnityEngine.GUILayout.EndScrollView();
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

        

        void Regulus.Game.IStage<Main>.Leave(Main obj)
        {

            _Unbind(obj.User.PlayerProvider);
            _Unbind(obj.User.ObservedAbilityProvider);


            
            obj.DrawEvent -= obj_DrawEvent;
            _Release(obj);
        }

        private void _Unbind(Regulus.Remoting.Ghost.IProviderNotice<IObservedAbility> noti)
        {
            noti.Supply -= _Main.OnEntityInto;
            noti.Unsupply -= _Main.OnEntityLeft;

            noti.Supply -= _ObservedAbility;
        }

        private void _Unbind(Regulus.Remoting.Ghost.IProviderNotice<IPlayer> noti)
        {
            noti.Supply -= _Initial;
        }

        void Regulus.Game.IStage<Main>.Update(Main obj)
        {
            
        }
    }
}
