using UnityEngine;
using System.Collections;

public class ChipUpAnimation : MonoBehaviour {
	
	float mDeltaTime;
	//Vector3 minitialPosition;
	Vector3 mFinalPosition;
	float mfColorValue=1f;
	// Use this for initialization
	float mfBoundaryCondition;
	Color mcolor;
	float mfSHRatio;
	GameObject mAnimationObj = null;
	
	void Start () 
	{
		mfSHRatio=Screen.height/768f;
		GameObject isObj=GameObject.Find("TotalWinningAmount(Clone)");
		mFinalPosition=Camera.main.ViewportToWorldPoint(isObj.transform.position);
		mFinalPosition=new Vector3(mFinalPosition.x,mFinalPosition.y+Screen.height/768f,mFinalPosition.z);
		mfBoundaryCondition=mFinalPosition.y-Screen.height/768f;
		mcolor= renderer.material.color;
		ScaleObject();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(mDeltaTime<=1.0f)
		mDeltaTime+=Time.deltaTime;
		if(mDeltaTime>1){
		
		this.transform.position=Vector3.Lerp(this.transform.position,mFinalPosition,2.5f*Time.deltaTime*mfSHRatio);
		mfColorValue=Mathf.Lerp(mfColorValue,0,5.0f*Time.deltaTime);
		mcolor.a = mfColorValue;
		renderer.material.color = mcolor;
		if(transform.position.y>=mfBoundaryCondition)
		{
				RT_DataHandler._BettedChipCount--;
				if(RT_DataHandler._BettedChipCount==0)
				{
					GameObject obj = GameObject.Find("GamesceneGUIHandler");
					if(obj != null)
					{
						GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
						int isTotalFund=RT_DataHandler.GetTotalFund();
						if(RT_DataHandler._TotalRebettingAmount<=isTotalFund)
							handlerScr.RebetButton.setDisabled(false);
						else
							handlerScr.RebetButton.setDisabled(true);
					}
					RT_DataHandler._TouchDisable=false;
				}
			Destroy(this.gameObject);
		}
	}
	}
	
	
	void ScaleObject()
	{
//		if(mAnimationObj != null)
//			StopAllActions();
		
		mAnimationObj = new GameObject();
		mAnimationObj.name = "Animation Object";
		
		CScaleTo scale1 = mAnimationObj.AddComponent<CScaleTo>();
		scale1.actionWith(gameObject,new Vector2(transform.localScale.x+0.2f,transform.localScale.y+0.2f),0.3f);
			
		CScaleTo scale2 = mAnimationObj.AddComponent<CScaleTo>();
		scale2.actionWith(gameObject,new Vector2(transform.localScale.x-0.2f,transform.localScale.y-0.2f),0.3f);
		
		CScaleTo scale3 = mAnimationObj.AddComponent<CScaleTo>();
		scale3.actionWith(gameObject,new Vector2(transform.localScale.x,transform.localScale.y),0.3f);
		
		//CMoveTo move = mAnimationObj.AddComponent<CMoveTo>();
		//move.actionWith(gameObject,gameObject.transform.position + new Vector3(0,0.1f,0),_Duration);
		
	
		CSequence seq = mAnimationObj.AddComponent<CSequence>();
		seq.actionWithActions(scale1,scale2,scale3);
		
		//CCallFunc call = mAnimationObj.AddComponent<CCallFunc>();
		//call.actionWithCallBack(onActioncomplete);
		
		//CSequence seq = mAnimationObj.AddComponent<CSequence>();
		//seq.actionWithActions(seq1,call);
		
//		CRepeat repeat = mAnimationObj.AddComponent<CRepeat>();
//		repeat.actionWithAction(seq,1);
		
		seq.runAction();
	
		
	}
}
