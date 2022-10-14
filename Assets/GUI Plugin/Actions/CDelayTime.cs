using UnityEngine;
using System.Collections;

public class CDelayTime : CAction {

	// Use this for initialization
	float mfCompletion = 0.0f;
	float mfRate = 0.0f;
    ActionState meState = ActionState.ActionNotRunning;
	
	
    override public void runAction()
	{
		mfCompletion = 0.0f;
		meState = ActionState.ActionRun;
		mfRate = 1.0f/_duration;
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
	
	public void actionWithDuration(float duration)
	{
		_duration = duration;
	}
	
	override public void  setCompletion(float completion)
	{
		
	}
	
    // Update is called once per frame
    void Update () 
    {
        if(meState == ActionState.ActionRun)
        {
			mfCompletion += Time.deltaTime * mfRate;
            if(mfCompletion >= 1.0f) 
            {
				meState = ActionState.ActionComplete;
                if(internalAnimationCallBack != null)
					internalAnimationCallBack(this);
            }
        }
    }
}
