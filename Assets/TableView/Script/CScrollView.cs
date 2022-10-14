using UnityEngine;
using System.Collections;

public enum eScrollType
{
	ScorllNone,
	ScorllVertical,
	ScorllHorizontal,
	ScorllAll,
}

public enum eScrollState
{
	Scorll_Selected = 0,
	Scorll_Moved,
	Scorll_Thrown,
	Scorll_HitBound,
	Scorll_Settled,
}

[System.Serializable]
public class Maskdata
{
	public GameObject _MaskPrefab;
	public Vector3 _MaskScale;
	public Vector3 _MaskRelativePosition;
}


public class CScrollView : MonoBehaviour 
{
	public Maskdata[] _MaskData;
	public Rect _Bounds = new Rect(0,0,0,0);
	public eScrollType _ScrollType = eScrollType.ScorllVertical;
	public float _Sensitivity = 0.02f;
	public bool _InvertContol = false;
	public bool _BounceEffect = true;
	public float _StaticDrag = 0.8f;
	public float _ScrollSpeedFactor = 2.0f;
		
	ArrayList _MaskArray = new ArrayList();
	Vector3 mvGameObjectInitialPosition;
	Vector3 mvClickBasePosition;
	Vector3 mvClickMovePosition;
	Vector3 mvPercentageDisplacement;
	Vector3 mvTargetPosition;
	Vector3 mvVelocityOfGameObject;
	
	eScrollState meState = eScrollState.Scorll_Settled;
	float mvMinSpeed = 0.05f;
	bool mbMouseActive = false;
	bool mbInputDisabled = false;
	
	//TEMPORARY VALUES
	Vector3 tvPercentageDisplacement;
	Vector3 tvClickMovePosition;
	Vector3 tvClickBasePosition;
	Vector3 tvClickDisplacement;
	
	// Use this for initialization
	void Start () 
	{
		InitScroll();
	}
	
