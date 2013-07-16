using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExportZone : EditorWindow
{
	private string _MapName = "";
	private Object _Map;

	[UnityEditor.MenuItem("Regulus/ExportZone")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(ExportZone));
	}
	
	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		_Map = EditorGUILayout.ObjectField(new GUIContent("地圖物件"), _Map, typeof(MapInfomation), true);
		if(GUILayout.Button("匯出"))
		{
			_Export(_Map as GameObject);
		}
		EditorGUILayout.EndVertical();
	}

	private void _Export(GameObject gameObject)
	{
		var map = gameObject.GetComponent<MapInfomation>();
		if(map != null)
		{
			
		}
		else
		{
			
		}
	}
}
