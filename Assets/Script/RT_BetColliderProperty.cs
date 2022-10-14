using UnityEngine;
using System.Collections;

public class RT_BetColliderProperty : MonoBehaviour
{
	public bool _betOn;
	public int _bettingAmountOnThis;
	public float _ZPosition; 
	public float _YPosition;
	public  ArrayList _ChipArray=new ArrayList();
	int[] michipAmountArray=new int[13] ;//{10000,5000,1000,500,100};
	public GameObject[] _Chips;
	
	
	void Start()
	{
		//michipAmountArray=new int[15];
//		if(this.tag=="RaceTrack")
//		{
//			if(RT_DataHandler._StageNumber==2)
//			{
//				_Chips=new GameObject[5];
//				michipAmountArray[0]=10000;
//				michipAmountArray[1]=5000;
//				michipAmountArray[2]=1000;
//				michipAmountArray[3]=500;
//				michipAmountArray[4]=100;
//				_Chips[0]=Resources.Load("Chip2/Chip_5") as GameObject;
//				_Chips[1]=Resources.Load("Chip2/Chip_4") as GameObject;
//				_Chips[2]=Resources.Load("Chip2/Chip_3") as GameObject;
//				_Chips[3]=Resources.Load("Chip2/Chip_2") as GameObject;
//				_Chips[4]=Resources.Load("Chip2/Chip_1") as GameObject;
//			}
//			if(RT_DataHandler._StageNumber==3)
//			{
//				_Chips=new GameObject[5];
//				michipAmountArray[0]=100000;
//				michipAmountArray[1]=75000;
//				michipAmountArray[2]=50000;
//				michipAmountArray[3]=25000;
//				michipAmountArray[4]=10000;
//				_Chips[0]=Resources.Load("Chip3/Chip_5") as GameObject;
//				_Chips[1]=Resources.Load("Chip3/Chip_4") as GameObject;
//				_Chips[2]=Resources.Load("Chip3/Chip_3") as GameObject;
//				_Chips[3]=Resources.Load("Chip3/Chip_2") as GameObject;
//				_Chips[4]=Resources.Load("Chip3/Chip_1") as GameObject;
//			}
//		}
		if(this.tag!="RaceTrack")
		{
			for(int i=0;i<13;i++)
			{
				michipAmountArray[i]=_Chips[i].GetComponent<ChipProperty>()._AmountOnthis;
			}
		}	
	}
	 
	public void ReplaceAllChipWithMinimum()
	{		
		int isTotalAmountOnThis=_bettingAmountOnThis;
		int iscount=0;
		int isTotalCoinNeeded=0;
		for(int i=0;i<michipAmountArray.Length;i++)
		{
			iscount=(isTotalAmountOnThis/michipAmountArray[i]);
			isTotalAmountOnThis=(isTotalAmountOnThis%michipAmountArray[i]);
			isTotalCoinNeeded=isTotalCoinNeeded+iscount;
			
		}
		if(isTotalCoinNeeded!=_ChipArray.Count&&isTotalAmountOnThis==0)
		{
			//	ReplaceChips(); ravi_naresh
		}
	}
	