	public void InitScroll () 
	{		
		for(int i = 0; i< _MaskData.Length;i++)
		{
			Maskdata data = _MaskData[i];
			GameObject tMask = Instantiate(data._MaskPrefab,data._MaskRelativePosition,data._MaskPrefab.transform.rotation) as GameObject;
			_MaskArray.Add(tMask);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		//DRAG CAN NEVER BE LESS THAN 1
		if(_StaticDrag < 1)
			_StaticDrag = 1;
		
		HandleMouseInput();
		CheckPercentageScroll();
		
		if(meState == eScrollState.Scorll_Thrown)
			MoveWithVelocity();
		else if(meState == eScrollState.Scorll_HitBound)
			moveTowardsTargetPosition();
		
		if(!mbMouseActive)
			LimitViewToBounds();
	}
	
	void HandleMouseInput()
	{
		if(Input.GetMouseButtonDown(0) && !mbInputDisabled)
	    {
			//WHEN CLICKED AND INPUT IS ACTIVE
			if(mbMouseActive == false)
			{			
				mbMouseActive = true;
				mvVelocityOfGameObject = Vector3.zero;
				tvClickDisplacement = Vector3.zero;
				tvClickBasePosition = Input.mousePosition;
				
				if(_ScrollType == eScrollType.ScorllVertical)
					tvClickBasePosition.x = 0;
				else if(_ScrollType == eScrollType.ScorllHorizontal)
					tvClickBasePosition.y = 0;
	
				meState = eScrollState.Scorll_Selected;
				tvClickBasePosition.z = 0;
				mvGameObjectInitialPosition = gameObject.transform.position;
			}
		}
		else if(Input.GetMouseButtonUp(0))
		{
			//EVEN IF INPUT IS DEACTIVATED LATER
			mbMouseActive = false;			
			if(mvVelocityOfGameObject.magnitude > 0)
				meState = eScrollState.Scorll_Thrown;
			else
				meState = eScrollState.Scorll_Moved;
			
			LimitViewToBounds();
		}
		
		else if(Input.GetMouseButton(0))
		{
			//ELSE IF MOUSE IS MOVING
			if(mbMouseActive == true)
			{
				int directionFactor = 1;
				if(_InvertContol)
					directionFactor = -1;
					
				tvClickMovePosition = Input.mousePosition;
				if(_ScrollType == eScrollType.ScorllVertical)
					tvClickMovePosition.x = 0;
				else if(_ScrollType == eScrollType.ScorllHorizontal)
					tvClickMovePosition.y = 0;
				
				tvClickMovePosition.z = tvClickBasePosition.z;
				Vector3 curClickDisplacement = (tvClickBasePosition - tvClickMovePosition);
				gameObject.transform.position = mvGameObjectInitialPosition + directionFactor * _Sensitivity * (tvClickBasePosition - tvClickMovePosition);
				
				//KEEP ADDING VELOCITY AS LONG AS MOUSE MOVES AND REDUCE IT ACCORDING TO HOW ACTIVE THE MOUSE IS
				Vector3 diffInDisplacement = curClickDisplacement - tvClickDisplacement;
				tvClickDisplacement = curClickDisplacement;
				mvVelocityOfGameObject += directionFactor * _Sensitivity * diffInDisplacement;
					
				if(mvVelocityOfGameObject.magnitude > 0)
					mvVelocityOfGameObject -= (mvVelocityOfGameObject * _StaticDrag *_ScrollSpeedFactor* Time.deltaTime);
			}		
		}	
	}
	
	void CheckPercentageScroll()
	{
		//NORMALIZE THE DISPLACEMENT OF GAME OBJECT FROM ITS ORIGINAL
		//POSITIONS WITH THAT OF ITS BOUNDS
		tvPercentageDisplacement = gameObject.transform.position;
		tvPercentageDisplacement.x -= _Bounds.x;
		tvPercentageDisplacement.y -= _Bounds.y;
		
		//FIND THE DISPLACEMENTS FOR EACH AXIS
		if(_Bounds.width > 0)
			tvPercentageDisplacement.x = tvPercentageDisplacement.x/_Bounds.width;
		else
			tvPercentageDisplacement.x = 0;
		
		if(_Bounds.height > 0)
			tvPercentageDisplacement.y = tvPercentageDisplacement.y/_Bounds.height;
		else
			tvPercentageDisplacement.y = 0;
		
		//LIMIT VALUE BETWEEN 1 AND 0 CAUSE OF BOUNCE EFFECT
		if(tvPercentageDisplacement.x > 1)
			tvPercentageDisplacement.x = 1;
		else if(tvPercentageDisplacement.x < 0)
			tvPercentageDisplacement.x = 0;
		if(tvPercentageDisplacement.y > 1)
			tvPercentageDisplacement.y = 1;
		else if(tvPercentageDisplacement.y < 0)
			tvPercentageDisplacement.y = 0;
	
		//SET THIS VALUE TO ANY REQUIRED DELEGATE
		//Debug.Log("Percentage X: "+tvPercentageDisplacement.x+" Y: "+tvPercentageDisplacement.y);
	}
	
	void LimitViewToBounds()
	{
		//HERE WE CHECK IF THE VIEW HAS MOVED BEYOND BOUNDS
		bool positionWithinBounds = true;
		Vector3 targetPos = gameObject.transform.position;
		Vector3 newTargetPos = gameObject.transform.position;

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
			if(_BounceEffect == false && meState != eScrollState.Scorll_Settled)
			{
				//IN CASE THE BOUNCE EFFECT IS OFF... LIMIT THE POSITION TO BOUNDS
				//SET VELOCITY TO 0 WHEN IT REACHES THE BOUND
				meState = eScrollState.Scorll_Settled;
				mvVelocityOfGameObject = Vector3.zero;
				gameObject.transform.position = newTargetPos;
			}
			else if(_BounceEffect == true && meState != eScrollState.Scorll_HitBound)
			{
				//MOVE TOWARDS THE TARGET POSITION
				meState = eScrollState.Scorll_HitBound;
				mvTargetPosition = newTargetPos;
			}
		}
	}
	
	void moveTowardsTargetPosition()
	{
		//MOVE TOWARDS TARGET POSITION AND SETTLE THERE
		Vector3 positionDiff = mvTargetPosition - gameObject.transform.position;
		if(positionDiff.magnitude > mvMinSpeed && meState != eScrollState.Scorll_Settled)
			gameObject.transform.Translate(positionDiff*_StaticDrag*Time.deltaTime);
		else if(meState != eScrollState.Scorll_Settled)
			meState = eScrollState.Scorll_Settled;
	}
	
	void MoveWithVelocity()
	{
		//MOVE WITH REQUIRED VELOCITY AND SETTLE LATER
		if(mvVelocityOfGameObject.magnitude > mvMinSpeed)
		{
			gameObject.transform.Translate(mvVelocityOfGameObject);
			mvVelocityOfGameObject -= (mvVelocityOfGameObject * _StaticDrag * Time.deltaTime);
		}
		else if(meState != eScrollState.Scorll_Settled)
		{
			meState = eScrollState.Scorll_Settled;
			mvVelocityOfGameObject = Vector3.zero;
		}
	}
	
	void OnDestroy() 
	{
		//Destroy all dependancies
	}
}
