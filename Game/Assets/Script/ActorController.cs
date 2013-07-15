using UnityEngine;
using System.Collections;

public class ActorController : MonoBehaviour {

	// Use this for initialization
    public Animation ActorAnimation;
	void Start () 
    {
        Info.ShowActionEvent += Info_ShowActionEvent;
	}

    float _Speed;
    Vector3 _Direction;
    void Info_ShowActionEvent(Regulus.Project.TurnBasedRPG.Serializable.MoveInfomation obj)
    {
        _Direction = UnityRegulus.Vector3(obj.MoveDirection);
        ActorAnimation.Play(obj.ActionStatue.ToString());
        Vector3 v = UnityRegulus.Vector3(obj.BeginPosition);
        v.y = Terrain.activeTerrain.SampleHeight(v);
        gameObject.transform.position = v;
        
        _Speed = obj.Speed;

        
        gameObject.transform.rotation = Quaternion.Euler(0, (obj.MoveDirectionAngle) % 360, 0);
        //gameObject.transform.Rotate(new Vector3(0, obj.MoveDirectionAngle % 360, 0), Space.World);
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (_Speed > 0)
        {
            _Update(UnityEngine.Time.deltaTime);
        }
	}

    void _Update(float deltaTime)
    {
        var offset = deltaTime * _Speed;
        var pos = new Vector3();
        pos.x = offset * _Direction.x + gameObject.transform.position.x;
        pos.z = offset * _Direction.z + gameObject.transform.position.z;

        pos.y = Terrain.activeTerrain.SampleHeight(gameObject.transform.position);
        gameObject.transform.position = pos;
    }

    public Regulus.Project.TurnBasedRPG.IObservedAbility Info { get; set; }
    public Map Map { get; set; }
}
