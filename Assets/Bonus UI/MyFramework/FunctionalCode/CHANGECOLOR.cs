using UnityEngine;
using System.Collections;

public class CHANGECOLOR : MonoBehaviour {
	
	public delegate void ObjectAnimator(GameObject isObject,Color strat,Color end,float val);
	public ObjectAnimator _DelegateFunction;
	
	public delegate void CallBack(object obj);
	CallBack mCallBack;

	GameObject mObject;
	Color mStartColor;
	Color mEndColor;
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
				_DelegateFunction(mObject,mStartColor,mEndColor,mvalue);
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
	
	public void SetInitialValue(GameObject isObject,Color strat,Color end,float val,float isDelay,int isRepeat,bool RepeatWithdelay,bool isPingPongStatus,CallBack callBackFun)
	{
		mObject=isObject;
		mStartColor=strat;
		mEndColor=end;
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
		Color istemp=mStartColor;
		mStartColor=mEndColor;
		mEndColor=istemp;
	}
}
