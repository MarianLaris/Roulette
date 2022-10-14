using UnityEngine;
using System.Collections;

public enum eADS_DISPLAY_INTERVAL_TYPE
{
	TimeDelay = 0,
	Counter,
}

public class AdsHandler : MonoBehaviour 
{
	float 			mfPrevAdsShownTime = 0.0f;
	public float 	_fAdsDisplayInterval = 0.0f;
	
	public bool IsAdsDisplayAllowed(eEXTERNAL_REQ_TYPE eExtRequestType)
	{
		if (eExtRequestType != eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads)
			return true;
		if(Time.realtimeSinceStartup - mfPrevAdsShownTime >= _fAdsDisplayInterval)
		{
			_fAdsDisplayInterval = 30f;
			mfPrevAdsShownTime = Time.realtimeSinceStartup;
			return true;
		}
		
		return false;
	}
}
