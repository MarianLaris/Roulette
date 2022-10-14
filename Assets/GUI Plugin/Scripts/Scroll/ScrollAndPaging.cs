using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScrollerData
{
	public GameObject _IndicatorPrefab;
	public Vector3 _StartPosition;
	public Vector3 _EndPosition;
	
	public bool _alignCenterAccordingToScreen = true;
    public bool _adjustSizeAccordingToScreen = true;
}

[System.Serializable]
public class PagingData
{
	public GameObject _IndicatorPrefab;
	public Vector3 _PositionDiff;
	public Vector3 _BasePosition;
}

public class ScrollAndPaging : GUIItemsManager
{
	//RELEATED TO DELEGATE OBJECT
	public ScrollerData _ScrollData;
	public PagingData _PagingData;
	PagingDelegate _pagingDelegate = null;
	ScrollDelegate _scrollDelegate = null;
	GameObject _Delegate = null;
	
	public eScrollType _ScrollType = eScrollType.ScorllVertical;
	public float _Sensitivity = 0.02f;
	public bool _InvertContol;
	public bool _pagingMode = true;
	public bool _bounceEffect = true;
	public Rect _ViewRect = new Rect(0,0,1,1);
	public int _Pages = 1;
	public GameObject _cameraPrefab;
	
	Rect _Bounds = new Rect(0,0,0,0);
	Vector3 cameraBasePos;
	Vector3 clickBasePosition;
	Vector3 clickMovePosition;
	Vector3 percentageDisplacement;
	bool mouseActive = false;
	bool inputDisabled = false;
	int miCurPage = 1;
	Vector2 mPageSize = new Vector2(0,0);
	GameObject animationGameObj = null;
	GameObject cameraObject;
	
	// Use this for initialization
	public void InitScroll () 
	{		
		base.Init();
		
		//NUMBER OF PAGES CANNOT BE ZERO
		if(_Pages <= 0)
			_Pages = 1;
		
		//SET UP DELEGATE
		if(_Delegate == null)
		{
			if(_pagingMode && _PagingData._IndicatorPrefab != null)
			{
				_Delegate = new GameObject();
				_Delegate.name = "PagingDelegate";
				_Delegate.transform.position = new Vector3(0,0,0);
				
				_pagingDelegate = _Delegate.AddComponent<PagingDelegate>();
				_pagingDelegate._IndicatorPrefab = _PagingData._IndicatorPrefab;
				_pagingDelegate._PositionDiff = _PagingData._PositionDiff;
				_pagingDelegate._BasePosition = _PagingData._BasePosition;
				_pagingDelegate.OnSetNumberOfPages(_Pages);
				_pagingDelegate.OnPageChanged(miCurPage);
			
				if(_ScrollType == eScrollType.ScorllHorizontal)
				{
					_Bounds.width = _Pages;
					_Bounds.height = 0;
				}
				else if(_ScrollType == eScrollType.ScorllVertical)
				{
					_Bounds.height = _Pages;
					_Bounds.width = 0;
				}
				
				mPageSize = new Vector2(_Bounds.width/_Pages,_Bounds.height/_Pages);
			}
			else if(!_pagingMode && _ScrollData._IndicatorPrefab != null)
			{
				_Delegate = new GameObject();
				_Delegate.name = "ScrollDelegate";
				_Delegate.transform.position = new Vector3(0,0,0);
				
				_scrollDelegate = _Delegate.AddComponent<ScrollDelegate>();
				_scrollDelegate._IndicatorPrefab =  _ScrollData._IndicatorPrefab;
				_scrollDelegate._StartPosition =  _ScrollData._StartPosition;
				_scrollDelegate._EndPosition =  _ScrollData._EndPosition;
				
				_scrollDelegate._adjustSizeAccordingToScreen = _ScrollData._adjustSizeAccordingToScreen;
                _scrollDelegate._alignCenterAccordingToScreen = _ScrollData._alignCenterAccordingToScreen;
				
				if(_ScrollType == eScrollType.ScorllHorizontal)
				{
					_Bounds.width = _Pages;
					_Bounds.height = 0;
				}
				else if(_ScrollType == eScrollType.ScorllVertical)
				{
					_Bounds.height = _Pages;
					_Bounds.width = 0;
				}
			}
			else
			{
				//Debug.Log("Missing Indicator prefab in "+gameObject.name);
			}
		}
			
		//SET UP VIEW CAMERA
		Camera viewCam = Camera.main;
		if(_cameraPrefab != null)
		{
			GameObject obj = Instantiate(_cameraPrefab) as GameObject;			
			viewCam = obj.GetComponent<Camera>();
			//viewCam.rect = _ViewRect;
		}
		cameraObject = viewCam.gameObject;
		
		//SET CAMERA TO ALL BUTTONS
		for(int i = 0; i < _menuItems.Length;i++)
		{
			GameObject obj = _menuItems[i];
			GUIItemButton buttonScr = obj.GetComponent<GUIItemButton>();
			if(buttonScr != null)
			{
				buttonScr.setCamera(viewCam);
			}
		}
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		if(meState == eGUI_ITEMS_MANAGER_STATE.Settled)
		{
			HandleMouseInput();
			checkPercentageCompletion();
		}
	}
	
