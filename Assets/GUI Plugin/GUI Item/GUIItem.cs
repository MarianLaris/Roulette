using UnityEngine;
using System.Collections;

public enum eGUI_ITEM_TYPE
{
	Image = 0,
	Text,
	Button,
}

public class GUIItem : MonoBehaviour 
{
	//------------- For Designers only -----------------
	public string _TempScreenToMove = "";
	//--------------------------------------------------
	
	public GUIItemsManager _guiItemsManager;
	public bool _bResizeRelativelyWithScreen = true;
	public bool _bMaintainAspectRatio = true;
	protected eGUI_ITEM_TYPE meGUIItemType;
	
	public float pi_X;
    public float pi_Y;
    public float pi_Width;
    public float pi_Height;
	
    private Rect tempRect;
	private GUITexture mGUITexture; //---- being used for for type image and button
	private GUIText mGUIText;
	private bool mbInitialized = false;
	
	//--------------------------- Exposed Methods -------------------------
	public void Init() 
	{
		mbInitialized = true;
	    mGUITexture = gameObject.guiTexture;
		pi_X = mGUITexture.pixelInset.x;
		pi_Y = mGUITexture.pixelInset.y;
		switch(meGUIItemType)
		{
			case eGUI_ITEM_TYPE.Text:
			case eGUI_ITEM_TYPE.Image:
			case eGUI_ITEM_TYPE.Button:
				InitializeImageBasedItem();
				break;
		}
		
		if(_guiItemsManager == null)
		{
			_guiItemsManager = (GUIItemsManager)gameObject.transform.parent.GetComponent<GUIItemsManager>();
		}
	}
	
	
	public void UpdateSizeWithViewPort()
	{
		switch(meGUIItemType)
		{
			case eGUI_ITEM_TYPE.Image:
			case eGUI_ITEM_TYPE.Button:
			case eGUI_ITEM_TYPE.Text:
				UpdateTextParamsWithViewPort();
			break;
		}
	}
	
	//--------------------------- Private Methods -------------------------
	void InitializeTextBasedItem()
	{
		
	}
	
	void InitializeImageBasedItem()
	{
		pi_Width =  mGUITexture.pixelInset.width;
		pi_Height = mGUITexture.pixelInset.height;
		
		if(_bResizeRelativelyWithScreen)
		{
			pi_Width =  pi_Width * (ResizingDefaultSizes.Instance.DefaultResolutionWidth/Screen.width);
			pi_Height = pi_Height * (ResizingDefaultSizes.Instance.DefaultResolutionHeight/Screen.height);
		}
	}
	
	private void UpdateTextParamsWithViewPort()
	{
		if(_bResizeRelativelyWithScreen && ResizingViewport._instance != null)
		{
			if(!mbInitialized)
				Init();
		    tempRect.x = pi_X * ResizingViewport._instance._ratio;
	        tempRect.y = pi_Y * ResizingViewport._instance._ratio;
	        if (mGUITexture.pixelInset != tempRect)
	            mGUITexture.pixelInset = tempRect;
		
		}
	}
	
	private void UpdateImageParamsWithViewPort()
	{
		if(_bResizeRelativelyWithScreen && ResizingViewport._instance != null)
		{
			if(!mbInitialized)
				Init();
			
			Debug.Log("Resizing here");
		    tempRect.x = pi_X * ResizingViewport._instance._ratio;
	        tempRect.y = pi_Y * ResizingViewport._instance._ratio;
	        tempRect.width = pi_Width * ResizingViewport._instance._ratio;
	        tempRect.height = pi_Height * ResizingViewport._instance._ratio;
	        if (mGUITexture.pixelInset != tempRect)
	            mGUITexture.pixelInset = tempRect;
			
		}
	}
			
	// Update is called once per frame
	void Update () 
	{
		
	}
}
