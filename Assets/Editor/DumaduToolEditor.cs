using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class DumaduToolEditor
{
	[MenuItem("DumaduGames/ClearSavedData")]
	static void ShowWindow()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("All Player Prefs Data Deleted");
	}
}
