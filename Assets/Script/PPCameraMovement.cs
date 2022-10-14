using UnityEngine;
using System.Collections;

public class PPCameraMovement : MonoBehaviour {

	// Use this for initialization
	float mfDestination;
	float mfIntialPos;
	public bool _forWordMovement;
	float mfSWR;
	//float mfSpeed;
	
	GameObject mballPrefab;
	GameObject mBall;
	RT_Sounds mSound;
	// Use this for initialization
	void Start () 
	{
	//	Debug.Log(Screen.width);
		mfSWR=Screen.width/1024f;
		mfDestination=-13.65f*mfSWR;//15f*mfSWR;
		//if(Screen.width>=2048)
			//mfDestination=-13.65f*(Screen.width/2048f);
		mfIntialPos=transform.position.x;
		//mfSpeed=Screen.width/100f;
		if(Screen.width>=2048)
		{
			mfDestination=(-13.65f*mfSWR)/2f;
			//Debug.Log("initial position  "+mfDestination);
			//transform.position=new Vector3(transform.position.x/2f,transform.position.y,transform.position.z);
			//Debug.Log("initial position  "+transform.position);
			mfIntialPos=mfIntialPos/2f;
			//mfSpeed=Screen.width/200f;
		}
		mballPrefab=Resources.Load("Sphere") as GameObject;
		
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_forWordMovement==true)
		{
			//if(Screen.width>=2048)
				//transform.position=Vector3.MoveTowards(new Vector3(transform.position.x/2,transform.position.y,transform.position.z),new Vector3(mfDestination,transform.position.y,transform.position.z),1.35f*mfSpeed*Time.deltaTime);
			//else
				transform.position=Vector3.Lerp(transform.position,new Vector3(mfDestination,transform.position.y,transform.position.z),/*1.35f*mfSpeed*Time.deltaTime*0.25f*/0.09f);
			if((transform.position.x-mfDestination)<0.01f)
			{
				transform.position=new Vector3(mfDestination,transform.position.y,transform.position.z);
				Switcher switcher = GameObject.Find("Switcher").GetComponent<Switcher>();
				if(RT_DataHandler._StageNumber==1)
				{
					GameObject isObj=switcher._RouletteWheelArray[0].transform.FindChild("Object003").gameObject;
					GameObject isCollider=switcher._RouletteWheelArray[0].transform.FindChild("collider").gameObject;
			        isObj.AddComponent("RouletteRotation");
					isObj.GetComponent<RouletteRotation>()._Collider=isCollider.transform;
					float radius=2.55f;
					float angle =0;
					float isXPos=radius*(Mathf.Cos(angle * Mathf.Deg2Rad));
					float isYPos=radius*(Mathf.Sin(angle * Mathf.Deg2Rad));	
					if(Screen.width>=2048)
					{
					 	 mBall=Instantiate(mballPrefab,new Vector3((-13.65f*mfSWR)/2f+isXPos,mballPrefab.transform.position.y,isYPos),mballPrefab.transform.rotation) as GameObject;
						//Debug.Log("bal po "+mBall.transform.position);
					}
					else
						mBall=Instantiate(mballPrefab,new Vector3(-13.65f*mfSWR+isXPos,mballPrefab.transform.position.y,isYPos),mballPrefab.transform.rotation) as GameObject;
					switcher._Ball=mBall;
					
					mSound.PlaySound(mSound._RoulettRotation);
					//GameObject.Find("SoundManager").audio.loop=true;
				}
				else
				{
					GameObject isObj=switcher._RouletteWheelArray[1].transform.FindChild("Object003").gameObject;
					GameObject isCollider=switcher._RouletteWheelArray[1].transform.FindChild("collider").gameObject;
			        isObj.AddComponent("RouletteRotation");
					isObj.GetComponent<RouletteRotation>()._Collider=isCollider.transform;
					float radius=2.55f;
					float angle =0;
					float isXPos=radius*(Mathf.Cos(angle * Mathf.Deg2Rad));
					float isYPos=radius*(Mathf.Sin(angle * Mathf.Deg2Rad));			
					if(Screen.width>=2048)
					 	 mBall=Instantiate(mballPrefab,new Vector3((-13.65f*mfSWR)/2f+isXPos,mballPrefab.transform.position.y,isYPos),mballPrefab.transform.rotation) as GameObject;
					else
						mBall=Instantiate(mballPrefab,new Vector3(-13.65f*mfSWR+isXPos,mballPrefab.transform.position.y,isYPos),mballPrefab.transform.rotation) as GameObject;
					switcher._Ball=mBall;
				
					mSound.PlaySound(mSound._RoulettRotation);
					
					
				}
				GameObject RolleteCamera;
				RolleteCamera=GameObject.Find("Camera2");
				Zoomcamera mZoomCamera;
				mZoomCamera=RolleteCamera.GetComponent("Zoomcamera")as Zoomcamera;
				mZoomCamera.enabled=true;
				transform.GetComponent<PPCameraMovement>().enabled=false;
			}
		}
		else
		{
			//transform.position=Vector3.MoveTowards(transform.position,new Vector3(mfIntialPos,transform.position.y,transform.position.z),1.4f*mfSpeed*Time.deltaTime);
			//if(transform.position.x==mfIntialPos)
			//{
			//Debug.Log("Initial Position "+mfIntialPos);
			transform.position=Vector3.Lerp(transform.position,new Vector3(mfIntialPos,transform.position.y,transform.position.z),/*1.35f*mfSpeed*Time.deltaTime*0.25f*/0.09f);
		//	Debug.Log(mfIntialPos-transform.position.x);
			if(((mfIntialPos)+-transform.position.x)<0.01f)
			{
				//Debug.Log("pp camera setteled");
				transform.position=new Vector3(mfIntialPos,transform.position.y,transform.position.z);
				Switcher switcher = GameObject.Find("Switcher").GetComponent<Switcher>();
				if(RT_DataHandler._StageNumber==1)
				{
					GameObject isObj=switcher._RouletteWheelArray[0].transform.FindChild("Object003").gameObject;
					Destroy(isObj.GetComponent<RouletteRotation>());
					switcher.ActivateWheelAtIndex(0,false);
				}
				else
				{
					GameObject isObj=switcher._RouletteWheelArray[1].transform.FindChild("Object003").gameObject;
					Destroy(isObj.GetComponent<RouletteRotation>());
					switcher.ActivateWheelAtIndex(1,false);
				}
				Destroy(mBall);
				RT_DataHandler.WinLoss(RT_DataHandler._WinningNo);
				//transform.camera.fieldOfView=40f;
				transform.GetComponent<PPCameraMovement>().enabled=false;
			}
		}
	}
}
