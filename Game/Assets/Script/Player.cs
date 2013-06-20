using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour 
{
    public Regulus.Project.TurnBasedRPG.IPlayer _GamePlayer;
    public Regulus.Project.TurnBasedRPG.IPlayer GamePlayer
    {
        get
        {
            return _GamePlayer;
        }
        set
        {
            _GamePlayer = value;
            
        }
    }
	// Use this for initialization
    Regulus.Project.TurnBasedRPG.ActorMoverAbility _ActorMoverAbility;
    Samebest.Remoting.Time _Time;
	void Start () 
    {
        _ActorMoverAbility = new Regulus.Project.TurnBasedRPG.ActorMoverAbility(_GamePlayer.Direction);
        _ActorMoverAbility.ActionEvent += _StartAction;
        _ActorMoverAbility.PositionEvent += _UpdatePosition;

        Main main = GameObject.Find("Main").GetComponent<Main>();
        _Time = main.Time;
	}

    void _StartAction(long begin_time, float speed, float direction, Regulus.Types.Vector2 vector, Regulus.Project.TurnBasedRPG.ActionStatue action_status)
    {
        
        animation.Play(action_status.ToString());
        gameObject.transform.rotation = Quaternion.Euler(0, (direction) % 360, 0);
    }

    void _UpdatePosition(long time, Regulus.Types.Vector2 arg2)
    {        
        gameObject.transform.position = gameObject.transform.position + UnityRegulus.Vector3(arg2);
    }

    


    enum MoveDirection
    {        
        LeftFront,
        Front,
        RightFront,
        Left,
        Stop,
        Right,
        LeftBack, 
        Back, 
        RightBack
    };

    MoveDirection _Current = MoveDirection.Stop;

    class MoveCommandParam
    {
        public float Direction { get; set; }
        public float Speed { get; set; }
        public Regulus.Project.TurnBasedRPG.ActionStatue ActionStatus { get; set; }
    }
	void Update () 
    {
        
        
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");

        MoveDirection md = _GetMoveDirection(v , h);
        if (md != _Current)
        {
            MoveCommandParam mcp = _GetMoveCommandParam(md);
            //(_ActorMoverAbility as Regulus.Project.TurnBasedRPG.IMoverAbility).Act(mcp.ActionStatus, _GamePlayer.Speed * mcp.Speed, mcp.Direction);
            _Current = md;

            if (mcp.ActionStatus == Regulus.Project.TurnBasedRPG.ActionStatue.Idle)
            {
                _GamePlayer.Stop(mcp.Direction);
            }
            else
            {
                _GamePlayer.Walk(mcp.Direction);
            }
        }
       // (_ActorMoverAbility as Regulus.Project.TurnBasedRPG.IMoverAbility).Update(_Time.Ticks , null);
	}

    private MoveCommandParam _GetMoveCommandParam(MoveDirection md)
    {
        var mcps = new MoveCommandParam[] 
        {
            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Run , Direction = 360-20 , Speed = 1},
            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Run , Direction = 0 , Speed = 1},
            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Run , Direction = 20 , Speed = 1},

            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Idle , Direction = 360-90 , Speed = 0},
            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Idle , Direction = 0 , Speed = 0},
            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Idle , Direction = 90 , Speed = 0},

            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Idle , Direction = 180-20 , Speed = 0},
            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Idle , Direction = 180 , Speed = 0},
            new MoveCommandParam() { ActionStatus = Regulus.Project.TurnBasedRPG.ActionStatue.Idle , Direction = 180+20 , Speed = 0},
        };
        return mcps[(int)md];
    }

    

    private MoveDirection _GetMoveDirection(float v, float h)
    {
        if(v == 0 && h == 0)
            return MoveDirection.Stop;
        if (v > 0 && h == 0)
            return MoveDirection.Front;
        if (v < 0 && h == 0)
            return MoveDirection.Back;

        if (v > 0 && h > 0 )
            return MoveDirection.RightFront;
        if (v > 0 && h < 0)
            return MoveDirection.LeftFront;

        if (v == 0 && h > 0)
            return MoveDirection.Right;
        if (v == 0 && h < 0)
            return MoveDirection.Left;

        if (v < 0 && h > 0)
            return MoveDirection.RightBack;
        if (v < 0 && h < 0)
            return MoveDirection.LeftBack;



        return MoveDirection.Stop;
    }
}
