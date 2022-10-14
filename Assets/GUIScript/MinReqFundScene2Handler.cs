using UnityEngine;
using System.Collections;

public class MinReqFundScene2Handler : GUIItemsManager {

	// Use this for initialization
	RT_Sounds mSound;
	void Start ()
	{
		base.Init();
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
	}
	
	// Update is called once per frame
	public override void OnSelectedEvent(GUIItem item)
	{
	   if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
       {
			if(item.name=="BackButton")
			{
				mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("StageSelectionScreen");
			}
			if(item.name=="CollectFundButton")
			{
				 mSound.PlaySound(mSound._ButtonClick);
				_screenManager.LoadScreen("GetFundScreenStageSelection");
			}
	   }
	}
}
