using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	int width = 200;
	int height = 50;
	int coins = 0;
    bool isGamePlay;
	void Awake()
	{
		ExternalInterfaceHandler.Instance.SendRequest (eEXTERNAL_REQ_TYPE.Initialize);
		AlphansoUpdate();
	}
	
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
		GUILayout.BeginVertical();
		GUILayout.Label("Consumable Purchase"); 
		//......1..........//
		if(GUILayout.Button("1", GUILayout.Width(width), GUILayout.Height(height)))
		{
#if UNITY_STANDALONE_OSX
			// disable current Screen button
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
#endif
			
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"1",InAppPurchase);
			
		}
		//......2..........//
		if(GUILayout.Button("2",GUILayout.Width(width), GUILayout.Height(height)))
		{
#if UNITY_STANDALONE_OSX
			// disable current Screen button
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
#endif
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"2",InAppPurchase);
		}
		GUILayout.Label("NonConsumable Purchase");  //2
		//......3..........//
		if(GUILayout.Button("1",GUILayout.Width(width), GUILayout.Height(height)))
		{
#if UNITY_STANDALONE_OSX
			// disable current Screen button
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
#endif
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppNonConsumable,"1",InAppPurchase);
		}
		//......4..........//
		if(GUILayout.Button("2",GUILayout.Width(width), GUILayout.Height(height)))
		{
#if UNITY_STANDALONE_OSX
			// disable current Screen button
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
#endif
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppNonConsumable,"2",InAppPurchase);
		}
		
		//......5..........//
		if(GUILayout.Button("Submit Score _ int",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Score,"100");
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Score,"100.01");
		}
		
		
		
		//......6..........//
		if(GUILayout.Button("Submit Score with LB",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Score,"2_100");
		}
		
		//......7..........//
		if(GUILayout.Button("Submit Achievement",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"1_80");
		}
		
		//......7..........//
		if(GUILayout.Button("Submit Achievement_100%",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"1"); // pass only acheivement number
		}
		
		if(GUILayout.Button("Submit Flurry",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Flurry,"ssss");
		}
		
		
		
		
		GUILayout.EndVertical();
		
		GUILayout.Space(10);
		GUILayout.BeginVertical();
		if(GUILayout.Button("FullAds", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads,"1");
		}
		if(GUILayout.Button("TopBannerAds", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Banner_Top_Ads);
		}
		if(GUILayout.Button("BottomBannerAds", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads);
		}
		if(GUILayout.Button("HideBannerAds", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads);
		}
		if(GUILayout.Button("Show PopUp", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.ShowPopup,"Test_Displayed");
		}
		
		if(GUILayout.Button("Show Score", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Score);
		}
		if(GUILayout.Button("Show Achievent", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Achievement);
		}
		
		if(GUILayout.Button("Like On FaceBook", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.LikeOnFacebook);
		}
		
		if(GUILayout.Button("Follow Twitter", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.FollowOnTwitter);
		}
		if(GUILayout.Button("MoreGames", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.MoreGames);
		}
		
		
		
		GUILayout.EndVertical();
		GUILayout.BeginVertical();
		if(GUILayout.Button("Application Quit", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.ApplicationQuit);
		}
		
		if(GUILayout.Button("Rate Application", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
		}
		
		if(GUILayout.Button("Submit Score _ float",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Score,"100.05");
		}
		
		if(GUILayout.Button("Loading Screen",GUILayout.Width(width), GUILayout.Height(height)))
		{
			
#if UNITY_STANDALONE_OSX
			// disable current Screen button
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
#endif
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.GetInAppData,"",GetInAppData);
		}
		if(GUILayout.Button("Share Screen",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.CaptureScreenShot();
		}
		if(GUILayout.Button("Show Notification",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendNotification(1, 5, "Test 5 Sec", "Working Fine", new Color32(0xff, 0x44, 0x44, 255));
		}
		if(GUILayout.Button("Show Video Ads",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Video_Ads,"",ShowVideoAds);
		}
		if(GUILayout.Button("Open AlphonsoLink", GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.AlphonsoLink);
		}
		if(GUILayout.Button("Show offerWall",GUILayout.Width(width), GUILayout.Height(height)))
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.ShowOfferWall,"",ShowOfferWall);
		}
		GUILayout.Label("Background Ads State : "+((ExternalInterfaceHandler.Instance.ExternalBackGroundAds == eEXTERNAL_BackGround_FullAds.Activate)?"Activate":"Deactivate"));
        if(GUILayout.Button("ToggleGameState",GUILayout.Width(width), GUILayout.Height(height)))
        {
			switch(ExternalInterfaceHandler.Instance.ExternalBackGroundAds)
			{
			case eEXTERNAL_BackGround_FullAds.Activate:
				ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Deactivate;
				break;
			case eEXTERNAL_BackGround_FullAds.Deactivate:
				ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Activate;
				break;
			}
           	
        }

		GUILayout.EndVertical();
		GUILayout.Label("Coins : "+coins); 
		GUILayout.EndHorizontal();
	}
	
	void InAppPurchase(eEXTERNAL_REQ_TYPE reqType , string requestedId , string receivedStatus)
	{
#if UNITY_STANDALONE_OSX
			// enable current Screen button
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StopLoadingScreen);
#endif
		Debug.Log("InAppPurchase().....Data Received..."+receivedStatus);
		
		switch(reqType)
		{
		case eEXTERNAL_REQ_TYPE.InAppConsumable:
			if(receivedStatus == "true")
			{
				switch(requestedId)
				{
					case "1":
					Debug.Log("InAppConsumable: Request 1 completed");
					break;
					case "2":
					Debug.Log("InAppConsumable: Request 2 completed");
					break;
					case "3":
					break;
				}
			}
			else
			{
				// purchase failed..
			}
			break;
		case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
			if(receivedStatus == "true")
			{
				switch(requestedId)
				{
					case "1":
					Debug.Log("InAppNonConsumable: Request 1 completed");
					break;
					case "2":
					Debug.Log("InAppNonConsumable: Request 1 completed");
					break;
					case "3":
					break;
				}
			}
			else
			{
				// purchase failed.
			}
			break;
		default:
			Debug.Log("InAppPurchase()....Invalid received Data");
			break;
			
		}
		
	}
	
	void GetInAppData(eEXTERNAL_REQ_TYPE reqType , string requestedId , string receivedStatus)
	{
		Debug.Log("GetInAppData()....Data Received_"+receivedStatus);
#if UNITY_STANDALONE_OSX
			// enable current Screen button
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StopLoadingScreen);
#endif
		switch(reqType)
		{
			case eEXTERNAL_REQ_TYPE.GetInAppData:
			if(receivedStatus == "true")
			{
				// update currency gui text with stored player prefs string type.
				// use "kimberley Dynamic" for currency value available in resource folder.
			}
			else
			{
				// enable all button except inapp purchase button.
				// leave currency value empty.
			}
			break;
		default:
			Debug.Log("GetInAppData()....Invalid received Data");
			break;
		}
	}
	void ShowVideoAds(eEXTERNAL_REQ_TYPE reqType , string requestedId , string receivedStatus)
	{
		Debug.Log("ShowVideoAds().....Data Received..."+receivedStatus);
		
		switch(reqType)
		{
		case eEXTERNAL_REQ_TYPE.Show_Video_Ads:
			if(receivedStatus == "true")
			{
				// video ads success
			}
			else
			{
				// No Video Ads or canceled in middle
			}
			break;
		default:
			Debug.Log("ShowVideoAds()....Invalid received Data");
			break;
			
		}
		
	}
	void ShowOfferWall(eEXTERNAL_REQ_TYPE reqType , string requestedId , string receivedStatus)
	{
		Debug.Log("ShowOfferWall().....Data Received..."+receivedStatus);
		
		switch(reqType)
		{
		case eEXTERNAL_REQ_TYPE.ShowOfferWall:
			if(receivedStatus == "true")
			{
				// video ads success
			}
			else
			{
				// No Video Ads or canceled in middle
			}
			break;
		default:
			Debug.Log("ShowOfferWall()....Invalid received Data");
			break;
			
		}
		
	}
	void AlphansoUpdate()
	{
		ExternalInterfaceHandler.Instance.AlphonsoType = eEXTERNAL_ALPHONSO_Type.With_Alphonso;
		switch(ExternalInterfaceHandler.Instance.AlphonsoType)
		{
		    case eEXTERNAL_ALPHONSO_Type.Without_Alphonso:
			Debug.Log("Without Alphonso");
			break;
		    case eEXTERNAL_ALPHONSO_Type.With_Alphonso:
			Debug.Log("With Alphonso");
			break;
		}
	}

}
