using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExportCollider : EditorWindow
{

	[UnityEditor.MenuItem("Regulus/ExportCollider")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(ExportCollider));		
	}

	UnityEngine.Object _Root;
	
	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		

		_Root = EditorGUILayout.ObjectField(new GUIContent("置放要匯出的物件"), _Root, typeof(UnityEngine.Object), new GUILayoutOption[] { });
		if (GUILayout.Button("Export"))
		{

			var path = EditorUtility.SaveFilePanel("輸出檔名" , "","01" , "map" );
			_Export(path , _Root as GameObject);
		}
		EditorGUILayout.EndVertical();
	}


	void _Export(string path,GameObject obj)
	{
		var bcs = obj.GetComponentsInChildren<BoxCollider>();
		System.Collections.Generic.List<Regulus.Utility.OBB> obbs = new System.Collections.Generic.List<Regulus.Utility.OBB>();
		foreach (var bc in bcs)
		{

            float x = bc.gameObject.transform.position.x;
			float y = bc.gameObject.transform.position.z;
            float w = bc.gameObject.transform.localScale.x * bc.size.x;
            float h = bc.gameObject.transform.localScale.z * bc.size.z;
			float r = bc.gameObject.transform.rotation.eulerAngles.y;

			Debug.Log("x" + x + " " + "y" + y + " " + "w" + w + " " + "h" + h + " " + "r" + r + " ");
			var obb = new Regulus.Utility.OBB(x,y,w,h);
			obb.setRotation(r);
			obbs.Add(obb);
		}
		Debug.Log("Expoty obb count : " + obbs.Count );
		Regulus.Utility.OBB.Write(path, obbs.ToArray());
        foreach( var obb in Regulus.Utility.OBB.Read(path))
        {
            Debug.Log("x" + obb.getX() + " " + "y" + obb.getY() + " " + "w" + obb.getWidth() + " " + "h" + obb.getHeight() + " " + "r" + obb.getRotation() + " ");
        }
	}
}
