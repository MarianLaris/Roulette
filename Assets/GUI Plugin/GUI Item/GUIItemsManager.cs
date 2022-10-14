using UnityEngine;
using System.Collections;

public enum eGUI_ITEMS_MANAGER_STATE
{
	Enter = 0, 		//On Insantiate
	Settled,		//After AllAnimations
	Exiting,		//On Exit called from Screen manager
	ExitComplete,	//After All Items Exit
}

public class GUIItemsManager : MonoBehaviour 
{
	//Delegate Methods
	public delegate void actionComplete();
	public actionComplete onEnterCompleteCallBack = null;
	public actionComplete onExitCompleteCallBack = null;
	public ScreenManager _screenManager = null;
		
	//State
	protected eGUI_ITEMS_MANAGER_STATE meState = eGUI_ITEMS_MANAGER_STATE.Enter;
	public GameObject[] _menuItems = null;
	int numberOfActiveItems = 0;
	
	// Use this for initialization
	void Start () 
	{
		//Init();	
	}
	
	public void Init()
	{
		numberOfActiveItems = 0;
		//Debug.Log("--- Add Menu Items -----");
		_menuItems = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; i++)
		{
			_menuItems[i] = transform.GetChild(i).gameObject;
		}
		State = eGUI_ITEMS_MANAGER_STATE.Enter;
		
	}
	
	public void OnEnterCallBack(GameObject pGameObject)
	{
		numberOfActiveItems++;	
		if(numberOfActiveItems >= _menuItems.Length)
		{
			State = eGUI_ITEMS_MANAGER_STATE.Settled;
			if(onEnterCompleteCallBack != null)
				onEnterCompleteCallBack();
		}
	}
		
	public void OnExitComplete(GameObject pObject)
	{
		//CHECK IF ALL OBJECTS COMPLETED ACTIONS 
		numberOfActiveItems--;
		if(numberOfActiveItems <= 0)
		{
			//DESTROY CONTAINER OBJECT IF REQUIRED
			OnExitAnimationCompleted();
			State = eGUI_ITEMS_MANAGER_STATE.ExitComplete;
			if(onExitCompleteCallBack != null)
				onExitCompleteCallBack();
		}
	}
	
	//NEED TO OVERRIDE THIS FUNCTION
	public virtual void OnSelectedEvent(GUIItem item)
	{
		//ON CALL BACK FROM THE BUTTONS
		Debug.Log("------- IN base Class.. GUI item selected : " + item.name);
	}
	
	public virtual void OnExitAnimationCompleted()
	{
		
	}

	public eGUI_ITEMS_MANAGER_STATE State
    {
		//Setter Getter For State
        get { return meState; }
        set 
		{ 
			meState = value; 
			UpdateState();
		}
    }

	
	protected void UpdateState()
	{
		switch(meState)
		{
			case eGUI_ITEMS_MANAGER_STATE.Enter:
			//Debug.Log("--- enter state ----");
				if(_menuItems != null)
				{
					for(int i = 0; i< _menuItems.Length;i++)
					{
						GameObject tMenuItem = _menuItems[i];
						MenuItemEntryAnim tMenuEntryAnim = tMenuItem.GetComponent<MenuItemEntryAnim>();
						if(tMenuEntryAnim != null)
							tMenuEntryAnim.beginAnimationWithCallBack(OnEnterCallBack);
					}
				}
				break;
			case eGUI_ITEMS_MANAGER_STATE.Exiting:
				//Do action for exiting
				if(_menuItems != null)
				{
					//ANIMATE ALL ACTIVE
					for(int i = 0; i< _menuItems.Length;i++)
					{
						GameObject tMenuItem = _menuItems[i];
						MenuItemExitAnim tMenuEntryAnim = tMenuItem.GetComponent<MenuItemExitAnim>();
						if(tMenuEntryAnim != null)
							tMenuEntryAnim.beginAnimationWithCallBack(OnExitComplete);
					}
				}
				break;
			default:
				break;
		}
	}
}
