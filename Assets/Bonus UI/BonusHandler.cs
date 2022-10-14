using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BonusHandler : MonoBehaviour {

	static BonusHandler instance = null;
	
	int[] _arrData;
	
	
	
	public delegate void ReceivedCallBack(int bonus);
	ReceivedCallBack receivedCallBack;
	
	// Use this for initialization
	void Start () 
	{
		
	
	}
	
	public static BonusHandler Instance
	{
		get
		{
			if(instance == null)
			{
				GameObject obj = new GameObject();
				obj.name = "BonusHandler";
				obj.AddComponent<BonusHandler>();
				instance = obj.GetComponent<BonusHandler>();
			}
			return instance;
		}
	}
	
	public bool StartBonus(GameObject prefabRef ,int[] _arr,GameObject wheel,GameObject wheelCamera,ReceivedCallBack onCallBack )
	{
		
		bool status = false;
		string storedDateTime = System.DateTime.Now.Year + "_"+ System.DateTime.Now.Month + "_"+System.DateTime.Now.Day + "_"+System.DateTime.Now.Hour
			+  "_" + System.DateTime.Now.Minute + "_"+System.DateTime.Now.Second;
		Debug.Log("StoredDateTime...."+storedDateTime);

		if(PlayerPrefs.GetString("StoredDateTime") == "")
		{
			PlayerPrefs.SetString("StoredDateTime",storedDateTime);
			status = true;
		}
		else
		{
			string[] array = PlayerPrefs.GetString("StoredDateTime").Split('_');
			if(System.DateTime.Now.Year != int.Parse(array[0]) || System.DateTime.Now.Month != int.Parse(array[1]) || System.DateTime.Now.Day != int.Parse(array[2]))
			{
				status = true;
			}
			else
			{
				status = false;
			}
		}
		
		
		if(status)
		{
			PlayerPrefs.SetString("StoredDateTime",storedDateTime);
			Instantiate(prefabRef);
			Instantiate(wheel);
			GameObject wCamera= Instantiate(wheelCamera)as GameObject;
			wCamera.camera.orthographicSize=Screen.height/200f;
			_arrData = new int[_arr.Length];
			randomizeArrayData(_arr).CopyTo(_arrData,0);
			receivedCallBack = null;
			receivedCallBack = onCallBack;
			//RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY");
		}
		return status;
	}
	
	public void SendCallBack( int selectedIndex)
	{
		if(receivedCallBack != null)
		{
			if(selectedIndex == -1)
			{
				receivedCallBack(0);
			}
			else
			{
				receivedCallBack(_arrData[selectedIndex]);
			}
		}
		receivedCallBack = null;
		Destroy(gameObject);
	}
	// get available bonus value based on index
	public int GetAvailableBonusValue(int index)
	{
		return _arrData[index];
	}
	// randomize array data
	public int[] randomizeArrayData(int[] _arr)
	{
		List<int> listArray = new List<int>();
		for(int index = 0;index < _arr.Length;index++)
		{
			listArray.Add(_arr[index]);
		}
		List<int> dummy = new List<int>();
		while(listArray.Count > 0)
		{
			int index = Random.Range(0,listArray.Count);
			dummy.Add(listArray[index]);
			listArray.RemoveAt(index);
		}
		return dummy.ToArray();
	}
}
