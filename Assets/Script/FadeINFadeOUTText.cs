using UnityEngine;
using System.Collections;

public class FadeINFadeOUTText : MonoBehaviour {
	
	public float _Duration = 1.0f;
	GameObject mAnimationObj = null;
	// Use this for initialization
		void Start () 
	{
		
		if(mAnimationObj != null)
			StopAllActions();
		
		mAnimationObj = new GameObject();
		mAnimationObj.name = "Animation Object";
			
		CChangeColorMat change1 = mAnimationObj.AddComponent<CChangeColorMat>();
		change1.actionWith(gameObject.GetComponent<GUIText>().material,new Color(1,1,1,0.9f),_Duration);
		
		CChangeColorMat change2 = mAnimationObj.AddComponent<CChangeColorMat>();
		change2.actionWith(gameObject.GetComponent<GUIText>().material,new Color(1,1,1,0),_Duration);
		
		CSequence seq1 =  mAnimationObj.AddComponent<CSequence>();
		seq1.actionWithActions(change1,change2);
		
		CCallFunc call = mAnimationObj.AddComponent<CCallFunc>();
		call.actionWithCallBack(onActioncomplete);
		
		CSequence seq = mAnimationObj.AddComponent<CSequence>();
		seq.actionWithActions(seq1,call);
		seq.runAction();
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
		if(gameObject.name=="Winningtext(Clone)")
		{
			GameObject.Find("Switcher").GetComponent<Switcher>()._AnimatioComplete=true;
		}
	//	Debug.Log("Animation complete");
		StopAllActions();
		Destroy(this.gameObject);
	}
}
