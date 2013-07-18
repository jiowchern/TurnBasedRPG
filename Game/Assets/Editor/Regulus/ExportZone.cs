using UnityEngine;
using UnityEditor;
using System.Collections;

public class ExportZone : EditorWindow
{
	
	private Object _Map;

	[UnityEditor.MenuItem("Regulus/ExportZone")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(ExportZone));
	}
	
	void OnGUI()
	{
		EditorGUILayout.BeginVertical();
		_Map = EditorGUILayout.ObjectField(new GUIContent("�a�Ϫ���"), _Map, typeof(MapInfomation), true);
		if(GUILayout.Button("�ץX"))
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
