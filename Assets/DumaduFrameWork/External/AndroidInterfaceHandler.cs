#if UNITY_ANDROID
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
public class AndroidInterfaceHandler : ExternalInterfaceHandler 
{	
	public List<string> mArrNotificationMessages = null;
	

	public static AndroidJavaClass javaClass = new AndroidJavaClass("com.dumadugames.unityandroid.JarClass");
    static string fullClassName = "com.dumadugames.unitynotification.UnityNotificationManager";
	public static AndroidJavaObject getCurrentActivity () {
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	}
	public static void _Initialize() {
		javaClass.CallStatic("initialize", getCurrentActivity());
	}
	
	public static void _SendInAppRequest(bool isConsumable , int itemId) {
		javaClass.CallStatic("purchaseItem", isConsumable, itemId, getCurrentActivity());
	}

	public static void _SendNonConsumableRestore() {
		javaClass.CallStatic("restorePurchaseItem", getCurrentActivity());
	}

	public static void _FullScreenAds(int adsType = 1) {
		javaClass.CallStatic("showIntrestitial",adsType, getCurrentActivity());
	}
	
	public static void _BannerAds(bool isVisible,bool isOnTop) {
		if(isVisible) {
			javaClass.CallStatic("showBanner", isOnTop, getCurrentActivity());
		}
		else {
			javaClass.CallStatic("hideBanner", isOnTop, getCurrentActivity());
		}
	}
	public static void _ShowVideoAds(int id) {
		javaClass.CallStatic("showVideoAds",id, getCurrentActivity());
	}
	public static void _SendScore(int id, int score) {
		javaClass.CallStatic("submitScore", id, score, getCurrentActivity());
	}
	
	public static void _SendScore(int id, float score) {
		javaClass.CallStatic("submitScore", id, score, getCurrentActivity());
	}
	
	public static void _SendAchievement(int id) {
		javaClass.CallStatic("unlockAchievement", id , getCurrentActivity());
	}
	
	
	
	public static void _SendAchievement(int id, int status) {
		javaClass.CallStatic("incrementAchievement", id, status, getCurrentActivity());
	}
	
	public static void _ShowScores() {
		javaClass.CallStatic("showLeaderBoards", getCurrentActivity());
	}
	
	public static void _ShowAchievements() {
		javaClass.CallStatic("showAchievements", getCurrentActivity());
	}
	
	public static void _ShowPopUp(string title, string msg , bool isQuitRequested) {
		if(isQuitRequested)
			javaClass.CallStatic("showExitPopUp", getCurrentActivity());
		else
			javaClass.CallStatic("showPopUp", title, msg, getCurrentActivity());
	}
	
	public static void _RateApp(bool isCallBackRequired = false) {
		javaClass.CallStatic("rateApp",isCallBackRequired, getCurrentActivity());
	}

	public static void _ShareScreen() {
		javaClass.CallStatic("shareScreen", getCurrentActivity());
	}
	public static void _SubmitFlurryEvent(string key) {
		javaClass.CallStatic("submitFlurryEvent", key, getCurrentActivity());
	}	
	
	public static void _LikeOnFaceBook() {
		javaClass.CallStatic("like", getCurrentActivity());
	}
	
	public static void _FollowOnTwitter() {
		javaClass.CallStatic("follow", getCurrentActivity());
	}
	public static void  _ShareApp()
	{
		javaClass.CallStatic("shareGame",getCurrentActivity());

	}
	public static void _MoreGames() {
		javaClass.CallStatic("moreGames", getCurrentActivity());
	}	
	
	public static void _LocalNotification(string date, string msg, int classId) {
		javaClass.CallStatic("localNotification", date, msg, classId, getCurrentActivity());
	}
	
	public static void _CancelAllLocalNotifications()
	{
		javaClass.CallStatic("cancellocalNotification", true, getCurrentActivity());
	}


	public static void _ShareScoreFaceBook(int id, int score)
	{
		javaClass.CallStatic("shareScore", id, score, getCurrentActivity());
	}
	
	public static void  _ShareScoreFaceBook(int id , float pScore)
	{
		javaClass.CallStatic("shareScore", id, pScore, getCurrentActivity());
		
	}

