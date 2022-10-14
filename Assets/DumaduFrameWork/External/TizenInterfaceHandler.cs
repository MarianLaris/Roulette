#if UNITY_TIZEN
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

using Tizen.IAP;


public class TizenInterfaceHandler : ExternalInterfaceHandler 
{
	string[] InappIds = null;

	private String ItemGroupInappId = "100000086187";

	void Awake()
	{
		// Add keys in the same order like IOS
		InappIds = new string[] {
			"000000597443",
			"000000597444",
			"000000597445",
			"000000597446"
		};
	}

	public override bool SendRequest(eEXTERNAL_REQ_TYPE eRequestType, string strData, ReceivedCallBack callback)
	{
		if(callback != null)
		{
			mePrevRequestedType = eRequestType;
			mPrevRequestedData = strData;
			OnCallBack = callback;
		}
     
		switch (eRequestType) 
		{
			case eEXTERNAL_REQ_TYPE.Initialize:
			{
				if(!mIsInitialized)
				{
					Initialise();
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
//				if(Time.realtimeSinceStartup - mfPrevTimeOfAdsDisplay > _minTimeDurationBetweenAds)
//				{
//					mtempIsAdsBeingDisplayed = !mtempIsAdsBeingDisplayed;
//					mfPrevTimeOfAdsDisplay = Time.realtimeSinceStartup;
//					if(PlayerPrefs.GetInt("DeerAds")!=1)
//					{
//						Debug.Log("Nouthing Happened");
//					}
//						
//				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.InAppConsumable:
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);
				try{
					StoreProvider storeObj = StoreProvider.Instance;
					IAP_AppRequest appReq = new IAP_AppRequest ();
					appReq.ItemId = InappIds[(int.Parse(strData)-1)];
					appReq.ItemGroupId = ItemGroupInappId;
					storeObj.PurchaseItem (appReq);
					appReq.RequestCallbackHandler = InAppConsumableCallBackHandler;
				}
				catch(Exception e)
				{
					Debug.Log("Error : "+e.Message);
				}
			}
			break;
			case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
			{   
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StartLoadingScreen);

				try
				{
//					StoreProvider storeObj = StoreProvider.Instance;
//					IAP_AppRequest appReq = new IAP_AppRequest ();
//					appReq.ItemId=InappId+strData;
//					appReq.ItemGroupId = InappId+strData;
//					storeObj.PurchaseItem (appReq);
//					appReq.RequestCallbackHandler = InAppNonConsumableCallBackHandler;
				}
				catch(Exception e)
				{
					Debug.Log("Error : "+e.Message);
				}
			}
			break;
		case eEXTERNAL_REQ_TYPE.ApplicationQuit:
			{
				Application.Quit ();
			}
			break;
			case eEXTERNAL_REQ_TYPE.Hide_Banner_Ads:
			case eEXTERNAL_REQ_TYPE.MoreGames:	
			case eEXTERNAL_REQ_TYPE.Show_Achievement:	
			case eEXTERNAL_REQ_TYPE.Show_Score:	
			case eEXTERNAL_REQ_TYPE.Send_Score:	
			case eEXTERNAL_REQ_TYPE.Send_Achievement:	
			case eEXTERNAL_REQ_TYPE.sendLiveTileNotify:
			case eEXTERNAL_REQ_TYPE.ShowPopup:
			case eEXTERNAL_REQ_TYPE.GoogleAnalyticsScreen:
			case eEXTERNAL_REQ_TYPE.GoogleAnalyticsEvent:
			{			     
				Debug.Log("Nouthing Happened");
			}
			break;
			default:
			break;
		}

        return false;
	}
	public void InAppConsumableCallBackHandler(IAP_AppResponse response)
	{
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StopLoadingScreen);
		if (response.ErrorCode == ErrorType.ERROR_ALL_SUCCESS) 
		{
			if (response.RequestCode == RequestType.FUNC_PURCHASE) {
				ExternalInterfaceHandler.Instance.Receiver ("InApp_Consumable_true");
			} else {
				ExternalInterfaceHandler.Instance.Receiver("InApp_Consumable_true");
			}
		} 
		else 
		{
			ExternalInterfaceHandler.Instance.Receiver("InApp_Consumable_true");
		}
	}
	public void InAppNonConsumableCallBackHandler(IAP_AppResponse response)
	{
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.StopLoadingScreen);
		if (response.ErrorCode == ErrorType.ERROR_ALL_SUCCESS) 
		{
			if (response.RequestCode == RequestType.FUNC_PURCHASE) {
				ExternalInterfaceHandler.Instance.Receiver ("InApp_NonConsumable_true");
			} else {
				ExternalInterfaceHandler.Instance.Receiver("InApp_NonConsumable_false");
			}
		}
		else 
		{
			ExternalInterfaceHandler.Instance.Receiver("InApp_NonConsumable_false");
		}
	}
}

#endif
