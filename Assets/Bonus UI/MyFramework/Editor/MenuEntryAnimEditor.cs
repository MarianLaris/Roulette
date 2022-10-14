using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MenuItemEntryAnimation))]
public class MenuEntryAnimEditor : Editor 
{
	MenuItemEntryAnimation mmenuItemEntryAnimation;
	void OnEnable ()
	{
		mmenuItemEntryAnimation=(MenuItemEntryAnimation)target;
	}
	
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical();
			EditorGUILayout.PrefixLabel("Start Condition");
				 EditorGUI.indentLevel=2;
				mmenuItemEntryAnimation._StartPosition=EditorGUILayout.Vector3Field("Position",mmenuItemEntryAnimation._StartPosition);
				mmenuItemEntryAnimation._StartRotation=EditorGUILayout.Vector3Field("Rotation",mmenuItemEntryAnimation._StartRotation);
				mmenuItemEntryAnimation._StartScale=EditorGUILayout.Vector3Field("Scale",mmenuItemEntryAnimation._StartScale);
		EditorGUILayout.EndVertical();
		
		EditorGUI.indentLevel=0;
		EditorGUILayout.BeginVertical();
			EditorGUILayout.PrefixLabel("End Condition");
				EditorGUI.indentLevel=1;
				mmenuItemEntryAnimation._EndPosition=EditorGUILayout.Vector3Field("Position",mmenuItemEntryAnimation._EndPosition);
				mmenuItemEntryAnimation._EndRotation=EditorGUILayout.Vector3Field("Rotation",mmenuItemEntryAnimation._EndRotation);
				mmenuItemEntryAnimation._EndScale=EditorGUILayout.Vector3Field("Scale",mmenuItemEntryAnimation._EndScale);
		EditorGUILayout.EndVertical();
		
		EditorGUI.indentLevel=0;
			mmenuItemEntryAnimation._SequenceAnimation=EditorGUILayout.Toggle("Sequence",mmenuItemEntryAnimation._SequenceAnimation);
			GUI.enabled=mmenuItemEntryAnimation._SequenceAnimation;
		EditorGUI.indentLevel=1;
			mmenuItemEntryAnimation._SequenceCombination=(eSequenceCombination)EditorGUILayout.EnumPopup("Combination",mmenuItemEntryAnimation._SequenceCombination);
			SequenceOption(mmenuItemEntryAnimation._SequenceCombination);
		GUI.enabled=true;
		EditorGUI.indentLevel=0;
			mmenuItemEntryAnimation._SpawnAnimation=EditorGUILayout.Toggle("Spawn",mmenuItemEntryAnimation._SpawnAnimation);
			GUI.enabled=mmenuItemEntryAnimation._SpawnAnimation;
		EditorGUI.indentLevel=1;
			mmenuItemEntryAnimation._SpawnCombination=(eSpawnCombination)EditorGUILayout.EnumPopup("Combnation",mmenuItemEntryAnimation._SpawnCombination);
			ShowAnimationOption(mmenuItemEntryAnimation._Spawn);	
	}
	
	void SequenceOption(eSequenceCombination comb)
	{
		switch(comb)
		{
			case eSequenceCombination.T:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
			break;
			case eSequenceCombination.R:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
			break;
			case eSequenceCombination.S:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
			break;
			case eSequenceCombination.TR:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
			break;
			case eSequenceCombination.TS:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
			break;
			case eSequenceCombination.RS:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
			break;
			case eSequenceCombination.RT:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
			break;
			case eSequenceCombination.ST:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
			break;
			case eSequenceCombination.SR:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
			break;
			case eSequenceCombination.TRS:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
			break;
			case eSequenceCombination.TSR:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
			break;
			case eSequenceCombination.RTS:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
			break;
			case eSequenceCombination.RST:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
			break;
			case eSequenceCombination.STR:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
			break;
			case eSequenceCombination.SRT:
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Scale");
				ShowAnimationOption(mmenuItemEntryAnimation._Scale);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Rotation");
				ShowAnimationOption(mmenuItemEntryAnimation._Rotate);
				EditorGUI.indentLevel=1;
				EditorGUILayout.PrefixLabel("Translation");
				ShowAnimationOption(mmenuItemEntryAnimation._Trans);
			break;
		}
	}
	
	void ShowAnimationOption(AnimationParam Obj)
	{	
		EditorGUILayout.BeginHorizontal();
			EditorGUI.indentLevel=2;
			EditorGUILayout.PrefixLabel("Duration");
			Obj._Duration=EditorGUILayout.FloatField(Obj._Duration,GUILayout.Width(50));
			EditorGUILayout.PrefixLabel("Delay");
			Obj._Delay=EditorGUILayout.FloatField(Obj._Delay,GUILayout.Width(50));					
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("RepeatCount");
			Obj._RepeatCount=EditorGUILayout.IntField(Obj._RepeatCount,GUILayout.Width(50));
			Obj._LoopType=(eLoopType)EditorGUILayout.EnumPopup("Loop Type",Obj._LoopType);
			if(Obj._LoopType==eLoopType.PingPong)
				Obj._PingPog=true;
			else
				Obj._PingPog=false;
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
			Obj._easeType=(_EaseType)EditorGUILayout.EnumPopup("Easy Type",Obj._easeType);
		EditorGUILayout.EndHorizontal();
	}
}

