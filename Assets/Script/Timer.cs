using UnityEngine;
using System.Collections;
//using System;
public class Timer : MonoBehaviour {
//	
//	System.DateTime mtime;
//	System.DateTime mtimelater;
//	System.TimeSpan mdifference;
//	//Double isecond;
//	 public GUIText _Timetext;
//	 public string _TimeFormate;
//	 public bool _BonusButtonActive;
//	
//	// Use this for initialization
//	void Awake() 
//	{
//        RT_DataHandler.SetOneTimeInitialFund();
//		mtime=RT_DataHandler.GetLastCollectionTime();
//		mtimelater=mtime.Add(new TimeSpan(0,0,5,0));
//		//mdifference=mtimelater-mtime;	
//    }
//	// Update is called once per frame
//	void Update () 
//	{
//		double isTimeInSecond=(mtimelater-DateTime.Now).TotalSeconds;
//		System.TimeSpan t = System.TimeSpan.FromSeconds(isTimeInSecond);  
//		string timerFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
//		if(isTimeInSecond<=0)
//		{
//			_BonusButtonActive=true;
//			_Timetext.text="00:00:00";
//		}
//		else
//		_Timetext.text=timerFormatted;
//	 	
//	}
}