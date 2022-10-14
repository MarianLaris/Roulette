#if UNITY_IOS
using UnityEngine;
using System;
using System.Collections;

using System.Runtime.InteropServices;

public class iOSInterfaceHandler : ExternalInterfaceHandler 
{

	[DllImport ("__Internal")]
	private static extern void _Initialize();
	[DllImport ("__Internal")]
	private static extern void _SendInAppRequest(bool isConsumable , int requestedID);
	[DllImport ("__Internal")]
	private static extern void _SendNonConsumableRestore();
	
	[DllImport ("__Internal")]
	private static extern void _FullScreenAds(int adsType = 1);
	
	[DllImport ("__Internal")]
	private static extern void _BannerAds(bool isVisible,bool isOnTop);

	[DllImport ("__Internal")]
	private static extern void _ShowVideoAds(int id);

	[DllImport ("__Internal")]
	private static extern void _SendScore(int id , float score);
	
	[DllImport ("__Internal")]
	private static extern void _SendAchievement(int id,float percentage);
	
	[DllImport ("__Internal")]
	private static extern void _SendFlurry(string key);
	
	[DllImport ("__Internal")]
	private static extern void _ShowScore();
	
	[DllImport ("__Internal")]
	private static extern void _ShowAchievement();
	
	[DllImport ("__Internal")]
	private static extern void _RateApplication(bool isCallBackRequired = false);
	
	[DllImport ("__Internal")]
	private static extern void _ShowPopUp(string Heading, string msg);
	
	[DllImport ("__Internal")]
	private static extern void _LikeFaceBook();

	[DllImport ("__Internal")]
	private static extern void _ShareApp();

	[DllImport ("__Internal")]
	private static extern void _FollowOnTwitter();
	
	[DllImport ("__Internal")]
	private static extern void _MoreGames();
	
	[DllImport ("__Internal")]
	private static extern void _AnalyticsScreenLoaded(string pdata);
	
	[DllImport ("__Internal")]
	private static extern void _AnalyticsOnEvent(string pdata);

	[DllImport ("__Internal")]
	private static extern void _ShareScore(int lbId , float score);

	[DllImport ("__Internal")]
	private static extern void _ShareScreen();

	[DllImport ("__Internal")]
	private static extern void _ShowOfferWall(int pId = 1);

	[DllImport ("__Internal")]
	private static extern void _MicStatus(bool pStatus);

	[DllImport ("__Internal")]
	private static extern void _InitExternalSDK();



