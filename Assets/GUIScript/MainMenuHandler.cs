using UnityEngine;
using System.Collections;
using System;


public class MainMenuHandler : GUIItemsManager 
{
	System.DateTime mtime;
	System.DateTime mtimelater;

	System.TimeSpan mdifference;
	
	 public GUIText _Timetext;
	public GameObject _AddedFundText,_Achievment,_AddFundButton,_HighscoreButton,_StatsButton,_ReturnToMenu;
	 public string _TimeFormate;
	 public bool _BonusButtonActive;
	 public GUIItemButton BonusButton;
	 public GUIItemText _TotalFundYouHaveText;
	 RT_Sounds mSound;
	 int mTotalFund;
	 GUITexture mTextView;
	// GUITexture mTextViewObject;
	 GUITexture mTextViewClose;
	 GUITexture mTextViewObjectClose;
	 GUITexture mAchievement;
	 GUITexture mAchievementObject;
	 GUITexture mHighScore;
	 GUITexture mHighScoreObject;
	 bool setViewButton;
	
	 public GameObject _TableViewPrefab;
	 GameObject tableView;
	
	
	 public GameObject DailyBonusCamera;
	public GameObject _Wheel;
	public GameObject _wheelCamera;
	 public int[] mRandonBonusValue;
	  bool bonusStatus = false;
	 
	// Use this for initialization
	void Awake()
	{
		#if UNITY_STANDALONE_OSX 
		_Achievment.SetActive (false);
		_HighscoreButton.SetActive (false);
		_StatsButton.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.47f;
		#else 
		_ReturnToMenu.SetActive (false);
		Custom ();
		#endif 
	}
	void Start () 
	{  
		ExternalInterfaceHandler.Instance.SendRequest (eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads, "", null); //for testing
		ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Activate ;
		mTotalFund= PlayerPrefs.GetInt("TotalFund");

		RT_DataHandler.SetOneTimeInitialFund();
		
		GameObject.Find("SoundManager").audio.volume=RT_DataHandler.GetSoundValue();
		Camera.main.audio.volume=RT_DataHandler.GetMusicValue();

		mtime=RT_DataHandler.GetLastCollectionTime();
		//Debug.Log (mtime);
		mtimelater=mtime.Add(new TimeSpan(0,12,00,00));

	
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		base.Init();
		_TotalFundYouHaveText.guiText.text=""+RT_DataHandler.GetTotalFund();
		//print("TotalFund"+mTotalFund);
		
		//AchievementAndTextView
		mTextView=Resources.Load("AchievementAndTableview/TextView",typeof(GUITexture))as GUITexture;
		mTextView.pixelInset=new Rect(-Screen.width/2, -Screen.height/2, Screen.width, Screen.height);
		//mTextViewObject=Instantiate(mTextView,mTextView.transform.position,mTextView.transform.rotation) as GUITexture;
		
		mTextViewClose=Resources.Load("AchievementAndTableview/TextViewClose",typeof(GUITexture))as GUITexture;
		mTextViewClose.pixelInset=new Rect(-400*Screen.width/1024, -275*Screen.height/768, 267*Screen.width/1024, 72*Screen.height/768);
		

		
		
		if(mTotalFund>=5000 && mTotalFund<10000)
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"thefirstwin1", null);
		if(mTotalFund>=10000 && mTotalFund<20000)
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"jackpot1", null);
		if(mTotalFund>=20000 && mTotalFund<100000 )
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"dollarseverywhere1", null);
		if(mTotalFund>=100000 && mTotalFund<500000 )
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"thegambler1", null);
		if(mTotalFund>=500000 && mTotalFund<=1000000)
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"dollarhunter1", null);
		if(mTotalFund>=1000000 && mTotalFund<2000000 )
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"millionaire1", null);
		if(mTotalFund>=2000000 && mTotalFund<3000000)
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"doublemillionaire1", null);
		if(mTotalFund>=3000000 && mTotalFund<4000000 )
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"casinomaster1", null);
		if(mTotalFund>=4000000 && mTotalFund<5000000)
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"casinohero1", null);
		if(mTotalFund>=5000000 )
		  ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Send_Achievement,"casinoroyale1", null);
		
		
		bonusStatus = BonusHandler.Instance.StartBonus(DailyBonusCamera,mRandonBonusValue,_Wheel,_wheelCamera,CallBack);
		Debug.Log("bonusStatus......"+bonusStatus);
