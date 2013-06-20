using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	// Use this for initialization
    public Regulus.Project.TurnBasedRPG.IObservedAbility Info { get; set; }
    public string Id { get 
    {
        if (Info != null)
        {
            return Info.Id.ToString();
        }
        return null;
    } }
	void Start () 
    {
        gameObject.transform.position = new Vector3(Info.Position.X , 0 , Info.Position.Y);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        
	}
}
