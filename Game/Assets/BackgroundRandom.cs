using UnityEngine;
using System.Collections;

public class BackgroundRandom : MonoBehaviour {

	// Use this for initialization
    public GUITexture Target;
    public Texture[] Textures;
	void Start () 
    {
        if (Textures.Length > 0)
            Target.texture = Textures[Random.Range(0, Textures.Length )];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
