using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GUIButton))]
public class GUIButtonEditor : Editor 
{
	GUIButton mgUIButton;
	// Use this for initialization
	void OnEnable()
	{
		mgUIButton=(GUIButton)target;
	}
	
	public override  void OnInspectorGUI()
	{
		EditorGUILayout.BeginHorizontal();
		mgUIButton._UsingSpriteSheet=EditorGUILayout.Toggle("Using SpriteSheet",mgUIButton._UsingSpriteSheet);
		EditorGUILayout.EndHorizontal();
		if(mgUIButton._UsingSpriteSheet)
		{
			mgUIButton._NormalStateMesh=EditorGUILayout.ObjectField("Normal State Mesh",mgUIButton._NormalStateMesh,typeof(Mesh),false) as Mesh;
			mgUIButton._SelectedStateMesh=EditorGUILayout.ObjectField("Selected State Mesh",mgUIButton._SelectedStateMesh,typeof(Mesh),false) as Mesh;
			mgUIButton._HoverStateMesh=EditorGUILayout.ObjectField("Hover State Mesh",mgUIButton._HoverStateMesh,typeof(Mesh),false) as Mesh;
			mgUIButton._DisableStateMesh=EditorGUILayout.ObjectField("Disable State Mesh",mgUIButton._DisableStateMesh,typeof(Mesh),false) as Mesh;
		}
		else
		{
			mgUIButton._NormalStateTex=EditorGUILayout.ObjectField("Normal State Texture",mgUIButton._NormalStateTex,typeof(Texture2D),false) as Texture2D;
			mgUIButton._SelectedStateTex=EditorGUILayout.ObjectField("Selected State Texture",mgUIButton._SelectedStateTex,typeof(Texture2D),false) as Texture2D;
			mgUIButton._HoverStateTex=EditorGUILayout.ObjectField("Hover State Texture",mgUIButton._HoverStateTex,typeof(Texture2D),false) as Texture2D;
			mgUIButton._DisableStateTex=EditorGUILayout.ObjectField("Disable State Texture",mgUIButton._DisableStateTex,typeof(Texture2D),false) as Texture2D;
		}
	}
	
}
