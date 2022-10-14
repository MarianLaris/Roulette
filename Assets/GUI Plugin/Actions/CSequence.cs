using UnityEngine;
using System.Collections;

public class CSequence : CAction 
{

	// Use this for initialization
	CAction[] marrActionsArray;
	int actionIndex = 0;
	int actionCount = 0;
	void Start () 
	{
	
	}
	
	public void actionWithActions(params CAction[] actions)
	{
		marrActionsArray = actions;
		actionCount = 0;
		foreach (CAction action in actions)
    	{
        	action.internalAnimationCallBack = onInternalCallBack;
			actionCount++;
    	}
	}
	
	override public void runAction()
	{
		if(actionCount > 0)
		{
			actionIndex = 0;
			//Debug.Log("Action number: "+actionIndex);
			if(marrActionsArray[actionIndex] != null)
				marrActionsArray[actionIndex].runAction();
		}
	}
	
	override public void pauseAction()
	{
		foreach (CAction action in marrActionsArray)
    	{
        	action.pauseAction();
    	}
	}
	
	override public void resumeAction()
	{
		foreach (CAction action in marrActionsArray)
    	{
        	action.resumeAction();
    	}
	}
	
	void onInternalCallBack(CAction action)
	{
		actionIndex++;
		//action.enabled = false;
		if(actionIndex >= actionCount)
		{
			if(internalAnimationCallBack != null)
			internalAnimationCallBack(this);
		}
		else
		{
			//Debug.Log("Action number: "+actionIndex);
			if(marrActionsArray[actionIndex] != null)
				marrActionsArray[actionIndex].runAction();
		}
	}
}
