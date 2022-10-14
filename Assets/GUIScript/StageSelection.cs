using UnityEngine;
using System.Collections;

public class StageSelection : GUIItemsManager {
	
	public GameObject _GameManager,_AddFundButton;
	public GUIItemText _TotalFundYouHaveText;
	RT_Sounds mSound;
	// Use this for initialization
	void Start () 
	{
		Custom();
		base.Init();
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		_TotalFundYouHaveText.guiText.text=" "+RT_DataHandler.GetTotalFund();
		
		if(RT_DataHandler.GetPurchaseStatus() == 0)
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads,"",null);
	}
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState==eGUI_ITEMS_MANAGER_STATE.Settled) 
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
			
			_screenManager.LoadScreen("MainMenuScreen");
			
		}
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
			_AddFundButton.gameObject.SetActive (false );
			break;
		case eEXTERNAL_Build_Type.CustomWithQuit:
			_AddFundButton.gameObject.SetActive (false );
			break;
		case eEXTERNAL_Build_Type.CustomWithIAP:
			_AddFundButton.gameObject.SetActive (true);
			break;
		case eEXTERNAL_Build_Type.FreeWithOutIAP :
			_AddFundButton.gameObject.SetActive (false );
			break;
			
		}
		
		
	}

	
	// Update is called once per frame
	public override void OnSelectedEvent(GUIItem item)
	{
	   if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
       {
           if(item.name=="American")
           {
				//print ("american");
				mSound.PlaySound(mSound._ButtonClick);
				Camera.main.audio.audio.clip=mSound._GameSound;
				Camera.main.audio.Play();
				RT_DataHandler._StageNumber=1;
				VideoAdsHandler.instance.miEnabledButtonSet =1;
				Instantiate(_GameManager,Vector3.zero,Quaternion.identity);
				_screenManager.LoadScreen("GameSceneGUI");
				RT_DataHandler._TotalfundYouHave=RT_DataHandler.GetTotalFund();
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);

           }
		   if(item.name=="European")
           {
				mSound.PlaySound(mSound._ButtonClick);
				Camera.main.audio.audio.clip=mSound._GameSound;
				Camera.main.audio.Play();
				
				if(RT_DataHandler.GetTotalFund()>=100)
				{
					RT_DataHandler._StageNumber = 2;
					VideoAdsHandler.instance.miEnabledButtonSet = 2;
					Instantiate(_GameManager,Vector3.zero,Quaternion.identity);
					_screenManager.LoadScreen("GameSceneGUI");
					RT_DataHandler._TotalfundYouHave=RT_DataHandler.GetTotalFund();
				    ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);

				}
				else{
					_screenManager.LoadScreen("minReqFundScene1");
				}

           }
		   if(item.name=="French")
           {
				mSound.PlaySound(mSound._ButtonClick);
				Camera.main.audio.audio.clip=mSound._GameSound;
				Camera.main.audio.Play();
				
				if(RT_DataHandler.GetTotalFund()>=10000)
				{
					RT_DataHandler._StageNumber=3;
					VideoAdsHandler.instance.miEnabledButtonSet =3;
					Instantiate(_GameManager,Vector3.zero,Quaternion.identity);
					_screenManager.LoadScreen("GameSceneGUI");
					RT_DataHandler._TotalfundYouHave=RT_DataHandler.GetTotalFund();
				    ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);

				}
				else{
					_screenManager.LoadScreen("minReqFundScene2");
				}
				

           }
		  
			if(item.name=="AddFundButton")
            {
			#if UNITY_STANDALONE_OSX  
				mSound.PlaySound(mSound._AddAmount);
				_screenManager.LoadScreen("GetFundSSMAC");
              #else
				mSound.PlaySound(mSound._AddAmount);
				_screenManager.LoadScreen("GetFundScreenStageSelection");
			  #endif 
		

		    }
		   	
			
		   if(item.name=="BackButton")
           {
				 mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("MainMenuScreen");
           }
          
       }
	}
}
