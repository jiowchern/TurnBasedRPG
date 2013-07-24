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
            var map = _Map as MapInfomation;
            var path = EditorUtility.SaveFilePanel("輸出檔名", "", map.Name, "map");
            _Export(map , path);
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
        if (kind == EntityBuilder.Kind.Portal)
        {
            Regulus.Project.TurnBasedRPG.Data.PortalEntity entity = new Regulus.Project.TurnBasedRPG.Data.PortalEntity();
            return _Build(entity , game_object);
        }
        throw new System.Exception("沒有對應的Entity Builder " + kind);		
	}

	private Regulus.Project.TurnBasedRPG.Data.Entity _Build(Regulus.Project.TurnBasedRPG.Data.Entity entity, GameObject game_object)
	{
		entity.Id = System.Guid.NewGuid();
		return entity;
	}
    
	private Regulus.Project.TurnBasedRPG.Data.Entity _Build(Regulus.Project.TurnBasedRPG.Data.StaticEntity entity, GameObject game_object)
	{
		var bc = game_object.GetComponent<BoxCollider>();

        if (bc != null)
        {
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
        throw new System.Exception("BoxCollider is null " + game_object.name);
		
	}

    private Regulus.Project.TurnBasedRPG.Data.Entity _Build(Regulus.Project.TurnBasedRPG.Data.PortalEntity entity, GameObject game_object)
    {
        
        var pe = game_object.GetComponent<global::ProtalEntity>();
        var bc = game_object.GetComponent<BoxCollider>();
        
        entity.TargetMap = pe.TargetMap;
        entity.TargetPosition.X = pe.TargetPosition.x;
        entity.TargetPosition.Y = pe.TargetPosition.y;

        float x = game_object.transform.position.x;

        float y = game_object.transform.position.z;

        float w = game_object.transform.localScale.x * bc.size.x;

        float h = game_object.transform.localScale.z * bc.size.z;
        entity.Vision.Left = x - w/2;
        entity.Vision.Top = y - h/2;
        entity.Vision.Right = entity.Vision.Left + w;
        entity.Vision.Bottom = entity.Vision.Top + h;                        
        
        return _Build(entity as Regulus.Project.TurnBasedRPG.Data.Entity, game_object);
    }

	

	
}
