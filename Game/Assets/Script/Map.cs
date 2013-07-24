using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour 
{
    
	// Use this for initialization
	void Start () 
    {
        var camera = UnityEngine.GameObject.FindWithTag("MainCamera");
        SmoothFollow smoothFollow = camera.GetComponent<SmoothFollow>();
        smoothFollow.target = CameraTarget;

        Debug.Log("Reset Camera");
	}

	
	// Update is called once per frame
	void Update () 
    {
     
	}

    public Transform CameraTarget { get; set; }
}
