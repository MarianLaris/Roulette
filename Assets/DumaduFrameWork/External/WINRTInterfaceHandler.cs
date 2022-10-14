using UnityEngine;
using System.Collections;
using System;

public class WINRTInterfaceHandler : ExternalInterfaceHandler 
{
	public event Action<eEXTERNAL_REQ_TYPE,string>	OnSendRequestEvent;
	private float _minTimeDurationBetweenAds = 30.0f;
	private float mfPrevTimeOfAdsDisplay = 0.0f; 
	private bool mtempIsAdsBeingDisplayed = true;
	
	public override bool SendRequest(eEXTERNAL_REQ_TYPE eRequestType, string strData, ReceivedCallBack callback)
	{

		mePrevRequestedType = eRequestType;
		mPrevRequestedData = strData;
		OnCallBack = callback;
     
		switch (eRequestType) 
		{
			case eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads:
			{
				if(Time.realtimeSinceStartup - mfPrevTimeOfAdsDisplay > _minTimeDurationBetweenAds)
				{
					mtempIsAdsBeingDisplayed = !mtempIsAdsBeingDisplayed;
					mfPrevTimeOfAdsDisplay = Time.realtimeSinceStartup;
					
						if(OnSendRequestEvent != null)
						{
							OnSendRequestEvent(eRequestType,strData);	
						}
				}
				
			}
				break;
			case eEXTERNAL_REQ_TYPE.Show_Banner_Top_Ads:
			case eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads:
			{    
				    //Debug.Log("Ads Calling");
					if(OnSendRequestEvent != null)
					{
						OnSendRequestEvent(eRequestType,strData);
						
					}
			}
			break;
			case eEXTERNAL_REQ_TYPE.ApplicationQuit:
			case eEXTERNAL_REQ_TYPE.Hide_Banner_Ads:
			case eEXTERNAL_REQ_TYPE.MoreGames:	
			case eEXTERNAL_REQ_TYPE.LikeOnFacebook:
			case eEXTERNAL_REQ_TYPE.FollowOnTwitter:
			case eEXTERNAL_REQ_TYPE.InAppConsumable:
			case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
			case eEXTERNAL_REQ_TYPE.Show_Achievement:	
			case eEXTERNAL_REQ_TYPE.Show_Score:	
			case eEXTERNAL_REQ_TYPE.Send_Score:	
			case eEXTERNAL_REQ_TYPE.Send_Achievement:	
			case eEXTERNAL_REQ_TYPE.sendLiveTileNotify:
			case eEXTERNAL_REQ_TYPE.ShowPopup:
			{
			     
				if(OnSendRequestEvent != null)
				{
					OnSendRequestEvent(eRequestType,strData);	
				}
			}
			break;
			default:
			break;
		}

        return false;
	}
	
}
