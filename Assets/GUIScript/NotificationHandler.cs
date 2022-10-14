using UnityEngine;
using System.Collections;

public class NotificationHandler : MonoBehaviour {
	public int _NotificationCount;
	public static NotificationHandler Instance;
	// Use this for initialization
	void Start () {
		Instance = this;
		_NotificationCount = PlayerPrefs.GetInt("NotificationValue");
	}
	
	public int UpdateNotificationCount(int pValue,bool pStates)
	{
		switch(pStates)
		{
		    case true:
			_NotificationCount += pValue;
			break;
		    case false:
			_NotificationCount = 0;
			break;

		}
		 PlayerPrefs.SetInt("NotificationValue",_NotificationCount);
		 return PlayerPrefs.GetInt("NotificationValue");
	}
}
