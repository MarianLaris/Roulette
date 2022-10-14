using UnityEngine;
using System.Collections;
using System;

public class VideoAdsHandler : MonoBehaviour {
	public string Timer;
	DateTime _VideoAdTime;
	public static VideoAdsHandler  instance;
	public int miEnabledButtonSet;
	// Use this for initialization
	void Start () 
	{
		instance = this;
		if(PlayerPrefs.GetString("FirstTime") != "false")
		{
			PlayerPrefs.SetString("FirstTime", "false");
			
			RT_DataHandler.SetVideoAdTime (DateTime.Now.AddHours(24).ToString ());
			//PlayerPrefs.SetString("Sound", "SoundOn");
			
		}
		i = PlayerPrefs.GetInt("PressCount");
	}

	public void CheckPressCount()
	{
		if (i >= 15) 
		{
			if(GameObject.FindGameObjectWithTag ("VideoTag") != null)
			GameObject.FindGameObjectWithTag ("VideoTag").GetComponent<GUIItemButton> ().setDisabled (true);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if(PlayerPrefs.HasKey("VideoAdsTime"))
		{
			if(DateTime.Now <= DateTime.Parse(RT_DataHandler.GetVideoAdTime()))
			{
				DateTime dtTempTime = DateTime.Parse(RT_DataHandler.GetVideoAdTime());
				Timer = (-DateTime.Now.Subtract(dtTempTime).Hours)+":"+(-DateTime.Now.Subtract(dtTempTime).Minutes)+":"+ (-DateTime.Now.Subtract(dtTempTime).Seconds);
			} 
			else if (DateTime.Now > DateTime.Parse(RT_DataHandler.GetVideoAdTime()))
			{
				i = 0;
				PlayerPrefs.SetInt("PressCount",0);
				RT_DataHandler.SetVideoAdTime (DateTime.Now.AddHours(24).ToString ());
				if (GameObject.FindGameObjectWithTag ("VideoTag")!=null )
				{
					GameObject.FindGameObjectWithTag ("VideoTag").GetComponent<GUIItemButton> ().setDisabled (false);
				}
			}
		}
		else
		{
			RT_DataHandler.SetVideoAdTime (DateTime.Now.AddHours(24).ToString ());
		}

		
	} 
	public  void ShowVideoAds()
	{
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Video_Ads ,"1",UpdateRecievedStatus);
	}
	public void UpdateRecievedStatus(eEXTERNAL_REQ_TYPE reqType , string requestedId , string receivedStatus)
	{
		if(receivedStatus == "true")
		{
			switch(reqType)
			{
			case eEXTERNAL_REQ_TYPE.Show_Video_Ads:
				switch(requestedId)
				{
				case "1":
					
					//DataHandler.setFunds (DataHandler.getFunds () + 25);
				
					VideoBtnClicked ();

					break;
					
				}
				break;			
				
			}
			//			_FundText.text = ""+DataHandler.getFunds();
		}
	}
	public  int i;
	public void VideoBtnClicked()
	{
		
		if (i >=15) 
		{ 
			GameObject.FindGameObjectWithTag ("VideoTag").GetComponent<GUIItemButton> ().setDisabled (true);
		} 
		else
		{
			i++;
			PlayerPrefs.SetInt("PressCount",PlayerPrefs.GetInt("PressCount")+1);
			if (i == 15) 
			{
				GameObject.FindGameObjectWithTag ("VideoTag").GetComponent<GUIItemButton> ().setDisabled (true);
			}
			IncreaseFund (20);
			//RT_DataHandler.setFunds (RT_DataHandler.getFunds () + 25);
			if (GameObject.Find ("GetFundScreenMainNenu(Clone)")!=null )
			{
				GameObject.Find ("GetFundScreenMainNenu(Clone)").GetComponent<GetFundHandlerMainMenu>()._TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if (GameObject.Find ("GetFundScreenStageSelection(Clone)")!=null )
			{
				GameObject.Find ("GetFundScreenStageSelection(Clone)").GetComponent<GetFundScreenSSHAndler>()._TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if (GameObject.Find ("GetFundGameplay(Clone)")!=null )
			{
				GameObject.Find ("GetFundGameplay(Clone)").GetComponent<GetFundHandlerGamePlay>()._TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if (GameObject.Find ("GetFundScreen(Clone)")!=null )
			{
				GameObject.Find ("GetFundScreen(Clone)").GetComponent<GetFundHandler>()._TotalFundUHave.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if (GameObject.Find ("minReqFundScene2(Clone)")!=null )
			{
				GameObject.Find ("minReqFundScene2(Clone)").GetComponent<MinReqFundScene1Handler>()._TotalFundYouHaveText.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}
			if (GameObject.Find ("minReqFundScene1(Clone)")!=null )
			{
				GameObject.Find ("minReqFundScene1(Clone)").GetComponent<MinReqFundScene1Handler>()._TotalFundYouHaveText.guiText.text=" "+(RT_DataHandler.GetTotalFund());
			}

		}
		
	}
	void IncreaseFund(int _noOfCoins)
	{
		
		int isfunndYouHave=RT_DataHandler.GetTotalFund();
		RT_DataHandler.SetTotalFund(isfunndYouHave+_noOfCoins);
		
	}
}
