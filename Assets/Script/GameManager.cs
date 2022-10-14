using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	float mfSWRatio;
	float mfSHRatio;
	GameObject mGameSceneBG;
	GameObject mTableCollider;
	GameObject[] mchip;
	public GameObject _WheelObj;
	GameObject mRBG;
	public Texture[] TextureArray;
	int mNoOfChipsTyps;
	
	public Vector3[] InitialPos;
	// Use this for initialization
	void Start () 
	{
//		  Debug.Log("iPhone.generation     : " + iPhone.generation);
//
//        Debug.Log("SystemInfo.deviceType : " + SystemInfo.deviceType); 
//
//        Debug.Log("SystemInfo.deviceModel: " + SystemInfo.deviceModel); 
		
		
		mfSWRatio=Screen.width/1024f;
		mfSHRatio=Screen.height/768f;
		mNoOfChipsTyps=15;
		InitialPos=new Vector3[mNoOfChipsTyps];
		Camera.main.orthographicSize=Screen.height/200f;
		GameObject.Find("Camera").camera.orthographicSize=Screen.height/200f;
		mchip=new GameObject[mNoOfChipsTyps];
		mRBG=Instantiate(Resources.Load("GameSceneBG/BG1"),new Vector3(0,0,0.2f),new Quaternion(0,180,0,0))as GameObject;
		mRBG.transform.localScale=new Vector3(10.24f*mfSWRatio,7.68f*mfSHRatio,1f);
		mRBG.renderer.material.mainTexture=TextureArray[RT_DataHandler._StageNumber-1];
		switch(RT_DataHandler._StageNumber)
		{
		case 1:
			mGameSceneBG=Instantiate(Resources.Load("GameSceneBG/AmericanRouletteTable"),new Vector3(0,0,0.1f),new Quaternion(0,180,0,0))as GameObject;
			mGameSceneBG.transform.localScale=new Vector3(9.58f*mfSWRatio,3.73f*mfSHRatio,1f);
			mGameSceneBG.transform.position=new Vector3(-0.03f*mfSWRatio,0.05f*mfSHRatio,0.1f);
			mTableCollider=Instantiate(Resources.Load("Collider/AmericanTableCollider"),Vector3.zero,Quaternion.identity) as GameObject;
			mTableCollider.transform.localScale=new Vector3(mfSWRatio,mfSHRatio,0.01f);
			break;
		case 2:
			mGameSceneBG=Instantiate(Resources.Load("GameSceneBG/EuropeanrRouletteTable"),new Vector3(0,0,0.1f),new Quaternion(0,180,0,0)) as GameObject;
			mGameSceneBG.transform.localScale=new Vector3(9.82f*mfSWRatio,5.75f*mfSHRatio,1f);
			mGameSceneBG.transform.position=new Vector3(0.05f*mfSWRatio,0.25f*mfSHRatio,0.1f);
			mTableCollider=Instantiate(Resources.Load("Collider/EuropianTableCollider"),Vector3.zero,Quaternion.identity) as GameObject;
			mTableCollider.transform.localScale=new Vector3(mfSWRatio,mfSHRatio,0.01f);
			mTableCollider=Instantiate(Resources.Load("Collider/RaceTrackCollider"),Vector3.zero,Quaternion.identity) as GameObject;
			mTableCollider.transform.localScale=new Vector3(mfSWRatio,mfSHRatio,0.01f);
			break;
		case 3:
			mGameSceneBG=Instantiate(Resources.Load("GameSceneBG/FrenchRouletteTable"),new Vector3(0,0,0.1f),new Quaternion(0,180,0,0))as GameObject;
			mGameSceneBG.transform.localScale=new Vector3(9.96f*mfSWRatio,5.65f*mfSHRatio,1f);
			mGameSceneBG.transform.position=new Vector3(-0.01f*mfSWRatio,0.30f*mfSHRatio,0.1f);
			mTableCollider=Instantiate(Resources.Load("Collider/FrenchTableCollider"),Vector3.zero,Quaternion.identity) as GameObject;
			mTableCollider.transform.localScale=new Vector3(mfSWRatio,mfSHRatio,0.01f);
			mTableCollider=Instantiate(Resources.Load("Collider/RaceTrackCollider"),Vector3.zero,Quaternion.identity) as GameObject;
			mTableCollider.transform.localScale=new Vector3(mfSWRatio,mfSHRatio,0.01f);
			break;
		}
		CreateChips();

//		switch(RT_DataHandler._StageNumber)
//		{
//		case 1:
//			for(int i=1;i<=mNoOfChipsTyps;i++)
//			{
//			 mchip[i-1]= Instantiate(Resources.Load("Chip1/Chip_"+i),new Vector3((-2f+i*0.9f)*mfSWRatio,-4.2f*mfSHRatio,-0.01f),new Quaternion(0,180,0,0))as GameObject;
//			 mchip[i-1].transform.localScale=new Vector3(0.71f*mfSHRatio,0.70f*mfSHRatio,1f);
//				InitialPos[i-1]=mchip[i-1].transform.position;
//			}
//			break;
//			case 2:
//			for(int i=1;i<=mNoOfChipsTyps;i++)
//			{
//			 mchip[i-1]= Instantiate(Resources.Load("Chip2/Chip_"+i),new Vector3((-2f+i*0.9f)*mfSWRatio,-4.2f*mfSHRatio,-0.01f),new Quaternion(0,180,0,0))as GameObject;
//			 mchip[i-1].transform.localScale=new Vector3(0.71f*mfSHRatio,0.70f*mfSHRatio,1f);
//				InitialPos[i-1]=mchip[i-1].transform.position;
//			}
//			break;
//			case 3:
//			for(int i=1;i<=mNoOfChipsTyps;i++)
//			{
//			 mchip[i-1]= Instantiate(Resources.Load("Chip3/Chip_"+i),new Vector3((-2f+i*0.9f)*mfSWRatio,-4.2f*mfSHRatio,-0.01f),new Quaternion(0,180,0,0))as GameObject;
//			 mchip[i-1].transform.localScale=new Vector3(0.71f*mfSHRatio,0.70f*mfSHRatio,1f);
//				InitialPos[i-1]=mchip[i-1].transform.position;
//			}
//			break;
//		}
	}
	
	void CreateChips()
	{
		int ChipCount=0;
		for(int j=1;j<=3;j++)
		{
			for(int i=1;i<=5;i++)
			{
			    mchip[ChipCount]= Instantiate(Resources.Load("Chip"+j+"/Chip_"+i),new Vector3((-2f+i*0.9f)*mfSWRatio,-4.2f*mfSHRatio,-0.01f),new Quaternion(0,180,0,0))as GameObject;
			    mchip[ChipCount].transform.localScale=new Vector3(0.71f*mfSHRatio,0.70f*mfSHRatio,1f);
				InitialPos[ChipCount]=mchip[ChipCount].transform.position;
				if(RT_DataHandler._StageNumber!=j)
				{
					 mchip[ChipCount].renderer.enabled=false;
					 mchip[ChipCount].collider.enabled=false;
				}
			   ChipCount++;
			}
		}
	}
	
	public GameObject[] GetChips()
	{
		return mchip;
	}
	// Update is called once per frame
}
