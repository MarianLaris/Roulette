using UnityEngine;
using System.Collections;

public class FadeINFadeOUT : MonoBehaviour {

	public float _Duration = 1.0f;
	GameObject mAnimationObj = null;
	// Use this for initialization
	void Start () 
	{
		
		if(mAnimationObj != null)
			StopAllActions();
		
		mAnimationObj = new GameObject();
		mAnimationObj.name = "Animation Object";
		
		CChangeColorGUITexture change1 = mAnimationObj.AddComponent<CChangeColorGUITexture>();
		change1.actionWith(gameObject.GetComponent<GUITexture>(),new Color(1,1,1,0.9f),_Duration);
			
		CChangeColorGUITexture change2 = mAnimationObj.AddComponent<CChangeColorGUITexture>();
		change2.actionWith(gameObject.GetComponent<GUITexture>(),new Color(1,1,1,0),_Duration);
		
		//CMoveTo move = mAnimationObj.AddComponent<CMoveTo>();
		//move.actionWith(gameObject,gameObject.transform.position + new Vector3(0,0.1f,0),_Duration);
		
	
		CSequence seq1 = mAnimationObj.AddComponent<CSequence>();
		seq1.actionWithActions(change1,change2);
		
		CCallFunc call = mAnimationObj.AddComponent<CCallFunc>();
		call.actionWithCallBack(onActioncomplete);
		
		CSequence seq = mAnimationObj.AddComponent<CSequence>();
		seq.actionWithActions(seq1,call);
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
}
