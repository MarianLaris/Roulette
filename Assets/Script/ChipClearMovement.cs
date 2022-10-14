using UnityEngine;
using System.Collections;

public class ChipClearMovement : MonoBehaviour {
	
	//Vector3 minitialPosition;
	Vector3 mFinalPosition;
	float mfSHRatio;
	float mfSWRatio;
	// Use this for initialization
	void Start () 
	{
		 mfSHRatio=Screen.height/768f;
		mfSWRatio=Screen.width/1024f;
		mFinalPosition=this.GetComponent<ChipProperty>()._intialPosition;
		mFinalPosition=new Vector3((-2f+3*0.9f)*mfSWRatio,mFinalPosition.y,mFinalPosition.z);
//		
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position=Vector3.Lerp(this.transform.position,mFinalPosition,8f*Time.deltaTime*mfSHRatio);
		
		if(Vector3.Distance(transform.position,mFinalPosition)<0.01f)
		{
				transform.position=mFinalPosition;
				RT_DataHandler._BettedChipCount--;
				if(RT_DataHandler._BettedChipCount==0)
				{
					RT_DataHandler._TouchDisable=false;
				}
			Destroy(this.gameObject);
		}
	}
}
