using UnityEngine;
using System.Collections;

public class characterRotate : MonoBehaviour {

	public GameObject frog;
	
	
	
	private Rect FpsRect ;
	private string frpString;
	
	private GameObject instanceObj;
	public GameObject[] gameObjArray=new GameObject[9];
	public AnimationClip[] AniList  = new AnimationClip[4];
	
	float minimum = 2.0f;
	float maximum = 50.0f;
	float touchNum = 0f;
	string touchDirection ="forward"; 
	private GameObject Villarger_A_Girl_prefab;
	
	// Use this for initialization
	void Start () {
		
		//frog.animation["dragon_03_ani01"].blendMode=AnimationBlendMode.Blend;
		//frog.animation["dragon_03_ani02"].blendMode=AnimationBlendMode.Blend;
		//Debug.Log(frog.GetComponent("dragon_03_ani01"));
		
		//Instantiate(gameObjArray[0], gameObjArray[0].transform.position, gameObjArray[0].transform.rotation);
	}
	
 void OnGUI() {
	  if (GUI.Button(new Rect(20, 20, 70, 40),"Idle")){
		 frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Idle");
	  }
	    if (GUI.Button(new Rect(90, 20, 70, 40),"Greeting")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Greeting");
	  }
		   if (GUI.Button(new Rect(160, 20, 70, 40),"Bow")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Bow");
	  }
	     if (GUI.Button(new Rect(230, 20, 70, 40),"Talk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Talk");
	  } 
		if (GUI.Button(new Rect(300, 20, 70, 40),"Walk")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Walk");
	  }
		if (GUI.Button(new Rect(370, 20, 70, 40),"Run")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Run");
	  }
			if (GUI.Button(new Rect(440, 20, 70, 40),"Happy")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Happy");
	  }
			if (GUI.Button(new Rect(510, 20, 70, 40),"Sad")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Sad");
	  }
			if (GUI.Button(new Rect(580, 20, 140, 40),"GangnamStyle")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("GangnamStyle");
	  }  
				if (GUI.Button(new Rect(600, 480, 140, 40),"Ver 1.5")){
		  frog.animation.wrapMode= WrapMode.Loop;
		  	frog.animation.CrossFade("Idle");
	  }
		
		
 }
	
	// Update is called once per frame
	void Update () {
		
		//if(Input.GetMouseButtonDown(0)){
		
			//touchNum++;
			//touchDirection="forward";
		 // transform.position = new Vector3(0, 0,Mathf.Lerp(minimum, maximum, Time.time));
			//Debug.Log("touchNum=="+touchNum);
		//}
		/*
		if(touchDirection=="forward"){
			if(Input.touchCount>){
				touchDirection="back";
			}
		}
	*/
		 
		//transform.position = Vector3(Mathf.Lerp(minimum, maximum, Time.time), 0, 0);
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		//frog.transform.Rotate(Vector3.up * Time.deltaTime*30);
	}
	
}
