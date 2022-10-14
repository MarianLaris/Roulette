using UnityEngine;
using System;
using System.Collections;
using System.IO;

public enum eEXTERNAL_REQ_TYPE
{
	Initialize, // initializing internet check and other settings
	GetInAppData,
	InAppConsumable,	
	InAppNonConsumable,
	InAppNonConsumableRestore,
	Show_Banner_Top_Ads,	
	Show_Banner_Bottom_Ads,	
	Show_FullScreen_Ads,
	Show_DirectFullScreen_Ads,
	Hide_Banner_Ads,
	Show_Video_Ads,
	Send_Score,				
	Send_Achievement,		
	Send_Flurry,			
	Show_Score,				
	Show_Achievement,		
	RateApplication,
	DirectRateApplication,
	ShowPopup,				
	LikeOnFacebook,
	FollowOnTwitter,
	ShareApp,
	MoreGames,				
	ApplicationQuit,
	ShareScreen,
	StartLoadingScreen,
	StopLoadingScreen,
	GoogleAnalyticsScreen,
	GoogleAnalyticsEvent,
	ShareScore,
	sendLiveTileNotify,
	SignIn,
	SignOut,
	ShowOfferWall,
	MicStatus,
	AlphonsoLink,
	InitExternalSDK
}
public enum eEXTERNAL_BackGround_FullAds
{
	Activate,Deactivate
}
/// <summary>
//	1. Free:
//		- Ads
//		- Remove ads Button
//		- IAP	
//		- Social NW
//		- More games link
//		- Leaderboard and achievement
//	2. Premium:
//		- No Ads
//		- No Remove ads Button
//	3. Custom:       
//		-  No Remove ads Button
//		-  No InApp
//		- No Social NW buttons        
//      - No More games link
//		- No Leaderboard and achievement Buttons
//		- in short- no external call related buttons
        
        /// </summary>
public enum eEXTERNAL_Build_Type
{
	Free = 0,
	Premium = 1,
	Custom = 2,
	CustomWithQuit = 3,
    CustomWithIAP  =  4,
	FreeWithOutIAP  =  5,
	FreeWithAlphonso = 6
}
public enum eEXTERNAL_ALPHONSO_Type
{
	Without_Alphonso,
	With_Alphonso
}

public enum NotificationExecuteMode
{
	Inexact = 0,
	Exact = 1,
	ExactAndAllowWhileIdle = 2
};
public class ExternalInterfaceHandler : MonoBehaviour 
{
	static ExternalInterfaceHandler mInstance = null;
	protected static string mstrMainActivityClassName;
	protected static AdsHandler mAdsHander;
	
	public delegate void ReceivedCallBack(eEXTERNAL_REQ_TYPE eRequestType, string strRequestedData, string result);
	public ReceivedCallBack OnCallBack;
	
	protected string mPrevRequestedData;
	
	protected eEXTERNAL_REQ_TYPE mePrevRequestedType;
	protected eEXTERNAL_BackGround_FullAds meBackGroundFullAds;
	protected eEXTERNAL_Build_Type meBuildType;
	public eEXTERNAL_ALPHONSO_Type meAlphonsoType;
	protected static bool mIsInitialized = false;
	protected bool mAdsActive = true;
	protected bool mInAppActive = true;
	protected bool mSocialSharingActive = true;
	protected bool mMoreGamesActive = true;
	protected int mDefaultCurrencyValue = 1000;
	protected Texture2D bg;
	protected Texture2D _tRotateTexture;
	protected float rotAngle = 0;
	protected Vector2 pivotPoint;
	protected Rect rect1;
	protected Rect rect2;
	protected int speed;
	protected string mstrScreenshotPath;
	protected int mLaunchAdsCount = 0;
	protected int mRateAppCount = 0;
	protected int mRateAppFactor = 5;
	private bool mIsUserSignedIn = false;
	private bool mIsMicOn = false;

