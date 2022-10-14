using UnityEngine;
using System.Collections;

	
public enum ActionState
{
	ActionNotRunning,
	ActionRun,
	ActionPause,
	ActionComplete,
};

public class CAction : MonoBehaviour 
{

	// Use this for initialization
	//ANIMATION DELEGATE
	public delegate void internalCallBack(CAction pAction);
	public internalCallBack internalAnimationCallBack = null;
	public bool _SelfUpdate = true;
	public float _duration = 1.0f;

	public virtual void runAction()
	{
		
	}
	
	public virtual void pauseAction()
	{
		
	}
	
	public virtual void resumeAction()
	{
		
	}
	
	public virtual void setCompletion(float completion)
	{
		
	}
	
}
