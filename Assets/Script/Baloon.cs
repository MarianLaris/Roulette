using UnityEngine;
using System.Collections;

public class Baloon : MonoBehaviour {
	
	public float _Duration = 1.0f;
	GameObject mAnimationObj = null;
	// Use this for initialization
	void Start () 
	{
		
		if(mAnimationObj != null)
			StopAllActions();
		
		mAnimationObj = new GameObject();
		mAnimationObj.name = "Animation Object";
			
		CChangeColorGUITexture change = mAnimationObj.AddComponent<CChangeColorGUITexture>();
		change.actionWith(gameObject.GetComponent<GUITexture>(),new Color(1,1,1,0),_Duration);
		
		CMoveTo move = mAnimationObj.AddComponent<CMoveTo>();
		move.actionWith(gameObject,gameObject.transform.position + new Vector3(0,0.1f,0),_Duration);
		
		CSpawnAction spw =  mAnimationObj.AddComponent<CSpawnAction>();
		spw.actionWithActions(change,move);
		
		CCallFunc call = mAnimationObj.AddComponent<CCallFunc>();
		call.actionWithCallBack(onActioncomplete);
		
		CSequence seq = mAnimationObj.AddComponent<CSequence>();
		seq.actionWithActions(spw,call);
		seq.runAction();
//		
	}
	
	void StopAllActions()
	{
		if(mAnimationObj != null)
		{
			Destroy(mAnimationObj);
			mAnimationObj = null;
		}
	}
	
	void onActioncomplete()
	{
		//ACTIION COMPLETE
		//Debug.Log("Animation complete");
		StopAllActions();
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{

		
	}
}
