using UnityEngine;
using System.Collections;

public class ChipStartUpAnimation : MonoBehaviour {

	// Use this for initialization
	int miTime;
	float mfSHRatio;
	 Vector3 mInitialPosition;
	void Start () 
	{
			mfSHRatio=Screen.height/768f;
			//_InitialPosition=new Vector3(transform.position.x,-2.9f*mfSHRatio,transform.position.z);
		//Debug.Log(transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		miTime++;
		if(miTime>=60)
		{
			transform.position=Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,-2.9f*mfSHRatio,transform.position.z),2f*Time.deltaTime*mfSHRatio);
			//print(transform.position.y);
			//print("tdddddddd"+ -3.15f*mfSHRatio);
			if(this.transform.position.y>=(-3.1f*mfSHRatio))
			{
				//print("Destroy");
				Destroy(gameObject.GetComponent<ChipStartUpAnimation>());
			}
		}
	}
	
	public Vector3 InitialPosition()
	{
		mInitialPosition=new Vector3(transform.position.x,-2.9f*(Screen.height/768f),transform.position.z);
		//Debug.Log(transform.position);
		return(mInitialPosition);
	}
}
