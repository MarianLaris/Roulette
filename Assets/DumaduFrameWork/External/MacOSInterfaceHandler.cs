#if UNITY_STANDALONE_OSX
using UnityEngine;
using System;
using System.Collections;

using System.Runtime.InteropServices;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class MacOSInterfaceHandler : ExternalInterfaceHandler 
{

	public delegate void UnityCallbackDelegate(IntPtr objectName,IntPtr commandName,IntPtr commandData);
	
	[DllImport("UnityMacPlugin")]
	public static extern void ConnectCallback([MarshalAs(UnmanagedType.FunctionPtr)]UnityCallbackDelegate callbackMethod);
	
	[DllImport("UnityMacPlugin")]
	public  static extern void SendUnityBridgeMessage(IntPtr objectName, IntPtr messageName, IntPtr parameterString);
	
	[DllImport ("UnityMacPlugin")]
	public static extern void InitPlugin();
	
	[DllImport ("UnityMacPlugin")]
	private static extern void _SendInAppRequest(bool isConsumable , int requestedID);
	
	string[] achievementIds = null;
	string[] LdIds = null;
	
	void Awake()
	{
		// Add keys in the same order like IOS
		achievementIds = new string[] 
		{
			"basket25goals",
			"score5000inarcademode",
			"score5000intimeattackmode",
			"basket250goals",
			"basket300perfectgoals",
			"reachacomboof10",
			"unlockallballs",
			"unlockallcharacters",
			"reachacomboof25",
			"basket15quickshotsinsinglegame"
		};
		
		LdIds = new string[]
		{
			"macarcadehighscores",
			"macbbstreetherotimeattackhighscores"
		};

		Social.localUser.Authenticate (success => {
			if (success) {
//			Debug.Log ("Authentication successful");
//			string userInfo = "Username: " + Social.localUser.userName + 
//				"\nUser ID: " + Social.localUser.id + 
//				"\nIsUnderage: " + Social.localUser.underage;
//			Debug.Log (userInfo);
			}
//		else
//			Debug.Log ("Authentication failed");
		});

		ConnectCallback((objectName, commandName, commandData) => {
		string objName = Marshal.PtrToStringAuto(objectName);
		string commName = Marshal.PtrToStringAuto(commandName);
		string commData = Marshal.PtrToStringAuto(commandData);
		
		GameObject foundObject = GameObject.Find(objName);
		if(foundObject != null)
		{
			foundObject.SendMessage(commName,commData);
		}
		});
		
	}
	void OnGUI() {
	 
		pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
		rect1 = new Rect(pivotPoint.x - bg.width/2,pivotPoint.y - bg.height/2,bg.width,bg.height);
		pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2-20);
		rect2 = new Rect(pivotPoint.x - _tRotateTexture.width/2,pivotPoint.y - _tRotateTexture.height/2,_tRotateTexture.width,_tRotateTexture.height);
		GUI.DrawTexture(rect1,bg);
		GUIUtility.RotateAroundPivot(rotAngle, pivotPoint);
        rotAngle += speed * Time.deltaTime;
		GUI.DrawTexture(rect2,_tRotateTexture);
	}
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
			
		case eEXTERNAL_REQ_TYPE.Initialize:
			if(!mIsInitialized)
			{
				base.Initialise();
				InitPlugin();
			}

			break;
		case eEXTERNAL_REQ_TYPE.GetInAppData:
		{
			SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
			IntPtr objectName = Marshal.StringToHGlobalAuto(this.name);
			IntPtr messageName = Marshal.StringToHGlobalAuto("Receiver");
			IntPtr parameterName = Marshal.StringToHGlobalAuto("GET");
			SendUnityBridgeMessage(objectName,messageName,parameterName);
		}
			break;
		case eEXTERNAL_REQ_TYPE.InAppConsumable:
		{
			SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
			IntPtr objectName = Marshal.StringToHGlobalAuto(this.name);
			IntPtr messageName = Marshal.StringToHGlobalAuto("Receiver");
			IntPtr parameterName = Marshal.StringToHGlobalAuto(("1_"+strData));
			SendUnityBridgeMessage(objectName,messageName,parameterName);
		}
			break;

		case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
		{
			SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
			IntPtr objectName = Marshal.StringToHGlobalAuto(this.name);
			IntPtr messageName = Marshal.StringToHGlobalAuto("Receiver");
			IntPtr parameterName = Marshal.StringToHGlobalAuto(("2_"+strData));
			SendUnityBridgeMessage(objectName,messageName,parameterName);
		}
		break;
		
		case eEXTERNAL_REQ_TYPE.InAppNonConsumableRestore:
		{
			SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
			IntPtr objectName = Marshal.StringToHGlobalAuto(this.name);
			IntPtr messageName = Marshal.StringToHGlobalAuto("Receiver");
			IntPtr parameterName = Marshal.StringToHGlobalAuto(("Restore"));
			SendUnityBridgeMessage(objectName,messageName,parameterName);
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
		case eEXTERNAL_REQ_TYPE.Send_Achievement:
		{
			Social.ReportProgress(achievementIds[int.Parse(strData)],100, null);
		}
			break;
		case eEXTERNAL_REQ_TYPE.Show_Achievement:
		{
			Social.ShowAchievementsUI();
		}
			break;
		case eEXTERNAL_REQ_TYPE.Send_Score:
		{
			float tScore = 0;
			int ldIdNo = 1;
			string[] strarray = strData.Split('_');
				
			if(strarray.Length == 2)
			{
				tScore = float.Parse(strarray[1]);
				ldIdNo = int.Parse(strarray[0]);
				ReportScore((long)tScore,LdIds[ldIdNo-1]);
			}
			else
			{
				tScore = float.Parse(strData);
				ReportScore((long)tScore,LdIds[0]);
				Debug.Log(tScore);
			}
		}
			break;
		case eEXTERNAL_REQ_TYPE.Show_Score:
		{
//			Social.ReportProgress(achievementIds[1],100, null);
			Social.ShowLeaderboardUI();
			//GameCenterPlatform.ShowLeaderboardUI("macarcadehighscores",  UnityEngine.SocialPlatforms.TimeScope.AllTime);
		}
			break;
		default:
		{
		}
			break;
		}

		return false;
	}
	void ReportScore (long score, string leaderboardID) {
		Debug.Log ("Reporting score " + score + " on leaderboard " + leaderboardID);
		Social.ReportScore (score, leaderboardID, success => {
			Debug.Log(success ? "Reported score successfully" : "Failed to report score");
		});
	}
}
#endif
