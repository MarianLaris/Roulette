using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionScreenManager : GUIItemsManager {

	// Use this for initialization
	 RT_Sounds mSound;
	
	public GameObject particleEffect;
	public GUIItemButton _SpinButton;
	public GUIItemButton _CloseButton;
	public GameObject _AdditionalFund;
	public GameObject _TotalBonusFund;
	Camera UICam;
	public List<Transform> _bonusButtons = new List<Transform>();
	public List<Transform> _bonusValues = new List<Transform>();
	int selectedBonusIndex = -1;
	
	int miAdditionalFund=0;
	int miTotalBonusFund=0;
	GameObject SoundManager;
	void Start ()
	{
		SoundManager=GameObject.Find("SoundManager");
		if(SoundManager)
		{
			SoundManager.audio.volume=1;
			mSound=SoundManager.GetComponent<RT_Sounds>();
		}
		_AdditionalFund.guiText.text="0";
		_TotalBonusFund.guiText.text="0";
		
		//Animations.Scale(_CloseButton.gameObject,_CloseButton.transform.localScale,_CloseButton.transform.localScale-new Vector3(0.1f,0.1f,0),0.5f,2.5f,_EaseType.linear,2,true);
	base.Init();
		_CloseButton.gameObject.SetActive(false);
//		UICam = _screenManager.camera;
//		if(_bonusValues.Count == 0)
//		{
//			GameObject obj = null;
//			int i = 0;
//			string name = "Bonus"+(i+1)+"Value";
//			obj = transform.Find(name).gameObject;
//			
//			while( obj!= null)
//			{
//				obj.GetComponent<GUIText>().text = BonusHandler.Instance.GetAvailableBonusValue(i) + "";
//				name = "Bonus"+(i+1)+"Button";
//				obj = transform.Find(name).gameObject;
//				_bonusButtons.Add(obj.transform);
//				
//				name = "Bonus"+(i+1)+"Value";
//				obj = transform.Find(name).gameObject;
//				_bonusValues.Add(obj.transform);
//				//name = "Bonus"+(i+1)+"ValueBG";
//				//obj = transform.Find(name).gameObject;
//				
//				
//				
//				_bonusValues[i].gameObject.SetActive(false);
//				obj.SetActive(false);
//				i++;
//				name = "Bonus"+(i+1)+"Value";
//				if(transform.Find(name) == null)
//					break;
//				else
//				obj = transform.Find(name).gameObject;
//			}
//		}
		
	
	}
	
	public override void OnSelectedEvent(GUIItem item)
	{
		if(meState == eGUI_ITEMS_MANAGER_STATE.Settled)
		{
			if(item.name=="SpinButton")
			{
				_SpinButton.setDisabled(true);
				//_CloseButton.setDisabled(true);
				WheelRotation._wheelRotation.StartWheelRotation(fun=>ReturnFundAnimation());
				_SpinButton.GetComponent<Zooming>().enabled=false;
				mSound.PlaySound(mSound._SpinSound);
				//Debug.Log("Spin");
			}
//			if(item.name == "Bonus1Button")
//			{
//				OnBonusButtonClick(0,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//			//	RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus2Button")
//			{
//				OnBonusButtonClick(1,item);
//			//	System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//			 //   RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus3Button")
//			{
//				OnBonusButtonClick(2,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//				//RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus4Button")
//			{
//				OnBonusButtonClick(3,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//				//RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus5Button")
//			{
//				OnBonusButtonClick(4,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//				//RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus6Button")
//			{
//				OnBonusButtonClick(5,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//				//RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus7Button")
//			{
//				OnBonusButtonClick(6,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//				//RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus8Button")
//			{
//				OnBonusButtonClick(7,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//				//RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
//			if(item.name == "Bonus9Button")
//			{
//				OnBonusButtonClick(8,item);
//				//System.TimeSpan timeRemainToFinishADay=RT_DataHandler.RemainingTimeToCompleteDay(System.DateTime.Now);
//			//	RT_DataHandler.createDailyBonusNotification("HI!! YOUR CHANCE TO WIN FREE CHIPS IS HERE !! COME PLAY",timeRemainToFinishADay.Hours,timeRemainToFinishADay.Minutes);
//			}
		
			if(item.name == "CloseButton")
			{
				if(RT_DataHandler.GetNotifcation()==0){
					int Notificationvalue = NotificationHandler.Instance.UpdateNotificationCount(1,true);
					Debug.Log("Notificationvalue "+Notificationvalue);
					ExternalInterfaceHandler.Instance.SendNotification(Notificationvalue,86400,"Your daily reward is ready to be claimed.!","Go check your prize now!!",new Color32(0xff, 0x44, 0x44, 255),true,false);
					Debug.Log("sent notif........");
				}
					//_CloseButton.setDisabled(true);
				SoundManager.audio.volume=0.5f;
				mSound.PlaySound(mSound._CollectNow);
				ClaimTotalFund();
				_screenManager.closeScreenManager();
				WheelAnimation._wheelAnimation.ExitAnimation();
				BonusHandler.Instance.SendCallBack(selectedBonusIndex);
				
			}
		}
	}
	
	void ReturnFundAnimation()
	{
		miAdditionalFund=AdditionalBonus();
		ValueChangeAnimation.StartTextValueChangeAnimation(_AdditionalFund,0,miAdditionalFund,1.5f,TatalFundAnimation);
	}
	void TatalFundAnimation()
	{
		miTotalBonusFund=ReturnSpinFund()+miAdditionalFund;
		ValueChangeAnimation.StartTextValueChangeAnimation(_TotalBonusFund,0,miTotalBonusFund,1.5f,EnableClameButton);
	}
	
	void EnableClameButton()
	{
		_CloseButton.gameObject.SetActive(true);
	}
	int AdditionalBonus()
	{
		PlayerPrefs.SetInt("AdditionalBonus",PlayerPrefs.GetInt("AdditionalBonus")+1);
		if(PlayerPrefs.GetInt("AdditionalBonus")>30)
			PlayerPrefs.SetInt("AdditionalBonus",1);
		return 10*PlayerPrefs.GetInt("AdditionalBonus");
		
	}
	
	int ReturnSpinFund()
	{
		//AdditionalBonus();
		_CloseButton.setDisabled(false);
		int receivedBonus=0;
		switch(WheelRotation._wheelRotation._BlockNumber)
		{
		case 1:
			receivedBonus=75;
			//Debug.Log("100");
			break;
		case 2:
			receivedBonus=0;
			//Debug.Log("200");
			break;
		case 3:
			receivedBonus=20;
			//Debug.Log("300");
			break;
		case 4:
			receivedBonus=50;
			//Debug.Log("400");
			break;
		case 5:
			receivedBonus=10;
			//Debug.Log("500");
			break;
		case 6:
			receivedBonus=5;
			//Debug.Log("600");
			break;
		case 7:
			receivedBonus=25;
			//Debug.Log("700");
			break;
		case 8:
			receivedBonus=100;
			//Debug.Log("800");
			break;
		}
		return receivedBonus;
	}
	
	void ClaimTotalFund()
	{
		RT_DataHandler.SetTotalFund(RT_DataHandler.GetTotalFund()+miTotalBonusFund);
		GameObject MainMenu=GameObject.Find("MainMenuScreen(Clone)");
		if(MainMenu)
		{
			MainMenu.GetComponent<MainMenuHandler>()._TotalFundYouHaveText.guiText.text=""+RT_DataHandler.GetTotalFund();
		}
		else
			Debug.Log("Main Menu Not Found..");
		//return RT_DataHandler.GetTotalFund();
	}
//	public void OnBonusButtonClick(int buttonIndex , GUIItem item)
//	{
//		selectedBonusIndex = buttonIndex;
//		item.GetComponent<GUITexture>().texture = item.GetComponent<GUIItemButton>()._texSelectedState;
//		
//		Vector3 inputPos = Input.mousePosition;
//		
//		inputPos.z = 2;
//		Vector3 worldPos = UICam.ScreenToWorldPoint(inputPos);
//		Instantiate(particleEffect,worldPos,Quaternion.identity);
//		
//		
//		
//		foreach(Transform obj in _bonusButtons)
//		{
//			obj.GetComponent<GUIItemButton>().enabled = false;
//			if(item.name != obj.name)
//			{
//				obj.GetComponent<Zooming>().SetDefaultScale();
//				Destroy(obj.GetComponent<Zooming>());
//			}
//			else
//			{
//				obj.GetComponent<Zooming>()._btwAnimationDelay = 2;
//			}
//			
//			
//		}
//		string clickedObjectValue = item.name.Substring(0,6) + "Value";
//		foreach(Transform obj in _bonusValues)
//		{
//			if(obj.name == clickedObjectValue)
//			{
//				obj.gameObject.SetActive(true);
//				//clickedObjectValue = item.name.Substring(0,6) + "ValueBG";
//				//Transform bg = null;
//				//bg = transform.Find(clickedObjectValue);
//				//bg.gameObject.SetActive(true);
//				_bonusValues.Remove(obj);
//				break;
//			}
//			
//		}
//		
//		
//		
//		
//		//StartCoroutine(ShowOtherBonusBoxes());
//		
//	}
//	
//	IEnumerator ShowOtherBonusBoxes()
//	{
//		yield return new WaitForSeconds(1);
//		foreach(Transform obj in _bonusValues)
//		{
//			yield return new WaitForSeconds(.1f);
//			obj.gameObject.SetActive(true);
//			//transform.Find((obj.name.Substring(0,6)+"ValueBG")).gameObject.SetActive(true);
//		}
//		
////		yield return new WaitForSeconds(.1f);
////		foreach(Transform obj in _bonusValues)
////		{
////			yield return new WaitForSeconds(.1f);
////			obj.gameObject.SetActive(false);
////			//transform.Find((obj.name.Substring(0,6)+"ValueBG")).gameObject.SetActive(false);
////		}
//	}
//	
	
}
