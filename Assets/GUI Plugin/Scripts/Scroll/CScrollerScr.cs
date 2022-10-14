using UnityEngine;
using System.Collections;

//public enum eScrollType
//{
//	ScorllNone,
//	ScorllVertical,
//	ScorllHorizontal,
//	ScorllAll,
//}

public class CScrollerScr : MonoBehaviour 
{
	// Use this for initialization
	public eScrollType _ScrollType = eScrollType.ScorllVertical;
	public GameObject _Target;
	public GameObject _Pointer;
	public bool _HidePointerOnInactive = false;
	public bool _PointerIsGUI = false;
	public float _Sensitivity = 0.02f;
	public bool _InvertContol;
	public int _NumberOfPages = 1;
	public Rect _Bounds = new Rect(0,0,0,0);
	
	Vector2 pageSize = new Vector2(0,0);	
	Vector3 pointerPos;
	Vector3 cameraBasePos;
	Vector3 clickBasePosition;
	Vector3 clickMovePosition;
	bool mouseActive = false;
	bool inputDisabled = false;
	GameObject animationGameObj = null;
	
	void Start () 
	{
		cameraBasePos = gameObject.transform.position;
		if(_Pointer != null && _HidePointerOnInactive)
			_Pointer.SetActive(false);
		else if(_Pointer != null)
		{
			Screen.showCursor = false;
		}
		if(_NumberOfPages <= 0)
			_NumberOfPages = 1;
		
		pageSize = new Vector2(_Bounds.width/_NumberOfPages,_Bounds.height/_NumberOfPages);
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		HandleMouseInput();
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
	
				clickBasePosition.z = _Target.transform.position.z;
				cameraBasePos = _Target.transform.position;
				
				pointerPos = Input.mousePosition;
				pointerPos.z = 1;
				if(_Pointer != null)
				{
					if(!_PointerIsGUI)
						_Pointer.transform.position = Camera.main.ScreenToWorldPoint(pointerPos);
					else 
						_Pointer.transform.position = Camera.main.ScreenToViewportPoint(pointerPos);
					
					if(_HidePointerOnInactive)
						_Pointer.SetActive(true);					
				}
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			mouseActive = false;
			cameraBasePos = _Target.transform.position;
			CheckBounds();
			if(_Pointer != null && _HidePointerOnInactive)
				_Pointer.SetActive(false);
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
				_Target.transform.position = cameraBasePos + directionFactor * _Sensitivity *(clickBasePosition - clickMovePosition);
				
			}		
		}
		
		if(_Pointer != null)
		{
			pointerPos = Input.mousePosition;
			pointerPos.z = 1;
			
			if(!_PointerIsGUI)
				_Pointer.transform.position = Camera.main.ScreenToWorldPoint(pointerPos);
			else 
				_Pointer.transform.position = Camera.main.ScreenToViewportPoint(pointerPos);
		}
	}
	
	void CheckBounds()
	{
		bool positionWithinBounds = true;
		Vector2 targetPos = _Target.transform.position;
		Vector2 newTargetPos = _Target.transform.position;
		
		//WIDTH BOUNDS
		if(targetPos.x < _Bounds.x)
		{
			newTargetPos.x = _Bounds.x;
			positionWithinBounds = false;
		}
		else if(targetPos.x > _Bounds.x + _Bounds.width)
		{
			newTargetPos.x = _Bounds.x + _Bounds.width;
			positionWithinBounds = false;
		}
		
		//HEIGHT BOUNDS
		if(targetPos.y < _Bounds.y)
		{
			newTargetPos.y = _Bounds.y;
			positionWithinBounds = false;
		}
		else if(targetPos.y > _Bounds.y + _Bounds.height)
		{
			newTargetPos.y = _Bounds.y + _Bounds.height;
			positionWithinBounds = false;
		}
		
		
		if(positionWithinBounds == false)
		{
			stopAllAnimations();
			animationGameObj = new GameObject();
			animationGameObj.name = "ScrollAnimation";
			CMoveTo move = animationGameObj.AddComponent<CMoveTo>();
			move.actionWith(_Target,newTargetPos,1.0f);
			
			CCallFunc call = animationGameObj.AddComponent<CCallFunc>();
			call.actionWithCallBack(callBackAfterMoveTo);
			
			CSequence seq = animationGameObj.AddComponent<CSequence>();
			seq.actionWithActions(move,call);
			seq.runAction();
			
//			CEaseElastic ease = animationGameObj.AddComponent<CEaseElastic>();
//			ease.actionWithAction(move,EaseType.EaseOut);
//			ease.runAction();
		}
		
	}
	
	
	public void moveToPosition(Vector3 pPosition)
	{
		stopAllAnimations();
		
		Vector2 targetPos = pPosition;
		Vector2 newTargetPos = targetPos;
		Vector3 newTargetPos3d = new Vector3(newTargetPos.x,newTargetPos.y,transform.position.z);

		//WIDTH BOUNDS
		if(targetPos.x < _Bounds.x)
			newTargetPos.x = _Bounds.x;
		else if(targetPos.x > _Bounds.x + _Bounds.width)
			newTargetPos.x = _Bounds.x + _Bounds.width;
		
		//HEIGHT BOUNDS
		if(targetPos.y < _Bounds.y)
			newTargetPos.y = _Bounds.y;
		else if(targetPos.y > _Bounds.y + _Bounds.height)
			newTargetPos.y = _Bounds.y + _Bounds.height;
		
		inputDisabled = true;
		animationGameObj = new GameObject();
		animationGameObj.name = "ScrollAnimation";
		
		float animationSpeed = 10;
		float time = Vector3.Magnitude(transform.position - newTargetPos3d)/animationSpeed;
		CMoveTo move = animationGameObj.AddComponent<CMoveTo>();
		move.actionWith(_Target,newTargetPos,time);
		
		CCallFunc call = animationGameObj.AddComponent<CCallFunc>();
		call.actionWithCallBack(callBackAfterMoveTo);
		
		CSequence seq = animationGameObj.AddComponent<CSequence>();
		seq.actionWithActions(move,call);
		seq.runAction();
	}
	
	void callBackAfterMoveTo()
	{
		//Debug.Log("CallBack");
		stopAllAnimations();
		inputDisabled = false;
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
