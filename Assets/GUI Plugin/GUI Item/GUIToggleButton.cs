using UnityEngine;
using System.Collections;

public class GUIToggleButton : GUIItem 
{
	public Texture[] _TextureForState;
	public string[] _StateNames;
	string mstrObjectName;
	int miCurrentSelectionIndex;
	bool miTouchActive = false;
	
	public bool _RespondsToClick = true;
	public bool _EnableLimitedIndex = true; //Disable next/Previous button after Limit
	public GameObject _toggleNextButton = null; //Should be MenuItemButton
	public GameObject _togglePreviousButton = null;
	public string _playerPrefForStorage = null;
	
	bool mbNextDisabled = false;
	bool mbPreviousDisabled = false;
	
	// Use this for initialization
	void Start () 
	{
		//SET DEFAULT STATE NAME AND TEXTURE
		mstrObjectName = gameObject.name;
		GUITexture tGUITexture = GetComponent<GUITexture>();
		if(_TextureForState == null && tGUITexture != null)
		{
			_TextureForState = new Texture2D[1];
			_TextureForState[0] = tGUITexture.texture;
			
			_StateNames = new string[1];
			_StateNames[0] = mstrObjectName;
		}
		else
		{
			mstrObjectName = _StateNames[0];
			gameObject.name = mstrObjectName;
		}
		
		//SET TEXTURE ACCORDING TO STATE NAME (RETRIEVED FROM PLAYER PREFS)
		if(_playerPrefForStorage != null && _playerPrefForStorage != "")
		{
			string selectedName = PlayerPrefs.GetString(_playerPrefForStorage);
			for(int i=0;i<_StateNames.Length;i++)
			{
				if(_StateNames[i] == selectedName)
				{
					if(_TextureForState.Length > i)
					{
						tGUITexture.texture = _TextureForState[i];
						mstrObjectName = selectedName;
						miCurrentSelectionIndex = i;
					}
				}
			}
		}
		
		//SET UP NEXT BUTTON
		if(_toggleNextButton != null)
		{
			GUIItemButton buttonScr = _toggleNextButton.GetComponent<GUIItemButton>();
			if(buttonScr)
				buttonScr._CallBack = onNextButton;
		}
		
		//SET UP PREVIOUS BUTTON
		if(_togglePreviousButton != null)
		{
			GUIItemButton buttonScr = _togglePreviousButton.GetComponent<GUIItemButton>();
			if(buttonScr)
				buttonScr._CallBack = onPreviousButton;
		}
		
		checkNextPreviousDisableState();
		
		if(_guiItemsManager == null)
		{
			MenuItemEntryAnim tMenuEntryAnim = gameObject.GetComponent<MenuItemEntryAnim>();
			if(tMenuEntryAnim != null)
				tMenuEntryAnim.beginAnimationWithCallBack(null);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateSizeWithViewPort();

	    #if UNITY_IPHONE
			HandleTouch();
		#else
			HandleMouseClick();
	    #endif
	}
	
	
	void HandleTouch()
	{
		// update finger count
		int touchCount = Input.touchCount;
	    if(touchCount > 0)
	    {
			Touch touch = Input.GetTouch(touchCount-1);
			if(_RespondsToClick)
			{
				if (touch.phase == TouchPhase.Began)
				{
					if (guiTexture.HitTest(touch.position, Camera.main) && miTouchActive == false)
					{
						miTouchActive = true;
						onNextButton();
					}
				}
			
				// stop updating the button 
				else if (touch.phase == TouchPhase.Ended)
				{
					if(miTouchActive == true)
						miTouchActive = false;
				}
			}
		}
	}
	
	//HANDLE MOUSE INPUT
	void HandleMouseClick()
	{
		if(_RespondsToClick)
		{
			if (Input.GetMouseButtonDown(0))
	        {
	            if (guiTexture.HitTest(Input.mousePosition, Camera.main) && miTouchActive == false)
	            {
					miTouchActive = true;
					onNextButton();
	            }
	        }
	
	        // stop updating the button 
	        else if (Input.GetMouseButtonUp(0))
	        {
				if(miTouchActive == true)
					miTouchActive = false;
	        }
		}
	}
	
	public void toggleBackToNormalAction()
	{
		if(miCurrentSelectionIndex >= _StateNames.Length)
			gameObject.name = mstrObjectName;
		else
			gameObject.name = _StateNames[miCurrentSelectionIndex];
		
		guiTexture.texture = _TextureForState[miCurrentSelectionIndex];
	}
	
	public void onNextButton()
	{
		//WHEN NEXT BUTTON IS PRESSED
		int nextSelectionIndex = miCurrentSelectionIndex+1;
		nextSelectionIndex %= _TextureForState.Length;
		if(miCurrentSelectionIndex != nextSelectionIndex)
		{
			miCurrentSelectionIndex = nextSelectionIndex;
			guiTexture.texture = _TextureForState[miCurrentSelectionIndex];
			
			if(miCurrentSelectionIndex < _StateNames.Length)
				mstrObjectName = _StateNames[miCurrentSelectionIndex];
			gameObject.name = mstrObjectName;
			
			PlayerPrefs.SetString(_playerPrefForStorage,mstrObjectName);
			checkNextPreviousDisableState();
		}
		
		if(_guiItemsManager != null)
			_guiItemsManager.OnSelectedEvent(this);
	}
	
	public void onPreviousButton()
	{
		//WHEN PREVIOUS BUTTON IS PRESSED
		int previousSelectionIndex = miCurrentSelectionIndex-1;
		if(previousSelectionIndex < 0)
			previousSelectionIndex = (_TextureForState.Length - 1);
		
		if(miCurrentSelectionIndex != previousSelectionIndex)
		{
			miCurrentSelectionIndex = previousSelectionIndex;
			guiTexture.texture = _TextureForState[miCurrentSelectionIndex];
			
			if(miCurrentSelectionIndex < _StateNames.Length)
				mstrObjectName = _StateNames[miCurrentSelectionIndex];
			gameObject.name = mstrObjectName;
			
			PlayerPrefs.SetString(_playerPrefForStorage,mstrObjectName);
			checkNextPreviousDisableState();
		}
		
		if(_guiItemsManager != null)
			_guiItemsManager.OnSelectedEvent(this);
	}
		
	void checkNextPreviousDisableState()
	{
		//CHECKS ALL CONDITIONS TO
		if(!_EnableLimitedIndex)
			return;
		
		if(_TextureForState.Length == 1)
		{
			//IF NO TOGGLE OPTION DISABLE NEXT AND PREVIOUS BUTTON
			if(_toggleNextButton != null)
			{
				mbNextDisabled = true;
				_toggleNextButton.GetComponent<GUIItemButton>().setDisabled(mbNextDisabled);
			}
			if(_togglePreviousButton != null)
			{
				mbPreviousDisabled = true;
				_togglePreviousButton.GetComponent<GUIItemButton>().setDisabled(mbPreviousDisabled);
			}
		}
		else
		{
			//DISABLE OR ENABLE CONDITION FOR NEXT BUTTON
			if(_toggleNextButton != null)
			{
				if(miCurrentSelectionIndex >= (_TextureForState.Length - 1) && !mbNextDisabled)
				{
					mbNextDisabled = true;
					_toggleNextButton.GetComponent<GUIItemButton>().setDisabled(mbNextDisabled);
				}
				else if(mbNextDisabled)
				{
					mbNextDisabled = false;
					_toggleNextButton.GetComponent<GUIItemButton>().setDisabled(mbNextDisabled);
				}
			}
			
			//DISABLE OR ENABLE CONDITION FOR NEXT BUTTON
			if(_togglePreviousButton != null)
			{
				if(miCurrentSelectionIndex <= 0 && !mbPreviousDisabled)
				{
					mbPreviousDisabled = true;
					_togglePreviousButton.GetComponent<GUIItemButton>().setDisabled(mbPreviousDisabled);
				}
				else if(mbPreviousDisabled)
				{
					mbPreviousDisabled = false;
					_togglePreviousButton.GetComponent<GUIItemButton>().setDisabled(mbPreviousDisabled);
				}
			}
		}
	}
}