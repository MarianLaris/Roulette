using UnityEngine;
using System.Collections;

public class GUIMovement : MonoBehaviour {
	
	// Vector3 _Destination;//=new Vector3(10f/1024f,0,-10);
	   float mfDestination;
	   float mfIntialPos;
	   public bool _forWordMovement;
	   bool ObjectFound;
	   float mfSWR;
	   Transform Obj;
	   float mfXPos;
	// Use this for initialization
	void Start () 
	{
		mfSWR=Screen.width/1024f;
		
		mfDestination=transform.position.x+Camera.main.WorldToViewportPoint(new Vector3(10.24f*mfSWR,0,0)).x-0.5f;
		mfIntialPos=transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_forWordMovement==true)
		{
			//transform.position=Vector3.MoveTowards(transform.position,new Vector3(mfDestination,transform.position.y,transform.position.z),1f*Time.deltaTime);
				transform.position=Vector3.Lerp(transform.position,new Vector3(mfDestination,transform.position.y,transform.position.z),/*Time.deltaTime*2.1f*/0.09f);

				//if(transform.position.x-mfDestination)
			if((transform.position.x-mfDestination)>0.01f)
			{
				transform.position=new Vector3(mfDestination,transform.position.y,transform.position.z);
				transform.GetComponent<GUIMovement>().enabled=false;
			}
		}
		else
		{
			//transform.position=Vector3.MoveTowards(transform.position,new Vector3(mfIntialPos,transform.position.y,transform.position.z),1f*Time.deltaTime);	
			transform.position=Vector3.Lerp(transform.position,new Vector3(mfIntialPos,transform.position.y,transform.position.z),/*Time.deltaTime*2.1f*/0.09f);
			//if(transform.position.x==mfIntialPos)
			if((transform.position.x-mfIntialPos)<0.01f)
			{
				//Debug.Log("Gui Setteled");
				transform.position=new Vector3(mfIntialPos,transform.position.y,transform.position.z);
						transform.GetComponent<GUIMovement>().enabled=false;
			}
		}
	}
}
