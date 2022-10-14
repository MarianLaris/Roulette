using UnityEngine;
using System.Collections;

public class GetFundSSMAC : GUIItemsManager {
	RT_Sounds mSound;
	public GUIItemText _TotalFundUHave;
	public GUIItemText PurchaceStatusText;
	public static GetFundSSMAC instance;
	public NMFScreenHandler _NMFScreeenHandler;
	public GUIItemButton _Button1; 
	public GUIItemButton _Button2; 
	public GUIItemButton _Button3; 
	public GUIItemButton _Button4; 
	public GUIItemButton _Button5; 
	public GUIItemButton _Button6; 
	public GUIItemButton _Button7;
	public GUIItemButton _Backbutton;
	public GUIText[] _FundValues;
	public bool isEscape;
	void Start ()
	{
		base.Init();
		VideoAdsHandler.instance.CheckPressCount(); 
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);
		instance = this;
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
		if(RT_DataHandler.GetPurchaseStatus()==1)
			PurchaceStatusText.guiText.text="";
		Buttondisable ();
		ExternalInterfaceHandler.Instance.SendRequest (eEXTERNAL_REQ_TYPE.GetInAppData, "", GetInAppData);
		//ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);
	}
	void GetInAppData(eEXTERNAL_REQ_TYPE requestType,string requestedID,string result)
	{
		#if UNITY_STANDALONE_OSX || UNITY_EDITOR
		Buttonenable();
		#endif
		switch(requestType)
		{
		case eEXTERNAL_REQ_TYPE.GetInAppData:
			if(result == "true")
			{
				for(int i = 0; i < _FundValues.Length; i++)
				{
					_FundValues[i].GetComponent<GUIText>().text = "$0.99";
				}
			}
			else
			{
				_screenManager.LoadScreen("StageSelectionScreen");
			}
			break;
		}
	}
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState == eGUI_ITEMS_MANAGER_STATE.Settled &&!isEscape) 
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
			_screenManager.LoadScreen("StageSelectionScreen");
			
		}
	}

	void Buttondisable()
	{
		isEscape = true;
		_Button1.setDisabled(true); 
		_Button2.setDisabled(true); 
		_Button3.setDisabled(true); 
		_Button4.setDisabled(true); 
		_Button5.setDisabled(true); 
		_Button6.setDisabled(true); 
		_Button7.setDisabled(true); 
		_Backbutton.setDisabled(true); 
	}
	void Buttonenable()
	{
		isEscape = false;
		_Button1.setDisabled(false); 
		_Button2.setDisabled(false); 
		_Button3.setDisabled(false); 
		_Button4.setDisabled(false); 
		_Button5.setDisabled(false); 
		_Button6.setDisabled(false); 
		_Button7.setDisabled(false); 
		_Backbutton.setDisabled(false); 
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
			if(item.name=="Button0.99")
			{
				Buttondisable ();
				mSound.PlaySound(mSound._ButtonClick);
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"1",UpdateReceivedStatus);
				//int isfunndYouHave=RT_DataHandler.GetTotalFund();
				//RT_DataHandler.SetTotalFund(isfunndYouHave+5000);
				//Debug.Log(RT_DataHandler.GetTotalFund());
				//_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			
			if(item.name=="Button2.99")
			{
				Buttondisable ();
				mSound.PlaySound(mSound._ButtonClick);
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"2",UpdateReceivedStatus);
				//int isfunndYouHave=RT_DataHandler.GetTotalFund();
				//RT_DataHandler.SetTotalFund(isfunndYouHave+5000);
				//Debug.Log(RT_DataHandler.GetTotalFund());
				//_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if(item.name=="Button4.99")
			{
				Buttondisable ();
				mSound.PlaySound(mSound._ButtonClick);
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"3",UpdateReceivedStatus);
				
				//int isfunndYouHave=RT_DataHandler.GetTotalFund();
				//RT_DataHandler.SetTotalFund(isfunndYouHave+50000);
				//_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if(item.name=="Button9.99")
			{
				Buttondisable ();
				mSound.PlaySound(mSound._ButtonClick);
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"4",UpdateReceivedStatus);
				//int isfunndYouHave=RT_DataHandler.GetTotalFund();
				//RT_DataHandler.SetTotalFund(isfunndYouHave+150000);
				//_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if(item.name=="Button14.99")
			{
				Buttondisable ();
				mSound.PlaySound(mSound._ButtonClick);
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"5",UpdateReceivedStatus);
				//				int isfunndYouHave=RT_DataHandler.GetTotalFund();
				//				 RT_DataHandler.SetTotalFund(isfunndYouHave+400000);
				//				_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			
			if(item.name=="Button19.99")
			{
				Buttondisable ();
				mSound.PlaySound(mSound._ButtonClick);
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"6",UpdateReceivedStatus);
				//				int isfunndYouHave=RT_DataHandler.GetTotalFund();
				//				 RT_DataHandler.SetTotalFund(isfunndYouHave+400000);
				//				_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			
			if(item.name=="Button49.99")
			{
				Buttondisable ();
				mSound.PlaySound(mSound._ButtonClick);
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InAppConsumable,"7",UpdateReceivedStatus);
				//				int isfunndYouHave=RT_DataHandler.GetTotalFund();
				//				 RT_DataHandler.SetTotalFund(isfunndYouHave+400000);
				//				_TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
//			if(item.name=="Button20 free")
//			{
//				mSound.PlaySound(mSound._ButtonClick);
//				//ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Video_Ads,"",ShowVideoAds);
//				VideoAdsHandler.instance.ShowVideoAds ();
//				//IncreaseFund (20);
//			}
		}
	}
