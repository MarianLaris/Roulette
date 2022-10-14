using UnityEngine;
using System.Collections;

public class RT_BettingSelection : MonoBehaviour {
	
	RT_Sounds mSound;
	
	GameObject mPreviousSelectedObj;
	Ray mray;
	RaycastHit mhit;
	public GameObject [] _objArray;
	public GameObject [] _RaceTrackObs;
	GameObject mcurrentselectObject;
    GameObject mPopUpBox;
	GameObject mBetAmounttext;
	GameObject mBetAmountOnThis;
	Transform mcolliderObject;
	Vector3 mObjectInitialPosition;
	Vector3 mchipDistancefromMouse;
	Vector3 difference;
	Vector3 mTextPos;
	public GUIText _ObjectName;
	GUIText mName;
	bool mbBettinginDone;
	bool mbPopUpCreation;
	bool mbrun;
	bool mbResetPosition;
	GameObject mCollederObj;
	int SelectedCoinAmount;
	int miRacetrackObjindex;
	int PopUpTime;
	float mfSHRatio;
	float mfSWRatio;
	public GameObject[] _GuiElementArray;
	public GameObject[] _GUITextArray;
	
	//GameObject mFundScreen;
	///public BetSelection1 _BS;
	
	
	
	// Use this for initialization
	void Start ()
	{
	//	Input.multiTouchEnabled=false;
		mPreviousSelectedObj=null;
		mfSHRatio=Screen.height/768f;
		mfSWRatio=Screen.width/1024f;
		_GUITextArray=new GameObject[3];
		mSound=GameObject.Find("SoundManager").GetComponent<RT_Sounds>();
		
	//	Debug.Log(Screen.width);
	    mName=Instantiate(_ObjectName,_ObjectName.transform.position,_ObjectName.transform.rotation) as GUIText;
		if(Screen.height<768f)
			//mchipDistancefromMouse=new Vector3(0,1.2f*mfSHRatio,0);
			mchipDistancefromMouse=new Vector3(-0.3f*mfSHRatio,0.5f*mfSHRatio,0);
		else
			mchipDistancefromMouse=new Vector3(0,0.7f*mfSHRatio,0);
		
		if(RT_DataHandler._StageNumber==1)
		_objArray=GameObject.Find("AmericanTableCollider(Clone)").GetComponent<RT_TableCollidersObjects>()._Objs;
		
		if(RT_DataHandler._StageNumber==2)
		{
			_objArray=GameObject.Find("EuropianTableCollider(Clone)").GetComponent<RT_TableCollidersObjects>()._Objs;
			_RaceTrackObs=GameObject.Find("RaceTrackCollider(Clone)").GetComponent<RT_TableCollidersObjects>()._RaceTrackObjs;
		}
		
		if(RT_DataHandler._StageNumber==3)
		{
			_objArray=GameObject.Find("FrenchTableCollider(Clone)").GetComponent<RT_TableCollidersObjects>()._Objs;
			_RaceTrackObs=GameObject.Find("RaceTrackCollider(Clone)").GetComponent<RT_TableCollidersObjects>()._RaceTrackObjs;
		}
		
		
		mPopUpBox=Resources.Load("PopUp/PopUpBox") as GameObject;
		mBetAmounttext=Resources.Load("PopUp/BetAmount") as GameObject;
		
		mTextPos=Camera.main.WorldToViewportPoint(new Vector3(1.3f*mfSWRatio,3.6f*mfSHRatio,1f));
		RT_DataHandler._TotalbettingAmountText=Instantiate(Resources.Load("PopUp/TotalbettingAmount") as GameObject,new Vector3(mTextPos.x,mTextPos.y,0),Quaternion.identity) as GameObject;
		RT_DataHandler._TotalbettingAmountText.guiText.fontSize=(int)(27f*mfSHRatio);
		mTextPos=Camera.main.WorldToViewportPoint(new Vector3(4.6f*mfSWRatio,3.6f*mfSHRatio,1f));
		RT_DataHandler._TotalfundYouHaveText=Instantiate(Resources.Load("PopUp/TotalfundYouHave") as GameObject,new Vector3(mTextPos.x,mTextPos.y,0),Quaternion.identity) as GameObject;
		RT_DataHandler._TotalfundYouHaveText.guiText.text=" "+RT_DataHandler.GetTotalFund();
		RT_DataHandler._TotalfundYouHaveText.guiText.fontSize=(int)(27f*mfSHRatio);
		//mTotalWinningAmountText=Resources.Load("PopUp/TotalWinningAmountText") as GameObject;
		mTextPos=Camera.main.WorldToViewportPoint(new Vector3(-0.85f*mfSWRatio,3.6f*mfSHRatio,1f));
		RT_DataHandler._TotalWinningAmountText=Instantiate(Resources.Load("PopUp/TotalWinningAmount") as GameObject,new Vector3(mTextPos.x,mTextPos.y,0),Quaternion.identity) as GameObject;
		RT_DataHandler._TotalWinningAmountText.guiText.fontSize=(int)(27f*mfSHRatio);
		 _GuiElementArray= GameObject.FindGameObjectsWithTag("GUI");
		_GUITextArray[0]=RT_DataHandler._TotalWinningAmountText;
		_GUITextArray[1]=RT_DataHandler._TotalbettingAmountText;
		_GUITextArray[2]=RT_DataHandler._TotalfundYouHaveText;
	}
	void Update ()
	{
		if(RT_DataHandler._TouchDisable==false)
		{
			//DestroyAmountPopUp();
			if(InputHandler.instance.isTouchStationary() && mbResetPosition==false)

				ChipSelectionAndBetRenderer();
			if(InputHandler.instance.isTouchEnded() && mbrun==true)
			{
				mbrun=false;
				//mcurrentselectObject.collider.enabled=false;
				int isMaxAmount=RT_DataHandler.GetTotalFund();
				int isAountTimes=1;
				if(mcolliderObject!=null)
				 isAountTimes=TotalCoinAmount(mcolliderObject.gameObject);
				if(RT_DataHandler._rebettingCanBeDone==true&&mbBettinginDone==true)
				{
					//Debug.Log("clear Previous bet");
					RT_DataHandler.BetClear();
				}
				if(mbBettinginDone==true&&RT_DataHandler._TotalbettingAmount+SelectedCoinAmount*isAountTimes<=isMaxAmount)
				{
//					Debug.Log("Selected Coin Amount "+SelectedCoinAmount);
					    mSound.PlaySound(mSound._CoinPlaced);
						mcurrentselectObject.tag="BettedChip";
						GameObject obj = GameObject.Find("GamesceneGUIHandler");
						if(obj != null)
						{
							GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
							handlerScr.SpinButton.setDisabled(false);
							handlerScr.ClearButton.setDisabled(false);
						}
					    RT_BetColliderProperty isobjData=mcolliderObject.GetComponent<RT_BetColliderProperty>();
						mcurrentselectObject.transform.position=new Vector3(mcolliderObject.position.x,mcolliderObject.position.y+isobjData._YPosition,mcolliderObject.position.z+isobjData._ZPosition);
					    mcurrentselectObject.GetComponent<ChipProperty>()._bettedObject=mcolliderObject.gameObject;
						
						
					//	Debug.Log(isobjData._ChipArray.Count);
						if(mcolliderObject.tag=="RaceTrack")
						{
						    if(mcolliderObject.name=="Jeu0")
							RT_DataHandler.JeuBet(SelectedCoinAmount,mcurrentselectObject);
						    else if(mcolliderObject.name=="Orphelins")
								RT_DataHandler.OrphlinsBet(SelectedCoinAmount,mcurrentselectObject);
						    else if(mcolliderObject.name=="Tiers")
								RT_DataHandler.TierBet(SelectedCoinAmount,mcurrentselectObject);
							else if(mcolliderObject.name=="Voisins")
								RT_DataHandler.VoisinsBet(SelectedCoinAmount,mcurrentselectObject);
							else 
								RT_DataHandler.NNBet(SelectedCoinAmount,mcurrentselectObject,mcolliderObject.name);
						
						//mcolliderObject.GetComponent<RT_BetColliderProperty>()._bettingAmountOnThis+=mcurrentselectObject.GetComponent<ChipProperty>()._AmountOnthis;
						//isobjData.ReplaceAllChipWithMinimum();
						}
						else
						{
							isobjData._ZPosition-=0.00001f;
					    	isobjData._YPosition+=0.03f;
					    	isobjData._ChipArray.Add(mcurrentselectObject);
							mcolliderObject.GetComponent<RT_BetColliderProperty>()._bettingAmountOnThis+=SelectedCoinAmount;
							isobjData.ReplaceAllChipWithMinimum();
							RT_DataHandler._TotalbettingAmount+=SelectedCoinAmount;
							RT_DataHandler._TotalfundYouHave-=SelectedCoinAmount;
						    CreateAmountPopUp(mcolliderObject,mcolliderObject.GetComponent<RT_BetColliderProperty>()._bettingAmountOnThis);
							
							BetChecker(mcolliderObject);
					 	
							//DataForAchieveMent
						    GameObject mAchievementhandler;
						    mAchievementhandler=GameObject.Find("AchieveMentHandler");
						    AchieveFundHandler sAchivefundhandler;
						    sAchivefundhandler=mAchievementhandler.GetComponent("AchieveFundHandler")as AchieveFundHandler;
						    sAchivefundhandler.Funddata( RT_DataHandler._TotalbettingAmount,RT_DataHandler._TotalbettingAmount,RT_DataHandler._TotalfundYouHave); 
						  //  print(RT_DataHandler.GetTotalFund());
						   //DataForAchieveMent
						
						
						}
						
					    RT_DataHandler._TotalbettingAmountText.guiText.text=" "+RT_DataHandler._TotalbettingAmount;
						RT_DataHandler._TotalfundYouHaveText.guiText.text=" "+(RT_DataHandler._TotalfundYouHave);
					    
					     						
				}
				else
				{
					if(mbBettinginDone==false)
					{
						mSound.PlaySound(mSound._CoinMissed);
						mbResetPosition=true;
					}
					else if(RT_DataHandler._TotalbettingAmount+SelectedCoinAmount*isAountTimes>isMaxAmount)
					{
						RT_DataHandler._TouchDisable=true;
						GameObject obj = GameObject.Find("GamesceneGUIHandler");
						if(obj != null)
						{
							GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
							RT_DataHandler._SpinButtonDisabledStatus=handlerScr.SpinButton.getDisabled();
							RT_DataHandler._RebetButtonDisabledStatus=handlerScr.RebetButton.getDisabled();
							RT_DataHandler._ClearButtonDisabledStatus=handlerScr.ClearButton.getDisabled();
							handlerScr.MenuButton.setDisabled(true);
							handlerScr.HelpButton.setDisabled(true);
							handlerScr.SpinButton.setDisabled(true);
							handlerScr.ClearButton.setDisabled(true);
							handlerScr.RebetButton.setDisabled(true);
							handlerScr.AddFundButton .setDisabled(true);
//							handlerScr._prevButton .setDisabled(true);
//							handlerScr._NextButton.setDisabled(true);
						}
						Destroy(mcurrentselectObject);
						mSound.PlaySound(mSound._NotEnoughAmount);
						GameObject.Find("ScreenManager").GetComponent<ScreenManager>().LoadScreen("NoMoreFundScreen");
					}
				}
				foreach(GameObject obj in _objArray )
			    {
					if(obj.renderer.enabled==true)
						obj.renderer.enabled=false;
				}
				if(RT_DataHandler._StageNumber!=1)
				{
					foreach(GameObject obj in _RaceTrackObs )
				    {
						if(obj.renderer.enabled==true)
							obj.renderer.enabled=false;
					}
				}
			}	
			if(mbResetPosition==true)
			{
				mcurrentselectObject.transform.position= Vector3.Lerp( mcurrentselectObject.transform.position,mObjectInitialPosition,5f*Time.deltaTime);
				if(Vector3.Distance(mcurrentselectObject.transform.position,mObjectInitialPosition)<0.05f)
				{
					Destroy(mcurrentselectObject);
					mbResetPosition=false;
				}
			}
		}
	}
	
