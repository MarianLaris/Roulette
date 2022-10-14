using UnityEngine;
using System.Collections;

public class Switcher : MonoBehaviour {

	// Use this for initialization
	public Camera[] _CameraArray;
	public Texture [] _ColortextureArray;
	public GameObject[] _RouletteWheelArray;
	
	public GUIText _WinningNo;
	float mfSWRatio;
	float mfSHRatio;
	public GameObject _Ball;
	public bool _IsSleep;
	bool mbOnce;
	RaycastHit mHit;
	bool mbFlag;
	public bool _AnimatioComplete;
	int miTimer;
	public GameObject _WinAmountTextBg;
	public GameObject _WinAmounttext;
	public GameObject _WinnigTextBgColor;
	
	//Zoom Camera
	GameObject RolleteCamera;		
	Zoomcamera mZoomCamera;
	
	public bool _ZoomOut;
	
	//Zoom Camera
	
	//RT_Sounds mSound;
	void Start () 
	{
		 mfSWRatio=Screen.width/1024f;
		 mfSHRatio=Screen.height/768f;
		_RouletteWheelArray[0].transform.position=new Vector3(-13.65f*mfSWRatio,0,0);
	    _RouletteWheelArray[1].transform.position=new Vector3(-13.65f*mfSWRatio,0,0);
		if(Screen.width>=2048)
		{
			_RouletteWheelArray[0].transform.position=new Vector3((-13.65f*mfSWRatio)/2f,0,0);
	    	_RouletteWheelArray[1].transform.position=new Vector3((-13.65f*mfSWRatio)/2f,0,0);
			Debug.Log("r pod "+_RouletteWheelArray[0].transform.position);
		}
		
//		Debug.Log("r pod "+_RouletteWheelArray[0].transform.position);
		// mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		
		//Camera Zoom
		  	
			RolleteCamera=GameObject.Find("Camera2");
			mZoomCamera=RolleteCamera.GetComponent("Zoomcamera")as Zoomcamera;
		
		
		//Camera Zoom
		
		
	}
	
