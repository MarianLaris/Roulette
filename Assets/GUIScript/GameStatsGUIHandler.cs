using UnityEngine;
using System.Collections;

public class GameStatsGUIHandler :  GUIItemsManager {

	// Use this for initialization
	public GameObject _TableViewPrefab;
	GameObject tableView;
	 RT_Sounds mSound;

	void Start () 
	{
		base.Init();
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		if(_TableViewPrefab != null)
			tableView = Instantiate(_TableViewPrefab) as GameObject;
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.Escape) && meState == eGUI_ITEMS_MANAGER_STATE.Settled) 
		{
			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.RateApplication);
			Destroy(tableView);
			_screenManager.LoadScreen("MainMenuScreen");
			
		}
	}
	// Update is called once per frame
	public override void OnSelectedEvent(GUIItem item)
    {
       if(meState==eGUI_ITEMS_MANAGER_STATE.Settled)
       {
		   if(item.name=="BackButton")
           {
				mSound.PlaySound(mSound._ButtonClick);
				Destroy(tableView);
			    _screenManager.LoadScreen("MainMenuScreen");
           }
	   }
	}
}
