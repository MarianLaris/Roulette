using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour 
{
	public GameObject[] _GUIItmManagerArr = null;
	GUIItemsManager mcurGUIItemsManager = null;
	string mStrNextScreenToLoad = null;
	bool mbShouldRemove = false;
	
	public delegate void actionComplete();
	public actionComplete onScreenExitComplete = null;
	
	public ArrayList mPersistantGameObjects = new ArrayList();	
	public ArrayList mUnwantedPersistantGameObjects = new ArrayList();
	public string _PersistantObjectState = null;

	void Start () 
	{
		//LOAD THE FIRST SCREEN	
		if(_GUIItmManagerArr != null)
		{
			if(_GUIItmManagerArr.Length <= 0)
				Debug.Log("No screen available in screen manager");
			else
			{
				if(RT_DataHandler._LoadSceneValue==0)
					LoadScreen(_GUIItmManagerArr[0].name);
				else
				{
					LoadScreen(_GUIItmManagerArr[0].name);
					//LoadScreen("MainMenuScreen");
					ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_Banner_Bottom_Ads ,"",null); //for testing
					
				}
			}
		}		
	}
	
	public void AddPersistantObject(GameObject obj)
	{
		mPersistantGameObjects.Add(obj);
		MenuItemEntryAnim anim = obj.GetComponent<MenuItemEntryAnim>();
		if(anim != null)
			anim.beginAnimationWithCallBack(null);
	}
	
	public void AnimateAllPersistantGameObjects()
	{
		//SEND EXIT ANIMATION TO ALL PERSISTANT OBJECTS AND TREAT THEM AS UNWANTED
		foreach(GameObject obj in mPersistantGameObjects)
		{
			mUnwantedPersistantGameObjects.Add(obj);
			MenuItemEntryAnim anim = obj.GetComponent<MenuItemEntryAnim>();
			if(anim != null)
				anim.beginAnimationWithCallBack(null);
		}
	}
	
	public void RemoveAllPersistantGameObjects()
	{
		//SEND EXIT ANIMATION TO ALL PERSISTANT OBJECTS AND TREAT THEM AS UNWANTED
		foreach(GameObject obj in mPersistantGameObjects)
		{
			mUnwantedPersistantGameObjects.Add(obj);
			MenuItemExitAnim anim = obj.GetComponent<MenuItemExitAnim>();
			if(anim != null)
				anim.beginAnimationWithCallBack(RemovePersistantGameObjectCallBack);
		}
		mPersistantGameObjects.RemoveRange(0,mPersistantGameObjects.Count);
		_PersistantObjectState = null;
	}
	
	public void RemovePersistantGameObjectCallBack(GameObject pObj)
	{
		//FIRST REMOVE ITE FORM ARRAY THEN DESTROY IT
		mUnwantedPersistantGameObjects.Remove(pObj);
		DestroyObject(pObj);
	}
	
	public void LoadScreen(string strName)
	{
		//SEARCH FOR PREFAB WITH THIS NAME
		mStrNextScreenToLoad = strName;
		GameObject managerObj = null;
		if(_GUIItmManagerArr == null || _GUIItmManagerArr.Length < 1)
			return;
		foreach(GameObject go in _GUIItmManagerArr)
		{
			if(go.name == strName)
			{
				mStrNextScreenToLoad = go.name;
				managerObj = go;
				break;
			}
		}
		
		//DONT LOAD IF THIS PREFAB DOESNT EXIST
		if(managerObj == null)
			return;
		
		if(mcurGUIItemsManager == null)
		{
			//Debug.Log("Loading screen " +strName);
			//CREATING NEW ITEM MANAGER FROM PREFAB ARRAY
			GameObject go = Instantiate(managerObj, Vector3.zero, Quaternion.identity) as GameObject;
			mcurGUIItemsManager = go.GetComponent<GUIItemsManager>();
			mcurGUIItemsManager.onEnterCompleteCallBack = onEnterCompleteCallBack;
			mcurGUIItemsManager.onExitCompleteCallBack = onExitCompleteCallBack;
			mcurGUIItemsManager._screenManager = this;
		}
		else
		{
			//IF A ITEMS MANAGER EXISTS MAKE IT EXIT
			mcurGUIItemsManager.State = eGUI_ITEMS_MANAGER_STATE.Exiting;
		}	
	}
		
	public void onEnterCompleteCallBack()
	{
		//NEW MENU ENTERED
		//Debug.Log("Entry Complete");
	}
		
	public void onExitCompleteCallBack()
	{
		//CALLED AFTER ALL ITEM ANIMATIONS
		if(mbShouldRemove == false)
		{
			if(mcurGUIItemsManager != null)
			{
				//Debug.Log("Exit Complete");
				Destroy(mcurGUIItemsManager.gameObject);
				mcurGUIItemsManager = null;
			}
			LoadScreen(mStrNextScreenToLoad);
		}
		else
		{
			//SEND MESSAGE THAT THE SCREEN HAS EXITED
			if(onScreenExitComplete != null)
				onScreenExitComplete();
			
			//REMOVING THE GAME OBJECTS
			Destroy(mcurGUIItemsManager.gameObject);
			Destroy(gameObject);
		}
	}
	
	public void closeScreenManager()
	{
		//CLOSE THIS SCREEN MANAGER AFTER ALL ANIMATIONS
		mbShouldRemove = true;
		if(mcurGUIItemsManager != null)
		{
			mcurGUIItemsManager.State = eGUI_ITEMS_MANAGER_STATE.Exiting;
		}
	}
}