	void Update()
	{
//		if (Input.GetKey (KeyCode.Escape) && transform.name.Contains ("Switcher"))
//			
//		{
//			ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.ApplicationQuit); 
//			Debug.Log ("quit"); 
//			
//		}


		if(_Ball!=null)
		{
			 Physics.Raycast(_Ball.transform.position, -Vector3.up, out mHit);
			if(Mathf.Abs(_Ball.rigidbody.velocity.x)<=0.25f&&Mathf.Abs(_Ball.rigidbody.velocity.y)<=0.25f&&Mathf.Abs(_Ball.rigidbody.velocity.z)<=0.25f && _IsSleep == true && mHit.collider.tag=="Collider"&&!mbFlag)
			{	

				_ZoomOut=true;
				
				float isHR=1;
				if(Screen.width>=2048)
				{
					isHR=1;
				}
				mbFlag=true;
				Vector3 ispos=Camera.main.WorldToViewportPoint(new Vector3(-10.24f*mfSWRatio,0,0));
	    		GameObject isWinningNoBg=Instantiate(_WinAmountTextBg,new Vector3(ispos.x,(0.49f)*isHR,1),Quaternion.identity) as GameObject;
				isWinningNoBg.GetComponent<FadeINFadeOUT>()._Duration=1f;
			    GameObject isWinningNoTextBgColor=Instantiate(_WinnigTextBgColor,new Vector3(ispos.x,(0.5f)*isHR,2),Quaternion.identity) as GameObject;
				if(mHit.collider.name=="1"||mHit.collider.name=="3"||mHit.collider.name=="5"||mHit.collider.name=="7"||mHit.collider.name=="9"||mHit.collider.name=="12"||mHit.collider.name=="14"||mHit.collider.name=="16"||mHit.collider.name=="18"||mHit.collider.name=="19"||mHit.collider.name=="21"||mHit.collider.name=="23"||mHit.collider.name=="25"||mHit.collider.name=="27"||mHit.collider.name=="30"||mHit.collider.name=="32"||mHit.collider.name=="34"||mHit.collider.name=="36")
				{
					isWinningNoTextBgColor.guiTexture.texture=_ColortextureArray[0];
				}
				else if(mHit.collider.name=="2"||mHit.collider.name=="4"||mHit.collider.name=="6"||mHit.collider.name=="8"||mHit.collider.name=="10"||mHit.collider.name=="11"||mHit.collider.name=="13"||mHit.collider.name=="15"||mHit.collider.name=="17"||mHit.collider.name=="20"||mHit.collider.name=="22"||mHit.collider.name=="24"||mHit.collider.name=="26"||mHit.collider.name=="28"||mHit.collider.name=="29"||mHit.collider.name=="31"||mHit.collider.name=="33"||mHit.collider.name=="35")
				{
					isWinningNoTextBgColor.guiTexture.texture=_ColortextureArray[1];
				}
				else
				{
					isWinningNoTextBgColor.guiTexture.texture=_ColortextureArray[2];
				}
				isWinningNoTextBgColor.GetComponent<FadeINFadeOUT>()._Duration=1f;
					//isBetAmountBox.transform.localScale=new Vector3(isBetAmountBox.transform.localScale.x*mfSWRatio,isBetAmountBox.transform.localScale.y*mfSHRatio,1);
				GameObject   IsWinningNo=Instantiate(_WinAmounttext,new Vector3(ispos.x,(0.49f)*isHR,3),Quaternion.identity) as GameObject;
				IsWinningNo.guiText.fontSize=(int)(100*mfSHRatio);
				IsWinningNo.guiText.text=""+mHit.collider.name;
				IsWinningNo.GetComponent<FadeINFadeOUTText>()._Duration=1f;
				//mSound.PlaySound(mSound._WinningNo);
				if(mHit.collider.tag=="Collider")
				{
					if(mHit.collider.name=="00")
					{
						RT_DataHandler._WinningNo=-1;
					}
					else
					{
						int.TryParse(mHit.collider.name, out RT_DataHandler._WinningNo);
					}
				  // Debug.Log("Winning no    "+RT_DataHandler._WinningNo);
				}	
				RT_DataHandler.isWinningNo(RT_DataHandler._WinningNo);
			}
		}


		
		if(_AnimatioComplete&&mZoomCamera.mInceraseView==false && mZoomCamera.CameraAnimationCoplete)
		{
				mZoomCamera.mInceraseView=false;
				mZoomCamera.CameraAnimationCoplete=false;
				mZoomCamera.mDeltaTime=0;
				mZoomCamera.enabled=false;
				//_CameraArray[2].camera.fieldOfView=40.0f;
			 //   Debug.Log("Conditon satisfy   ");
				_ZoomOut=false;
				 mbFlag=false;
				_AnimatioComplete=false;
				_WinningNo.text="";
			    Switcher switcher = GameObject.Find("Switcher").GetComponent<Switcher>();
				Camera [] isCameraArray=switcher._CameraArray;
				isCameraArray[0].gameObject.GetComponent<CameraMovment>().enabled=true;
				isCameraArray[1].gameObject.GetComponent<CameraMovment>().enabled=true;
				isCameraArray[2].gameObject.GetComponent<PPCameraMovement>().enabled=true;
				isCameraArray[0].gameObject.GetComponent<CameraMovment>()._forWordMovement=false;
				isCameraArray[1].gameObject.GetComponent<CameraMovment>()._forWordMovement=false;
				isCameraArray[2].gameObject.GetComponent<PPCameraMovement>()._forWordMovement=false;
				Transform isGUiObj=GameObject.Find("GamesceneGUIHandler").transform;
			    ExternalInterfaceHandler.Instance.SendRequest(eEXTERNAL_REQ_TYPE.Show_FullScreen_Ads,"2",null);
			   // RT_DataHandler._TotalbettingAmount =0;
				for(int i=0;i<isGUiObj.childCount;i++)
				{
					isGUiObj.GetChild(i).GetComponent<GUIMovement>().enabled=true;
					isGUiObj.GetChild(i).GetComponent<GUIMovement>()._forWordMovement=false;
				}
				GameObject[] isGUiEleArray=GameObject.Find("GameManager(Clone)").GetComponent<RT_BettingSelection>()._GuiElementArray;
				foreach(GameObject Obj in isGUiEleArray)
				{
					Obj.GetComponent<GUIMovement>().enabled=true;
					Obj.GetComponent<GUIMovement>()._forWordMovement=false;
				}	
			isGUiObj.GetComponent<GamesceneGUIHandler>().IsSpinning=false;
				_IsSleep=false;
				_Ball=null;
			 
		}
	}
		
		

	public void ActivateWheelAtIndex(int pIndex,bool pActive)
	{
		if(pIndex != -1 && pIndex < _RouletteWheelArray.Length)
		{
			_RouletteWheelArray[pIndex].gameObject.SetActive(pActive);
		}
	}
}
