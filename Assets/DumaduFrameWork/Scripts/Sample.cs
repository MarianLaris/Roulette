using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.InteropServices;

public class Sample : MonoBehaviour {

	private GUIStyle guiStyle = new GUIStyle(); 
//	[DllImport ("libcom.dumadugames.sharedlibrary.so")]
//	private static extern float FooPluginFunction();

	[DllImport ("libcom.dumadugames.unityplugin.so")]
	private static extern string GetDeviceIdentifier();

//	[DllImport ("libcom.dumadugames.sharedlibrary.so")]
//	private static extern double IdentifierFunction();

	void Start () {

		Debug.Log ("Ramappa");
	}

	void OnGUI() {
		
		guiStyle.fontSize = 20; //change the font size
		GUI.Label(new Rect(100, 100, 300, 20), "IMEI Number " + GetDeviceIdentifier () , guiStyle);
	}

	// Update is called once per frame
	void Update () {
	}
}
