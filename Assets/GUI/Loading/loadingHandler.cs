using UnityEngine;
using System.Collections;

public class loadingHandler : GUIItemsManager {

	public GameObject _loadingBar;
	
	public delegate void callBackDelegate();
	public callBackDelegate callBack = null;

	bool sceneChange = false;
	
	public float percentage=0;
	public Texture2D _LoadingTex;
	
	float mfSWRatio;
	float mfSHRatio;

	float _loadingTime;

	void Awake()
	{
//		ExternalInterfaceHandler.Instance.CancelNotification(-1);
//		ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Deactivate ;
//		if(RT_DataHandler.GetPurchaseStatus() == 0)
//			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads,"",null);
//		else if(RT_DataHandler.GetPurchaseStatus() == 1)
//			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);

	}
	
	// Use this for initialization
	void Start () 
	{
		ExternalInterfaceHandler.Instance.ExternalBackGroundAds = eEXTERNAL_BackGround_FullAds.Deactivate ;
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Hide_Banner_Ads,"",null);
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.InitExternalSDK);
		base.Init();
		mfSWRatio=Screen.width/1024f;
		mfSHRatio=Screen.height/768f;
		if(RT_DataHandler._LoadSceneValue==1)
		_screenManager.LoadScreen("MainMenuScreen");
		else 
		{
			#if UNITY_IOS
			_loadingTime = 4f;
			#else
			_loadingTime = 3f;
			#endif

		}
	}

	void Update () 
    {
		if(!sceneChange)
		{
			percentage += Time.deltaTime/_loadingTime;
			percentage = Mathf.Clamp(percentage,0.0f,1.0f);
			
			if(percentage>=1)
			{
				sceneChange = true;
				LoadMainMenu();
			}
		}
    }

	void LoadMainMenu()
	{
		int Notificationvalue = NotificationHandler.Instance.UpdateNotificationCount(1,false);
		ExternalInterfaceHandler.Instance.CancelNotification(-1);
		ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Initialize);
		_screenManager.RemoveAllPersistantGameObjects();
		_screenManager.LoadScreen("MainMenuScreen");
	}

	void OnGUI()
	{
		GUI.BeginGroup(new Rect(291*mfSWRatio,620*mfSHRatio,513*mfSWRatio*percentage,21*mfSHRatio));
		GUI.DrawTexture(new Rect(0,0,513*mfSWRatio, 21*mfSHRatio),_LoadingTex);
		GUI.EndGroup();
	}
	
	
}
