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
    float _ChangeTime;
	void Update () 
    {
        _ChangeTime += Time.deltaTime;

        if (_ChangeTime > 5)
        {
            if (Textures.Length > 0)
                Target.texture = Textures[Random.Range(0, Textures.Length)];
            _ChangeTime = 0;
        }
        float textureHeight = Target.texture.height;
        float textureWidth = Target.texture.width;

        if (textureWidth < textureHeight)
        {
            float textureAspectRatio = textureWidth / textureHeight;

            Target.gameObject.transform.localScale = new Vector2(textureAspectRatio, 1);
        }
        else
        {
            float textureAspectRatio = textureHeight / textureWidth;

            Target.gameObject.transform.localScale = new Vector2(1, textureAspectRatio);
        }
        
	}
}
