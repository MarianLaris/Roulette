using UnityEngine;
using System.Collections;
using FBKit;
//using System;

//using System.Collections.Generic;
//using System.Reflection;
public class RT_DataHandler 
{
// 	public static  float _SoundValue=0.5f;
//	public static  float _MusicValue=0.5f;
	public static  float _SpeedValue=0.0f;	
	
	//public static int _SceneSelectedValue;
	
	public static int _LoadSceneValue;
	
   public static int _StageNumber;
   public static RaycastHit _hit;
   public static int _TotalbettingAmount;	
   public static int _TotalWinningAmount;
   public static int _TotalRebettingAmount;	
   public static int _GrandtotalWinningAmount;	
   public static int _WinnObjectAmount;	
   public static int _TotalfundYouHave=GetTotalFund();
   public static int _WinningNo=2222;	
   public static int _BettedChipCount;
   public static bool _TouchDisable;
   public static bool _rebettingCanBeDone;	
	
   public static bool _SpinButtonDisabledStatus=true;
   public static bool _RebetButtonDisabledStatus=true;
   public static bool _ClearButtonDisabledStatus=true;
  // public static bool _ChipAnimation;	
 
   public static GameObject _TotalWinningAmountText;
   public static GameObject _TotalbettingAmountText;
   public static GameObject _TotalfundYouHaveText;
   public static bool _bUpdateTableView=false;

	public static  float GetSoundValue()
	{
		return(PlayerPrefs.GetFloat("SoundValue"));
	}
	public static  void SetSoundValue(float value)
	{
		PlayerPrefs.SetFloat("SoundValue",value);
	}

	public static  float GetMusicValue()
	{
		return(PlayerPrefs.GetFloat("MusicValue"));
	}
	public static  void SetMusicValue(float value)
	{
		PlayerPrefs.SetFloat("MusicValue",value);
	}

	public static void SetTotalFund(int isFund)
	{
		PlayerPrefs.SetInt("TotalFund",isFund);
	}
	
	public static int GetTotalFund()
	{
		return(PlayerPrefs.GetInt("TotalFund"));
	}
	
	public static void SetVideoAdTime(string n)
	{
		PlayerPrefs.SetString("VideoAdsTime", n);
	}
	
	public static string GetVideoAdTime()
	{
		return PlayerPrefs.GetString("VideoAdsTime");
	}
	public static void  SetScore(int isScore)
	{
		PlayerPrefs.SetInt("HighScore",isScore);
	}
	
	public static int GetScore()
	{
		return(PlayerPrefs.GetInt("HighScore"));
	}
	
	public static void  SetPurchaseStatus(int Status)
	{
		PlayerPrefs.SetInt("Status",Status);
	}
	
	public static int GetRateIt()
	{
		return(PlayerPrefs.GetInt("RateIt"));
	}
	
	public static void  SetRateIt(int Status)
	{
		PlayerPrefs.SetInt("RateIt",Status);
	}
	
	public static int GetPurchaseStatus()
	{
		return(PlayerPrefs.GetInt("Status"));
	}
	
	public static void SetOneTimeInitialFund()
	{
		if(PlayerPrefs.GetString("FirstLaunch")=="")
		{
			//Debug.Log("fund >>>>>>");
			PlayerPrefs.SetString("FirstLaunch","false");
			if(ExternalInterfaceHandler.Instance.BuildType == eEXTERNAL_Build_Type.Premium)
				SetTotalFund(10000);
			else
				SetTotalFund(500);

			SetLastCollectionTime(System.DateTime.Now);
			ExternalInterfaceHandler.Instance.SendNotification(1,43200,"Bonus is waiting for you !!","Get back and claim!!",new Color32(0xff, 0x44, 0x44, 255),true,false);
			
			SetMusicValue(0.5f);
			SetSoundValue(0.5f);
		
			//createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
			//Debug.Log(GetTotalFund());
		}
		if(PlayerPrefs.GetString("NFL")=="")
		{
			
			PlayerPrefs.SetString("NFL","false");
			string storedDateTime = System.DateTime.Now.Year + "_"+ System.DateTime.Now.Month + "_"+System.DateTime.Now.Day + "_"+System.DateTime.Now.Hour
			+  "_" + System.DateTime.Now.Minute + "_"+System.DateTime.Now.Second;
		//if(PlayerPrefs.GetString("StoredDateTime") == "")
		//{
		//	PlayerPrefs.SetString("StoredDateTime",storedDateTime);
			
		//}
			createHourlyBonusNotification("50 CHIPS ARE READY FOR YOU TO COLLECT!! COME BACK!!",12,0,0);
			//System.TimeSpan timeRemainToFinishADay=RemainingTimeToCompleteDay(System.DateTime.Now);
		}
		
		
		
	}
	
	public static void  SetNotifcation(int notification)
	{
		PlayerPrefs.SetInt("notification",notification);
	}
	
	public static int GetNotifcation()
	{
		return(PlayerPrefs.GetInt("notification"));
	}
	public static void SetLastCollectionTime(System.DateTime isTime)
	{
		PlayerPrefs.SetString("LastCollectionTime",""+isTime);
	}
	
	public static System.DateTime GetLastCollectionTime()
	{
		return System.DateTime.Parse(PlayerPrefs.GetString("LastCollectionTime"));
	}
	
	
	public static System.TimeSpan RemainingTimeToCompleteDay( System.DateTime date)
    {
		System.TimeSpan t=System.TimeSpan.FromSeconds( (new System.DateTime(date.Year, date.Month, date.Day+1)-System.DateTime.Now).TotalSeconds);
		Debug.Log("t...."+t);
		return t;
    }
	public static void createHourlyBonusNotification(string str,float hours,float min,float sec)
	{
//		Debug.Log("createHourlyBonusNotification is called......");
//		NotificationServices.CancelAllLocalNotifications();
//		LocalNotification localNotification1 = new LocalNotification();
//		localNotification1.applicationIconBadgeNumber = 1; // No to display over the icon, can also be set to a static no
//		localNotification1.alertBody = str;	
//		System.DateTime fireDate = System.DateTime.Now; // new System.DateTime(storedYear, storedMonth, storedDay+1);
//		fireDate = fireDate.AddHours(hours);
//		fireDate = fireDate.AddMinutes(min);
//		fireDate = fireDate.AddSeconds(sec);
//		//Debug.Log("OriginalData : "+(System.DateTime.Now)+" FireDate : "+fireDate);
//		localNotification1.fireDate = fireDate;
//		localNotification1.alertAction = "Show me";
//		localNotification1.repeatInterval = CalendarUnit.Day;
//		localNotification1.soundName = LocalNotification.defaultSoundName;
//		NotificationServices.ScheduleLocalNotification(localNotification1);
	}
	
//	public static void createDailyBonusNotification(string str)
//	{
//	//	Debug.Log("firsy line");
//		LocalNotification localNotification1 = new LocalNotification();
//		localNotification1.applicationIconBadgeNumber = NotificationServices.localNotificationCount+1; // No to display over the icon, can also be set to a static no
//		localNotification1.alertBody = str;			
//		
//		string[] array = PlayerPrefs.GetString("StoredDateTime").Split('_');
//		int storedeYear = int.Parse(array[0]);
//		int storedMonth = int.Parse(array[1]);
//		int storedDay = int.Parse(array[2]);
//		
//		System.DateTime fireDate = new System.DateTime(storedeYear, storedMonth, storedDay+1);
//		fireDate = fireDate.AddHours(Random.Range(8, 20));
//		fireDate = fireDate.AddMinutes(Random.Range(0, 61));
//		fireDate = fireDate.AddSeconds(Random.Range(0, 61));
//		//Debug.Log("OriginalData : "+(System.DateTime.Now)+" FireDate : "+fireDate);
//		localNotification1.fireDate = fireDate;
//		localNotification1.alertAction = "Show me";
//		localNotification1.repeatInterval = CalendarUnit.Day;
//		localNotification1.soundName = LocalNotification.defaultSoundName;
//		NotificationServices.ScheduleLocalNotification(localNotification1);
//		//Debug.Log("coming last line daily");
//	}
	
