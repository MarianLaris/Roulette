using UnityEngine;
using System.Collections;

public class Zoomcamera : MonoBehaviour {

 public	float mDeltaTime;
 GameObject mBall;
 public	bool mInceraseView;
 Switcher switcher;	
 public GamesceneGUIHandler GuiHandler;	
 RT_BettingSelection chipselection;	
public bool	CameraAnimationCoplete;
bool zoomInAnimation;
 public GameObject[] _GuiElementArray; 	 
	int delay;
	// Use this for initialization
	void Start () 
	{
		//_GuiElementArray=new GameObject[3];
		switcher=GameObject.Find("Switcher").GetComponent<Switcher>();
		//GuiHandler=GameObject.Find("GamesceneGUIHandler").GetComponent<GamesceneGUIHandler>();
		chipselection=GameObject.Find("GameManager(Clone)").GetComponent<RT_BettingSelection>();
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log("sdsdsdfsf");
		if(mDeltaTime<=0.5f)
		mDeltaTime+=Time.deltaTime;
		if(mDeltaTime>0.5f && !mInceraseView)
		{
//			Debug.Log("  name "+_GuiElementArray.Length);
			transform.camera.fieldOfView=Mathf.MoveTowards(transform.camera.fieldOfView,30,0.1f);
			
			//_GuiElementArray[0].transform.position=Vector3.MoveTowards(_GuiElementArray[0].transform.position,new Vector3(_GuiElementArray[0].transform.position.x,1.097f,_GuiElementArray[0].transform.position.z),0.001f);
			GuiHandler._HudArray[0].transform.position=Vector3.MoveTowards(GuiHandler._HudArray[0].transform.position,new Vector3(GuiHandler._HudArray[0].transform.position.x,1.097f,GuiHandler._HudArray[0].transform.position.z),0.001f);
			GuiHandler._HudArray[1].transform.position=Vector3.MoveTowards(GuiHandler._HudArray[1].transform.position,new Vector3(GuiHandler._HudArray[1].transform.position.x,1.11f,GuiHandler._HudArray[1].transform.position.z),0.001f);
			GuiHandler._HudArray[2].transform.position=Vector3.MoveTowards(GuiHandler._HudArray[2].transform.position,new Vector3(GuiHandler._HudArray[2].transform.position.x,1.1f,GuiHandler._HudArray[2].transform.position.z),0.001f);
			for(int i=0;i<3;i++)
			{
				chipselection._GUITextArray[i].transform.position=Vector3.MoveTowards(chipselection._GUITextArray[i].transform.position,new Vector3(chipselection._GUITextArray[i].transform.position.x,1.10875f,chipselection._GUITextArray[i].transform.position.z),0.001f);
			}
			
			if(transform.eulerAngles.x!=90 && transform.position.z!=0)
			{
				transform.eulerAngles=new Vector3( Mathf.MoveTowards(transform.eulerAngles.x,90,Time.deltaTime*15)	,transform.eulerAngles.y,transform.eulerAngles.z);
				transform.position=new Vector3(transform.position.x,transform.position.y,Mathf.MoveTowards(transform.position.z,0,Time.deltaTime*3.5f));
			}
			if(transform.camera.fieldOfView==30.0f/*&&transform.position.z==0f&&transform.eulerAngles.x==90*/)
			{
				 mInceraseView=true;	
				zoomInAnimation=true;
				 mBall=GameObject.Find("Sphere(Clone)");
			}	
		}
		if(mInceraseView && mBall.rigidbody.velocity.magnitude<0.60f&&switcher._ZoomOut==true&&switcher._AnimatioComplete==true&&zoomInAnimation)
		{
			
		 	transform.camera.fieldOfView=Mathf.Lerp(transform.camera.fieldOfView,40,4f*Time.deltaTime);
			if(40.0f-transform.camera.fieldOfView<0.005f)
				transform.camera.fieldOfView = 40.0f;
			
		 	GuiHandler._HudArray[0].transform.position=Vector3.MoveTowards(GuiHandler._HudArray[0].transform.position,new Vector3(GuiHandler._HudArray[0].transform.position.x,0.957f,GuiHandler._HudArray[0].transform.position.z),0.002f);
			GuiHandler._HudArray[1].transform.position=Vector3.MoveTowards(GuiHandler._HudArray[1].transform.position,new Vector3(GuiHandler._HudArray[1].transform.position.x,0.97f,GuiHandler._HudArray[1].transform.position.z),0.002f);
			GuiHandler._HudArray[2].transform.position=Vector3.MoveTowards(GuiHandler._HudArray[2].transform.position,new Vector3(GuiHandler._HudArray[2].transform.position.x,0.96f,GuiHandler._HudArray[2].transform.position.z),0.002f);	
			for(int i=0;i<3;i++)
			{
				chipselection._GUITextArray[i].transform.position=Vector3.MoveTowards(chipselection._GUITextArray[i].transform.position,new Vector3(chipselection._GUITextArray[i].transform.position.x,0.96875f,chipselection._GUITextArray[i].transform.position.z),0.002f);
			}
		 	if(transform.eulerAngles.x!=55 && transform.position.z!=-7.5f)
			 {
				 transform.eulerAngles=new Vector3( Mathf.Lerp(transform.eulerAngles.x,55,Time.deltaTime*4f)	,transform.eulerAngles.y,transform.eulerAngles.z);
			 	 transform.position=new Vector3(transform.position.x,transform.position.y,Mathf.Lerp(transform.position.z,-7.5f,Time.deltaTime*4f));
				
				if((transform.eulerAngles.x-55.0f)<0.005f)
					transform.eulerAngles = new Vector3(55.0f,transform.eulerAngles.y,transform.eulerAngles.z);
				if((transform.position.z+7.50f)<0.005f)
					transform.position = new Vector3(transform.position.x,transform.position.y,-7.5f);
				
				
			 }	
			if(transform.camera.fieldOfView==40.0f  && transform.position.z==-7.5f )
			{
				mInceraseView=false;
				mDeltaTime=0;
				CameraAnimationCoplete=true;
				zoomInAnimation=false;
			//	Debug.Log("camera............. ");
				//switcher._ZoomOut=false;
			}
		}
		
		
	
	}
	
	
	//void MoveCameraIntime()
	
}
