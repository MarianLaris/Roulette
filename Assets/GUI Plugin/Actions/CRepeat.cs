using UnityEngine;
using System.Collections;


//UNAUTORIZED MODIFICATION IS NOT ALLOWED
public class CRepeat : CAction 
{
	// Use this for initialization
    private int _loop = 0;
	private CAction mTargetAction;

 	public void actionWithAction(CAction pAction,int pLoops)
	{
		mTargetAction = pAction;
		mTargetAction.internalAnimationCallBack = OnInternalCallBack;
		_loop = pLoops;
	}
	
	override public void runAction()
	{
		if(mTargetAction != null)
			mTargetAction.runAction();
	}
	
	override public void pauseAction()
	{
		if(mTargetAction != null)
			mTargetAction.pauseAction();
	}
	
	override public void resumeAction()
	{
		if(mTargetAction != null)
			mTargetAction.resumeAction();
	}
	
	void OnInternalCallBack(CAction action)
	{
		Debug.Log("Repeat callback");
		if(_loop != 0)
        {
            if(_loop > 0)
                _loop--;
			
			mTargetAction.runAction();
        }
		else if(_loop == 0)
		{
			if(internalAnimationCallBack != null)
				internalAnimationCallBack(this);
		}
	}
}