	/*   --------------------------for achivements-------------------- */
	
	public static void SetEvenBetWin(int Count)
	{
		PlayerPrefs.SetInt("EvenBetWin",Count);
	}
	
	public static int GetEvenBetWin()
	{
		return PlayerPrefs.GetInt("EvenBetWin");
	}
	
	public static void SetColorBetWin(int Count)
	{
		PlayerPrefs.SetInt("ColorBetWin",Count);
	}
	
	public static int GetColorBetWin()
	{
		return PlayerPrefs.GetInt("ColorBetWin");
	}
	
	public static void SetOutSideBetWin(int Count)
	{
		PlayerPrefs.SetInt("OutSideBetWin",Count);
	}
	
	public static int GetOutSideBetWin()
	{
		return PlayerPrefs.GetInt("OutSideBetWin");
	}
	
//	public static void SetRedColorBetCount(int isCount)
//	{
//		PlayerPrefs.SetInt("RedColorBetCount",isCount);
//	}
//	
//	public static void SetBlackColorBetCount(int isCount)
//	{
//		PlayerPrefs.SetInt("BlackColorBetCount",isCount);
//	}
//	
//	public static int GetRedColorBetCount()
//	{
//		return (PlayerPrefs.GetInt("RedColorBetCount"));
//	}
//	
//	public static int GetBlackColorBetCount(int isCount)
//	{
//		return (PlayerPrefs.GetInt("BlackColorBetCount"));
//	}
	/*------------- American Table Bet Types ---------*/
	
	
//	/*-----------------------------*/
//	
//	/*-------  RaceTrack Bet --------*/
//	
//	public static int _VoisinsBetCount;
//	public static int _TiersBetCount;
//	public static int _OrphelinesBetCount;
//	public static int _JeuBetCount;
//	public static int _NNBetCount;
	
	//public static ArrayList _WinningBets=new ArrayList();
	public static ArrayList _StraightBets=new ArrayList();
	public static ArrayList _CornerBets=new ArrayList();
	public static ArrayList _HSplitBets=new ArrayList();
	public static ArrayList _VSplitBets=new ArrayList();
	public static ArrayList _StreetBets=new ArrayList();
	public static ArrayList _LineBets=new ArrayList();
	public static ArrayList _FFBets=new ArrayList();
	public static ArrayList _BasketBets=new ArrayList();
	public static ArrayList _ColumnBets=new ArrayList();
	public static ArrayList _DozenBets=new ArrayList();
	public static ArrayList _OddBets=new ArrayList();
	public static ArrayList _EvenBets=new ArrayList();
	public static ArrayList _BlackBets=new ArrayList();
	public static ArrayList _RedBets=new ArrayList();
	public static ArrayList _HighBets=new ArrayList();
	public static ArrayList _Lowbets=new ArrayList();
	
	public static ArrayList _JeuBets=new ArrayList();
	public static ArrayList _VoisinsBets=new ArrayList();
	public static ArrayList _OrphelinsBets=new ArrayList();
	public static ArrayList _TiersBets=new ArrayList();
	public static ArrayList _NNbets=new ArrayList();
		
