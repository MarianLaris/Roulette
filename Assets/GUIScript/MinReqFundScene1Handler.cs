using UnityEngine;
using System.Collections;

public class MinReqFundScene1Handler : GUIItemsManager {
	// Use this for initialization
	RT_Sounds mSound;
	public GUIItemText _TotalFundYouHaveText;
	public GameObject _Freecoins,_FundButton, _CustomText;
	 
	void Start ()
	{  
		_CustomText.gameObject.SetActive(false);
		#if UNITY_STANDALONE 
		_Freecoins.gameObject.SetActive(false);
		_FundButton.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.5f;
		#else
		Custom();
		#endif 
		base.Init();
		VideoAdsHandler.instance.CheckPressCount(); 
		 
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		_TotalFundYouHaveText.guiText.text=" "+RT_DataHandler.GetTotalFund();
		//_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
		 

	}

	void Custom()
	{
		switch (ExternalInterfaceHandler.Instance.BuildType)
		{
		case eEXTERNAL_Build_Type.Free: 
			break;
		case eEXTERNAL_Build_Type.Premium:
			break;
		case eEXTERNAL_Build_Type.Custom:
			_Freecoins.gameObject.SetActive(false) ;
			_FundButton.gameObject.SetActive(false) ;
			_CustomText.gameObject.SetActive(true);
			break;
		case eEXTERNAL_Build_Type.CustomWithQuit:
			_Freecoins.gameObject.SetActive(false) ;
			_FundButton.gameObject.SetActive(false) ;
			_CustomText.gameObject.SetActive(true);
			break;
		case eEXTERNAL_Build_Type.CustomWithIAP:
			break;
		case eEXTERNAL_Build_Type.FreeWithOutIAP :
			_FundButton.gameObject.SetActive(false) ;
			_Freecoins.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.5f;
			break;
			
		}
	}
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState == eGUI_ITEMS_MANAGER_STATE.Settled) 
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
			
			_screenManager.LoadScreen("StageSelectionScreen");
			
		}
	}

	
	// Update is called once per frame
	public override void OnSelectedEvent(GUIItem item)
	{
	   if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
       {
			if(item.name=="BackButton")
			{
			     ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
				
				mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("StageSelectionScreen");
			}
			if(item.name=="FundButton")
			{
				#if UNITY_STANDALONE 
				mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("GetFundSSMAC");
				#else
				mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("GetFundScreenStageSelection");
				#endif 

			}
			if(item.name=="Freecoins")
			{
				
				mSound.PlaySound(mSound._ButtonClick);
				//ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Video_Ads,"",ShowVideoAds);
				VideoAdsHandler.instance.ShowVideoAds ();
				
			}

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
				IncreaseFund (20);// video ads success
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
	int coinvalue;
	void IncreaseFund(int _noOfCoins)
	{
		
//		int isfunndYouHave=RT_DataHandler.GetTotalFund();
//		RT_DataHandler.SetTotalFund(isfunndYouHave+_noOfCoins);
//     	RT_DataHandler._TotalfundYouHaveText.guiText.text = "" +(RT_DataHandler.GetTotalFund());
		int isfunndYouHave=RT_DataHandler.GetTotalFund();
		RT_DataHandler.SetTotalFund(isfunndYouHave+_noOfCoins);
		_TotalFundYouHaveText.guiText.text=""+(RT_DataHandler.GetTotalFund());


		
	}
}