using UnityEngine;
using System.Collections;

public class ChipRightAnimation : MonoBehaviour {

	Vector3 mFinalPosition;
	float mfSHRatio;
	float mDeltaTime;
	Color mcolor;
	public float _delay;
	float mfcolorValue;
	// Use this for initialization
	void Start () 
	{
		 mfSHRatio=Screen.height/768f;
		 mFinalPosition=new Vector3(7f*Screen.width/1024f,0,0);
		mfcolorValue=0.9f;
		mcolor=gameObject.renderer.material.color;
		mcolor.r=mfcolorValue;
		mcolor.g=mfcolorValue;
		mcolor.b=mfcolorValue;
		this.gameObject.renderer.material.color=mcolor;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mfcolorValue=Mathf.MoveTowards(mfcolorValue,0.35f,Time.deltaTime);
	
		if(mfcolorValue!=0.35)
		{
				//Debug.Log(mfcolorValue);
			//mcolor=gameObject.renderer.material.color;
			mcolor.r=mfcolorValue;
			mcolor.g=mfcolorValue;
			mcolor.b=mfcolorValue;
			this.gameObject.renderer.material.color=mcolor;
		}
		if(mDeltaTime<=_delay)
		mDeltaTime+=Time.deltaTime;
		if(mDeltaTime>_delay){
			
		transform.position = Vector3.Lerp(transform.position, mFinalPosition, 4f*Time.deltaTime*mfSHRatio);	
		if(Vector3.Distance(transform.position,mFinalPosition)<0.02f)
		{
				transform.position=mFinalPosition;
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
}
