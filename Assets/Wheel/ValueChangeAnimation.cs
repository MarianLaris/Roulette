using UnityEngine;
using System.Collections;

public class ValueChangeAnimation : MonoBehaviour 
{
	int m_diff =0;
	bool isAnim = false;
	float speedInc = 0.0f,mf_lifeTime;
	private float currentValue ;
	GameObject m_trgetObject;
	int mi_targetValue;
	System.Action m_callBack;
	//System.Action _callBack;
	float mf_Total_Time;
	void Start ()
	{
	
	}
	void Update () 
	{
		if(isAnim)
		{
			currentValue += Time.deltaTime*speedInc;
			m_trgetObject.guiText.text = ""+(int)currentValue;
			mf_lifeTime += Time.deltaTime;
			if(mi_targetValue ==(int)currentValue || mf_lifeTime>= mf_Total_Time)
				FinishCallBack();
		}
	}
	
	
	public void StartValueChangeAnimation(GameObject targetObject,int fromvalue,int tillValue,float durationInSec,System.Action _callback)
	{
		m_diff = tillValue- fromvalue;
		isAnim = true;
		speedInc = m_diff/durationInSec;		
		currentValue = fromvalue;
		m_trgetObject = targetObject;
		mi_targetValue = tillValue;
		m_callBack = _callback;
		mf_Total_Time = durationInSec;
	}
	
	public static void StartTextValueChangeAnimation(GameObject targetObject,int fromvalue,int tillValue,float durationInSec,System.Action _callback)
	{
		ValueChangeAnimation anim = targetObject.AddComponent<ValueChangeAnimation>();
		anim.StartValueChangeAnimation( targetObject, fromvalue, tillValue, durationInSec, _callback);
	}
	
	private void FinishCallBack()
	{
		m_trgetObject.guiText.text =""+mi_targetValue;
		isAnim = false;
		Destroy(this);
		if(m_callBack != null)
			m_callBack();
	}
	
}
