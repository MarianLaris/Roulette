using UnityEngine;
using System.Collections;

public class BettingInfoHandler : GUIItemsManager {

	// Use this for initialization
	RT_Sounds mSound;
	void Start ()
	{
		base.Init();
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
	}
	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState == eGUI_ITEMS_MANAGER_STATE.Settled) 
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
			RT_DataHandler._TouchDisable=false;
			_screenManager.LoadScreen("GameSceneGUI");
			
		}
	}
	
	public override void OnSelectedEvent(GUIItem item)
	{
	    if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
        {
           if(item.name=="BackButton")
			{
				 mSound.PlaySound(mSound._ButtonClick);
				 RT_DataHandler._TouchDisable=false;
				_screenManager.LoadScreen("GameSceneGUI");
           }
		}
	}
}
