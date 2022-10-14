using UnityEngine;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using LitJson;
public class parseJSON
{
	public string clicked;
	public string installed;

}
public class PostURL : MonoBehaviour {
	string  mDeviceId = "0";
	public string _GameId = "0";

	[DllImport ("libcom.dumadugames.unityplugin.so")]
	private static extern string GetDeviceIdentifier();


	void Start () {
		
		mDeviceId = GetDeviceIdentifier();
		if(PlayerPrefs.GetInt("IdCallUpdate") == 0)
		StartCoroutine("GetValue");
	}

	IEnumerator GetValue()
	{

		string url = "https://tizenincentive.firebaseio.com/response/"+ mDeviceId +"/" + _GameId +".json";
		Debug.Log(url);
		WWW www = new WWW(url);
		yield return www;
		if (www.error == null)
		{
			Processjson(www.data);
		}
		else
		{

		}        
	}  
	private void Processjson(string jsonString)
	{

		if(jsonString.ToString() != "null")
		{
			JsonData jsonvale = JsonMapper.ToObject(jsonString);
			parseJSON parsejson;
			parsejson = new parseJSON();
		//	parsejson.clicked = jsonvale["clicked"].ToString();
			parsejson.installed = jsonvale["installed"].ToString();
			//Debug.Log(parsejson.clicked );
			Debug.Log(parsejson.installed );
			if(parsejson.installed == "False")
			{
				Debug.Log("Start Update");
				SendDeviceStatus();
			}
		}

	
	}
	void SendDeviceStatus()
	{

		string url = "https://tizenincentive.firebaseio.com/response/"+ mDeviceId +"/" + _GameId +".json?x-http-method-override=PATCH";
		Debug.Log(url); 
		byte[] bytePostData = Encoding.UTF8.GetBytes("{\"installed\":true}");
		WWW www = new WWW(url, bytePostData);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
	    yield return www;

		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
			PlayerPrefs.SetInt("IdCallUpdate",1);

		} 
		else 
		{
			Debug.Log("WWW Error: "+ www.error);
		}    
	
	}  

}