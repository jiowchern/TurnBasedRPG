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
		_Map = EditorGUILayout.ObjectField(new GUIContent("地圖物件"), _Map, typeof(MapInfomation), true);
		if(GUILayout.Button("匯出") && _Map != null)
		{
			var path = EditorUtility.SaveFilePanel("輸出檔名", "", "01", "map");
			_Export(_Map as MapInfomation, path);
		}
		EditorGUILayout.EndVertical();
	}
	delegate Regulus.Project.TurnBasedRPG.Data.Entity Builder(GameObject obj);
	private void _Export(MapInfomation root, string path)
	{
		
		var mapInfo = root;
		if(mapInfo != null)
		{			
			var entityBuilders = root.GetComponentsInChildren<EntityBuilder>();
			System.Collections.Generic.List<Regulus.Project.TurnBasedRPG.Data.Entity> entitys = new System.Collections.Generic.List<Regulus.Project.TurnBasedRPG.Data.Entity>();
			foreach (var entityBuilder in entityBuilders)
			{
				
				entitys.Add( _Build( entityBuilder.EntityKind, entityBuilder.gameObject ) );
			}

			Regulus.Project.TurnBasedRPG.Data.Map mapData = new Regulus.Project.TurnBasedRPG.Data.Map();	
			mapData.Name = mapInfo.Name;
			mapData.Entitys = entitys.ToArray();

			Regulus.Utility.IO.Serialization.Write<Regulus.Project.TurnBasedRPG.Data.Map>(mapData , path);
		}
		else
		{
			
		}
	}

	private Regulus.Project.TurnBasedRPG.Data.Entity _Build(EntityBuilder.Kind kind, GameObject game_object)
	{
		if (kind == EntityBuilder.Kind.Static)
		{
			Regulus.Project.TurnBasedRPG.Data.StaticEntity entity = new Regulus.Project.TurnBasedRPG.Data.StaticEntity();
			return _Build(entity , game_object);
		}
		return null;
	}

	private Regulus.Project.TurnBasedRPG.Data.Entity _Build(Regulus.Project.TurnBasedRPG.Data.Entity entity, GameObject game_object)
	{
		entity.Id = System.Guid.NewGuid();
		return entity;
	}
	private Regulus.Project.TurnBasedRPG.Data.Entity _Build(Regulus.Project.TurnBasedRPG.Data.StaticEntity entity, GameObject game_object)
	{
		var bc = game_object.GetComponent<BoxCollider>();
		
		float x = game_object.transform.position.x;

		float y = game_object.transform.position.z;

		float w = game_object.transform.localScale.x * bc.size.x;

		float h = game_object.transform.localScale.z * bc.size.z;

		float r = game_object.transform.rotation.eulerAngles.y;

		Debug.Log("x" + x + " " + "y" + y + " " + "w" + w + " " + "h" + h + " " + "r" + r + " ");
		var obb = new Regulus.Utility.OBB(x, y, w, h);
		obb.setRotation(r);
		
		entity.Obb = obb;
		return _Build(entity as Regulus.Project.TurnBasedRPG.Data.Entity, game_object);
	}
	


	

	
}
