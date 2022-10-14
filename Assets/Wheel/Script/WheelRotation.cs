using UnityEngine;
using System.Collections;

public class WheelRotation : MonoBehaviour {
	
	public int _SpinCycle=4;
	public int _BlockNo=8;
	public float _Duration=3f;
	public float _RotationOffset=22.5f;
	public bool _ClockWiseRotation=true;
	
	public delegate void PerformOpration();
	public PerformOpration OnCallback;
	
	float mfAngleOfBlock;
	float mfStopAtAngle;
	int miReturnBlock;
	
	public static WheelRotation _wheelRotation=null;
	
	public int  _BlockNumber
	{
		get{
			return miReturnBlock;
		}
//		set{
//			miReturnBlock=value;
//		}
	}
	// Use this for initialization
	
	void Start()
	{
		_wheelRotation=this;
	}
	void AngleToRotate()
	{
		mfAngleOfBlock=360/_BlockNo;
		miReturnBlock=Random.Range(0,_BlockNo);
		mfStopAtAngle=360*_SpinCycle+mfAngleOfBlock*miReturnBlock+_RotationOffset;
		miReturnBlock=ReturnBlockNumbre();
	}
	
	public void StartWheelRotation(RTS.CallBack pOnCallBack)
	{
		AngleToRotate();
		if(_ClockWiseRotation) 
			Animations.Rotate(gameObject,Vector3.zero,-Vector3.forward*mfStopAtAngle,_Duration,0.1f,_EaseType.easeOutQuad,0,false,false,pOnCallBack);
		else
			Animations.Rotate(gameObject,Vector3.zero,Vector3.forward*mfStopAtAngle,_Duration,0.1f,_EaseType.easeOutQuad,0,false,false,pOnCallBack);
	}
	
	int ReturnBlockNumbre()
	{
		if(_ClockWiseRotation)
			return _BlockNo-miReturnBlock;
		else
			return miReturnBlock+1;
	}
	
	void Display()
	{
		Debug.Log("Animation Complete....Block No......"+_BlockNumber);
	}
}
