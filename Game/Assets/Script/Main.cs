using UnityEngine;
using System.Collections;
using Regulus.Project.TurnBasedRPG.Unity;
using System.Linq;
public class Main : MonoBehaviour 
{
    
    Samebest.Game.StageMachine<Main> _StageMachine;
    public Regulus.Project.TurnBasedRPG.User User {get;private set;}
    Regulus.Project.TurnBasedRPG.User _UserFramework;
    public string IpAddress;
	void Start () 
    {
        IpAddress = "114.34.90.217:5055";
        //IpAddress = "127.0.0.1:5055";
        User = new Regulus.Project.TurnBasedRPG.User(new Samebest.Remoting.Ghost.Config() { Address = IpAddress , Name = "TurnBasedRPGComplex" });
        
        _StageMachine = new Samebest.Game.StageMachine<Main>(this);        
        _UserFramework = User;
        ToFirst();
        
	}
    
    void User_LinkFail(string obj)
    {
        UnityEngine.Debug.Log(obj);
        ToFirst();
    }

    public event System.Action DrawEvent;
    void OnGUI()
    { 
        if(DrawEvent != null)
            DrawEvent();

        UnityEngine.GUILayout.BeginHorizontal();
        UnityEngine.GUILayout.Label(IpAddress);    
        UnityEngine.GUILayout.EndHorizontal();
    }
	
	// Update is called once per frame
	void Update () 
    {
        _Time.Update();        
        try
        {
            if (_UserFramework != null)
                _UserFramework.Update();

            _StageMachine.Update();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            throw;
        }
   
        
	}

    internal void ToWaitVerify(string account, string password)
    {
        _Account = account;
        _Password = password;
        var cs = new Regulus.Project.TurnBasedRPG.Unity.ConnectStage();
        cs.ConnectResultEvent += _OnVerifyConnectResult;
        _StageMachine.Push(cs);
    }

    private void _OnVerifyConnectResult(bool res)
    {
        if (res)
        {
            var vs = new Regulus.Project.TurnBasedRPG.Unity.VerifyStage(_Account, _Password);
            vs.VerifyResultEvent += vs_VerifyResultEvent;
            _StageMachine.Push(vs);
        }
        else
        {
            ToFirst();
        }
    }

    void vs_VerifyResultEvent(bool obj)
    {
        if (obj)
            ToParking();
        else
            ToFirst();
    }

    

    internal void StartConnect()
    {
        _UserFramework.Launch();
    }

    internal void EndConnect()
    {
        _UserFramework.Shutdown();
    }

    internal void ToParking()
    {
        _StageMachine.Push(new Regulus.Project.TurnBasedRPG.Unity.ParkingStage());        
    }

    internal void ToFirst()
    {
        _StageMachine.Push(new Regulus.Project.TurnBasedRPG.Unity.FirstStage());        
    }

    internal void ToGame()
    {
        _StageMachine.Push(new Regulus.Project.TurnBasedRPG.Unity.GameStage());        
    }

    public GameObject Entity;
    public GameObject Player;
    internal void OnEntityInto(Regulus.Project.TurnBasedRPG.IObservedAbility obj)
    {


        if (true)
        {
            GameObject eo = UnityEngine.GameObject.Instantiate(Entity) as GameObject;
            var ent = eo.GetComponent<Entity>();
            ent.Info = obj;

            var ac = eo.GetComponent<ActorController>();
            ac.Info = obj;
            ac.Map = GetComponent<Map>();

            if (Id == obj.Id)
            {
                var plr = eo.AddComponent<Player>();                
                plr._GamePlayer = _Player;

                var camera = UnityEngine.GameObject.FindWithTag("MainCamera");
                SmoothFollow smoothFollow = camera.GetComponent<SmoothFollow>();
                smoothFollow.target = eo.transform;
            }
        }
        
            
        
    }

    internal void OnEntityLeft(Regulus.Project.TurnBasedRPG.IObservedAbility obj)
    {        
        var entityObjects = from eo in UnityEngine.GameObject.FindGameObjectsWithTag("Entity")
                            let info = eo.GetComponent<Entity>().Info
                            where info == obj
                            select eo;
        foreach (var eo in entityObjects)
        {
            UnityEngine.GameObject.DestroyObject(eo);
        }        
    }

    internal void SetPlayerId(System.Guid id)
    {
        Id = id;
    }

    public System.Guid Id { get; private set; }

    Regulus.Project.TurnBasedRPG.IPlayer _Player;
    internal void SetPlayer(Regulus.Project.TurnBasedRPG.IPlayer player)
    {
        _Player = player;
    }

    public void SetMap(Regulus.Project.TurnBasedRPG.IMapInfomation info)
    {
        GetComponent<Map>().Info = info;
    }
    Samebest.Remoting.Time _Time = new Samebest.Remoting.Time();

    public Samebest.Remoting.Time Time { get { return _Time;  } }
    internal void SetTime(Samebest.Remoting.ITime obj)
    {
        _Time = new Samebest.Remoting.Time(obj);
    }
    internal void ResetTime()
    {
        _Time = new Samebest.Remoting.Time();
    }

    string _Account;
    string _Password;
    internal void ToCreateAccount(string account, string password)
    {
        _Account = account;
        _Password = password;

        var cs = new Regulus.Project.TurnBasedRPG.Unity.ConnectStage();
        cs.ConnectResultEvent += _OnCreateAccountConnectResult;
        _StageMachine.Push(cs);
    }

    void _OnCreateAccountConnectResult(bool connect_result)
    {
        if (connect_result == false)
        {
            ToFirst();
        }
        else
        {
            var cas = new Regulus.Project.TurnBasedRPG.Unity.CreateAccountStage(_Account, _Password);
            cas.CreateResult += cas_CreateResult;
            _StageMachine.Push(cas);
        }
    }

    void cas_CreateResult(bool obj)
    {
        ToFirst();
    }
}