	public bool IsUserSignedIn
	{
		get{return mIsUserSignedIn;}
	}
	public bool IsMicOn
	{
		get{return mIsMicOn;}
	}
	public eEXTERNAL_Build_Type BuildType
	{
		get{return meBuildType;}

    }
	public eEXTERNAL_ALPHONSO_Type AlphonsoType
	{
		get{return meAlphonsoType;}
		set{meAlphonsoType = value;}
	}
	public static ExternalInterfaceHandler Instance // Makes sure object is there available always in the scene
	{
		get
		{
			if(mInstance == null)
			{
				GameObject obj = new GameObject();
				obj.name = "ExternalInterfaceHandler";
				DontDestroyOnLoad(obj);
				#if UNITY_EDITOR || UNITY_WEBPLAYER
				obj.AddComponent<ExternalInterfaceHandler>();
				mInstance = obj.GetComponent<ExternalInterfaceHandler>();
				#elif UNITY_IPHONE || UNITY_TVOS
				obj.AddComponent<iOSInterfaceHandler>();
				mInstance = obj.GetComponent<iOSInterfaceHandler>();
				#elif UNITY_ANDROID
				obj.AddComponent<AndroidInterfaceHandler>();
				mInstance = obj.GetComponent<AndroidInterfaceHandler>();
				#elif UNITY_STANDALONE_OSX
				obj.AddComponent<MacOSInterfaceHandler>();
				mInstance = obj.GetComponent<MacOSInterfaceHandler>();
				#elif UNITY_WINRT
				obj.AddComponent<WINRTInterfaceHandler>();
				mInstance = obj.GetComponent<WINRTInterfaceHandler>();
				#elif UNITY_TIZEN
				obj.AddComponent<TizenInterfaceHandler>();
				mInstance = obj.GetComponent<TizenInterfaceHandler>();
				#endif
				mAdsHander = obj.AddComponent<AdsHandler>();
			}
			return mInstance;
		}
	}
	public eEXTERNAL_BackGround_FullAds ExternalBackGroundAds
	{
		set
		{
			meBackGroundFullAds = value;
			Debug.Log("Background Ads Status : "+meBackGroundFullAds);
		}
		get{return meBackGroundFullAds;}
	}
	// Use this for loading assets from resource
	void LoadAssetsFromResource()
	{

		bg = Resources.Load("bg", typeof(Texture2D)) as Texture2D;
		_tRotateTexture = Resources.Load("loading_indicator", typeof(Texture2D)) as Texture2D;
		pivotPoint = new Vector2(Screen.width*.5f, Screen.height*.5f);
		rect1 = new Rect(pivotPoint.x - bg.width*.5f,pivotPoint.y - bg.height*.5f,bg.width,bg.height);
		pivotPoint = new Vector2(Screen.width*.5f, Screen.height*.5f-20);
		rect2 = new Rect(pivotPoint.x - _tRotateTexture.width/2,pivotPoint.y - _tRotateTexture.height*.5f,_tRotateTexture.width,_tRotateTexture.height);
		speed = 200;
		enabled = false;
	}
	protected void Initialise()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		mIsInitialized = true;
		LoadAssetsFromResource ();
		mLaunchAdsCount = PlayerPrefs.GetInt("mLaunchAdsCount");
		mLaunchAdsCount ++;
		PlayerPrefs.SetInt ("mLaunchAdsCount", mLaunchAdsCount);
		PlayerPrefs.Save ();
		SendRequest (eEXTERNAL_REQ_TYPE.Hide_Banner_Ads);
		if(mLaunchAdsCount > 5)
			SendRequest(eEXTERNAL_REQ_TYPE.Show_DirectFullScreen_Ads,"1");
	}
	void OnGUI() {
		
		if(bg == null)return;
		GUI.DrawTexture(rect1,bg);
		GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
		rotAngle += speed * Time.deltaTime;
		GUI.DrawTexture(rect2,_tRotateTexture);
	}

	public virtual bool SendRequest(eEXTERNAL_REQ_TYPE eRequestType, string strData = "",ReceivedCallBack pCallback = null)
	{
		Debug.Log(".....Editor Mode...."+ eRequestType + " " + strData );
		if(pCallback != null)
		{
			mePrevRequestedType = eRequestType;
			mPrevRequestedData = strData;
			OnCallBack = pCallback;
			
		}
		switch(eRequestType)
		{
			case eEXTERNAL_REQ_TYPE.Initialize:
			{
				if(!mIsInitialized)
				{
					Initialise();
					SetMicStatus("true");
					Debug.Log("External Initialized");
				
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.StartLoadingScreen:
			{
				enabled = true;
			}
			break;
			case eEXTERNAL_REQ_TYPE.StopLoadingScreen:
			{
				enabled = false;
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads:
			{
				if(mAdsHander.IsAdsDisplayAllowed(eRequestType))	
				GenericAppEvents("AdImpressions","Interstetial",strData);
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_DirectFullScreen_Ads:
			{
			GenericAppEvents("AdImpressions","Interstetial",strData);
			}
			break;
			case eEXTERNAL_REQ_TYPE.SignIn:
			{
				Receiver("GoogleSignIn_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.SignOut:
			{
				Receiver("GoogleSignIn_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.MicStatus:
			{	
				if(strData.Equals("1"))
				{
					Receiver("MicStatus_true");
				}
				else
				{
					Receiver("MicStatus_false");
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.Send_Score:
			{
				Receiver("GoogleSignIn_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.Send_Achievement:
			{
				Receiver("GoogleSignIn_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Score:
			{
				Receiver("GoogleSignIn_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Achievement:
			{
				Receiver("GoogleSignIn_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShowOfferWall:
			{
				Receiver("OfferWall_true_20");
            }
                break;
			case eEXTERNAL_REQ_TYPE.InAppConsumable:
			{
				Receiver("InApp_Consumable_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
			{
				Receiver("InApp_NonConsumable_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Video_Ads:
			{
				Receiver("VideoAds_true");
			}
			break;
            case eEXTERNAL_REQ_TYPE.RateApplication:
			{
				mRateAppCount++;
				if(mRateAppCount % mRateAppFactor == 0)
				{
					mRateAppCount = 0;	
					mRateAppFactor *= 2;
					Debug.Log("Rate App Called");
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.DirectRateApplication:
			{
				if (pCallback != null)
					Receiver ("true");
				Debug.Log("Direct Rate App Called");
			}
			break;
			case eEXTERNAL_REQ_TYPE.AlphonsoLink:
			{
				Receiver("AlphonsoLink_true");
			}
			break;
			case eEXTERNAL_REQ_TYPE.InitExternalSDK:
			{
				Debug.Log("External SDKs Initialised");
			}
			break;
			default:
			if(OnCallBack != null)
			{
				Debug.Log("Default Case");
				Receiver("true");
			}
			break;
			}

		return true;
	}
	
	public virtual void Receiver(string status)
	{
#if UNITY_STANDALONE
		SendRequest(eEXTERNAL_REQ_TYPE.StopLoadingScreen);
#endif
		if(OnCallBack == null)
			return;
		string[] array = status.Split('_');
		
		if(array[0].Equals("Restore") &&
				(mePrevRequestedType == eEXTERNAL_REQ_TYPE.InAppNonConsumable || mePrevRequestedType == eEXTERNAL_REQ_TYPE.InAppNonConsumableRestore))
		{
			ReceivedCallBack temp = OnCallBack;
			OnCallBack = null;
			for(int i = 1;i<array.Length;i++)
			{
				temp(eEXTERNAL_REQ_TYPE.InAppNonConsumable,array[i],"true");
			}
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_InApp_RestoreCompleted");
		}
		else if(array[0].Equals("GoogleSignIn") 
				&&(mePrevRequestedType == eEXTERNAL_REQ_TYPE.SignIn || mePrevRequestedType == eEXTERNAL_REQ_TYPE.SignOut))
		{
			ReceivedCallBack temp = OnCallBack;
			OnCallBack = null;
			switch(mePrevRequestedType)
			{
			case eEXTERNAL_REQ_TYPE.SignIn:
				if(array[1].Equals("true"))
				{
					mIsUserSignedIn = true;
				}
				temp(mePrevRequestedType,mPrevRequestedData,array[1]);
				break;
			case eEXTERNAL_REQ_TYPE.SignOut:
				if(array[1].Equals("true"))
				{
					mIsUserSignedIn = false;
				}
				temp(mePrevRequestedType,mPrevRequestedData,array[1]);
				break;
			default:
				mIsUserSignedIn = array[1].Equals("true") ? true:false;
				temp(mePrevRequestedType,mPrevRequestedData,array[1]);
				break;
			}
		}
		else if(array[0].Equals("OfferWall") && mePrevRequestedType == eEXTERNAL_REQ_TYPE.ShowOfferWall)
		{
			ReceivedCallBack temp = OnCallBack;
			OnCallBack = null;
			temp(mePrevRequestedType,mPrevRequestedData,array[1] +"_"+array[2]);
			if(array[1].Equals("true") && !array[2].Equals("0"))
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_OfferWall "+mPrevRequestedData+"_Success");
			}
			else
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_OfferWall "+mPrevRequestedData+"_Failed");
			}
        }
		else if(array[0].Equals("InApp")
				&& (mePrevRequestedType == eEXTERNAL_REQ_TYPE.InAppConsumable 
				|| mePrevRequestedType == eEXTERNAL_REQ_TYPE.InAppNonConsumable))
		{
			ReceivedCallBack temp = OnCallBack;
			OnCallBack = null;
			temp(mePrevRequestedType,mPrevRequestedData,array[2]);
			switch(mePrevRequestedType)
			{
			case eEXTERNAL_REQ_TYPE.InAppConsumable:
				if(array[2].Equals("true"))
				{
					ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_InAppConsumable "+" "+mPrevRequestedData+"_Success");
				}
				else
				{
					ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_InAppConsumable "+" "+mPrevRequestedData+"_Failed");
				}
				break;
			case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
				if(array[2].Equals("true"))
				{
					ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_InAppNonConsumable "+" "+mPrevRequestedData+"_Success");
				}
				else
				{
					ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_InAppNonConsumable "+" "+mPrevRequestedData+"_Failed");
				}
				break;
			}

		}
		else if(array[0].Equals("VideoAds")  && mePrevRequestedType == eEXTERNAL_REQ_TYPE.Show_Video_Ads)
		{
			ReceivedCallBack temp = OnCallBack;
			OnCallBack = null;
			temp(mePrevRequestedType,mPrevRequestedData,array[1]);
			if(array[1].Equals("true"))
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_"+array[0]+" "+mPrevRequestedData+"_Success");
			}
			else
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,"Conversion_"+array[0]+" "+mPrevRequestedData+"_Failed");
			}
		}
		else if(array[0].Equals("MicStatus") && mePrevRequestedType == eEXTERNAL_REQ_TYPE.MicStatus)
		{
				ReceivedCallBack temp = OnCallBack;
				OnCallBack = null;
				if(array[1].Equals("true"))
				{
					mIsMicOn = true;
				}
				else
				{
					mIsMicOn = false;
				}
				temp(mePrevRequestedType,mPrevRequestedData,array[1]);
			
		}
		else
		{
			ReceivedCallBack temp = OnCallBack;
			OnCallBack = null;
			temp(mePrevRequestedType,mPrevRequestedData,status);
        }

	}
	public virtual void CaptureScreenShot()
	{
		Debug.Log("screenshot capture Called");
		StartCoroutine(DelayInCaptureScreenShot());
	}
	IEnumerator DelayInCaptureScreenShot ()
	{
		yield return new WaitForEndOfFrame();
		Texture2D tex = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		tex.Apply ();
		Destroy(tex);
	}
	void ScreenShotPath(string pPath)
	{
		mstrScreenshotPath = pPath;
	}
	public void SetExternalPreferences(string pStr)
	{
		int t = 0;
		if(int.TryParse(pStr,out t))
		{
			meBuildType = (eEXTERNAL_Build_Type)(int.Parse(pStr));
		}
		else
			Debug.Log(".....Received wrong format for Build Type.......");
	}
	public void SetAlphansoStates(int pValue)
	{
		
	}
	public void SetMainActivityClassName(string pStr)
	{
		mstrMainActivityClassName = pStr;
	}
	public void SetDefaultCurrency(string pStr)
	{
		int t = 0;
		if(int.TryParse(pStr,out t))
		{
			mDefaultCurrencyValue = int.Parse(pStr);
		}
		else
			Debug.Log(".....Received wrong format for default Currency.......");
	}
	public void SetMicStatus(string pStatus)
	{
		if(pStatus.Equals("true"))
		{
			mIsMicOn = true;
		}
		else
		{
			mIsMicOn = false;
		}
	}
	protected void GenericAppEvents(string p1="",string p2="",string p3 = "")
	{
		string evnt = p1+"_"+p2+"_"+p3;
		SendRequest (eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent,evnt);
	}
	
	public virtual void SendNotification(int id, long delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", NotificationExecuteMode executeMode = NotificationExecuteMode.Inexact)
	{

	}
	
	public virtual void SendRepeatingNotification(int id, long delay, long timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
	{

	}
	
	public virtual void CancelNotification(int id)
	{

	}
	public virtual void CancelAllNotification()
	{

	}
	/// <summary>
	/// Raises the application pause event.
	/// </summary>
	/// <param name="pIsPause">If set to <c>true</c> p is pause.</param>
	public void OnApplicationPause(bool pIsPause)
	{
#if UNITY_ANDROID

#else
		Debug.Log ("Game is Paused - "+pIsPause);
		switch (ExternalBackGroundAds) {
		case eEXTERNAL_BackGround_FullAds.Activate:
			if(!pIsPause)
				SendRequest (eEXTERNAL_REQ_TYPE.Show_DirectFullScreen_Ads,"0");
			break;
		case eEXTERNAL_BackGround_FullAds.Deactivate:
			break;
		}
#endif
	}
}