	public void CornerBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		isName=isName.Remove(0,1);
		int isvalue;
		int.TryParse(isName, out isvalue);
		if(isvalue%2==0)
		{
			int isdiv=(isvalue/2)-1;
			foreach(GameObject obj in _objArray )
			{
				if(obj.name==""+(isvalue+isdiv)||obj.name==""+(isvalue+isdiv+1)||obj.name==""+(isvalue+isdiv+3)||obj.name==""+(isvalue+isdiv+4))
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
		}
		else
		{
			int isdiv=isvalue/2;
			foreach(GameObject obj in _objArray )
			{
				if(obj.name==""+(isvalue+isdiv)||obj.name==""+(isvalue+isdiv+1)||obj.name==""+(isvalue+isdiv+3)||obj.name==""+(isvalue+isdiv+4))
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
		}
	}
	public void HSplitBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		isName=isName.Remove(0,2);
		int isvalue;
		int.TryParse(isName, out isvalue);
		if(isName=="0")
		{
				foreach(GameObject obj in _objArray )
				{
					if(obj.name=="0"||obj.name=="-1")
					{
						if(obj.renderer.enabled==false)
						obj.renderer.enabled=true;
					}
					else if(obj.renderer.enabled==true)
					obj.renderer.enabled=false;
				}
		}
		else
		{
			if(isvalue%2==0)
			{
				int isdiv=(isvalue/2)-1;
				foreach(GameObject obj in _objArray )
				{
					if(obj.name==""+(isvalue+isdiv)||obj.name==""+(isvalue+isdiv+1))
					{
						if(obj.renderer.enabled==false)
						obj.renderer.enabled=true;
					}
					else if(obj.renderer.enabled==true)
					obj.renderer.enabled=false;
				}
			}
			else
			{
				int isdiv=isvalue/2;
				foreach(GameObject obj in _objArray )
				{
					if(obj.name==""+(isvalue+isdiv)||obj.name==""+(isvalue+isdiv+1))
					{
						if(obj.renderer.enabled==false)
						obj.renderer.enabled=true;
					}
					else if(obj.renderer.enabled==true)
					obj.renderer.enabled=false;
				}
			}
		}
	}
	public void VSplitBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		string s1="";
		string s2="";
		isName=isName.Remove(0,2);
		int isvalue;
		int.TryParse(isName, out isvalue);
			//int isdiv=(isvalue/2);
		if(isName=="01"||isName=="03"||isName=="02")
		{
			if(isName=="01")
			{
				s1="0";s2="1";
			}
			else if(isName=="02")
			{
				s1="0";s2="2";
			}
			else if(isName=="03")
			{
				s1="0";s2="3";
			}
			foreach(GameObject obj in _objArray )
			{
				if(obj.name==s1||obj.name==s2)
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
		}
		else
		{
			foreach(GameObject obj in _objArray )
			{
				if(obj.name==""+(isvalue)||obj.name==""+(isvalue+3))
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
		}
	}
	public void StreetBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		isName=isName.Remove(0,2);
		int isvalue;
		int.TryParse(isName, out isvalue);
			int isdiv=(isvalue*3);
			foreach(GameObject obj in _objArray )
			{
				if(obj.name==""+(isdiv)||obj.name==""+(isdiv-2)||obj.name==""+(isdiv-1))
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
	}
	public void LineBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		isName=isName.Remove(0,1);
		int isvalue;
		int.TryParse(isName, out isvalue);
			int isdiv=(isvalue*3);
			foreach(GameObject obj in _objArray )
			{
				if(obj.name==""+(isdiv)||obj.name==""+(isdiv-2)||obj.name==""+(isdiv-1)||obj.name==""+(isdiv+1)||obj.name==""+(isdiv+2)||obj.name==""+(isdiv+3))
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
	}
	public void FFBetRenderer()
	{
		foreach(GameObject obj in _objArray )
		{
				if(obj.name=="1"||obj.name=="2"||obj.name=="3"||obj.name=="0"||obj.name=="-1")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
	}
	public void BasketBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		foreach(GameObject obj in _objArray )
		{
			if(isName=="Bask1")
			{
				if(obj.name=="0"||obj.name=="1"||obj.name=="2")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
			else if(isName=="Bask2")
			{
				if(RT_DataHandler._StageNumber==1)
				{
					if(obj.name=="-1"||obj.name=="2"||obj.name=="3")
					{
						if(obj.renderer.enabled==false)
						obj.renderer.enabled=true;
					}
					else if(obj.renderer.enabled==true)
					obj.renderer.enabled=false;
				}
				else
				{
					if(obj.name=="0"||obj.name=="2"||obj.name=="3")
					{
						if(obj.renderer.enabled==false)
						obj.renderer.enabled=true;
					}
					else if(obj.renderer.enabled==true)
					obj.renderer.enabled=false;
				}
			}
		}
	}
	public void EvenBetRenderer()
	{
		foreach(GameObject obj in _objArray )
		{
				if(obj.name=="2"||obj.name=="4"||obj.name=="6"||obj.name=="8"||obj.name=="10"||obj.name=="12"||obj.name=="14"||obj.name=="16"||obj.name=="18"||obj.name=="20"||obj.name=="22"||obj.name=="24"||obj.name=="26"||obj.name=="28"||obj.name=="30"||obj.name=="32"||obj.name=="34"||obj.name=="36"||obj.name=="Even"||obj.name=="Pair")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
	}
	public void OddBetRenderer()
	{
		foreach(GameObject obj in _objArray )
		{
				if(obj.name=="1"||obj.name=="3"||obj.name=="5"||obj.name=="7"||obj.name=="9"||obj.name=="11"||obj.name=="13"||obj.name=="15"||obj.name=="17"||obj.name=="19"||obj.name=="21"||obj.name=="23"||obj.name=="25"||obj.name=="27"||obj.name=="29"||obj.name=="31"||obj.name=="33"||obj.name=="35"||obj.name=="Odd"||obj.name=="Impair")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
	}
	public void RedBetRenderer()
	{
		foreach(GameObject obj in _objArray )
		{
				if(obj.name=="1"||obj.name=="3"||obj.name=="5"||obj.name=="7"||obj.name=="9"||obj.name=="12"||obj.name=="14"||obj.name=="16"||obj.name=="18"||obj.name=="19"||obj.name=="21"||obj.name=="23"||obj.name=="25"||obj.name=="27"||obj.name=="30"||obj.name=="32"||obj.name=="34"||obj.name=="36"||obj.name=="Red")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
	}
	public void BlackBetRenderer()
	{
		foreach(GameObject obj in _objArray )
		{
				if(obj.name=="2"||obj.name=="4"||obj.name=="6"||obj.name=="8"||obj.name=="10"||obj.name=="11"||obj.name=="13"||obj.name=="15"||obj.name=="17"||obj.name=="20"||obj.name=="22"||obj.name=="24"||obj.name=="26"||obj.name=="28"||obj.name=="29"||obj.name=="31"||obj.name=="33"||obj.name=="35"||obj.name=="Black")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
	}
	public void DozenBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		isName=isName.Remove(0,5);
		int isvalue;
		int.TryParse(isName, out isvalue);
		//isvalue=(12*(isvalue-1));
		foreach(GameObject obj in _objArray )
		{
				if(obj.name==""+(1+12*(isvalue-1))||obj.name==""+(2+12*(isvalue-1))||obj.name==""+(3+12*(isvalue-1))||obj.name==""+(4+12*(isvalue-1))||obj.name==""+(5+12*(isvalue-1))||obj.name==""+(6+12*(isvalue-1))||obj.name==""+(7+12*(isvalue-1))||obj.name==""+(8+12*(isvalue-1))||obj.name==""+(9+12*(isvalue-1))||obj.name==""+(10+12*(isvalue-1))||obj.name==""+(11+12*(isvalue-1))||obj.name==""+(12+12*(isvalue-1))||obj.name=="Dozen"+isvalue||obj.name==RT_DataHandler._hit.collider.gameObject.name)
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}

	}
	public void ColumnBetRenderer()
	{
		string isName=RT_DataHandler._hit.collider.gameObject.name;
		isName=isName.Remove(0,3);
		int isvalue;
		int.TryParse(isName, out isvalue);
		foreach(GameObject obj in _objArray )
		{
				if(obj.name==""+(0+isvalue)||obj.name==""+(3+isvalue)||obj.name==""+(6+isvalue)||obj.name==""+(9+isvalue)||obj.name==""+(12+isvalue)||obj.name==""+(15+isvalue)||obj.name==""+(18+isvalue)||obj.name==""+(21+isvalue)||obj.name==""+(24+isvalue)||obj.name==""+(27+isvalue)||obj.name==""+(30+isvalue)||obj.name==""+(33+isvalue)||obj.name=="Col"+isvalue)
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
	}
	public void LowBetRenderer()
	{
		foreach(GameObject obj in _objArray )
		{
				if(obj.name=="1"||obj.name=="2"||obj.name=="3"||obj.name=="4"||obj.name=="5"||obj.name=="6"||obj.name=="7"||obj.name=="8"||obj.name=="9"||obj.name=="10"||obj.name=="11"||obj.name=="12"||obj.name=="13"||obj.name=="14"||obj.name=="15"||obj.name=="16"||obj.name=="17"||obj.name=="18"||obj.name=="Low"||obj.name=="Manque")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
	}
	public void HighBetRenderer()
	{
		foreach(GameObject obj in _objArray )
		{
				if(obj.name=="19"||obj.name=="20"||obj.name=="21"||obj.name=="22"||obj.name=="23"||obj.name=="24"||obj.name=="25"||obj.name=="26"||obj.name=="27"||obj.name=="28"||obj.name=="29"||obj.name=="30"||obj.name=="31"||obj.name=="32"||obj.name=="33"||obj.name=="34"||obj.name=="35"||obj.name=="36"||obj.name=="High"||obj.name=="Passe")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
	}
	public void VoisinsBetRenderer()
	{
		foreach(GameObject obj in _RaceTrackObs )
		{
				if(obj.name=="0"||obj.name=="32"||obj.name=="15"||obj.name=="19"||obj.name=="4"||obj.name=="21"||obj.name=="2"||obj.name=="26"||obj.name=="25"||obj.name=="3"||obj.name=="35"||obj.name=="12"||obj.name=="28"||obj.name=="7"||obj.name=="29"||obj.name=="18"||obj.name=="22"||obj.name=="Voisins"||obj.name=="Jeu0")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
		foreach(GameObject obj1 in _objArray )
		{
				if(obj1.name=="0"||obj1.name=="32"||obj1.name=="15"||obj1.name=="19"||obj1.name=="4"||obj1.name=="21"||obj1.name=="2"||obj1.name=="26"||obj1.name=="25"||obj1.name=="3"||obj1.name=="35"||obj1.name=="12"||obj1.name=="28"||obj1.name=="7"||obj1.name=="29"||obj1.name=="18"||obj1.name=="22")
				{
					if(obj1.renderer.enabled==false)
					obj1.renderer.enabled=true;
				}
				else if(obj1.renderer.enabled==true)
				obj1.renderer.enabled=false;
		}
	}
	public void TiersBetRenderer()
	{
		foreach(GameObject obj in _RaceTrackObs )
		{
				if(obj.name=="27"||obj.name=="13"||obj.name=="36"||obj.name=="11"||obj.name=="30"||obj.name=="8"||obj.name=="23"||obj.name=="10"||obj.name=="5"||obj.name=="24"||obj.name=="16"||obj.name=="33"||obj.name=="32"||obj.name=="Tiers")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
		foreach(GameObject obj1 in _objArray )
		{
				if(obj1.name=="27"||obj1.name=="13"||obj1.name=="36"||obj1.name=="11"||obj1.name=="30"||obj1.name=="8"||obj1.name=="23"||obj1.name=="10"||obj1.name=="5"||obj1.name=="24"||obj1.name=="16"||obj1.name=="33"||obj1.name=="32")
				{
					if(obj1.renderer.enabled==false)
					obj1.renderer.enabled=true;
				}
				else if(obj1.renderer.enabled==true)
				obj1.renderer.enabled=false;
		}
	}
	public void OrphelinesBetrenderer()
	{
		foreach(GameObject obj in _RaceTrackObs )
		{
				if(obj.name=="17"||obj.name=="34"||obj.name=="6"||obj.name=="9"||obj.name=="31"||obj.name=="14"||obj.name=="20"||obj.name=="1"||obj.name=="Orphelins")
				{
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
		foreach(GameObject obj1 in _objArray )
		{
				if(obj1.name=="17"||obj1.name=="34"||obj1.name=="6"||obj1.name=="9"||obj1.name=="31"||obj1.name=="14"||obj1.name=="20"||obj1.name=="1")
				{
					if(obj1.renderer.enabled==false)
					obj1.renderer.enabled=true;
				}
				else if(obj1.renderer.enabled==true)
				obj1.renderer.enabled=false;
		}
	}
	public void JeuBetRenderer()
	{
		foreach(GameObject obj in _RaceTrackObs )
		{
			
				if(obj.name=="12"||obj.name=="35"||obj.name=="3"||obj.name=="26"||obj.name=="0"||obj.name=="32"||obj.name=="15"||obj.name=="Jeu0")
				{
				   
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
		}
		foreach(GameObject obj1 in _objArray )
		{
			
				if(obj1.name=="12"||obj1.name=="35"||obj1.name=="3"||obj1.name=="26"||obj1.name=="0"||obj1.name=="32"||obj1.name=="15")
				{
				   
					if(obj1.renderer.enabled==false)
					obj1.renderer.enabled=true;
				}
				else if(obj1.renderer.enabled==true)
				obj1.renderer.enabled=false;
		}
	}
	public void NNBetRenderer(string isName)
	{
		string s1="";
		string s2="";
		string s3="";
		string s4="";
		string s5="";
		switch(isName)
		{
		case "1" :
				s1="14";s2="20";s3="1";s4="33";s5="16";
			break;
		case "2":	
				s1="4";s2="21";s3="2";s4="25";s5="17";
			break;
		case "3":
				s1="0";s2="26";s3="3";s4="35";s5="12";
			break;
		case "4":
				s1="15";s2="19";s3="4";s4="21";s5="2";
			break;
		case "5":
				s1="23";s2="10";s3="5";s4="24";s5="16";
			break;
		case "6":
				s1="17";s2="34";s3="6";s4="27";s5="13";
			break;
		case "7":
				s1="12";s2="28";s3="7";s4="29";s5="18";
			break;
		case "8":
				s1="11";s2="30";s3="8";s4="23";s5="10";
			break;
		case "9":
				s1="18";s2="22";s3="9";s4="31";s5="14";
			break;
		case "10":
				s1="24";s2="5";s3="10";s4="23";s5="8";
			break;
		case "11":
				s1="13";s2="36";s3="11";s4="30";s5="8";
			break;
		case "12":
				s1="3";s2="35";s3="12";s4="28";s5="7";
			break;
		case "13":
				s1="6";s2="27";s3="13";s4="36";s5="11";
			break;
		case "14":
				s1="9";s2="31";s3="14";s4="20";s5="1";
			break;
		case "15":
				s1="0";s2="32";s3="15";s4="19";s5="4";
			break;
		case "16":
				s1="1";s2="33";s3="16";s4="24";s5="5";
			break;
		case "17":
				s1="2";s2="25";s3="17";s4="34";s5="6";
			break;
		case "18":
				s1="7";s2="29";s3="18";s4="22";s5="9";
			break;
		case "19":
				s1="32";s2="15";s3="19";s4="4";s5="21";
			break;
		case "20":
				s1="31";s2="14";s3="20";s4="1";s5="33";
			break;
		case "21":
				s1="19";s2="4";s3="21";s4="2";s5="25";
			break;
		case "22":
				s1="29";s2="18";s3="22";s4="9";s5="31";
			break;
		case "23":
				s1="30";s2="8";s3="23";s4="10";s5="5";
			break;
		case "24":
				s1="33";s2="16";s3="24";s4="5";s5="10";
				break;
		case "25":
				s1="21";s2="2";s3="25";s4="17";s5="34";
				break;
		case "26":
				s1="32";s2="0";s3="26";s4="3";s5="35";
				break;
		case "27":
				s1="34";s2="6";s3="27";s4="13";s5="36";
				break;
		case "28":
				s1="35";s2="12";s3="28";s4="7";s5="29";
				break;
		case "29":
				s1="28";s2="7";s3="29";s4="18";s5="22";
				break;
		case "30":
				s1="36";s2="11";s3="30";s4="8";s5="23";
				break;
		case "31":
				s1="22";s2="9";s3="31";s4="14";s5="20";
				break;
		case "32":
				s1="26";s2="0";s3="32";s4="15";s5="19";
				break;
		case "33":
				s1="20";s2="1";s3="33";s4="16";s5="24";
				break;
		case "34":
				s1="25";s2="17";s3="34";s4="6";s5="27";
				break;
		case "35":
				s1="26";s2="3";s3="35";s4="12";s5="28";
				break;
		case "36":
				s1="27";s2="13";s3="36";s4="11";s5="30";
				break;
		case "0":
				s1="15";s2="32";s3="0";s4="26";s5="3";
				break;
		}
			foreach(GameObject obj in _RaceTrackObs )
			{
			
				if(obj.name==s1||obj.name==s2||obj.name==s3||obj.name==s4||obj.name==s5)
				{
				   
					if(obj.renderer.enabled==false)
					obj.renderer.enabled=true;
				}
				else if(obj.renderer.enabled==true)
				obj.renderer.enabled=false;
			}
			foreach(GameObject obj1 in _objArray )
			{
			
				if(obj1.name==s1||obj1.name==s2||obj1.name==s3||obj1.name==s4||obj1.name==s5)
				{
				   
					if(obj1.renderer.enabled==false)
					obj1.renderer.enabled=true;
				}
				else if(obj1.renderer.enabled==true)
				obj1.renderer.enabled=false;
			}
	}
	public void BetChecker(Transform isTran)
	{
		string isTagname=isTran.tag;
		switch(isTagname)
		{
			case "Straight" :
				
			    if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
				{
			     RT_DataHandler._StraightBets.Add(isTran.gameObject);
				 isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
				}
				break;
			case "Corner" :	
			
				if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
				{
			     RT_DataHandler._CornerBets.Add(isTran.gameObject);
				 isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
				}	
				break;
			case "Split":
					if(isTran.name.Remove(2)=="HS")
					{
				 		
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._HSplitBets.Add(isTran.gameObject);
						//	print(isTran.name);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					}
					if(isTran.name.Remove(2)=="VS")
					{
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._VSplitBets.Add(isTran.gameObject);
					//		print(isTran.name);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					}
				break;
			case "Street":
			 			if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._StreetBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
				break;
			case "Line" :
					    if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._LineBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
				break;
			case "FirstFive":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._FFBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
				break;
			case "Basket" :
					    if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._BasketBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
				break;
			case "OutSide":
				switch(RT_DataHandler._hit.collider.name)
				{
				case "Even":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._EvenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Odd":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._OddBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Red":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
							//print("Red");
			     			RT_DataHandler._RedBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Black":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._BlackBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;	
				case "Dozen1":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Dozen2":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Dozen3":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;	
				case "Col1":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._ColumnBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Col2":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._ColumnBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Col3":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._ColumnBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;	
				case "Low":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._Lowbets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "High":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._HighBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Manque":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._Lowbets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Passe":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._HighBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Impair":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._OddBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "Pair":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._EvenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "P11201":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
							print("asdasdsdd");
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "P21201":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
							print("asdasdsdd");
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "M11202":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "M21202":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "D11203":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;
				case "D21203":
						if(isTran.GetComponent<RT_BetColliderProperty>()._betOn==false)
						{
			     			RT_DataHandler._DozenBets.Add(isTran.gameObject);
							isTran.GetComponent<RT_BetColliderProperty>()._betOn=true;
						}
					break;	
				}
			break;

			}
		}
	public void CreateAmountPopUp(Transform isTran,int isAmount)
	{
//		float isHR=Screen.height/768f;
//		float isWR=Screen.width/1024f;
//		if(Screen.width>=2048)
//		{
//			isHR=Screen.height/1536f;
//			isWR=Screen.width/2048f;
//		}
//		Vector3 ispos=Camera.main.WorldToViewportPoint(new Vector3(isTran.position.x,isTran.position.y,isTran.position.z));
//	    GameObject isBetAmountBox=Instantiate(mPopUpBox,new Vector3(ispos.x,ispos.y+0.1f*isHR,1),Quaternion.identity) as GameObject;
//		isBetAmountBox.transform.localScale=new Vector3(isBetAmountBox.transform.localScale.x*isWR,isBetAmountBox.transform.localScale.y*isHR,1);
//		GameObject   isBetAmountOnThis=Instantiate(mBetAmounttext,new Vector3(ispos.x,ispos.y+0.117f*isHR,2),Quaternion.identity) as GameObject;
//		isBetAmountOnThis.guiText.fontSize=(int)(25*mfSHRatio);
//		isBetAmountOnThis.guiText.text=""+isAmount;
	}
	int TotalCoinAmount(GameObject obj)
	{
		int isAmountTimes=1;
		if(obj.tag=="RaceTrack")
		{
			if(obj.name=="Voisins")
			{
				isAmountTimes=9;
			}
			else if(obj.name=="Tiers")
			{
				isAmountTimes=6;
			}
			else if(obj.name=="Orphelins")
			{
				isAmountTimes=5;
			}
			else if(obj.name=="Jeu0")
			{
				isAmountTimes=4;
			}
			else
			{
				isAmountTimes=5;
			}
		}
		else
		{
			isAmountTimes=1;
		}
		return isAmountTimes;
	}
	void ChipSelectionAndBetRenderer()
	{
		if(InputHandler.instance.isTouchBegan())
		 {  
			mray= Camera.main.ScreenPointToRay(InputHandler.instance.touchPositions());
     		 if(Physics.Raycast(mray, out mhit,Mathf.Infinity,10000000)&&mbrun==false)         
      		 {                 
				GameObject isselectObject=mhit.collider.gameObject; 
				if(isselectObject.tag=="BettedChip")
				{
					SelectedCoinAmount=isselectObject.GetComponent<ChipProperty>()._AmountOnthis;
					mcurrentselectObject=isselectObject;
					ChipProperty ischipProperty= mcurrentselectObject.GetComponent<ChipProperty>();
					mObjectInitialPosition=ischipProperty._intialPosition;
				
				    GameObject isBettedObj=ischipProperty._bettedObject;
					
						RT_DataHandler._TotalbettingAmount-=ischipProperty._AmountOnthis;
						RT_DataHandler._TotalfundYouHave+=ischipProperty._AmountOnthis;
				
					SetValuesOnBetRemove(isBettedObj,ischipProperty._AmountOnthis);
					if(RT_DataHandler._TotalbettingAmount==0)
					{
						GameObject obj = GameObject.Find("GamesceneGUIHandler");
						if(obj != null)
						{
							GamesceneGUIHandler handlerScr = obj.GetComponent<GamesceneGUIHandler>();
							handlerScr.SpinButton.setDisabled(true);
							handlerScr.ClearButton.setDisabled(true);
						}
					}	
					if( isBettedObj.GetComponent<RT_BetColliderProperty>()._bettingAmountOnThis==0)
					{
						isBettedObj.GetComponent<RT_BetColliderProperty>()._betOn=false;
						RemoveBet(isBettedObj);
					}
					
					RT_DataHandler._TotalbettingAmountText.guiText.text=" "+RT_DataHandler._TotalbettingAmount;
					RT_DataHandler._TotalfundYouHaveText.guiText.text=" "+(RT_DataHandler._TotalfundYouHave);
					mbrun=true;
				}
        		else if(isselectObject.tag=="Chip")
        		{  
					mSound.PlaySound(mSound._CoinSelected);
					SelectedCoinAmount=isselectObject.GetComponent<ChipProperty>()._AmountOnthis;
				    mcurrentselectObject=Instantiate(isselectObject,isselectObject.transform.position,isselectObject.transform.rotation)as GameObject;
				    ChipProperty isChipProperty=mcurrentselectObject.GetComponent<ChipProperty>();
					isChipProperty._intialPosition=mcurrentselectObject.transform.position;
				    mObjectInitialPosition=mcurrentselectObject.transform.position;
					mbrun=true; 
        		}
				else
					mbrun=false;  
			}		
	  	}
  		if(mbrun==true)
  		{  
			
			mcurrentselectObject.transform.position=Camera.main.ScreenToWorldPoint(new Vector3(InputHandler.instance.touchPositions().x,InputHandler.instance.touchPositions().y,9.90f))+mchipDistancefromMouse;
			mray=new Ray(mcurrentselectObject.transform.position,Vector3.forward);
			if(Physics.Raycast(mray, out RT_DataHandler._hit,Mathf.Infinity,100000000))
   			{
				//Debug.Log("wddsd");
				mName.text=RT_DataHandler._hit.collider.name;
				if(RT_DataHandler._hit.collider.tag!="Untagged"&&RT_DataHandler._hit.collider.tag!="Chip")
				{
					mbBettinginDone=true;
					mcolliderObject=RT_DataHandler._hit.collider.transform;
						// Debug.Log("mbBettinginDone  "+mbBettinginDone);
				}else
				{
						mbBettinginDone=false;
						// Debug.Log("mbBettinginDone  "+mbBettinginDone);
				}
				/*-----------For renderer On/Off--------------*/
		
				if(mPreviousSelectedObj!=RT_DataHandler._hit.collider.gameObject)
				{
					mPreviousSelectedObj=RT_DataHandler._hit.collider.gameObject;
				string isTagname=RT_DataHandler._hit.collider.tag;
				switch(isTagname)
				{
				case "Straight" :
					foreach(GameObject obj in _objArray )
					{
						if(RT_DataHandler._hit.collider.gameObject.name==obj.name)
						{
							if(RT_DataHandler._hit.collider.renderer.enabled==false)
							RT_DataHandler._hit.collider.renderer.enabled=true;
						}
						else if(obj.renderer.enabled==true)
							obj.renderer.enabled=false;
					}
					break;
				case "Corner" :	
					CornerBetRenderer();
					break;
				case "Split":
					
					if(RT_DataHandler._hit.collider.name.Remove(2)=="HS")
						HSplitBetRenderer();
					if(RT_DataHandler._hit.collider.name.Remove(2)=="VS")
						VSplitBetRenderer();
					break;
				case "Street":
					StreetBetRenderer();
					break;
				case "Line" :
					LineBetRenderer();
					break;
				case "FirstFive":
					FFBetRenderer();
					break;
				case "Basket" :
					BasketBetRenderer();
					break;
				case "OutSide":
					switch(RT_DataHandler._hit.collider.name)
					{
					case "Even":
						EvenBetRenderer();
						break;
					case "Odd":
						OddBetRenderer();
						break;
					case "Red":
						RedBetRenderer();
						break;
					case "Black":
						BlackBetRenderer();
						break;	
					case "Dozen1":
						DozenBetRenderer();
						break;
					case "Dozen2":
						DozenBetRenderer();
						break;
					case "Dozen3":
						DozenBetRenderer();
						break;	
					case "Col1":
						ColumnBetRenderer();
						break;
					case "Col2":
						ColumnBetRenderer();
						break;
					case "Col3":
						ColumnBetRenderer();
						break;	
					case "Low":
						LowBetRenderer();
						break;
					case "High":
						HighBetRenderer();
						break;
					case "Manque":
						LowBetRenderer();
						break;
					case "Passe":
						HighBetRenderer();
						break;
					case "Impair":
						OddBetRenderer();
						break;
					case "Pair":
						EvenBetRenderer();
						break;
					case "P11201":
						DozenBetRenderer();
						break;
					case "P21201":
						DozenBetRenderer();
						break;
					case "M11202":
						DozenBetRenderer();
						break;
					case "M21202":
						DozenBetRenderer();
						break;
					case "D11203":
						DozenBetRenderer();
						break;
					case "D21203":
						DozenBetRenderer();
						break;	
						
					default :
						foreach(GameObject obj in _objArray )
						{
						 if(obj.renderer.enabled==true)
							obj.renderer.enabled=false;
						}
					break;
					}
					break;
				case "RaceTrack":
					switch(RT_DataHandler._hit.collider.name)
					{
						
					case "Jeu0":
						JeuBetRenderer();
						break;
					case "Orphelins":
						OrphelinesBetrenderer();
						break;
					case "Voisins":
						VoisinsBetRenderer();
						break;
					case "Tiers":
						TiersBetRenderer();
						break;
					default :
						NNBetRenderer(RT_DataHandler._hit.collider.name);
						break;
					}
					break;
				 default :
					foreach(GameObject obj in _objArray )
					{
						 if(obj.renderer.enabled==true)
							obj.renderer.enabled=false;
					}
					if(RT_DataHandler._StageNumber!=1)
					{
						foreach(GameObject obj1 in _RaceTrackObs )
						{
							 if(obj1.renderer.enabled==true)
								obj1.renderer.enabled=false;
						}
					}
					break;
				}
				}
			
			}
			if(InputHandler.instance.touchPositions().x>Screen.width||InputHandler.instance.touchPositions().x<0||InputHandler.instance.touchPositions().y>Screen.height||(InputHandler.instance.touchPositions().y< 0)) //mfSHRatio*100f
			{
					mbBettinginDone=false;
					foreach(GameObject obj in _objArray )
					{
					 if(obj.renderer.enabled==true)
						obj.renderer.enabled=false;
					}
					if(RT_DataHandler._StageNumber!=1)
					{
						foreach(GameObject obj1 in _RaceTrackObs )
						{
							 if(obj1.renderer.enabled==true)
								obj1.renderer.enabled=false;
						}
					}
				}
			}
		}
	void RemoveBet(GameObject isBettedObject)
	{
		string isTagname=isBettedObject.tag;
		switch(isTagname)
		{
		case "Straight" :
			RT_DataHandler._StraightBets.Remove(isBettedObject);
			break;
		case "Corner" :	
			RT_DataHandler._CornerBets.Remove(isBettedObject);
			break;
		case "Split":
			if(isBettedObject.name.Remove(2)=="HS")
				RT_DataHandler._HSplitBets.Remove(isBettedObject);
			if(isBettedObject.name.Remove(2)=="VS")
				RT_DataHandler._VSplitBets.Remove(isBettedObject);
			break;
		case "Street":
			RT_DataHandler._StreetBets.Remove(isBettedObject);
			break;
		case "Line" :
			RT_DataHandler._LineBets.Remove(isBettedObject);
			break;
		case "FirstFive":
			RT_DataHandler._FFBets.Remove(isBettedObject);
			break;
		case "Basket" :
			RT_DataHandler._BasketBets.Remove(isBettedObject);
			break;
		case "OutSide":
			switch(isBettedObject.name)
			{
			case "Even":
				RT_DataHandler._EvenBets.Remove(isBettedObject);
				break;
			case "Odd":
				RT_DataHandler._OddBets.Remove(isBettedObject);
				break;
			case "Red":
				RT_DataHandler._RedBets.Remove(isBettedObject);
				break;
			case "Black":
				RT_DataHandler._BlackBets.Remove(isBettedObject);
				break;	
			case "Dozen1":
				//Debug.Log("DozenBetBeforeRemove  "+RT_DataHandler._DozenBets.Count);
				RT_DataHandler._DozenBets.Remove(isBettedObject);
			//	Debug.Log("DozenBetRemove  "+RT_DataHandler._DozenBets.Count);
				break;
			case "Dozen2":
				//Debug.Log("DozenBetBeforeRemove  "+RT_DataHandler._DozenBets.Count);
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				//Debug.Log("DozenBetRemove  "+RT_DataHandler._DozenBets.Count);
				break;
			case "Dozen3":
				//Debug.Log("DozenBetBeforeRemove  "+RT_DataHandler._DozenBets.Count);
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				//Debug.Log("DozenBetRemove  "+RT_DataHandler._DozenBets.Count);
				break;	
			case "Col1":
				RT_DataHandler._ColumnBets.Remove(isBettedObject);
				break;
			case "Col2":
				RT_DataHandler._ColumnBets.Remove(isBettedObject);
				break;
			case "Col3":
				RT_DataHandler._ColumnBets.Remove(isBettedObject);
				break;	
			case "Low":
				RT_DataHandler._Lowbets.Remove(isBettedObject);
				break;
			case "High":
				RT_DataHandler._HighBets.Remove(isBettedObject);
				break;
			case "Manque":
				RT_DataHandler._Lowbets.Remove(isBettedObject);
				break;
			case "Passe":
				RT_DataHandler._HighBets.Remove(isBettedObject);
				break;
			case "Impair":
				RT_DataHandler._OddBets.Remove(isBettedObject);
				break;
			case "Pair":
				RT_DataHandler._EvenBets.Remove(isBettedObject);
				break;
			case "P11201":
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				break;
			case "P21201":
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				break;
			case "M11202":
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				break;
			case "M21202":
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				break;
			case "D11203":
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				break;
			case "D21203":
				RT_DataHandler._DozenBets.Remove(isBettedObject);
				break;		
			}
			break;
		}
	}
	
//	public void JeuBetRemover(int isAmount,GameObject isChip)
//	{
//		ArrayList isRelatedChip=isChip.GetComponent<ChipProperty>()._ChipRelatedtoRaceTrackChip;
//		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("26");
//		SetValuesOnRaceTrackBetRemove(mCollederObj1,isRelatedChip,isAmount);
//
//		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS12");
//			SetValuesOnRaceTrackBetRemove(mCollederObj2,isRelatedChip,isAmount);
//
//		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS32");
//		SetValuesOnRaceTrackBetRemove(mCollederObj3,isRelatedChip,isAmount);
//
//		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS03");
//		SetValuesOnRaceTrackBetRemove(mCollederObj4,isRelatedChip,isAmount);
//	}
//	
//	public void OrphlinsBetRemover(int isAmount,GameObject isChip)
//	{
//		ArrayList isRelatedChip=isChip.GetComponent<ChipProperty>()._ChipRelatedtoRaceTrackChip;
//		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("1");
//		SetValuesOnRaceTrackBetRemove(mCollederObj1,isRelatedChip,isAmount);
//
//		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS6");
//			SetValuesOnRaceTrackBetRemove(mCollederObj2,isRelatedChip,isAmount);
//
//		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS14");
//		SetValuesOnRaceTrackBetRemove(mCollederObj3,isRelatedChip,isAmount);
//
//		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS17");
//		SetValuesOnRaceTrackBetRemove(mCollederObj4,isRelatedChip,isAmount);
//		
//		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS31");
//		SetValuesOnRaceTrackBetRemove(mCollederObj5,isRelatedChip,isAmount);
//	}
//	
//	public void TierBetRemover(int isAmount,GameObject isChip)
//	{
//		ArrayList isRelatedChip=isChip.GetComponent<ChipProperty>()._ChipRelatedtoRaceTrackChip;
//		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS5");
//		SetValuesOnRaceTrackBetRemove(mCollederObj1,isRelatedChip,isAmount);
//
//		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS13");
//			SetValuesOnRaceTrackBetRemove(mCollederObj2,isRelatedChip,isAmount);
//
//		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS27");
//		SetValuesOnRaceTrackBetRemove(mCollederObj3,isRelatedChip,isAmount);
//
//		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS33");
//		SetValuesOnRaceTrackBetRemove(mCollederObj4,isRelatedChip,isAmount);
//		
//		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("HS7");
//		SetValuesOnRaceTrackBetRemove(mCollederObj5,isRelatedChip,isAmount);
//
//		Transform mCollederObj6=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("HS16");
//		SetValuesOnRaceTrackBetRemove(mCollederObj6,isRelatedChip,isAmount);
//	}
//	
//	public void VoisinsBetRemover(int isAmount,GameObject isChip)
//	{
//		ArrayList isRelatedChip=isChip.GetComponent<ChipProperty>()._ChipRelatedtoRaceTrackChip;
//		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS4");
//		SetValuesOnRaceTrackBetRemove(mCollederObj1,isRelatedChip,isAmount);
//
//		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS12");
//			SetValuesOnRaceTrackBetRemove(mCollederObj2,isRelatedChip,isAmount);
//
//		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS18");
//		SetValuesOnRaceTrackBetRemove(mCollederObj3,isRelatedChip,isAmount);
//
//		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS19");
//		SetValuesOnRaceTrackBetRemove(mCollederObj4,isRelatedChip,isAmount);
//	
//		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS32");
//		SetValuesOnRaceTrackBetRemove(mCollederObj5,isRelatedChip,isAmount);
//
//		Transform mCollederObj6=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("Bask2");
//			SetValuesOnRaceTrackBetRemove(mCollederObj6,isRelatedChip,isAmount);
//
//		Transform mCollederObj7=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("Bask2");
//		SetValuesOnRaceTrackBetRemove(mCollederObj7,isRelatedChip,isAmount);
//
//		Transform mCollederObj8=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("C17");
//		SetValuesOnRaceTrackBetRemove(mCollederObj8,isRelatedChip,isAmount);
//		
//		Transform mCollederObj9=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("C17");
//		SetValuesOnRaceTrackBetRemove(mCollederObj9,isRelatedChip,isAmount);
//	}
//	public void NNBetRemover(int isAmount,GameObject isChip,string isName)
//	{
//		string s1="";
//		string s2="";
//		string s3="";
//		string s4="";
//		string s5="";
//		switch(isName)
//		{
//		case "1" :
//				s1="14";s2="20";s3="1";s4="33";s5="16";
//			break;
//		case "2":	
//				s1="4";s2="21";s3="2";s4="25";s5="17";
//			break;
//		case "3":
//				s1="0";s2="26";s3="3";s4="35";s5="12";
//			break;
//		case "4":
//				s1="15";s2="19";s3="4";s4="21";s5="2";
//			break;
//		case "5":
//				s1="23";s2="10";s3="5";s4="24";s5="16";
//			break;
//		case "6":
//				s1="17";s2="34";s3="6";s4="27";s5="13";
//			break;
//		case "7":
//				s1="12";s2="28";s3="7";s4="29";s5="18";
//			break;
//		case "8":
//				s1="11";s2="30";s3="8";s4="23";s5="10";
//			break;
//		case "9":
//				s1="18";s2="22";s3="9";s4="31";s5="14";
//			break;
//		case "10":
//				s1="24";s2="5";s3="10";s4="23";s5="8";
//			break;
//		case "11":
//				s1="13";s2="36";s3="11";s4="30";s5="8";
//			break;
//		case "12":
//				s1="3";s2="35";s3="12";s4="28";s5="7";
//			break;
//		case "13":
//				s1="6";s2="27";s3="13";s4="36";s5="11";
//			break;
//		case "14":
//				s1="9";s2="31";s3="14";s4="20";s5="1";
//			break;
//		case "15":
//				s1="0";s2="32";s3="15";s4="19";s5="4";
//			break;
//		case "16":
//				s1="1";s2="33";s3="16";s4="24";s5="5";
//			break;
//		case "17":
//				s1="2";s2="25";s3="17";s4="34";s5="6";
//			break;
//		case "18":
//				s1="7";s2="29";s3="18";s4="22";s5="9";
//			break;
//		case "19":
//				s1="32";s2="15";s3="19";s4="4";s5="21";
//			break;
//		case "20":
//				s1="31";s2="14";s3="20";s4="1";s5="33";
//			break;
//		case "21":
//				s1="19";s2="4";s3="21";s4="2";s5="25";
//			break;
//		case "22":
//				s1="29";s2="18";s3="22";s4="9";s5="31";
//			break;
//		case "23":
//				s1="30";s2="8";s3="23";s4="10";s5="5";
//			break;
//		case "24":
//				s1="33";s2="16";s3="24";s4="5";s5="10";
//				break;
//		case "25":
//				s1="21";s2="2";s3="25";s4="17";s5="34";
//				break;
//		case "26":
//				s1="32";s2="0";s3="26";s4="3";s5="35";
//				break;
//		case "27":
//				s1="34";s2="6";s3="27";s4="13";s5="36";
//				break;
//		case "28":
//				s1="35";s2="12";s3="28";s4="7";s5="29";
//				break;
//		case "29":
//				s1="28";s2="7";s3="29";s4="18";s5="22";
//				break;
//		case "30":
//				s1="36";s2="11";s3="30";s4="8";s5="23";
//				break;
//		case "31":
//				s1="22";s2="9";s3="31";s4="14";s5="20";
//				break;
//		case "32":
//				s1="26";s2="0";s3="32";s4="15";s5="19";
//				break;
//		case "33":
//				s1="20";s2="1";s3="33";s4="16";s5="24";
//				break;
//		case "34":
//				s1="25";s2="17";s3="34";s4="6";s5="27";
//				break;
//		case "35":
//				s1="26";s2="3";s3="35";s4="12";s5="28";
//				break;
//		case "36":
//				s1="27";s2="13";s3="36";s4="11";s5="30";
//				break;
//		case "0":
//				s1="15";s2="32";s3="0";s4="26";s5="3";
//				break;
//		}
//		
//		ArrayList isRelatedChip=isChip.GetComponent<ChipProperty>()._ChipRelatedtoRaceTrackChip;
//		Transform mCollederObj1=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s1);
//		//Debug.Log("gggggfgfg"+mCollederObj1+"   "+s1);
//		SetValuesOnRaceTrackBetRemove(mCollederObj1,isRelatedChip,isAmount);
//
//		Transform mCollederObj2=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s2);
//			SetValuesOnRaceTrackBetRemove(mCollederObj2,isRelatedChip,isAmount);
//
//		Transform mCollederObj3=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s3);
//		SetValuesOnRaceTrackBetRemove(mCollederObj3,isRelatedChip,isAmount);
//
//		Transform mCollederObj4=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s4);
//		SetValuesOnRaceTrackBetRemove(mCollederObj4,isRelatedChip,isAmount);
//		
//		Transform mCollederObj5=GameObject.FindGameObjectWithTag("Collider").transform.FindChild(s5);
//		SetValuesOnRaceTrackBetRemove(mCollederObj5,isRelatedChip,isAmount);
//	}
//		
//	void SetValuesOnRaceTrackBetRemove(Transform isColliderObj,ArrayList isRelatedChips,int isAmount)
//	{
//		//Debug.Log(isColliderObj);
//		RT_BetColliderProperty isrT_BetColliderProperty=isColliderObj.GetComponent<RT_BetColliderProperty>();
//		isrT_BetColliderProperty._ZPosition+=0.000001f;
//		isrT_BetColliderProperty._YPosition-=0.03f;
//		foreach(GameObject Obj in isRelatedChips)
//		{
//			if(isrT_BetColliderProperty._ChipArray.Contains(Obj))
//			{
//				isrT_BetColliderProperty._ChipArray.Remove(Obj);
//				break;
//			}
//		}
//		isrT_BetColliderProperty._bettingAmountOnThis-=isAmount;
//		if(isrT_BetColliderProperty._bettingAmountOnThis==0)
//			isrT_BetColliderProperty._betOn=false;
//		RT_DataHandler._TotalbettingAmount-=isAmount;
//		RT_DataHandler._TotalfundYouHave+=isAmount;
//	}
//	
	void SetValuesOnBetRemove(GameObject isBettedObj,int isAmountOnthis)
	{
		RT_BetColliderProperty isrT_BetColliderProperty=isBettedObj.GetComponent<RT_BetColliderProperty>();
	    isrT_BetColliderProperty._bettingAmountOnThis-=isAmountOnthis;
		if(isrT_BetColliderProperty._bettingAmountOnThis==0)
			isrT_BetColliderProperty._betOn=false;
		isrT_BetColliderProperty._ZPosition+=0.000001f;
		isrT_BetColliderProperty._YPosition-=0.03f;
		isrT_BetColliderProperty._ChipArray.Remove(mcurrentselectObject);
	}
}
