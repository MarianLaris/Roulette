using UnityEngine;
using System.Collections;

public class ChipMovement : MonoBehaviour {
	
	Vector3 minitialPosition;
	Vector3 mFinalPosition;
	float mfSHRatio;
	float mfSWRatio;
	//float mSpeed;
	//float mfstartTime;
	// Use this for initialization
	void Start ()
	{
		mfSHRatio=Screen.height/768f;
		mfSWRatio=Screen.width/1024f;
		//mSpeed=10f;
		//mfstartTime=Time.time;
		minitialPosition=this.GetComponent<ChipProperty>()._intialPosition;
		minitialPosition=new Vector3((-2f+3*0.9f)*mfSWRatio,minitialPosition.y,minitialPosition.z);
		mFinalPosition=transform.position;
		this.transform.position=new Vector3(minitialPosition.x,minitialPosition.y,-0.02f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
			if(this.renderer.enabled==false)
				this.renderer.enabled=true;
		transform.position = Vector3.Lerp(transform.position, mFinalPosition, 8f*Time.deltaTime*mfSHRatio);	
		if(Vector3.Distance(transform.position,mFinalPosition)<0.01f)
		{
				transform.position=mFinalPosition;
				RT_DataHandler._BettedChipCount--;
				if(RT_DataHandler._BettedChipCount==0)
				{
					GameObject obj = GameObject.Find("GamesceneGUIHandler");
					if(obj != null)
					{
						GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
						handlerScr.SpinButton.setDisabled(false);
						handlerScr.ClearButton.setDisabled(false);
					}
				}
				Destroy(this.GetComponent<ChipMovement>());
			}
	}
}
