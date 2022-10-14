using UnityEngine;
using System.Collections;

public class GamesceneGUIHandler : GUIItemsManager {

	// Use this for initialization
	public GUIItemButton MenuButton;
	public GUIItemButton ClearButton;
	public GUIItemButton RebetButton;
	public GUIItemButton HelpButton;
	public GUIItemButton SpinButton;
	public GUIItemImage BG;
	public Texture[] TextureArray;
	public GameObject[] _HudArray;
	public GUIItemButton _NextButton;
	public GUIItemButton _prevButton;
	public GUIItemButton AddFundButton;
	public bool IsSpinning;
	GameObject [] mChips;
	RT_Sounds mSound;
	
	Zoomcamera zoom;


	
	//public bool _SpinButtonDisabledStatus;
	//public bool _ClearButtonDisabledStatus;
	//public bool _RebetButtonDisabledStatus;
	void Start () 
	{
		Custom();
		ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Deactivate ;
		GameObject Gamemanager=GameObject.Find("GameManager(Clone)");
		mChips=Gamemanager.GetComponent<GameManager>().GetChips();
		//Debug.Log(mChips.Length);
		base.Init();
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		BG.guiTexture.texture=TextureArray[RT_DataHandler._StageNumber-1];
		gameObject.name = "GamesceneGUIHandler";
		if(RT_DataHandler._ClearButtonDisabledStatus==true)
			ClearButton.setDisabled(true);
		else 
			ClearButton.setDisabled(false);
		if(RT_DataHandler._RebetButtonDisabledStatus==true)
			RebetButton.setDisabled(true);
		else
			RebetButton.setDisabled(false);
		if(RT_DataHandler._SpinButtonDisabledStatus==true)
			SpinButton.setDisabled(true);
		else 
			SpinButton.setDisabled(false);
		zoom=GameObject.Find("Camera2").GetComponent<Zoomcamera>();
		zoom.GuiHandler=this.GetComponent<GamesceneGUIHandler>();
		_HudArray[0]=this.transform.FindChild("MenuButton").gameObject;
		_HudArray[1]=this.transform.FindChild("HUDUP").gameObject;
		_HudArray[2]=this.transform.FindChild("HelpButton").gameObject;
		
		zoom._GuiElementArray=_HudArray;
		//Debug.Log(zoom._GuiElementArray[0]);
		VideoAdsHandler.instance.miEnabledButtonSet = VideoAdsHandler.instance.miEnabledButtonSet;
		Debug.Log(VideoAdsHandler.instance.miEnabledButtonSet);
		if(VideoAdsHandler.instance.miEnabledButtonSet==1)
			_prevButton.setDisabled(true);
		else if(VideoAdsHandler.instance.miEnabledButtonSet==3)
			_NextButton.setDisabled(true);
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
			AddFundButton.gameObject.SetActive (false );
			break;
		case eEXTERNAL_Build_Type.CustomWithQuit:
			AddFundButton.gameObject.SetActive (false );
			break;
		case eEXTERNAL_Build_Type.CustomWithIAP:
			AddFundButton.gameObject.SetActive (true);
			break;
		case eEXTERNAL_Build_Type.FreeWithOutIAP :
			AddFundButton.gameObject.SetActive (false );
			break;
			
		}
		
		
	}
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState == eGUI_ITEMS_MANAGER_STATE.Settled && !IsSpinning) 
		{
			
			//_screenManager.LoadScreen("MainMenuScreen");
			RT_DataHandler.RestartGame();
			RT_DataHandler._LoadSceneValue=1;
			Application.LoadLevel(0);
			
		}
	}
 
	public override void OnSelectedEvent(GUIItem item)
	{
	   if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
       {
           if(item.name=="MenuButton")
           {
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads,"2",null);
				//ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Activate;
				mSound.PlaySound(mSound._ButtonClick);
				RT_DataHandler.RestartGame();
				RT_DataHandler._LoadSceneValue=1;
				Application.LoadLevel(0);
           }
		   if(item.name=="SpinButton")
           {
				
				mSound.PlaySound(mSound._Spin);
				ClearButton.setDisabled(true);
				RebetButton.setDisabled(true);
				SpinButton.setDisabled(true);
				MenuButton.setDisabled(true);
				HelpButton.setDisabled(true);
				AddFundButton.setDisabled (true);
				AddFundButton.gameObject.SetActive(false);
				_prevButton.gameObject.SetActive(false);
				_NextButton.gameObject.SetActive(false);
				IsSpinning=true;
				RT_DataHandler._TouchDisable=true;

				Switcher switcher = GameObject.Find("Switcher").GetComponent<Switcher>();
				Camera [] isCameraArray=switcher._CameraArray;
				isCameraArray[0].gameObject.GetComponent<CameraMovment>().enabled=true;
				isCameraArray[1].gameObject.GetComponent<CameraMovment>().enabled=true;
				isCameraArray[2].gameObject.GetComponent<PPCameraMovement>().enabled=true;
				isCameraArray[0].gameObject.GetComponent<CameraMovment>()._forWordMovement=true;
				isCameraArray[1].gameObject.GetComponent<CameraMovment>()._forWordMovement=true;
				isCameraArray[2].gameObject.GetComponent<PPCameraMovement>()._forWordMovement=true;
				Transform isGUiObj=GameObject.Find("GamesceneGUIHandler").transform;
				
				for(int i=0;i<isGUiObj.childCount;i++)
				{
					if(isGUiObj.GetChild(i).tag=="GUI")
					isGUiObj.GetChild(i).GetComponent<GUIMovement>().enabled=true;
					isGUiObj.GetChild(i).GetComponent<GUIMovement>()._forWordMovement=true;
				}
				
				if(RT_DataHandler._StageNumber==1)
				{
					switcher.ActivateWheelAtIndex(0,true);
				}
				else
				{
					switcher.ActivateWheelAtIndex(1,true);	
				}
           }
		   if(item.name=="ClearButton")
           {
				//Debug.Log("dsdssfsf");
				ClearButton.setDisabled(true);
				RebetButton.setDisabled(true);
				mSound.PlaySound(mSound._Clear);
				RT_DataHandler.ClearBettedChipMovment();
				RT_DataHandler.BetClearOnButtonClick();
				RT_DataHandler._TouchDisable=true;
				
           }
		   if(item.name=="RebetButton")
           {
				ClearButton.setDisabled(true);
				RebetButton.setDisabled(true);
				mSound.PlaySound(mSound._ButtonClick);
				RT_DataHandler.Rebet();
				RT_DataHandler.BettedChipsMovement();
				
           }
		   if(item.name=="HelpButton")
           {
				
				mSound.PlaySound(mSound._ButtonClick);
				RT_DataHandler._SpinButtonDisabledStatus=SpinButton.getDisabled();
				RT_DataHandler._RebetButtonDisabledStatus=RebetButton.getDisabled();
				RT_DataHandler._ClearButtonDisabledStatus=ClearButton.getDisabled();
				ClearButton.setDisabled(true);
				RebetButton.setDisabled(true);
				SpinButton.setDisabled(true);
				MenuButton.setDisabled(true);
				HelpButton.setDisabled(true);
				RT_DataHandler._TouchDisable=true;
				_screenManager.LoadScreen("BettingInfoScreen");
           }
			if(item.name=="AddFundButton")
			{
				mSound.PlaySound(mSound._AddAmount);
				RT_DataHandler._SpinButtonDisabledStatus=SpinButton.getDisabled();
				RT_DataHandler._RebetButtonDisabledStatus=RebetButton.getDisabled();
				RT_DataHandler._ClearButtonDisabledStatus=ClearButton.getDisabled();
				ClearButton.setDisabled(true);
				RebetButton.setDisabled(true);
				SpinButton.setDisabled(true);
				MenuButton.setDisabled(true);
				HelpButton.setDisabled(true);
				RT_DataHandler._TouchDisable=true;
        		#if UNITY_STANDALONE 
					_screenManager.LoadScreen("GetFundPlayMAC");
       			#else 
					_screenManager.LoadScreen("GetFundGameplay");
      		   #endif

				//				int.TryParse(RT_DataHandler._TotalfundYouHaveText.guiText.text, out coins);
				//				RT_DataHandler.SetTotalFund(coins);
				
			}
			if(item.name=="NextButton")
			{
				mSound.PlaySound(mSound._ButtonClick);
				VideoAdsHandler.instance.miEnabledButtonSet++;
				int ChipCount=0;
				for(int i=1;i<=3;i++)
				{
					for(int j=1;j<=5;j++)
					{
						if(i==VideoAdsHandler.instance.miEnabledButtonSet)
						{
							mChips[ChipCount].SetActive(true);
							mChips[ChipCount].renderer.enabled=true;
							mChips[ChipCount].collider.enabled=true;
						}
						else
						{
							 mChips[ChipCount].SetActive(false);
						}
			 			  ChipCount++;
					}
				}
				if(VideoAdsHandler.instance.miEnabledButtonSet==3)
				{
					_NextButton.setDisabled(true);
				}
				else
				{
					_NextButton.setDisabled(false);
					_prevButton.setDisabled(false);
				}
			}
			if(item.name=="PrevButton")
			{
				mSound.PlaySound(mSound._ButtonClick);
				VideoAdsHandler.instance.miEnabledButtonSet--;
				int ChipCount=0;
				for(int i=1;i<=3;i++)
				{
					for(int j=1;j<=5;j++)
					{
						if(i==VideoAdsHandler.instance.miEnabledButtonSet)
						{
							mChips[ChipCount].SetActive(true);
							mChips[ChipCount].renderer.enabled=true;
							mChips[ChipCount].collider.enabled=true;
						}
						else
						{
							 mChips[ChipCount].SetActive(false);
						}
			 			  ChipCount++;
					}
				}
				if(VideoAdsHandler.instance.miEnabledButtonSet==1)
				{
					_prevButton.setDisabled(true);
				}
				else
				{
					_NextButton.setDisabled(false);
					_prevButton.setDisabled(false);
				}
			}
			
		}
	}
}
