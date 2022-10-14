using UnityEngine;
using System.Collections;

public class CameraMovment : MonoBehaviour {
	
	//public Vector3 _Destination=new Vector3(-10,0,-10);
	float mfXValue;
	float mfDestination;
	float mfIntialPos;
	//float mfSpeed;
	public bool _forWordMovement;
	float mfSWR;
	// Use this for initialization
	void Start () 
	{
		mfSWR=Screen.width/1024f;
		mfDestination=10.24f*mfSWR;
		mfIntialPos=transform.position.x;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_forWordMovement==true)
		{
			transform.position=Vector3.Lerp(transform.position,new Vector3(-mfDestination,transform.position.y,transform.position.z),/*mfSpeed*Time.deltaTime*0.25f*/0.09f);
			if((transform.position.x-mfDestination)>0.01f)
			{
				transform.position=new Vector3(-mfDestination,transform.position.y,transform.position.z);
				transform.GetComponent<CameraMovment>().enabled=false;
			}
		}
		else
		{
			transform.position=Vector3.Lerp(transform.position,new Vector3(-mfIntialPos,transform.position.y,transform.position.z),/*mfSpeed*Time.deltaTime*0.25f*/0.09f);
			if((mfIntialPos-transform.position.x)<0.01f)
			{
				transform.position=new Vector3(-mfIntialPos,transform.position.y,transform.position.z);
				transform.GetComponent<CameraMovment>().enabled=false;
			}
		}

	}
}