	public static void WinLoss(int isWinningNo)
	{
			_GrandtotalWinningAmount+=_TotalWinningAmount;
			
			SetTotalFund(_TotalfundYouHave+_TotalWinningAmount+_WinnObjectAmount);
			OnWinOrLoss();
	
		if(_TotalWinningAmount > GetScore())
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Score,""+_TotalWinningAmount,null);
			SetScore(_TotalWinningAmount);
		}
	}
	public static void StraightBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		int isvalue;
		int.TryParse(isName, out isvalue);
		if(isWinningNo==isvalue)
		{
//			WinningBets("StraightBet");
		     RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			//Debug.Log(_TotalWinningAmount);
			_TotalWinningAmount=isObjData._bettingAmountOnThis*35+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
		    foreach(GameObject chip in isObjData._ChipArray)
			chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
	}
	public static void CornerBetChecker(GameObject isObj,int isWinningNo)
	{
		
		string isName=isObj.name;
		isName=isName.Remove(0,1);
		int isvalue;
		int.TryParse(isName, out isvalue);
		if(isvalue%2==0)
		{
			int isdiv=(isvalue/2)-1;
			if(isWinningNo==(isvalue+isdiv)||isWinningNo==(isvalue+isdiv+1)||isWinningNo==(isvalue+isdiv+3)||isWinningNo==+(isvalue+isdiv+4))
			{
				//WinningBets("CornerBet");
				 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
				_TotalWinningAmount=isObjData._bettingAmountOnThis*8+_TotalWinningAmount;
				_WinnObjectAmount+=isObjData._bettingAmountOnThis;
				foreach(GameObject chip in isObjData._ChipArray)
				chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
		}
		else
		{
			int isdiv=isvalue/2;
			if(isWinningNo==(isvalue+isdiv)||isWinningNo==(isvalue+isdiv+1)||isWinningNo==(isvalue+isdiv+3)||isWinningNo==+(isvalue+isdiv+4))
			{
				//WinningBets("CornerBet");
				 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
				_TotalWinningAmount=isObjData._bettingAmountOnThis*8+_TotalWinningAmount;
				_WinnObjectAmount+=isObjData._bettingAmountOnThis;
				foreach(GameObject chip in isObjData._ChipArray)
				chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
		}
	}
	public static void HSplitBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		isName=isName.Remove(0,2);
		int isvalue;
		int.TryParse(isName, out isvalue);
		if(isName=="0")
		{
			if(isWinningNo==0||isWinningNo==-1)
			{
					// WinningBets("HSplitBet");
					 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
					_TotalWinningAmount=isObjData._bettingAmountOnThis*17+_TotalWinningAmount;
					_WinnObjectAmount+=isObjData._bettingAmountOnThis;
					 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
		}
		else
		{
			if(isvalue%2==0)
			{
				int isdiv=(isvalue/2)-1;
				
					if(isWinningNo==(isvalue+isdiv)||isWinningNo==(isvalue+isdiv+1))
					{
					   //  WinningBets("HSplitBet");
						 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
						_TotalWinningAmount=isObjData._bettingAmountOnThis*17+_TotalWinningAmount;
						_WinnObjectAmount+=isObjData._bettingAmountOnThis;
					     foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
					}
			}
			else
			{
				int isdiv=isvalue/2;
				
					if(isWinningNo==(isvalue+isdiv)||isWinningNo==(isvalue+isdiv+1))
					{	// WinningBets("HSplitBet");
						 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
						_TotalWinningAmount=isObjData._bettingAmountOnThis*17+_TotalWinningAmount;
						_WinnObjectAmount+=isObjData._bettingAmountOnThis;
						 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
					}
				
			}
		}
	}
	public static void VSplitBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		int s1=0;
		int s2=0;
		isName=isName.Remove(0,2);
		int isvalue;
		int.TryParse(isName, out isvalue);
		if(isName=="01"||isName=="03"||isName=="02")
		{
			if(isName=="01")
			{
				s1=0;s2=1;
			}
			else if(isName=="02")
			{
				s1=0;s2=2;
			}
			else if(isName=="03")
			{
				s1=0;s2=3;
			}
			if(isWinningNo==s1||isWinningNo==s2)
			{
				    //  WinningBets("VSplitBet");
					 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
					_TotalWinningAmount=isObjData._bettingAmountOnThis*17+_TotalWinningAmount;
					_WinnObjectAmount+=isObjData._bettingAmountOnThis;
					 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
				
		}	
		else
		{
			if(isWinningNo==(isvalue)||isWinningNo==(isvalue+3))
			{
				/// WinningBets("VSplitBet");
				 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
				_TotalWinningAmount=isObjData._bettingAmountOnThis*17+_TotalWinningAmount;
				_WinnObjectAmount+=isObjData._bettingAmountOnThis;
					 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
		}
	}
	public static void StreetBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		isName=isName.Remove(0,2);
		int isvalue;
		int.TryParse(isName, out isvalue);
			int isdiv=(isvalue*3);
			if(isWinningNo==(isdiv)||isWinningNo==(isdiv-2)||isWinningNo==(isdiv-1))
			{
				 // WinningBets("StreetBet");
				 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
				_TotalWinningAmount=isObjData._bettingAmountOnThis*11+_TotalWinningAmount;
				_WinnObjectAmount+=isObjData._bettingAmountOnThis;
				 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
	}
	public static void LineBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		isName=isName.Remove(0,1);
		int isvalue;
		int.TryParse(isName, out isvalue);
			int isdiv=(isvalue*3);
			if(isWinningNo==(isdiv)||isWinningNo==(isdiv-2)||isWinningNo==(isdiv-1)||isWinningNo==(isdiv+1)||isWinningNo==(isdiv+2)||isWinningNo==(isdiv+3))
			{
				// WinningBets("LineBet");
				 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
				_TotalWinningAmount=isObjData._bettingAmountOnThis*5+_TotalWinningAmount;
				_WinnObjectAmount+=isObjData._bettingAmountOnThis;
				 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
	}
	public static void FFBetChecker(GameObject isObj,int isWinningNo)
	{
		
			if(isWinningNo==1||isWinningNo==2||isWinningNo==3||isWinningNo==0||isWinningNo==-1)
			{
				//WinningBets("FFBet");
				 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
				_TotalWinningAmount=isObjData._bettingAmountOnThis*6+_TotalWinningAmount;
				_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
			}
	}
	public static void BasketBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		
			if(isName=="Bask1")
			{
				if(isWinningNo==0||isWinningNo==1||isWinningNo==2)
				{
					//WinningBets("BasketBet");
				      RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
					_TotalWinningAmount=isObjData._bettingAmountOnThis*11+_TotalWinningAmount;
					_WinnObjectAmount+=isObjData._bettingAmountOnThis;
					 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
				}
			}
			else if(isName=="Bask2")
			{
				if(isWinningNo==-1||isWinningNo==2||isWinningNo==3)
				{
					//WinningBets("BasketBet");
				      RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
					_TotalWinningAmount=isObjData._bettingAmountOnThis*11+_TotalWinningAmount;
					_WinnObjectAmount+=isObjData._bettingAmountOnThis;
				 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
				}
			}
	}
	public static void EvenBetChecker(GameObject isObj,int isWinningNo)
	{
		if(isWinningNo==20||isWinningNo==4||isWinningNo==6||isWinningNo==8||isWinningNo==10||isWinningNo==12||isWinningNo==14||isWinningNo==16||isWinningNo==18||isWinningNo==20||isWinningNo==22||isWinningNo==24||isWinningNo==26||isWinningNo==28||isWinningNo==30||isWinningNo==32||isWinningNo==34||isWinningNo==36)
		{
			//WinningBets("EvenBet");
			  RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			_TotalWinningAmount=isObjData._bettingAmountOnThis*1+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
	}
	public static void OddBetChecker(GameObject isObj,int isWinningNo)
	{
		
		if(isWinningNo==1||isWinningNo==3||isWinningNo==5||isWinningNo==7||isWinningNo==9||isWinningNo==11||isWinningNo==13||isWinningNo==15||isWinningNo==17||isWinningNo==19||isWinningNo==21||isWinningNo==23||isWinningNo==25||isWinningNo==27||isWinningNo==29||isWinningNo==31||isWinningNo==33||isWinningNo==35)
		{
			//WinningBets("OddBet");
			  RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			_TotalWinningAmount=isObjData._bettingAmountOnThis*1+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
		
	}
	public static void RedBetChecker(GameObject isObj,int isWinningNo)
	{
		if(isWinningNo==1||isWinningNo==3||isWinningNo==5||isWinningNo==7||isWinningNo==9||isWinningNo==12||isWinningNo==14||isWinningNo==16||isWinningNo==18||isWinningNo==19||isWinningNo==21||isWinningNo==23||isWinningNo==25||isWinningNo==27||isWinningNo==30||isWinningNo==32||isWinningNo==34||isWinningNo==36)
		{
			//WinningBets("RedBet");
			//SetRedColorBetCount(GetRedColorBetCount()+1);
			 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			_TotalWinningAmount=isObjData._bettingAmountOnThis*1+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
		//else 
			//SetRedColorBetCount(0);
	}
	public static void BlackBetChecker(GameObject isObj,int isWinningNo)
	{
		if(isWinningNo==2||isWinningNo==4||isWinningNo==6||isWinningNo==8||isWinningNo==10||isWinningNo==11||isWinningNo==13||isWinningNo==15||isWinningNo==17||isWinningNo==20||isWinningNo==22||isWinningNo==24||isWinningNo==26||isWinningNo==28||isWinningNo==29||isWinningNo==31||isWinningNo==33||isWinningNo==35)
		{	
			//WinningBets("BlackBet");
				 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
				_TotalWinningAmount=isObjData._bettingAmountOnThis*1+_TotalWinningAmount;
				_WinnObjectAmount+=isObjData._bettingAmountOnThis;
				 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
		
	}
	public static void DozenBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		isName=isName.Remove(0,5);
		int isvalue;
		int.TryParse(isName, out isvalue);
	
		if(isWinningNo==(1+12*(isvalue-1))||isWinningNo==(2+12*(isvalue-1))||isWinningNo==(3+12*(isvalue-1))||isWinningNo==(4+12*(isvalue-1))||isWinningNo==(5+12*(isvalue-1))||isWinningNo==(6+12*(isvalue-1))||isWinningNo==(7+12*(isvalue-1))||isWinningNo==(8+12*(isvalue-1))||isWinningNo==(9+12*(isvalue-1))||isWinningNo==(10+12*(isvalue-1))||isWinningNo==(11+12*(isvalue-1))||isWinningNo==(12+12*(isvalue-1)))
		{
			//WinningBets("DozenBet");
			int x=GetOutSideBetWin();
			x++;
			SetOutSideBetWin(x);
			 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			_TotalWinningAmount=isObjData._bettingAmountOnThis*2+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
	}
	public static void ColumnBetChecker(GameObject isObj,int isWinningNo)
	{
		string isName=isObj.name;
		isName=isName.Remove(0,3);
		int isvalue;
		int.TryParse(isName, out isvalue);
		
		if(isWinningNo==(0+isvalue)||isWinningNo==(3+isvalue)||isWinningNo==(6+isvalue)||isWinningNo==(9+isvalue)||isWinningNo==(12+isvalue)||isWinningNo==(15+isvalue)||isWinningNo==(18+isvalue)||isWinningNo==(21+isvalue)||isWinningNo==(24+isvalue)||isWinningNo==(27+isvalue)||isWinningNo==(30+isvalue)||isWinningNo==(33+isvalue))
		{
			//WinningBets("ColumnBet");
			 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			_TotalWinningAmount=isObjData._bettingAmountOnThis*2+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}	
		
	}
	public static void LowBetChecker(GameObject isObj,int isWinningNo)
	{
		
		if(isWinningNo==1||isWinningNo==2||isWinningNo==3||isWinningNo==4||isWinningNo==5||isWinningNo==6||isWinningNo==7||isWinningNo==8||isWinningNo==9||isWinningNo==10||isWinningNo==11||isWinningNo==12||isWinningNo==13||isWinningNo==14||isWinningNo==15||isWinningNo==16||isWinningNo==17||isWinningNo==18)
		{
			//WinningBets("LowBet");
			 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			_TotalWinningAmount=isObjData._bettingAmountOnThis*1+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
		
	}
	public static void HighBetChecker(GameObject isObj,int isWinningNo)
	{
		
		if(isWinningNo==19||isWinningNo==20||isWinningNo==21||isWinningNo==22||isWinningNo==23||isWinningNo==24||isWinningNo==25||isWinningNo==26||isWinningNo==27||isWinningNo==28||isWinningNo==29||isWinningNo==30||isWinningNo==31||isWinningNo==32||isWinningNo==33||isWinningNo==34||isWinningNo==35||isWinningNo==36)
		{
		//	WinningBets("HighBet");
			 RT_BetColliderProperty isObjData=isObj.GetComponent<RT_BetColliderProperty>();
			_TotalWinningAmount=isObjData._bettingAmountOnThis*1+_TotalWinningAmount;
			_WinnObjectAmount+=isObjData._bettingAmountOnThis;
			 foreach(GameObject chip in isObjData._ChipArray)
						chip.GetComponent<ChipProperty>()._IsWinningChip=true;
		}
	
	}
	public static void BetClear()
	{
		 _SpinButtonDisabledStatus=true;
 		 _RebetButtonDisabledStatus=true;
   		 _ClearButtonDisabledStatus=true;
		
		_TotalbettingAmount=0;
		_TotalWinningAmount=0;
		_WinnObjectAmount=0;
		_TotalRebettingAmount=0;
		_TotalfundYouHave=GetTotalFund();
		_WinningNo=2222;
		_BettedChipCount=0;
		_rebettingCanBeDone=false;
		
	    _TotalWinningAmountText.guiText.text=" "+_GrandtotalWinningAmount;
   	    _TotalbettingAmountText.guiText.text=" "+0;
		_TotalfundYouHaveText.guiText.text=" "+GetTotalFund();
		GameObject obj = GameObject.Find("GamesceneGUIHandler");
		if(obj != null)
		{
			GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
			handlerScr.MenuButton.setDisabled(false);
			handlerScr.HelpButton.setDisabled(false);
			handlerScr.SpinButton.setDisabled(true);
			handlerScr.ClearButton.setDisabled(true);
			handlerScr.RebetButton.setDisabled(true);
			if(ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Free || 
			   	ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Premium || 
			   		ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.CustomWithIAP ||
					   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.FreeWithAlphonso)
			{
				handlerScr.AddFundButton.gameObject.SetActive(true);
				handlerScr.AddFundButton.setDisabled(false);
			}
//			handlerScr._prevButton .setDisabled(false);
//			handlerScr._NextButton.setDisabled(false);
		}
		else
			Debug.Log("Couldnt find GamesceneGUIHandler");
	
		GameObject isColliderObj=null;
		if(_StageNumber==1)
	    isColliderObj=GameObject.Find("AmericanTableCollider(Clone)");
		else if(_StageNumber==2)
	    isColliderObj=GameObject.Find("EuropianTableCollider(Clone)");
		else if(_StageNumber==3)
	    isColliderObj=GameObject.Find("FrenchTableCollider(Clone)");
		int isNoOfChildren=isColliderObj.transform.childCount;
		for(int i=0;i<isNoOfChildren;i++)
		{
			RT_BetColliderProperty isColliderObjdata=isColliderObj.transform.GetChild(i).GetComponent<RT_BetColliderProperty>();
			isColliderObjdata._betOn=false;
			isColliderObjdata._bettingAmountOnThis=0;
			isColliderObjdata._YPosition=0;
			isColliderObjdata._ZPosition=0;
			isColliderObjdata._ChipArray.Clear();
		}
		if(_StageNumber!=1)
		{
			GameObject isRaceTrackColliderObj=null;	
			isRaceTrackColliderObj=GameObject.Find("RaceTrackCollider(Clone)");
			int isNoOfRaceTrackCooliderChildren=isRaceTrackColliderObj.transform.childCount;
			for(int i=0;i<isNoOfRaceTrackCooliderChildren;i++)
			{
				RT_BetColliderProperty isColliderObjdata=isRaceTrackColliderObj.transform.GetChild(i).GetComponent<RT_BetColliderProperty>();
				isColliderObjdata._betOn=false;
				isColliderObjdata._bettingAmountOnThis=0;
				isColliderObjdata._YPosition=0;
				isColliderObjdata._ZPosition=0;
				isColliderObjdata._ChipArray.Clear();
				
			}
		}
		GameObject[] isBettedChip=GameObject.FindGameObjectsWithTag("BettedChip");
		foreach(GameObject ischip in isBettedChip)
		{
			GameObject.Destroy(ischip);
		}
		 _StraightBets.Clear();
		 _CornerBets.Clear();
	 	 _HSplitBets.Clear();
		 _VSplitBets.Clear();
		 _StreetBets.Clear();
		 _LineBets.Clear();
		 _FFBets.Clear();
		 _BasketBets.Clear();
		 _ColumnBets.Clear();
		 _DozenBets.Clear();
		 _OddBets.Clear();
		 _EvenBets.Clear();
		 _BlackBets.Clear();
		 _RedBets.Clear();
	 	 _HighBets.Clear();
		 _Lowbets.Clear();
		 _JeuBets.Clear();
		 _VoisinsBets.Clear();
		 _OrphelinsBets.Clear();
		 _TiersBets.Clear();
		 _NNbets.Clear();
	//	Debug.Log("bet is cleared");
	//	Debug.Log(_StraightBets.Count);
	}
	public static void BetClearOnButtonClick()
	{
		 _SpinButtonDisabledStatus=true;
 		 _RebetButtonDisabledStatus=true;
   		 _ClearButtonDisabledStatus=true;
		
		_TotalbettingAmount=0;
		_TotalWinningAmount=0;
		_WinnObjectAmount=0;
		_TotalRebettingAmount=0;
		_TotalfundYouHave=GetTotalFund();
		_WinningNo=2222;
		_rebettingCanBeDone=false;
		
	    _TotalWinningAmountText.guiText.text=" "+_GrandtotalWinningAmount;
   	    _TotalbettingAmountText.guiText.text=" "+0;
		_TotalfundYouHaveText.guiText.text=" "+GetTotalFund();
		GameObject obj = GameObject.Find("GamesceneGUIHandler");
		if(obj != null)
		{
			Debug.Log("Check-2");
			GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
			handlerScr.MenuButton.setDisabled(false);
			handlerScr.HelpButton.setDisabled(false);
			handlerScr.SpinButton.setDisabled(true);
			handlerScr.ClearButton.setDisabled(true);
			handlerScr.RebetButton.setDisabled(true);
			if(ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Free || 
			   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Premium || 
			   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.CustomWithIAP ||
			   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.FreeWithAlphonso)
			{
				handlerScr.AddFundButton.gameObject.SetActive(true);
				handlerScr.AddFundButton.setDisabled(false);
			}
//			handlerScr._prevButton .setDisabled(false);
//			handlerScr._NextButton.setDisabled(false);
		}
		else
			Debug.Log("Couldnt find GamesceneGUIHandler");
	
		GameObject isColliderObj=null;
		if(_StageNumber==1)
	    isColliderObj=GameObject.Find("AmericanTableCollider(Clone)");
		else if(_StageNumber==2)
	    isColliderObj=GameObject.Find("EuropianTableCollider(Clone)");
		else if(_StageNumber==3)
	    isColliderObj=GameObject.Find("FrenchTableCollider(Clone)");
		int isNoOfChildren=isColliderObj.transform.childCount;
		for(int i=0;i<isNoOfChildren;i++)
		{
			RT_BetColliderProperty isColliderObjdata=isColliderObj.transform.GetChild(i).GetComponent<RT_BetColliderProperty>();
			isColliderObjdata._betOn=false;
			isColliderObjdata._bettingAmountOnThis=0;
			isColliderObjdata._YPosition=0;
			isColliderObjdata._ZPosition=0;
			isColliderObjdata._ChipArray.Clear();
		}
		if(_StageNumber!=1)
		{
//			GameObject[] isRaceTrackArray = GameObject.Find("GameManager(Clone)").GetComponent<RT_BettingSelection>()._RaceTrackObs;
//			foreach(GameObject isObjRT in isRaceTrackArray)
//			{
//				isObjRT.GetComponent<RT_BetColliderProperty>()._betOn=false;
//				isObjRT.GetComponent<RT_BetColliderProperty>()._bettingAmountOnThis=0;
//			}
			GameObject isRaceTrackColliderObj=null;	
			isRaceTrackColliderObj=GameObject.Find("RaceTrackCollider(Clone)");
			int isNoOfRaceTrackCooliderChildren=isRaceTrackColliderObj.transform.childCount;
			for(int i=0;i<isNoOfRaceTrackCooliderChildren;i++)
			{
				RT_BetColliderProperty isColliderObjdata=isRaceTrackColliderObj.transform.GetChild(i).GetComponent<RT_BetColliderProperty>();
				isColliderObjdata._betOn=false;
				isColliderObjdata._bettingAmountOnThis=0;
				isColliderObjdata._YPosition=0;
				isColliderObjdata._ZPosition=0;
				isColliderObjdata._ChipArray.Clear();
				
			}
		}
		GameObject[] isBettedChip=GameObject.FindGameObjectsWithTag("BettedChip");
		foreach(GameObject ischip in isBettedChip)
		{
			ischip.GetComponent<ChipProperty>()._IsWinningChip=false;
		}
		 _StraightBets.Clear();
		 _CornerBets.Clear();
	 	 _HSplitBets.Clear();
		 _VSplitBets.Clear();
		 _StreetBets.Clear();
		 _LineBets.Clear();
		 _FFBets.Clear();
		 _BasketBets.Clear();
		 _ColumnBets.Clear();
		 _DozenBets.Clear();
		 _OddBets.Clear();
		 _EvenBets.Clear();
		 _BlackBets.Clear();
		 _RedBets.Clear();
	 	 _HighBets.Clear();
		 _Lowbets.Clear();
		 _JeuBets.Clear();
		 _VoisinsBets.Clear();
		 _OrphelinsBets.Clear();
		 _TiersBets.Clear();
		 _NNbets.Clear();

	}
	public static void Rebet()
	{
		 RT_DataHandler._rebettingCanBeDone=false;
		_TotalWinningAmount=0;
		_WinnObjectAmount=0;
		_WinningNo=2222;
		_TotalfundYouHave=GetTotalFund()-_TotalRebettingAmount;
		_TotalbettingAmountText.guiText.text=" "+_TotalbettingAmount;
		_TotalfundYouHaveText.guiText.text=" "+_TotalfundYouHave;
		GameObject obj = GameObject.Find("GamesceneGUIHandler");
		if(obj != null)
		{
			Debug.Log("Check-3");
			GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
			handlerScr.MenuButton.setDisabled(false);
			handlerScr.HelpButton.setDisabled(false);
			handlerScr.SpinButton.setDisabled(true);
			handlerScr.ClearButton.setDisabled(true);
			handlerScr.RebetButton.setDisabled(true);
			if(ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Free || 
			   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Premium || 
			   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.CustomWithIAP ||
			   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.FreeWithAlphonso)
			{
				handlerScr.AddFundButton.gameObject.SetActive(true);
				handlerScr.AddFundButton.setDisabled(false);
			}
//			handlerScr._prevButton .setDisabled(false);
//			handlerScr._NextButton.setDisabled(false);
		}
		else
			Debug.Log("Couldnt find GamesceneGUIHandler");
		GameObject[] isBettedChip=GameObject.FindGameObjectsWithTag("BettedChip");
		foreach(GameObject ischip in isBettedChip)
		{
			ischip.GetComponent<ChipProperty>()._IsWinningChip=false;
		}
	}
	public static void RestartGame()
	{
		_SpinButtonDisabledStatus=true;
 		 _RebetButtonDisabledStatus=true;
   		 _ClearButtonDisabledStatus=true;
		
		_StageNumber=0;
   		_TotalbettingAmount=0;	
 		_TotalWinningAmount=0;
		_TotalRebettingAmount=0;
		_WinnObjectAmount=0;
		_GrandtotalWinningAmount=0;
   		_TotalfundYouHave=GetTotalFund();
   		_WinningNo=2222;	
		_BettedChipCount=0;
   		_TouchDisable=false;
		_rebettingCanBeDone=false;
		_TotalWinningAmountText=null;
  		_TotalbettingAmountText=null;
 		_TotalfundYouHaveText=null;
		 _StraightBets.Clear();
		 _CornerBets.Clear();
	 	 _HSplitBets.Clear();
		 _VSplitBets.Clear();
		 _StreetBets.Clear();
		 _LineBets.Clear();
		 _FFBets.Clear();
		 _BasketBets.Clear();
		 _ColumnBets.Clear();
		 _DozenBets.Clear();
		 _OddBets.Clear();
		 _EvenBets.Clear();
		 _BlackBets.Clear();
		 _RedBets.Clear();
	 	 _HighBets.Clear();
		 _Lowbets.Clear();
		 _JeuBets.Clear();
		 _VoisinsBets.Clear();
		 _OrphelinsBets.Clear();
		 _TiersBets.Clear();
		 _NNbets.Clear();
	}
	public static void AmountUpdate()
	{
		//_TotalWinningAmountText.guiText.text=" "+_GrandtotalWinningAmount;
		//Debug.Log("no of times");
		if(_TotalWinningAmount!=0)
		{
			GameObject   isWinningAmount=GameObject.Instantiate(_TotalWinningAmountText,new Vector3(_TotalWinningAmountText.transform.position.x,(_TotalWinningAmountText.transform.position.y-0.145f),_TotalWinningAmountText.transform.position.x),Quaternion.identity) as GameObject;
			isWinningAmount.guiText.fontSize=(int)(40*Screen.height/768f);
			isWinningAmount.guiText.text="+ "+_TotalWinningAmount;
			isWinningAmount.AddComponent("BaloonText");
			isWinningAmount.GetComponent<BaloonText>()._Duration=2f;
		//	Debug.Log("_TotalWinningAmount"+_TotalWinningAmount+"_TotalbettingAmount"+_TotalbettingAmount);
		}
		_TotalfundYouHaveText.guiText.text=" "+GetTotalFund();//(_TotalfundYouHave-_TotalbettingAmount+_TotalWinningAmount);
		//SetTotalFund(_TotalfundYouHave-_TotalbettingAmount+_TotalWinningAmount);
	}
	public static void BettedChipsRenderer()
	{
		GameObject[] isChipArray=GameObject.FindGameObjectsWithTag("BettedChip");
		foreach(GameObject isObj in isChipArray)
		{
			_BettedChipCount++;
			isObj.renderer.enabled=false;
		}
	}
	public static void BettedChipsMovement()
	{
		_BettedChipCount=0;
		GameObject[] isChipArray=GameObject.FindGameObjectsWithTag("BettedChip");
		foreach(GameObject isObj in isChipArray)
		{
			_BettedChipCount++;
			isObj.AddComponent("ChipMovement");
		}
	}
	public static void ClearBettedChipMovment()
	{
		_BettedChipCount=0;
		GameObject[] isChipArray=GameObject.FindGameObjectsWithTag("BettedChip");
		foreach(GameObject isObj in isChipArray)
		{
			_BettedChipCount++;
			isObj.AddComponent("ChipClearMovement");
		}
		//Debug.Log(_BettedChipCount);
	}
	public static void OnWinLossChipAnimation()
	{
		
		int count=0;
		int objCount=0;
		_BettedChipCount=0;
		GameObject[] isChipArray=GameObject.FindGameObjectsWithTag("BettedChip");
		GameObject [] isObjs=new GameObject[isChipArray.Length];
		foreach(GameObject isObj in isChipArray)
		{
			_BettedChipCount++;
			GameObject isChip=GameObject.Instantiate(isObj) as GameObject;
			isChip.name=isObj.name;
			isChip.renderer.enabled=true;
			isObjs[objCount]=isChip;
			objCount++;
			if(isChip.GetComponent<ChipProperty>()._IsWinningChip==true)
			{
				count++;
				isChip.AddComponent("ChipUpAnimation");
			}
			else
			{
				isChip.AddComponent("ChipRightAnimation");
				
			}
		}
		
		foreach(GameObject isObj1 in isObjs)
		{
			if(isObj1.GetComponent<ChipProperty>()._IsWinningChip==false)
			{
					if(count!=0)
						isObj1.GetComponent<ChipRightAnimation>()._delay=2.0f;
					else
						isObj1.GetComponent<ChipRightAnimation>()._delay=0.5f;
			}
		}
		
		OutSideAchivement(isChipArray);
		AmountAchivement(_TotalWinningAmount,_TotalbettingAmount);
		ZeroHeroAchivement(_WinningNo,_TotalWinningAmount);
		//Debug.Log("fdsuhfu"+_TotalWinningAmount);
		
	}
	public static void JeuBet(int isAmount,GameObject isChip)
	{
		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("26");
		RaceTrackBetValueUpdate(mCollederObj1,isChip,isAmount,_StraightBets);
		//mCollederObj1.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS12");
		RaceTrackBetValueUpdate(mCollederObj2,isChip,isAmount,_VSplitBets);
		//mCollederObj2.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS32");
		RaceTrackBetValueUpdate(mCollederObj3,isChip,isAmount,_VSplitBets);
		//mCollederObj3.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS03");
		RaceTrackBetValueUpdate(mCollederObj4,isChip,isAmount,_VSplitBets);
		//mCollederObj4.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		GameObject.Destroy(isChip);
		
	}
	public static void OrphlinsBet(int isAmount,GameObject isChip)
	{
		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("1");
		RaceTrackBetValueUpdate(mCollederObj1,isChip,isAmount,_StraightBets);
		//mCollederObj1.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		
		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS6");
		RaceTrackBetValueUpdate(mCollederObj2,isChip,isAmount,_VSplitBets);
		//mCollederObj2.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		
		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS14");
		RaceTrackBetValueUpdate(mCollederObj3,isChip,isAmount,_VSplitBets);
		//mCollederObj3.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		
		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS17");
		RaceTrackBetValueUpdate(mCollederObj4,isChip,isAmount,_VSplitBets);
		//mCollederObj4.GetComponent<RT_BetColliderProperty>().ReplaceAllChipWithMinimum();
		
		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS31");
		RaceTrackBetValueUpdate(mCollederObj5,isChip,isAmount,_VSplitBets);
		GameObject.Destroy(isChip);

	}
	public static void TierBet(int isAmount,GameObject isChip)
	{
		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS5");
		RaceTrackBetValueUpdate(mCollederObj1,isChip,isAmount,_VSplitBets);

		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS13");
		RaceTrackBetValueUpdate(mCollederObj2,isChip,isAmount,_VSplitBets);

		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS27");
		RaceTrackBetValueUpdate(mCollederObj3,isChip,isAmount,_VSplitBets);

		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS33");
		RaceTrackBetValueUpdate(mCollederObj4,isChip,isAmount,_VSplitBets);
		

		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("HS7");
		RaceTrackBetValueUpdate(mCollederObj5,isChip,isAmount,_HSplitBets);
		
		Transform mCollederObj6=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("HS16");
		RaceTrackBetValueUpdate(mCollederObj6,isChip,isAmount,_HSplitBets);
		GameObject.Destroy(isChip);

	}
	public static void VoisinsBet(int isAmount,GameObject isChip)
	{
		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS4");
		RaceTrackBetValueUpdate(mCollederObj1,isChip,isAmount,_VSplitBets);

		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS12");
		RaceTrackBetValueUpdate(mCollederObj2,isChip,isAmount,_VSplitBets);

		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS18");
		RaceTrackBetValueUpdate(mCollederObj3,isChip,isAmount,_VSplitBets);

		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS19");
		RaceTrackBetValueUpdate(mCollederObj4,isChip,isAmount,_VSplitBets);

		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS32");
		RaceTrackBetValueUpdate(mCollederObj5,isChip,isAmount,_VSplitBets);

		
		Transform mCollederObj6=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("Bask2");
		RaceTrackBetValueUpdate(mCollederObj6,isChip,isAmount,_BasketBets);

		
		Transform mCollederObj7=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("Bask2");
		RaceTrackBetValueUpdate(mCollederObj7,isChip,isAmount,_BasketBets);

		
		Transform mCollederObj8=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("C17");
		RaceTrackBetValueUpdate(mCollederObj8,isChip,isAmount,_CornerBets);

		
		Transform mCollederObj9=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("C17");
		RaceTrackBetValueUpdate(mCollederObj9,isChip,isAmount,_CornerBets);
		GameObject.Destroy(isChip);

	}	
	public static void NNBet(int isAmount,GameObject isChip,string isName)
	{
		string s1="";
		string s2="";
		string s3="";
		string s4="";
		string s5="";
		switch(isName)
		{
		case "1" :
				s1="14";s2="20";s3="1";s4="33";s5="16";
			break;
		case "2":	
				s1="4";s2="21";s3="2";s4="25";s5="17";
			break;
		case "3":
				s1="0";s2="26";s3="3";s4="35";s5="12";
			break;
		case "4":
				s1="15";s2="19";s3="4";s4="21";s5="2";
			break;
		case "5":
				s1="23";s2="10";s3="5";s4="24";s5="16";
			break;
		case "6":
				s1="17";s2="34";s3="6";s4="27";s5="13";
			break;
		case "7":
				s1="12";s2="28";s3="7";s4="29";s5="18";
			break;
		case "8":
				s1="11";s2="30";s3="8";s4="23";s5="10";
			break;
		case "9":
				s1="18";s2="22";s3="9";s4="31";s5="14";
			break;
		case "10":
				s1="24";s2="5";s3="10";s4="23";s5="8";
			break;
		case "11":
				s1="13";s2="36";s3="11";s4="30";s5="8";
			break;
		case "12":
				s1="3";s2="35";s3="12";s4="28";s5="7";
			break;
		case "13":
				s1="6";s2="27";s3="13";s4="36";s5="11";
			break;
		case "14":
				s1="9";s2="31";s3="14";s4="20";s5="1";
			break;
		case "15":
				s1="0";s2="32";s3="15";s4="19";s5="4";
			break;
		case "16":
				s1="1";s2="33";s3="16";s4="24";s5="5";
			break;
		case "17":
				s1="2";s2="25";s3="17";s4="34";s5="6";
			break;
		case "18":
				s1="7";s2="29";s3="18";s4="22";s5="9";
			break;
		case "19":
				s1="32";s2="15";s3="19";s4="4";s5="21";
			break;
		case "20":
				s1="31";s2="14";s3="20";s4="1";s5="33";
			break;
		case "21":
				s1="19";s2="4";s3="21";s4="2";s5="25";
			break;
		case "22":
				s1="29";s2="18";s3="22";s4="9";s5="31";
			break;
		case "23":
				s1="30";s2="8";s3="23";s4="10";s5="5";
			break;
		case "24":
				s1="33";s2="16";s3="24";s4="5";s5="10";
				break;
		case "25":
				s1="21";s2="2";s3="25";s4="17";s5="34";
				break;
		case "26":
				s1="32";s2="0";s3="26";s4="3";s5="35";
				break;
		case "27":
				s1="34";s2="6";s3="27";s4="13";s5="36";
				break;
		case "28":
				s1="35";s2="12";s3="28";s4="7";s5="29";
				break;
		case "29":
				s1="28";s2="7";s3="29";s4="18";s5="22";
				break;
		case "30":
				s1="36";s2="11";s3="30";s4="8";s5="23";
				break;
		case "31":
				s1="22";s2="9";s3="31";s4="14";s5="20";
				break;
		case "32":
				s1="26";s2="0";s3="32";s4="15";s5="19";
				break;
		case "33":
				s1="20";s2="1";s3="33";s4="16";s5="24";
				break;
		case "34":
				s1="25";s2="17";s3="34";s4="6";s5="27";
				break;
		case "35":
				s1="26";s2="3";s3="35";s4="12";s5="28";
				break;
		case "36":
				s1="27";s2="13";s3="36";s4="11";s5="30";
				break;
		case "0":
				s1="15";s2="32";s3="0";s4="26";s5="3";
				break;
		}
		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s1);
		RaceTrackBetValueUpdate(mCollederObj1,isChip,isAmount,_StraightBets);

		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s2);
		RaceTrackBetValueUpdate(mCollederObj2,isChip,isAmount,_StraightBets);
		
		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s3);
		RaceTrackBetValueUpdate(mCollederObj3,isChip,isAmount,_StraightBets);

		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s4);
		RaceTrackBetValueUpdate(mCollederObj4,isChip,isAmount,_StraightBets);

		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s5);
		RaceTrackBetValueUpdate(mCollederObj5,isChip,isAmount,_StraightBets);
		GameObject.Destroy(isChip);

	}	
	static void OnWinOrLoss()
	{
				_rebettingCanBeDone=true;
				AmountUpdate();
				BettedChipsRenderer();
				OnWinLossChipAnimation();
				_TotalbettingAmountText.guiText.text=" "+0;
				_TotalRebettingAmount=_TotalbettingAmount;
		      
		       GameObject mAchieveHandler;
		       mAchieveHandler=GameObject.Find("AchieveMentHandler");
		       AchieveFundHandler mAchiveFundHandler; 
		       mAchiveFundHandler =mAchieveHandler.GetComponent("AchieveFundHandler")as AchieveFundHandler;
		       mAchiveFundHandler.mTotaDebedetAmpont= _TotalbettingAmount;
		       mAchiveFundHandler.mTotalWinnigAmount=_TotalWinningAmount;
		       mAchiveFundHandler.updateTableView=true;
		
				GameObject obj = GameObject.Find("GamesceneGUIHandler");
				if(obj != null)
				{
					GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
					handlerScr.MenuButton.setDisabled(false);
					handlerScr.HelpButton.setDisabled(false);
					handlerScr.SpinButton.setDisabled(true);
					handlerScr.ClearButton.setDisabled(true);
					if(ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Free || 
					   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.Premium || 
					   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.CustomWithIAP ||
					   ExternalInterfaceHandler.Instance.BuildType==eEXTERNAL_Build_Type.FreeWithAlphonso)			
					{
						handlerScr.AddFundButton.gameObject.SetActive(true);
						handlerScr.AddFundButton.setDisabled(false);
					}
					handlerScr._prevButton.gameObject.SetActive(true);
					handlerScr._NextButton.gameObject.SetActive(true);

				}
				else
					Debug.Log("Couldnt find GamesceneGUIHandler");
	}
	public static void Receiver(eEXTERNAL_REQ_TYPE reqType , string requestedId , string receivedStatus)
	{
		//Debug.Log("Data Received");
		if(receivedStatus == "false")
		{
			
		}
		else if(receivedStatus == "true")
		{
			switch(reqType)
			{
			
				case eEXTERNAL_REQ_TYPE.InAppConsumable:
               {
                   switch(requestedId)
                   {
                       case "1":
                       // add coins based on request
					   
				       SetTotalFund(GetTotalFund()+5000);
					   UpdatingFundScreenValue();
                       break;
                       case "2":
				        SetTotalFund(GetTotalFund()+50000);
						 UpdatingFundScreenValue();
                       break;
					
				       case  "3":
						
						 SetTotalFund(GetTotalFund()+150000);
						 UpdatingFundScreenValue();
					
					   break;
					
					   case  "4":
						
						 SetTotalFund(GetTotalFund()+400000);
						 UpdatingFundScreenValue();
					   break;
					   
                   }
				break;
					
			}
		}
	 }
	}
	static void UpdatingFundScreenValue()
	{
		GameObject FundScreen=GameObject.FindGameObjectWithTag("Fund");
		GameObject fundYouHaveText =FundScreen.transform.FindChild("FundYouHaveText").gameObject;
		fundYouHaveText.guiText.text=" "+(GetTotalFund()-_TotalbettingAmount);
		
	}
	static void RaceTrackBetValueUpdate(Transform isColliderObj,GameObject isRaceTrackChip,int isAmount,ArrayList BetContainer)
	{
		RT_BetColliderProperty isrT_BetColliderProperty=isColliderObj.GetComponent<RT_BetColliderProperty>();
		isrT_BetColliderProperty._ZPosition-=0.000001f;
		isrT_BetColliderProperty._YPosition+=0.03f;
		GameObject chip= GameObject.Instantiate(isRaceTrackChip,new Vector3(isColliderObj.position.x,isColliderObj.position.y+isrT_BetColliderProperty._YPosition,isrT_BetColliderProperty._ZPosition),isRaceTrackChip.transform.rotation) as GameObject;
		//isRaceTrackChip.GetComponent<ChipProperty>()._ChipRelatedtoRaceTrackChip.Add(chip);
		
		ChipProperty ischipProperty=chip.GetComponent<ChipProperty>();
		//ischipProperty._ParentChip=isRaceTrackChip;
		ischipProperty._bettedObject=isColliderObj.gameObject;
		isrT_BetColliderProperty._ChipArray.Add(chip);
		isrT_BetColliderProperty._bettingAmountOnThis+=isAmount;
		_TotalbettingAmount+=isAmount;
		_TotalfundYouHave-=isAmount;
		if(isrT_BetColliderProperty._betOn==false)
		{
		     BetContainer.Add(isColliderObj.gameObject);
			 isrT_BetColliderProperty._betOn=true;
		}
	//	Debug.Log(isrT_BetColliderProperty._ChipArray[0]);
		isrT_BetColliderProperty.ReplaceAllChipWithMinimum();
	}
	
	public static void isWinningNo(int isWinningNo)
	{
			foreach(GameObject Obj in _StraightBets )
			{
				StraightBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _CornerBets )
			{
				CornerBetChecker(Obj,isWinningNo);
			}
		
		    foreach(GameObject Obj in _HSplitBets )
			{
				
				HSplitBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _VSplitBets )
			{
			   
				VSplitBetChecker(Obj,isWinningNo);
			}
		
		    foreach(GameObject Obj in _StreetBets )
			{
				StreetBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _LineBets )
			{
				LineBetChecker(Obj,isWinningNo);
			}
		
		    foreach(GameObject Obj in _FFBets )
			{
				FFBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _BasketBets )
			{
				BasketBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _OddBets )
			{
			  
				OddBetChecker(Obj,isWinningNo);
			}
		
		    foreach(GameObject Obj in _EvenBets )
			{
				EvenBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _Lowbets )
			{
				LowBetChecker(Obj,isWinningNo);
			}
		
		    foreach(GameObject Obj in _HighBets )
			{
				HighBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _RedBets )
			{
				RedBetChecker(Obj,isWinningNo);
			}
		
		    foreach(GameObject Obj in _BlackBets )
			{
				BlackBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _ColumnBets )
			{
				ColumnBetChecker(Obj,isWinningNo);
			}
		
			foreach(GameObject Obj in _DozenBets )
			{
				DozenBetChecker(Obj,isWinningNo);
			}

			RT_Sounds isSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
			//isSound.PlaySound(isSound._Win);
		
		if(_TotalWinningAmount/_TotalbettingAmount>=1&&_TotalWinningAmount/_TotalbettingAmount<2)
			isSound.PlaySound(isSound._AvgWin);
		else if(_TotalWinningAmount/_TotalbettingAmount>=2)
			isSound.PlaySound(isSound._BigWin);
		else
			isSound.PlaySound(isSound._Loss);
	}
	
	
	static void OutSideAchivement(GameObject[] Coins)
	{
		bool OutSideWinBetFound=false;
		bool ColorWinBetFound=false;
		bool EvenWinBetFound=false;
		int i=0;
		for(i=0;i<Coins.Length;i++)
		{
			ChipProperty ischipProperty=Coins[i].GetComponent<ChipProperty>();
			if(ischipProperty._IsWinningChip==true)
			{
				if(ischipProperty._bettedObject.tag=="OutSide")
				{
					OutSideWinBetFound=true;
					if(ischipProperty._bettedObject.name=="Red"||ischipProperty._bettedObject.name=="Black")
						ColorWinBetFound=true;
					if(ischipProperty._bettedObject.name=="Even")	
						EvenWinBetFound=true;
								
				}
			}
		}
		
		if(OutSideWinBetFound==true)
		{
			SetOutSideBetWin(GetOutSideBetWin()+1);
			if(ColorWinBetFound==true)
			{
				SetColorBetWin(GetColorBetWin()+1);
			}
			else
				SetColorBetWin(0);
			if(EvenWinBetFound==true)
			{
				SetEvenBetWin(GetEvenBetWin()+1);
			}
			else
				SetEvenBetWin(0);
		}
		else
		{
			SetOutSideBetWin(0);
			SetColorBetWin(0);
			SetEvenBetWin(0);
		}
		if(GetOutSideBetWin()==3)
		{
			if(GetColorBetWin()==3)
				 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"12", null);
			if(GetEvenBetWin()==3)
				 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"13", null);
					
			 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"14", null);		
		}
	}
	
 	static	void  AmountAchivement(int PerRoundWinningAmount,int betttedAmount)
	{
		if(PerRoundWinningAmount>=betttedAmount*2)
		{
			 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"15", null);
			if(PerRoundWinningAmount>=betttedAmount*5)
			{
				 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"16", null);
				if(PerRoundWinningAmount>=betttedAmount*10)
				{
					 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"17", null);
					if(PerRoundWinningAmount>=betttedAmount*25)
					{
						 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"18", null);
						if(PerRoundWinningAmount>=betttedAmount*35)
						{
						 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"19", null);
						}
					}
				}
			}
		}
	}
	
	static void ZeroHeroAchivement(int  WinningNo,int PerRoundWinningAmount)
	{
		int	Betcount=0;
		Betcount+=_StraightBets.Count;
		Betcount+=_CornerBets.Count;
		Betcount+=_HSplitBets.Count;
		Betcount+=_VSplitBets.Count;
		Betcount+=_StreetBets.Count;
		Betcount+=_LineBets.Count;
		Betcount+=_FFBets.Count;
		Betcount+=_BasketBets.Count;
		Betcount+=_ColumnBets.Count;
		Betcount+=_DozenBets.Count;
		Betcount+=_OddBets.Count;
		Betcount+=_EvenBets.Count;
		Betcount+=_BlackBets.Count;
		Betcount+=_RedBets.Count;
		Betcount+=_HighBets.Count;
		Betcount+=_Lowbets.Count;
		
		if(Betcount==1&&WinningNo==0&&PerRoundWinningAmount!=0)
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"11", null);
		}
	}
	
}