	void ReplaceChips()
	{
	
		int isTotalAmountOnThis=_bettingAmountOnThis;
		int iscount=0;
		int isTotalCoinNeeded=0;

		SetInitialValue();
		for(int i=0;i<michipAmountArray.Length;i++)
		{
			iscount=(isTotalAmountOnThis/michipAmountArray[i]);
			isTotalAmountOnThis=(isTotalAmountOnThis%michipAmountArray[i]);
			isTotalCoinNeeded=isTotalCoinNeeded+iscount;
			for(int j=0;j<iscount;j++)
			{
				GameObject isChip= Instantiate(_Chips[i],new Vector3(transform.position.x,transform.position.y+_YPosition,transform.position.z+_ZPosition),_Chips[i].transform.rotation) as GameObject;
				//isChip.collider.enabled=false;
				Vector3 isPos=GameObject.Find("GameManager(Clone)").GetComponent<GameManager>().InitialPos[15-i];
				isPos=new Vector3(isPos.x,-3.1f*(Screen.height/768f),isPos.z);
				Destroy(isChip.GetComponent<ChipStartUpAnimation>());
				isChip.transform.localScale=new Vector3(0.71f*Screen.height/768f,0.70f*Screen.height/768f,1f);
				isChip.tag="BettedChip";
				ChipProperty ischipProperty=isChip.GetComponent<ChipProperty>();
				ischipProperty._bettedObject=gameObject;
				ischipProperty._intialPosition=isPos;	
				_ZPosition-=0.00001f;
			    _YPosition+=0.03f;
				_ChipArray.Add(isChip);
			}
		}
	}
	
//	public void ReplaceJeuChips()
//	{
//		Transform[] isCollederObj=new Transform[5];
//		isCollederObj[0]=transform;
//		isCollederObj[1]=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("26");
//		isCollederObj[2]=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS12");
//		isCollederObj[3]=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS32");
//		isCollederObj[4]=GameObject.FindGameObjectWithTag("Collider").transform.FindChild("VS03");
//		RT_BetColliderProperty[] isrT_BetColliderProperty=new RT_BetColliderProperty[isCollederObj.Length];
//		int isTotalAmountOnThis=_bettingAmountOnThis;
//		int iscount=0;
//		int isTotalCoinNeeded=0;
//		for(int i=0;i<isCollederObj.Length;i++)
//		{
//			isrT_BetColliderProperty[i]=isCollederObj[i].GetComponent<RT_BetColliderProperty>();
//			isrT_BetColliderProperty[i].SetInitialValue();
//		}
//		
//		for(int i=0;i<michipAmountArray.Length;i++)
//		{
//			
//			iscount=(isTotalAmountOnThis/michipAmountArray[i]);
//			isTotalAmountOnThis=(isTotalAmountOnThis%michipAmountArray[i]);
//			isTotalCoinNeeded=isTotalCoinNeeded+iscount;
//			for(int j=0;j<iscount;j++)
//			{
//				GameObject isParent=null;
//				for(int k=0;k<isCollederObj.Length;k++)
//				{
//					GameObject isChip= Instantiate(_Chips[i],new Vector3(isCollederObj[k].position.x,isCollederObj[k].position.y+isrT_BetColliderProperty[k]._YPosition,isCollederObj[k].position.z+isrT_BetColliderProperty[k]._ZPosition),_Chips[i].transform.rotation) as GameObject;
//					Vector3 isPos=GameObject.Find("GameManager(Clone)").GetComponent<GameManager>().InitialPos[4-i];
//					isPos=new Vector3(isPos.x,-3.1f*(Screen.height/768f),isPos.z);
//					Destroy(isChip.GetComponent<ChipStartUpAnimation>());
//					isChip.transform.localScale=new Vector3(0.71f*Screen.height/768f,0.70f*Screen.height/768f,1f);
//					isChip.tag="BettedChip";
//					ChipProperty ischipProperty=isChip.GetComponent<ChipProperty>();
//					ischipProperty._bettedObject=isCollederObj[k].gameObject;
//					ischipProperty._intialPosition=isPos;
//					isrT_BetColliderProperty[k]._ZPosition-=0.00001f;
//				    isrT_BetColliderProperty[k]._YPosition+=0.03f;
//					isrT_BetColliderProperty[k]._ChipArray.Add(isChip);
//					if(k==0)
//						isParent=isChip;
//					if(k!=0)
//					{
//						ischipProperty._ParentChip=isParent;
//						isParent.GetComponent<ChipProperty>()._ChipRelatedtoRaceTrackChip.Add(isChip);
//					}
//				}
//			}
//			
//		}
//		for(int i=1;i<isCollederObj.Length;i++)
//		{
//			if(isrT_BetColliderProperty[0]._bettingAmountOnThis!=isrT_BetColliderProperty[i]._bettingAmountOnThis)
//			{
//				int isAmountDifference=isrT_BetColliderProperty[i]._bettingAmountOnThis-isrT_BetColliderProperty[0]._bettingAmountOnThis;
//				isrT_BetColliderProperty[i].ReplaceRemainnigChips(isAmountDifference);
//				
//			}
//		}
//		
//	}
//	
	void SetInitialValue()
	{
		foreach(GameObject isChip in _ChipArray)
			Destroy(isChip);
		_ChipArray.Clear();
		_ZPosition=0;
		_YPosition=0;
	}
	
//	void ReplaceRemainnigChips(int difference)
//	{
//		int isTotalAmountOnThis=difference;
//		int iscount=0;
//		int isTotalCoinNeeded=0;
//		for(int i=0;i<michipAmountArray.Length;i++)
//		{
//			iscount=(isTotalAmountOnThis/michipAmountArray[i]);
//			isTotalAmountOnThis=(isTotalAmountOnThis%michipAmountArray[i]);
//			isTotalCoinNeeded=isTotalCoinNeeded+iscount;
//			for(int j=0;j<iscount;j++)
//			{
//				GameObject isChip= Instantiate(_Chips[i],new Vector3(transform.position.x,transform.position.y+_YPosition,transform.position.z+_ZPosition),_Chips[i].transform.rotation) as GameObject;
//				//isChip.collider.enabled=false;
//				Vector3 isPos=GameObject.Find("GameManager(Clone)").GetComponent<GameManager>().InitialPos[4-i];
//				isPos=new Vector3(isPos.x,-3.1f*(Screen.height/768f),isPos.z);
//				Destroy(isChip.GetComponent<ChipStartUpAnimation>());
//				isChip.transform.localScale=new Vector3(0.71f*Screen.height/768f,0.70f*Screen.height/768f,1f);
//				isChip.tag="BettedChip";
//				ChipProperty ischipProperty=isChip.GetComponent<ChipProperty>();
//				ischipProperty._bettedObject=gameObject;
//				ischipProperty._intialPosition=isPos;
//				_ZPosition-=0.00001f;
//			    _YPosition+=0.03f;
//				_ChipArray.Add(isChip);
//			}
//			
//		}
//	}
}