//	
//	void ShowVideoAds(eEXTERNAL_REQ_TYPE reqType , string requestedId , string receivedStatus)
//	{
//		Debug.Log("ShowVideoAds().....Data Received..."+receivedStatus);
//		
//		switch(reqType)
//		{
//		case eEXTERNAL_REQ_TYPE.Show_Video_Ads:
//			if(receivedStatus == "true")
//			{
//				IncreaseFund (20);// video ads success
//			}
//			else
//			{
//				// No Video Ads or canceled in middle
//			}
//			break;
//		default:
//			Debug.Log("ShowVideoAds()....Invalid received Data");
//			break;
//			
//		}
//		
//	}
	//Inner Purchage
	public void IncreaseFund(int _noOfCoins)
	{
		
		int isfunndYouHave=RT_DataHandler.GetTotalFund();
		RT_DataHandler.SetTotalFund(isfunndYouHave+_noOfCoins);
		_TotalFundUHave.guiText.text=""+(RT_DataHandler.GetTotalFund());
		
	}
	
	void UpdateReceivedStatus(eEXTERNAL_REQ_TYPE requestType,string requestedID,string result)
	{
		Buttonenable();
		if(result == "true")
		{
			switch(requestType)
			{
			case eEXTERNAL_REQ_TYPE.InAppNonConsumable:
			{
				// put condition to remove ads
				
				
				
			}
				break;
			case eEXTERNAL_REQ_TYPE.InAppConsumable:
			{
				switch(requestedID)
				{
				case "1":
					/// coins
					IncreaseFund(5000);
					break;
				case "2":
					IncreaseFund(15600);
					break;
				case "3":
					IncreaseFund(26000);
					break;
				case "4":
					IncreaseFund(55000);
					break;
				case "5":
					IncreaseFund(90000);
					break;
				case "6":
					IncreaseFund(150000);
					break;
				case "7":
					IncreaseFund(500000);
					break;
				}
			}
				break;
			}
			PurchaceStatusText.guiText.text="";
			RT_DataHandler.SetPurchaseStatus(1);
		}
		
	}	

 
	 
}