	public override bool SendRequest(eEXTERNAL_REQ_TYPE eRequestType, string strData = "",ReceivedCallBack callback = null)
	{

		if(callback != null)
		{
			mePrevRequestedType = eRequestType;
			mPrevRequestedData = strData;
			OnCallBack = callback;
		}
		switch(eRequestType)
		{
			case eEXTERNAL_REQ_TYPE.Initialize://0
			{
				if(!mIsInitialized)
				{
					if(PlayerPrefs.GetInt("PaidVersion") == 0)
					{
						base.Initialise();	
						_Initialize();
					}

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
			case eEXTERNAL_REQ_TYPE.InAppConsumable://1
			{
				_SendInAppRequest(true,int.Parse(strData));
			}
			break;
			case eEXTERNAL_REQ_TYPE.InAppNonConsumable://2
			{
				_SendInAppRequest(false,int.Parse(strData));
			}
			break;
			case eEXTERNAL_REQ_TYPE.InAppNonConsumableRestore://2
			{
				_SendNonConsumableRestore();
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Banner_Top_Ads://3
			{
				if(PlayerPrefs.GetInt("PaidVersion") == 0)
				{
				_BannerAds(true,true);
				return true;	
				}
				else
				return true;
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads://4
			{
				if(PlayerPrefs.GetInt("PaidVersion") == 0)
				{
					_BannerAds(true,false);
					return true;
				}
				else
				return true;
			}
			break;
			case eEXTERNAL_REQ_TYPE.Hide_Banner_Ads://6
			{
				_BannerAds(false,true);
				return true;
			}

			break;
			case eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads://5
			{
				if(PlayerPrefs.GetInt("PaidVersion") == 0)
				{

					if(mAdsHander.IsAdsDisplayAllowed(eRequestType))
					{
						int result = 10;	
						if(int.TryParse(strData,out result))
							_FullScreenAds(int.Parse(strData));
						else
							Debug.Log("Full Ads Request : Type Missing");
						GenericAppEvents("AdImpressions","Interstetial",strData);
						return true;
					}
				}
				else
				return true;
						
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_DirectFullScreen_Ads://5
			{
				int result = 10;	
				if(int.TryParse(strData,out result))
					_FullScreenAds(int.Parse(strData));
				else
					Debug.Log("Full Ads Request : Type Missing");
			GenericAppEvents("AdImpressions","Interstetial",strData);
			return true;
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Video_Ads:
			{
				int result = 10;	
				if(int.TryParse(strData,out result))
				{
					_ShowVideoAds(int.Parse(strData));
				}
				else
				{
					_ShowVideoAds(1);
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.Send_Score://8
			{
				string[] array = strData.Split('_');
				
				if(array.Length == 2)
				{
					int result = 10;
					if(int.TryParse(array[1],out result))
						_SendScore(int.Parse(array[0]),float.Parse(array[1]));
					else
						_SendScore(int.Parse(array[0]),float.Parse(array[1])*100);
				}
				else
				{
					_SendScore(1,float.Parse(strData));
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.Send_Achievement://9
			{
				string[] array = strData.Split('_');
				
				if(array.Length == 2)
					_SendAchievement(int.Parse(array[0]),int.Parse(array[1]));
				else
					_SendAchievement(int.Parse(strData),100);
			}
			break;
			case eEXTERNAL_REQ_TYPE.Send_Flurry://10
			{
				_SendFlurry(strData);
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Score://11
			{
				_ShowScore();
			}
			break;
			case eEXTERNAL_REQ_TYPE.Show_Achievement://12
			{
				_ShowAchievement();
			}
			break;
			case eEXTERNAL_REQ_TYPE.RateApplication://13
			{
				mRateAppCount++;
				if(mRateAppCount % mRateAppFactor == 0)
				{
					mRateAppCount = 0;	
					mRateAppFactor *= 2;
					_RateApplication(false); 
				}		

			}	
			break;
			case eEXTERNAL_REQ_TYPE.DirectRateApplication:
			{
				_RateApplication(true);
			}	
			break;
			case eEXTERNAL_REQ_TYPE.ShowPopup://14
			{
				string[] array = strData.Split('_');
				
				if(array.Length == 2)
					_ShowPopUp(array[0],array[1]);
				else
					_ShowPopUp(array[0],"Content is missing");
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShareApp://15
			{
				_ShareApp();
			}
			break;
			case eEXTERNAL_REQ_TYPE.LikeOnFacebook://15
			{
				_LikeFaceBook();
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
			case eEXTERNAL_REQ_TYPE.ShareScore:
			{
				string[] array = strData.Split('_');
				if(array.Length == 2)
				{
					_ShareScore(int.Parse(array[0]),float.Parse(array[1]));
				}
				else
				{
					_ShareScore(1,float.Parse(strData));
				}
				
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShareScreen://20
			{
				_ShareScreen();
			}
			break;
			case eEXTERNAL_REQ_TYPE.ShowOfferWall:
			{
				int result = 10;	
				if(int.TryParse(strData,out result))
				{
					_ShowOfferWall(int.Parse(strData));
				}
				else
				{
					_ShowOfferWall(1);
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
			case eEXTERNAL_REQ_TYPE.InitExternalSDK:
			{
				_InitExternalSDK ();
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
		Application.CaptureScreenshot("ScreenShot.png");
		yield return new WaitForSeconds(1f);
		SendRequest(eEXTERNAL_REQ_TYPE.ShareScreen,"",null);
	}

	public override void SendNotification(int id, long delay, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "", NotificationExecuteMode executeMode = NotificationExecuteMode.Inexact)
	{
#if UNITY_5
		UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
#else
		LocalNotification notif = new LocalNotification();
#endif
		
		notif.applicationIconBadgeNumber = id;
		notif.fireDate = DateTime.Now.AddSeconds(delay);
		notif.hasAction = true;
//		notif.alertAction = title;
		notif.alertBody = title+"\n"+message;
#if UNITY_5
		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (notif);
#else
		NotificationServices.ScheduleLocalNotification (notif);
#endif
	}
	
	public override void SendRepeatingNotification(int id, long delay, long timeout, string title, string message, Color32 bgColor, bool sound = true, bool vibrate = true, bool lights = true, string bigIcon = "")
	{
#if UNITY_5
		UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
#else
		LocalNotification notif = new LocalNotification();
#endif
		notif.applicationIconBadgeNumber = id;
		notif.fireDate = DateTime.Now.AddSeconds(delay);
		notif.alertBody = title;

#if UNITY_5
		notif.repeatInterval = UnityEngine.iOS.CalendarUnit.Day;
#else
		notif.repeatInterval = CalendarUnit.Day;
#endif
#if UNITY_5
		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (notif);
#else
NotificationServices.ScheduleLocalNotification (notif);
#endif
	}
	
	public override void CancelNotification(int id)
	{
		Debug.Log("Send Notification");
//		#if UNITY_5
//		UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
//		#else
		LocalNotification notif = new LocalNotification();
//		#endif

		notif.applicationIconBadgeNumber = id;
		notif.fireDate = DateTime.Now.AddSeconds(0);
		notif.hasAction = true;

//		#if UNITY_5
//		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();
//		UnityEngine.iOS.NotificationServices.ScheduleLocalNotification (notif);
//		#else
		NotificationServices.CancelAllLocalNotifications();
		NotificationServices.ScheduleLocalNotification (notif);
//		#endif

	}
	public override void CancelAllNotification()
	{
#if UNITY_5
		UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications ();
#else
//		NotificationServices.CancelLocalNotification (notif);
#endif
	}
}
#endif
