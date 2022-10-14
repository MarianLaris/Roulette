using UnityEngine;
using System.Collections;

public class CSpawnAction : CAction {

	// Use this for initialization
	CAction[] marrActionsArray;
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
		foreach (CAction action in marrActionsArray)
    	{
        	action.runAction();
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
		actionCount--;
		if(actionCount <= 0)
		{
			if(internalAnimationCallBack != null)
			internalAnimationCallBack(this);
		}
	}
}