	public static void  _AnalyticsOnEvent(string str)
	{
		javaClass.CallStatic("analyticsEvent",str, getCurrentActivity());
	}
	public static void  _AnalyticsScreenLoaded(string str)
	{
		javaClass.CallStatic("analyticsScreen",str, getCurrentActivity());
		
	}
	public static void  _SignIn()
	{
		javaClass.CallStatic("gameServicesLogin", getCurrentActivity());
		
	}
	public static void  _SignOut()
	{
		javaClass.CallStatic("gameServicesLogout", getCurrentActivity());
		
	}
	public static void  _MicStatus(bool pStatus)
	{
		javaClass.CallStatic("micStatus",pStatus, getCurrentActivity());
		
	}
	public static void  _AlphonsoLink()
	{

	 javaClass.CallStatic("AlphonsoLinkShare",getCurrentActivity());

	}
	public static void  _ShowOfferWall(int id)
	{
		javaClass.CallStatic("showOfferwall",id,getCurrentActivity());
		
	}
//	public static void  _InitExternalSDK()
//	{
//		javaClass.CallStatic("initExternalSDK",getCurrentActivity());
//
//	}
	public override bool SendRequest(eEXTERNAL_REQ_TYPE eRequestType, string strData = "",ReceivedCallBack callback = null)
	{
		if(callback != null)
		{
			mePrevRequestedType = eRequestType;
			mPrevRequestedData = strData;
			OnCallBack = callback;
		}
		int result = 10;
		switch(eRequestType)
		{
			case eEXTERNAL_REQ_TYPE.Initialize:
				if(!mIsInitialized)
				{
					base.Initialise();	
					_Initialize();
					
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
            case eEXTERNAL_REQ_TYPE.InAppConsumable:
			{
				GenericAppEvents("Conversion_","InAppConsumable "+strData,"_Requested");	
				_SendInAppRequest(true,int.Parse(strData));
			}
			break;
			case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
			{
				GenericAppEvents("Conversion_","InAppNonConsumable "+strData,"_Requested");	
				_SendInAppRequest(false,int.Parse(strData));
			}
			break;
			case eEXTERNAL_REQ_TYPE.InAppNonConsumableRestore://2
			{
				_SendNonConsumableRestore();
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Banner_Top_Ads:
			{
				_BannerAds(true,true);
				return true;
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads:
			{
				_BannerAds(true,false);
				return true;
			}
			break;
			case eEXTERNAL_REQ_TYPE.Hide_Banner_Ads:
			{
				_BannerAds(false,true);
				return true;	
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads:
			{
				if(mAdsHander.IsAdsDisplayAllowed(eRequestType))
				{
					if(int.TryParse(strData,out result))
					{
						GenericAppEvents("AdImpressions","Interstetial",strData);
						_FullScreenAds(int.Parse(strData));
					}
						
					else
						Debug.Log("Full Ads Request : Type Missing");
					return true;
				}
					
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_DirectFullScreen_Ads:
			{
				if(int.TryParse(strData,out result))
				{
					GenericAppEvents("AdImpressions","Interstetial",strData);
					_FullScreenAds(int.Parse(strData));
				}
					
				else
					Debug.Log("Full Ads Request : Type Missing");
				return true;
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Video_Ads:
			{
				if(int.TryParse(strData,out result))
				{
					GenericAppEvents("Conversion_","VideoAds "+strData,"_Requested");	
					_ShowVideoAds(int.Parse(strData));
				}
				else
				{
					GenericAppEvents("Conversion_","VideoAds 1","_Requested");	
					_ShowVideoAds(1);
				}
			}
			break;
		case eEXTERNAL_REQ_TYPE.Send_Score:
			{
				string[] array = strData.Split('_');
				if(array.Length == 2)
				{
					if(int.TryParse(array[1],out result))
						_SendScore(int.Parse(array[0]),int.Parse(array[1]));
					else
						_SendScore(int.Parse(array[0]),float.Parse(array[1]));
				}
				else
				{
				if(int.TryParse(strData,out result))	
					_SendScore(1,int.Parse(strData));
				else
					_SendScore(1,float.Parse(strData));
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.Send_Achievement:
			{
				string[] array = strData.Split('_');
				
				if(array.Length == 2)
					_SendAchievement(int.Parse(array[0]),int.Parse(array[1]));
				else
					_SendAchievement(int.Parse(strData));
			}
			break;
			
			case eEXTERNAL_REQ_TYPE.Send_Flurry:
			{
				_SubmitFlurryEvent(strData);
			}
			break;
			
			case eEXTERNAL_REQ_TYPE.Show_Score:
			{
				_ShowScores();
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Achievement:
			{
				_ShowAchievements();
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShowPopup: 
			{
				string[] array = strData.Split('_');
				
				if(array.Length == 2)
					_ShowPopUp(array[0],array[1],false);
				else
					_ShowPopUp(array[0],"Content is missing",false);
			}
			break;
			case eEXTERNAL_REQ_TYPE.LikeOnFacebook://15
			{
				_LikeOnFaceBook();
			}
			break;
		case eEXTERNAL_REQ_TYPE.FollowOnTwitter://16
			{
				_FollowOnTwitter();
			}
			break;
			
			case eEXTERNAL_REQ_TYPE.MoreGames://17
			{
				_MoreGames();
			}
			break;
			case eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent://18
			{	
				_AnalyticsOnEvent(strData);
			}
			break;
				
			case eEXTERNAL_REQ_TYPE.GoogleAnalyticsScreen://19
			{
				_AnalyticsScreenLoaded(strData);
			}
			break;
			case eEXTERNAL_REQ_TYPE.ApplicationQuit://18
			{
				string[] array = strData.Split('_');
				
				if(array.Length == 2)
					_ShowPopUp(array[0],array[1],true);
				else
					_ShowPopUp(array[0],"Content is missing",true);
			}
			break;
			case eEXTERNAL_REQ_TYPE.RateApplication://18
			{
				mRateAppCount++;
				if(mRateAppCount % mRateAppFactor == 0)
				{
					mRateAppCount = 0;	
					mRateAppFactor *= 2;
					_RateApp(false); 
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.DirectRateApplication:
			{
				_RateApp(true); 
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShareApp:
			{
				_ShareApp();
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShareScreen:
			{
				_ShareScreen();
			}
			break;
		    case eEXTERNAL_REQ_TYPE.ShareScore:
		  	{
			 	string[] array = strData.Split('_');
			
				if(array.Length == 2)
				{
					if(int.TryParse(array[1],out result))
						_ShareScoreFaceBook(int.Parse(array[0]),int.Parse(array[1]));
					else
						_ShareScoreFaceBook(int.Parse(array[0]),float.Parse(array[1]));
				}
				else
				{
					if(int.TryParse(strData,out result))	
						_ShareScoreFaceBook(1,int.Parse(strData));
					else
						_ShareScoreFaceBook(1,float.Parse(strData));
				}
			
			}
			break;
			case eEXTERNAL_REQ_TYPE.SignIn:
			{
				_SignIn();
			}
			break;
			case eEXTERNAL_REQ_TYPE.SignOut:
			{
				_SignOut();
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShowOfferWall:
			{
				if(int.TryParse(strData,out result))
				{
					_ShowOfferWall(int.Parse(strData));
					GenericAppEvents("Conversion_","OfferWall "+strData,"_Requested");
				}
				else
				{
					_ShowOfferWall(1);
					GenericAppEvents("Conversion_","OfferWall 1","_Requested");
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.MicStatus:
			{
				if(strData.Equals("1"))
				{
					_MicStatus(true);
				}
				else
				{
					_MicStatus(false);
				}
			}
			break;
             case eEXTERNAL_REQ_TYPE.AlphonsoLink:
			{
              _AlphonsoLink();
			}
			break;
			case eEXTERNAL_REQ_TYPE.InitExternalSDK:
			{
//				_InitExternalSDK ();
			}
			break;
			default:
			{
			}
			break;
		}
		
		return false;
	}
	public override void CaptureScreenShot ()
	{
		StartCoroutine(DelayInCaptureScreenShot());
	}
	IEnumerator DelayInCaptureScreenShot ()
	{
		yield return new WaitForEndOfFrame();
		Texture2D tex = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);
		tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		tex.Apply ();
		var bytes = tex.EncodeToPNG();
		Debug.Log(mstrScreenshotPath);
		File.WriteAllBytes(mstrScreenshotPath, bytes);
		Destroy (tex);
		yield return new WaitForSeconds(.2f);
		SendRequest(eEXTERNAL_REQ_TYPE.ShareScreen,"",null);
	}

	public override void SendNotification(int id, long delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", NotificationExecuteMode executeMode = NotificationExecuteMode.Inexact)
	{
		AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
		if (pluginClass != null)
		{
			pluginClass.CallStatic("SetNotification", id, delay * 1000L, title, message, message, sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, "app_icon_l", "app_icon_s", bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, (int)executeMode, mstrMainActivityClassName);
		}
	}
	
	public override void SendRepeatingNotification(int id, long delay, long timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
	{
		AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
		if (pluginClass != null)
		{
			pluginClass.CallStatic("SetRepeatingNotification", id, delay * 1000L, title, message, message, timeout * 1000, sound ? 1 : 0, vibrate ? 1 : 0, lights ? 1 : 0, "app_icon_l", "app_icon_s", bgColor.r * 65536 + bgColor.g * 256 + bgColor.b, mstrMainActivityClassName);
		}
	}
	
	public override void CancelNotification(int id)
	{
		AndroidJavaClass pluginClass = new AndroidJavaClass(fullClassName);
		if (pluginClass != null) {
			pluginClass.CallStatic("CancelNotification", id);
		}
	}
}
#endif
