using UnityEngine;
using System.Collections;

public class RTS : MonoBehaviour {
	
	public delegate void ObjectAnimator(GameObject isObject,Vector3 strat,Vector3 end,float val);
	public ObjectAnimator _DelegateFunction;
	
	public delegate void CallBack(object obj);
	CallBack mCallBack;

	GameObject mObject;
	Vector3 mStartPosition;
	Vector3 mEndPosition;
	float mTime;
	float mvalue;
	float mDelay;
	float mCountDelay;
	int miRepeat;
	int miRepeatCount;
	bool mbPingPongStatus;
	bool mRepeatWithDelay;
	
	// Use this for initialization
	void Start () 
	{
		mvalue=0f;
		miRepeatCount=0;
		if(mbPingPongStatus)
			miRepeat=2*miRepeat+1;
		//InvokeRepeating("FunctionForCheck",0,Time.deltaTime);
		//StartCoroutine("FunctionForCheck");
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(mCountDelay<mDelay)
			mCountDelay+=Time.deltaTime;
		if(mCountDelay>=mDelay)
		{
				mvalue=Mathf.MoveTowards(mvalue,1,Time.deltaTime/mTime);
			if(mObject!=null)
				_DelegateFunction(mObject,mStartPosition,mEndPosition,mvalue);
			else
			{
				Debug.Log("Animated is Destroy");
				Destroy(gameObject);
			}
			if(mvalue==1)
			{
				if(miRepeatCount<miRepeat||miRepeat<=-1)
				{
					miRepeatCount++;
					mvalue=0;
					if(mRepeatWithDelay&&mbPingPongStatus==false)
						mCountDelay=0.0f;
					else if(mRepeatWithDelay&&mbPingPongStatus)
					{
						if(miRepeatCount%2==0)
							mCountDelay=0.0f;
					}
					
					if(mbPingPongStatus)
					SetPingPongValue();
				}
				else
				{
					if(mCallBack!=null)
					mCallBack(0);
					Destroy(gameObject);
				}
			}
		}
	}
	
	public void SetInitialValue(GameObject isObject,Vector3 strat,Vector3 end,float val,float isDelay,int isRepeat,bool RepeatWithdelay,bool isPingPongStatus,CallBack callBackFun)
	{
		mObject=isObject;
		mStartPosition=strat;
		mEndPosition=end;
		mTime=val;
		mDelay=isDelay;
		miRepeat=isRepeat;
		mbPingPongStatus=isPingPongStatus;
		mCallBack=callBackFun;
		mRepeatWithDelay=RepeatWithdelay;
	}
	void SetPingPongValue()
	{
		mvalue=0;
		Vector3 istemp=mStartPosition;
		mStartPosition=mEndPosition;
		mEndPosition=istemp;
	}
	
//	IEnumerator FunctionForCheck()
//	{
//		if(mCountDelay<mDelay)
//			mCountDelay+=Time.deltaTime;
//		if(mCountDelay>=mDelay)
//		{
//				mvalue=Mathf.MoveTowards(mvalue,1,Time.deltaTime/mTime);
//			if(mObject!=null)
//				_DelegateFunction(mObject,mStartPosition,mEndPosition,mvalue);
//			else
//			{
//				Debug.Log("Animated is Destroy");
//				yield return null;
//				Destroy(gameObject);
//			}
//			if(mvalue==1)
//			{
//				if(miRepeatCount<miRepeat||miRepeat<=-1)
//				{
//					miRepeatCount++;
//					mvalue=0;
//					if(mRepeatWithDelay&&mbPingPongStatus==false)
//						mCountDelay=0.0f;
//					else if(mRepeatWithDelay&&mbPingPongStatus)
//					{
//						if(miRepeatCount%2==0)
//							mCountDelay=0.0f;
//					}
//					
//					if(mbPingPongStatus)
//					SetPingPongValue();
//				}
//				else
//				{
//					if(mCallBack!=null)
//					mCallBack(0);
//					yield return null;
//					Destroy(gameObject);
//				}
//			}
//		}
//	}
}
