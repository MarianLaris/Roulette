using UnityEngine;
using System.Collections;

public class BaloonText : MonoBehaviour {
	
	
	public float _Duration = 1.0f;
	GameObject mAnimationObj = null;
	// Use this for initialization
	void Start () 
	{
		
		if(mAnimationObj != null)
			StopAllActions();
		
		mAnimationObj = new GameObject();
		mAnimationObj.name = "Animation Object";
			
		CChangeColorMat change = mAnimationObj.AddComponent<CChangeColorMat>();
		change.actionWith(gameObject.GetComponent<GUIText>().material,new Color(1,1,1,0),_Duration);
		
		CMoveTo move = mAnimationObj.AddComponent<CMoveTo>();
		move.actionWith(gameObject,gameObject.transform.position + new Vector3(0,0.1f,0),_Duration);
		
		CSpawnAction spw =  mAnimationObj.AddComponent<CSpawnAction>();
		spw.actionWithActions(change,move);
		
		CCallFunc call = mAnimationObj.AddComponent<CCallFunc>();
		call.actionWithCallBack(onActioncomplete);
		
		CSequence seq = mAnimationObj.AddComponent<CSequence>();
		seq.actionWithActions(spw,call);
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
		if(gameObject.name=="TotalWinningAmount(Clone)(Clone)")
		{
		   RT_DataHandler._TotalWinningAmountText.guiText.text=" "+RT_DataHandler._GrandtotalWinningAmount;
		}
		if(gameObject.name=="AddedFundText(Clone)")
		{
			GameObject.Find("MainMenuScreen(Clone)").transform.FindChild("FundYouHaveText").transform.guiText.text=" "+RT_DataHandler.GetTotalFund();
		}
	//	Debug.Log("Animation complete");
		StopAllActions();
		Destroy(this.gameObject);
	}

}
