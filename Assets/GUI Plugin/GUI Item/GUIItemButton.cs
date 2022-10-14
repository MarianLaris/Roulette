
using UnityEngine;
using System.Collections;

public class GUIItemButton : GUIItem 
{
	public Texture2D _texNormalState;
	public Texture2D _texSelectedState;
	public Texture2D _texHoverState;
	public Texture2D _texDisabledState;
	public bool _HighLiteOnHover = false;
	
	bool mbButtonHover = false;
	bool mbMouseClicked = false;
	bool mbDisabled = false;
	bool mbRespondsToInput = true;
		
	public delegate void clickCallBack();
	public clickCallBack _CallBack = null;
	Vector3 mViewRectPosOffset = new Vector3(0,0,0);
	Camera mCamera = null;
	// Use this for initialization
	void Start () 
	{
		//Debug.Log("Button start");
		meGUIItemType = eGUI_ITEM_TYPE.Button;
		if(mCamera == null)
		{
			mCamera = Camera.main;
			mViewRectPosOffset = new Vector3(mCamera.rect.x*Screen.width,mCamera.rect.y*Screen.height,0);
		}
	}
	
	public void setCamera(Camera pCam)
	{
		mCamera = pCam;
		mViewRectPosOffset = new Vector3(mCamera.rect.x*Screen.width,mCamera.rect.y*Screen.height,0);
		//Debug.Log(mViewRectPosOffset+" name: "+gameObject.name);
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateSizeWithViewPort();
		
		#if UNITY_EDITOR 
			HandleMouseClick();
	    #elif UNITY_IPHONE
			HandleTouch();
		#else
			HandleMouseClick();
	    #endif			
	}
	
	public void setRespondToInput(bool pResponds)
    {
        mbRespondsToInput = pResponds;
    }
	
	
	//HANDLE INPUT TOUCHES
	void HandleTouch()
	{
		if(mCamera == null || mbDisabled || !mbRespondsToInput)
			return;
		
		int touchCount = Input.touchCount;
	    if(touchCount > 0)
	    {
			if(mbDisabled)
				return;
			
			Touch touch = Input.GetTouch(touchCount-1);
			
			//IF THE BUTTON IS NOT YET CLICKED
			if (touch.phase == TouchPhase.Began && mbMouseClicked == false)
	        {
	            if (guiTexture.HitTest(touch.position-(Vector2)mViewRectPosOffset, mCamera))
	            {
	                //SET TO SELECTED TEXTURE
					mbMouseClicked = true;
	                guiTexture.texture = _texSelectedState;
				}
	        }
			
			//IF THE BUTTON IS ALREADY CLICKED
			if (touch.phase == TouchPhase.Ended && mbMouseClicked == true)
	        {
				mbMouseClicked = false;
	            if (guiTexture.HitTest(touch.position-(Vector2)mViewRectPosOffset, mCamera))
	            {
					//PERFORM ACTION
					guiTexture.texture = _texNormalState;
					if(_guiItemsManager !=null)
						_guiItemsManager.OnSelectedEvent(this);	
					else if(_CallBack != null)
						_CallBack();
	            }
	        }	
			
			
			//HIGHLITE ON HOVER
			if (!mbButtonHover)
			{
				//CHECK IF MOUSE IS HOVERING ON BUTTON
				if (guiTexture.HitTest(touch.position-(Vector2)mViewRectPosOffset, mCamera))
					mbButtonHover = true;
				
				if(mbButtonHover)
				{
					//HIGHLITE BUTTON (SELECTED OR HOVER MODE)
					if(mbMouseClicked)
						guiTexture.texture = _texSelectedState;
				}
			}
			
			//IF MOUSE IS ALREADY HOVERING ON BUTTON
			else if (mbButtonHover)
			{
				if (!guiTexture.HitTest(touch.position-(Vector2)mViewRectPosOffset, mCamera))
				{
					//HAPPENS ONLY IF MOUSE MOVES AWAY FROM BUTTON
					guiTexture.texture = _texNormalState;
					mbButtonHover = false;
				}
			}
		}
		
		else
		{
			mbMouseClicked = false;
			guiTexture.texture = _texNormalState;
		}
	}
	
	void HandleMouseClick()
	{
		if(mCamera == null || mbDisabled || !mbRespondsToInput)
			return;
				
		

		//IF THE BUTTON IS NOT YET CLICKED
		if (Input.GetMouseButtonDown(0) && mbMouseClicked == false)
        {
			
            if (guiTexture.HitTest((Input.mousePosition-mViewRectPosOffset), mCamera))
            {
                //SET TO SELECTED TEXTURE
				//Debug.Log("Hit offset "+ (Input.mousePosition-mViewRectPosOffset));
				mbMouseClicked = true;
                guiTexture.texture = _texSelectedState;
			}
        }
		
		//IF THE BUTTON IS ALREADY CLICKED
		if (Input.GetMouseButtonUp(0) && mbMouseClicked == true)
        {
			mbMouseClicked = false;
            if (guiTexture.HitTest((Input.mousePosition-mViewRectPosOffset), mCamera))
            {
				//PERFORM ACTION
				guiTexture.texture = _texNormalState;
				if(_guiItemsManager !=null)
					_guiItemsManager.OnSelectedEvent(this);	
				else if(_CallBack != null)
					_CallBack();
            }
        }
		
		//HIGHLITE ON HOVER
		if (!mbButtonHover)
		{
			//CHECK IF MOUSE IS HOVERING ON BUTTON
			if (guiTexture.HitTest((Input.mousePosition-mViewRectPosOffset), mCamera))
				mbButtonHover = true;
			
			if(mbButtonHover)
			{
				//HIGHLITE BUTTON (SELECTED OR HOVER MODE)
				if(mbMouseClicked)
					guiTexture.texture = _texSelectedState;
				else if(_HighLiteOnHover)
					guiTexture.texture = _texHoverState;
			}
		}
		
		//IF MOUSE IS ALREADY HOVERING ON BUTTON
		else if (mbButtonHover)
		{
			if (!guiTexture.HitTest((Input.mousePosition-mViewRectPosOffset), mCamera))
			{
				//HAPPENS ONLY IF MOUSE MOVES AWAY FROM BUTTON
				guiTexture.texture = _texNormalState;
				mbButtonHover = false;
			}
		}
   	}
	
	public void setDisabled(bool pState)
	{
		mbDisabled = pState;
		if(mbDisabled == true)
			guiTexture.texture = _texDisabledState;
		else
			guiTexture.texture = _texNormalState;
	
		
	}
	
	public bool getDisabled()
	{
		return mbDisabled;	
		
		
	}
}