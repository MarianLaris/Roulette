using UnityEngine;
using System.Collections;

public class GameSettingHandler : GUIItemsManager {

	// Use this for initialization
	public GameObject _OptOutOn,_OptOutoff;
	public GameObject[] _mCustomButtons;
	RT_Sounds mSound;
	void Start () 
	{
		base.Init();
		if (ExternalInterfaceHandler.Instance.BuildType == eEXTERNAL_Build_Type.FreeWithAlphonso)
		{
			#if UNITY_ANDROID
			InvokeRepeating("SetMicButtonStatus",0f,0.5f);
			#elif UNITY_IOS
			SetMicButtonStatus();
			#endif
		}
		else
		{
			for(int i =0; i<_mCustomButtons.Length; i++)
				_mCustomButtons[i].SetActive(false);
		}
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		
//		if(RT_DataHandler.GetPurchaseStatus() == 0)
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);
	}

	int mCount;
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState==eGUI_ITEMS_MANAGER_STATE.Settled) 
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
			_screenManager.LoadScreen("MainMenuScreen"); 
		}
	}
	
	public override void OnSelectedEvent(GUIItem item)
    {
       if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
       {
		   if(item.name=="BackButton")
           {
				mSound.PlaySound(mSound._ButtonClick);
			    _screenManager.LoadScreen("MainMenuScreen");
           }

			if(item.name=="OptOutOn")
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.MicStatus,"1",MicCallback);
			}
			else if(item.name=="OptOutOff")
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.MicStatus,"0",MicCallback);
			}
			else if(item.name == "PrivacyPolicy")
			{
				Application.OpenURL("http://dumadu.com/privacy-policy/");
			}

	   }
	}

	void MicCallback(eEXTERNAL_REQ_TYPE requestType,string requestedID,string result){
		SetMicButtonStatus();
	}
	
	void SetMicButtonStatus(){
		bool pIsMicOn = ExternalInterfaceHandler.Instance.IsMicOn;
//		Debug.Log(pIsMicOn);
		if(pIsMicOn){
			_OptOutoff.gameObject.SetActive(true);
			_OptOutOn.gameObject.SetActive(false);
		}
		
		else{
			_OptOutoff.gameObject.SetActive(false);
			_OptOutOn.gameObject.SetActive(true);
		}
	}
}
