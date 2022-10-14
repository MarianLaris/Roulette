using UnityEngine;
using System.Collections;

public class CCallFunc : CAction 
{
	// Use this for initialization
    public delegate void CallBack();
    public CallBack animationCompleteCallBack = null;
	
    void Start () 
    {
		
    }
	
	//SETTING CALL BACK DELEGATE
	public void actionWithCallBack(CallBack pCompletionCallBack)
	{
		animationCompleteCallBack = pCompletionCallBack;
	}
	
	override public void runAction()
	{
		onActionComplete();
	}
	
	void onActionComplete()
	{
		if(animationCompleteCallBack != null)
			animationCompleteCallBack();
		
		if(internalAnimationCallBack != null)
			internalAnimationCallBack(this);		
	}
}