	void HandleMouseInput()
	{
		if (Input.GetMouseButtonDown(0) && !inputDisabled)
	    {
			stopAllAnimations();
			if(mouseActive == false)
			{				
				mouseActive = true;
				clickBasePosition = Input.mousePosition;
				
				if(_ScrollType == eScrollType.ScorllNone)
				{
					clickBasePosition.x = 0;
					clickBasePosition.y = 0;
				}
				else if(_ScrollType == eScrollType.ScorllVertical)
					clickBasePosition.x = 0;
				else if(_ScrollType == eScrollType.ScorllHorizontal)
					clickBasePosition.y = 0;
	
				clickBasePosition.z = gameObject.transform.position.z;
				cameraBasePos = gameObject.transform.position;				
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			mouseActive = false;
			if(_pagingMode)
				HandlePaging();
			else if(_bounceEffect)
				CheckBounds();
		}
		
		else if(Input.GetMouseButton(0))
		{
			if(mouseActive == true)
			{
				int directionFactor = 1;
				if(_InvertContol)
					directionFactor = -1;
					
				clickMovePosition = Input.mousePosition;
				if(_ScrollType == eScrollType.ScorllNone)
				{
					clickMovePosition.x = 0;
					clickMovePosition.y = 0;
				}
				else if(_ScrollType == eScrollType.ScorllVertical)
					clickMovePosition.x = 0;
				else if(_ScrollType == eScrollType.ScorllHorizontal)
					clickMovePosition.y = 0;
				
				clickMovePosition.z = clickBasePosition.z;
				
				if(!_bounceEffect)
					CheckBoundEnforced(cameraBasePos + directionFactor * _Sensitivity *(clickBasePosition - clickMovePosition));
				else
					gameObject.transform.position = cameraBasePos + directionFactor * _Sensitivity *(clickBasePosition - clickMovePosition);
			}		
		}	
	}
	
	void checkPercentageCompletion()
	{
		percentageDisplacement = gameObject.transform.position;
		percentageDisplacement.x -= _Bounds.x;
		percentageDisplacement.y -= _Bounds.y;
		
		if(_Bounds.width > 0)
			percentageDisplacement.x = -1*percentageDisplacement.x/_Bounds.width;
		else
			percentageDisplacement.x = 0;
		
		if(_Bounds.height > 0)
			percentageDisplacement.y = 1*percentageDisplacement.y/_Bounds.height;
		else
			percentageDisplacement.y = 0;
		
		if(_ScrollType == eScrollType.ScorllHorizontal && _scrollDelegate != null)
			_scrollDelegate.OnScrollPercentage(percentageDisplacement.x);
		else if(_ScrollType == eScrollType.ScorllVertical && _scrollDelegate != null)
			_scrollDelegate.OnScrollPercentage(percentageDisplacement.y);
	}
	
	void HandlePaging()
	{		
//		Debug.Log("HandlePaging");
		float diff = 0;
		if(_ScrollType == eScrollType.ScorllVertical)
		{
			diff = clickBasePosition.y - clickMovePosition.y;
			if(diff > 0 && Mathf.Abs(diff) >= 10)
				movePagePrevious(0.4f);////changed here for decreaing page width
			else if(diff < 0 && Mathf.Abs(diff) >= 10)
				movePageNext(0.4f);
			else
				moveSamePage(0.4f);
		}
		else if(_ScrollType == eScrollType.ScorllHorizontal)
		{
			diff = clickBasePosition.x - clickMovePosition.x;
			if(diff < 0 && Mathf.Abs(diff) >= 10)
				movePagePrevious(-0.4f);
			else if(diff > 0 && Mathf.Abs(diff) >= 10)
				movePageNext(-0.4f);
			else
				moveSamePage(-0.4f);
		}
	}
	
	void moveSamePage(float direction)
	{
		Vector3 pageVec = new Vector3(mPageSize.x,mPageSize.y,0);
		moveToPosition(new Vector3(_Bounds.x,_Bounds.y,gameObject.transform.position.z) + pageVec*(miCurPage-1)*direction/Mathf.Abs(direction));
	}
	
	void movePageNext(float direction)
	{

		if(miCurPage < _Pages)
		{
			miCurPage++;			
			Vector3 pageVec = new Vector3(mPageSize.x,mPageSize.y,0);
			Vector3 pos = new Vector3(_Bounds.x,_Bounds.y,gameObject.transform.position.z) + pageVec*(miCurPage-1)*direction/Mathf.Abs(direction);
			moveToPosition(pos);
			
			if(_pagingDelegate != null)
				_pagingDelegate.OnPageChanged(miCurPage);
		}
		else
			moveSamePage(direction);
	}
	
	void movePagePrevious(float direction)
	{
		if(miCurPage > 1)
		{
			miCurPage--;
			Vector3 pageVec = new Vector3(mPageSize.x,mPageSize.y,0);
			moveToPosition(new Vector3(_Bounds.x,_Bounds.y,gameObject.transform.position.z) + pageVec*(miCurPage-1)*direction/Mathf.Abs(direction));
			
			if(_pagingDelegate != null)
				_pagingDelegate.OnPageChanged(miCurPage);
		}
		else
			moveSamePage(direction);
	}
	
	void CheckBoundEnforced(Vector3 requiredPos)
	{
		bool positionWithinBounds = true;
		Vector2 targetPos = requiredPos;
		Vector2 newTargetPos = requiredPos;
		Vector3 correctedPosition = requiredPos;

		//WIDTH BOUNDS
		if((float)targetPos.x > (float)_Bounds.x)
		{
			newTargetPos.x = _Bounds.x;
			positionWithinBounds = false;
		}
		
		if((float)targetPos.x < (float)(_Bounds.x - _Bounds.width + mPageSize.x))
		{
			newTargetPos.x = _Bounds.x - (_Bounds.width-mPageSize.x);
			positionWithinBounds = false;
		}
		
		//HEIGHT BOUNDS
		if(targetPos.y > _Bounds.y + _Bounds.height)
		{
			newTargetPos.y = _Bounds.y + _Bounds.height;
			positionWithinBounds = false;
		}
		
		if(targetPos.y < _Bounds.y )
		{
			newTargetPos.y = _Bounds.y;
			positionWithinBounds = false;
		}
		
		correctedPosition.x = newTargetPos.x;
		correctedPosition.y = newTargetPos.y;
		if(positionWithinBounds == false)
		{
			//stopAllAnimations();
			gameObject.transform.position = correctedPosition;
			//Debug.Log("out of bound");
		}
		else
		{
			gameObject.transform.position = requiredPos;
		}
		
	}
	
	
	void CheckBounds()
	{
		bool positionWithinBounds = true;
		Vector2 targetPos = gameObject.transform.position;
		Vector2 newTargetPos = gameObject.transform.position;
		

		//WIDTH BOUNDS
		if((float)targetPos.x > (float)_Bounds.x)
		{
			newTargetPos.x = _Bounds.x;
			positionWithinBounds = false;
		}
		
		if((float)targetPos.x < (float)(_Bounds.x - _Bounds.width))
		{
			newTargetPos.x = _Bounds.x - _Bounds.width;
			positionWithinBounds = false;
		}
		
		//HEIGHT BOUNDS
		if(targetPos.y > _Bounds.y + _Bounds.height)
		{
			newTargetPos.y = _Bounds.y + _Bounds.height;
			positionWithinBounds = false;
		}
		
		if(targetPos.y < _Bounds.y )
		{
			newTargetPos.y = _Bounds.y;
			positionWithinBounds = false;
		}
		
		if(positionWithinBounds == false)
		{
			stopAllAnimations();
			moveToPosition(new Vector3(newTargetPos.x,newTargetPos.y,0));
		}
		
	}
	
	
	public void moveToPosition(Vector3 pPosition)
	{
		stopAllAnimations();
		
		Vector2 targetPos = pPosition;
		Vector2 newTargetPos = targetPos;
		Vector3 newTargetPos3d = pPosition;
		
		float animationSpeed = 10;
		if(Vector3.Magnitude(transform.position - newTargetPos3d) > 0)
		{
			inputDisabled = true;
			animationGameObj = new GameObject();
			animationGameObj.name = "ScrollAnimation";
			float time = Vector3.Magnitude(transform.position - newTargetPos3d)/animationSpeed;
			CMoveTo move = animationGameObj.AddComponent<CMoveTo>();
			move.actionWith(gameObject,newTargetPos,time);
			
			CCallFunc call = animationGameObj.AddComponent<CCallFunc>();
			call.actionWithCallBack(callBackAfterMoveTo);
			
			CSequence seq = animationGameObj.AddComponent<CSequence>();
			seq.actionWithActions(move,call);
			seq.runAction();
		}
	}
	
	void callBackAfterMoveTo()
	{
		//Debug.Log("CallBack");
		stopAllAnimations();
		inputDisabled = false;
	}
	
	void OnDestroy() 
	{
		stopAllAnimations();
		if(cameraObject != null)
			Destroy(cameraObject);
		
		if(_Delegate != null)
			Destroy(_Delegate);
	}
	
	void stopAllAnimations()
	{
		if(animationGameObj != null)
		{
			Destroy(animationGameObj);
			animationGameObj = null;
		}
	}
}