//		bonusStatus = BonusHandler.Instance.StartBonus(DailyBonusCamera,mRandonBonusValue,CallBack);
//		if(RT_DataHandler.GetPurchaseStatus() == 0)
		//ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads,"",null)
	
	}
	
	public override void OnSelectedEvent(GUIItem item)
    {
       
       if(meState==eGUI_ITEMS_MANAGER_STATE.Settled   && mTextViewObjectClose==null && !bonusStatus )
       {
           if(item.name=="PlayButton")
           {
				mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("StageSelectionScreen");
           }
		   if(item.name=="SettingButton")
           {
				ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Activate ;
				 mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("GameSettingScreen");
           }
           if(item.name=="HelpButton")
           {
				ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Activate  ;
				 mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("HelpScreen");
           }
		   if(item.name=="FundResetButton")
           {
				if(RT_DataHandler.GetNotifcation()==0) 
				{
					int Notificationvalue = NotificationHandler.Instance.UpdateNotificationCount(1,true);
					Debug.Log("Notificationvalue "+Notificationvalue);
					ExternalInterfaceHandler.Instance.SendNotification(Notificationvalue,43200,"Bonus is waiting for you !!","Get back and claim!!",new Color32(0xff, 0x44, 0x44, 255),true,false);
					Debug.Log("sent notif........");
				}
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
				 mSound.PlaySound(mSound._CollectNow);
				int isFundYouHave=RT_DataHandler.GetTotalFund();
				RT_DataHandler.SetTotalFund(isFundYouHave+50);//naresh
				Vector3 ispos=Vector3.zero;
				if(Screen.width>=2048)
				{
					Debug.Log("heree");
				 ispos=Camera.main.WorldToViewportPoint(new Vector3(-3.4f*(Screen.width/2048f),2.8f*(Screen.height/1536f),0));
				}
				else
				{
					ispos=Camera.main.WorldToViewportPoint(new Vector3(-4.1f*(Screen.width/1024f),3.7f*(Screen.height/768f),0));
				}
	    		GameObject isAddedFundText=Instantiate(_AddedFundText,new Vector3(ispos.x,ispos.y,3),Quaternion.identity) as GameObject;
			//	Debug.Log(isAddedFundText.name);
				isAddedFundText.GetComponent<BaloonText>()._Duration=1f;
				isAddedFundText.guiText.fontSize=(int)(30*(Screen.height/768f));
				isAddedFundText.guiText.text="+ "+50;//naresh
				//_TotalFundYouHaveText.guiText.text=" "+RT_DataHandler.GetTotalFund();
				BonusButton.setDisabled(true);
				RT_DataHandler.SetLastCollectionTime(System.DateTime.Now);
				mtime=RT_DataHandler.GetLastCollectionTime();
				mtimelater=mtime.Add(new TimeSpan(0,12,00,00));
				Debug.Log(mtimelater);
           }
		   if(item.name=="AddFundButton")
		   {
			#if UNITY_STANDALONE_OSX 
				 mSound.PlaySound(mSound._AddAmount);
				_screenManager.LoadScreen("GetFundMainMAC");

			#else 
				mSound.PlaySound(mSound._AddAmount);
				_screenManager.LoadScreen("GetFundScreenMainNenu");
			#endif 

		   }
		  if(item.name=="Achievment")
			{
			 ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Achievement,"Test_Displayed",null);
	
			}
		 if(item.name=="HighscoreButton")
		  {
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Score,"Test_Displayed",null);

		  }
		 if(item.name=="StatsButton")
		  {
			_screenManager.LoadScreen("GameStatsScreen");
			
		  }
			if(item.name=="Return To Menu")
			{
				ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.ApplicationQuit); 
				Debug.Log ("QUIT GAME");
			}
       }
	}
	public GameObject[] _mCustomButtons;
	void DisableAll()
	{
		for(int i=0; i<_mCustomButtons.Length; i++)
		{
			_mCustomButtons[i].GetComponent<GUIItemButton>().setDisabled(true);
		}
	}
	
	public void EnableAll()
	{
		for(int i=0; i<_mCustomButtons.Length; i++)
		{
			_mCustomButtons[i].GetComponent<GUIItemButton>().setDisabled(false);
		}
	}
	bool isBonusEnabled;
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.Escape) && meState==eGUI_ITEMS_MANAGER_STATE.Settled)
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.ApplicationQuit); 
		}
		if (GameObject.Find("BonusUIButtonScreen4(Clone)") !=null)
		{
			DisableAll();
			//			Debug.Log (" spinnnn");
			isBonusEnabled = true;
		}
		else
		{
			if(isBonusEnabled)
				EnableAll();
			
			isBonusEnabled = false;
		}
		
		
		if(mTextViewObjectClose!=null  && Input.GetMouseButtonDown(0) && mTextViewObjectClose.HitTest (Input.mousePosition) )
		{
			print("TextViewclose");
			Destroy(tableView);
			Destroy(mTextViewObjectClose);
		   // Destroy(mTextViewObject);
		}
		
		//AchieveMentAndTableView
		
		
		double isTimeInSecond=(mtimelater-DateTime.Now).TotalSeconds;

		System.TimeSpan t = System.TimeSpan.FromSeconds(isTimeInSecond); 
		string timerFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
		if(isTimeInSecond<=0)
		{
			_BonusButtonActive=true;
			_Timetext.text="00:00:00";
			if(BonusButton.getDisabled()==true)
				BonusButton.setDisabled(false);
		}
		else
		{
			if(BonusButton.getDisabled()==false)
		 	BonusButton.setDisabled(true);
			_Timetext.text=timerFormatted;
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
			_Achievment.gameObject.SetActive (false );
			_AddFundButton.gameObject.SetActive (false );
			_HighscoreButton.SetActive (false);
			_StatsButton.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.71f;
			break;
		case eEXTERNAL_Build_Type.CustomWithQuit:
			_Achievment.gameObject.SetActive (false );
			_AddFundButton.gameObject.SetActive (false );
			_ReturnToMenu.gameObject.SetActive (true);
			_HighscoreButton.SetActive (false);
			_StatsButton.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.71f;
			break;
		case eEXTERNAL_Build_Type.CustomWithIAP:
			_Achievment.gameObject.SetActive (false );
			_HighscoreButton.SetActive (false);
			_StatsButton.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.71f;
			break;
		case eEXTERNAL_Build_Type.FreeWithOutIAP :
			_AddFundButton.gameObject.SetActive (false );
			_HighscoreButton.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.55f;
			_Achievment.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.69f;
			_StatsButton.GetComponent<MenuItemEntryAnim>()._EndPos.x=0.4f;
			break;

		}

	
	}
	
	
	
	public void CallBack(int receivedBonus)
	{
		//Debug.Log("Call back Received with Bonus : "+receivedBonus);
		bonusStatus=false;
		//PlayerPrefs.SetInt("TotalFund",PlayerPrefs.GetInt("TotalFund")+receivedBonus);
		RT_DataHandler.SetTotalFund(RT_DataHandler.GetTotalFund()+receivedBonus);
		_TotalFundYouHaveText.guiText.text=""+RT_DataHandler.GetTotalFund();
		
	}
}
