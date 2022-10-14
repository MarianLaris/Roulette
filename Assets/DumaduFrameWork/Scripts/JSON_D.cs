//using UnityEngine;
//using LitJson;
//using System;
//using System.Collections;
//
//public class ParseJSON
//{
//	public string _GameAds;
//
//
//}
//public class JSON_D : MonoBehaviour
//{
//	// Sample JSON for the following script has attached.
//	IEnumerator Start()
//	{
//		string url = "https://admanager-e0c07.firebaseio.com/iOS/com,anupama,arrowmoves.json";
//		WWW www = new WWW(url);
//		yield return www;
//		if (www.error == null)
//		{
//			Processjson(www.data);
//			Debug.Log("LoadLevel");
//		}
//		else
//		{
//			Debug.Log("NULL");
//		}        
//	}    
//	private void Processjson(string jsonString)
//	{
//		Debug.Log(jsonString.ToString());
//		if(jsonString.ToString() != "null")
//		{
//			JsonData jsonvale = JsonMapper.ToObject(jsonString);
//			ParseJSON parsejson;
//			parsejson = new ParseJSON();
//			parsejson._GameAds = jsonvale["ads"].ToString();
//			Debug.Log(parsejson._GameAds);
//
//			if(parsejson._GameAds == "True")
//			{
//				Debug.Log("Free Game");
//			}
//			else if(parsejson._GameAds == "False")
//			{
//				Debug.Log("Paid Game");
//			}
//		}
//	
//  
//	}
//}