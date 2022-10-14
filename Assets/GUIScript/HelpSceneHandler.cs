using UnityEngine;
using System.Collections;

public class HelpSceneHandler : GUIItemsManager
{
	public GameObject _HelpSubScreen;
	public GameObject _Camera;
	public GameObject _Slider;
	
	GameObject mHelpSubScreen;
	GameObject mCamera; 
	 
	RT_Sounds mSound;
	
	void Start ()
	{
		base.Init();
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		mHelpSubScreen=Instantiate(_HelpSubScreen) as GameObject;
	//mCamera=Instantiate(_Camera) as GameObject;
		
		if(RT_DataHandler.GetPurchaseStatus() == 0)
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads,"",null);
		//_Slider.transform.position=new Vector3(_Slider.transform.position.x*(Screen.width/1024f),_Slider.transform.position.y*(Screen.height/768f),_Slider.transform.position.z);
	
	}
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState==eGUI_ITEMS_MANAGER_STATE.Settled) 
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
			
			_screenManager.LoadScreen("MainMenuScreen");
			mHelpSubScreen.GetComponent<ScreenManager>().closeScreenManager();
			
		}
	}
	
	
   public override void OnSelectedEvent(GUIItem item)
   {
       
       if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
       {
           if(item.name=="BackButton")
           {
				
				_screenManager.LoadScreen("MainMenuScreen");
				mHelpSubScreen.GetComponent<ScreenManager>().closeScreenManager();
				mSound.PlaySound(mSound._ButtonClick);
								
				//Destroy(mHelpSubScreen);
				//Destroy(mCamera);
           }
       }
	}

}
