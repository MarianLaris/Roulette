using UnityEngine;
using System.Collections;

public class CScaleTo : CAction {

	// Use this for initialization
	public GameObject _TargetGameObject;
	
	public Vector2 _StartScale = new Vector2(0,0);
	public Vector2 _DestinationScale = new Vector2(0,0);
	
	private Vector2 mvNormalized = new Vector2(0,0);
	private Vector3 mvScale = new Vector3(0,0,0);
	private Transform mGameObjTransform = null;
	float mfDistanceMagnitude = 0.0f;
	float mfMagnitudeDisplacement = 0.0f;
	float mfRate = 0.0f;
	ActionState meState = ActionState.ActionNotRunning;
	
	// Use this for initialization
	void Start ()
	{
		_SelfUpdate = true;
		//internalAnimationCallBack = null;
	}
	
	public void actionWith(GameObject pTargetObject, Vector2 pEndScale,float pDuration)
	{
		_DestinationScale = pEndScale;
		_duration = pDuration;
		_TargetGameObject = pTargetObject;
	}
		
	override public void runAction()
	{
		if(_TargetGameObject == null)
		{
			Debug.Log("Missing target object in action script");
			return;
		}
		mGameObjTransform = _TargetGameObject.transform;
		_StartScale = mGameObjTransform.localScale;
		meState = ActionState.ActionRun;
		mfRate = 1.0f/_duration;
		mGameObjTransform.localScale = new Vector3(_StartScale.x,_StartScale.y,mGameObjTransform.localScale.z);
		mvNormalized = _DestinationScale - _StartScale;
		mfDistanceMagnitude = Mathf.Sqrt(Vector2.SqrMagnitude(mvNormalized));
		mvNormalized = new Vector2(mvNormalized.x/mfDistanceMagnitude,mvNormalized.y/mfDistanceMagnitude);
		mfMagnitudeDisplacement = 0.0f;
		mvScale = mGameObjTransform.localScale;
	}
	
	override public void pauseAction()
	{
		if(meState == ActionState.ActionRun)
			meState = ActionState.ActionPause;
	}
	
	override public void resumeAction()
	{
		if(meState == ActionState.ActionPause)
			meState = ActionState.ActionRun;
	}
	
	override public void  setCompletion(float completion)
	{
		if(mfDistanceMagnitude != 0)
		{
			Vector2 tScale2d;
			tScale2d = _StartScale + mvNormalized * (mfDistanceMagnitude*completion);
			mvScale.x = tScale2d.x;
			mvScale.y = tScale2d.y;
   			mGameObjTransform.localScale = mvScale;
		}
	}
	
	void  performAction()
	{
		if(mfMagnitudeDisplacement >= 0 && mfMagnitudeDisplacement < 1.0f)
		{
			setCompletion(mfMagnitudeDisplacement);
		}
	
		else if(mfMagnitudeDisplacement >= 1.0f)
		{
			mfMagnitudeDisplacement = 1.0f;
			setCompletion(1.0f);
			onActionComplete();
		}
	}
	
	void onActionComplete()
	{
		meState = ActionState.ActionComplete;
		if(internalAnimationCallBack != null)
			internalAnimationCallBack(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_SelfUpdate == true)
		{
			if(meState == ActionState.ActionRun)
			{
				mfMagnitudeDisplacement += Time.deltaTime * mfRate;
				performAction();
			}
		}
	}
}
