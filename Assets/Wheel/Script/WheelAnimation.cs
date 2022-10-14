using UnityEngine;
using System.Collections;

public class WheelAnimation : MonoBehaviour {

	// Use this for initialization
	float mfSWRatio;
	float mfSHRatio;
	public GameObject _Wheel;
	public GameObject _Arrow;
	public GameObject[] _Coins;
	public static WheelAnimation _wheelAnimation=null;
	void Awake()
	{
		float AngleGap = 360.0f/_Coins.Length;
		
		for(int i=0;i<_Coins.Length;i++)
		{
			float currentAngle = (AngleGap*(i+0.5f));
			_Coins[i].transform.Rotate(Vector3.forward*-currentAngle);						

			_Coins[i].renderer.material.color=new Color(_Coins[i].renderer.material.color.a,_Coins[i].renderer.material.color.g,_Coins[i].renderer.material.color.b,0);
		}
	}
	void Start () 
	{
		_wheelAnimation=this;
		//mfSWRatio=Screen.width/768;
		mfSHRatio=Screen.height/768f;
		transform.localScale=new Vector3(transform.localScale.x*mfSHRatio,transform.localScale.y*mfSHRatio,1);
		Animations.Fade(_Wheel,0,1f,0.1f,0.5f);
		Animations.Fade(_Arrow,0,1f,0.1f,0.5f);
		for(int i=0;i<_Coins.Length;i++)
		{
			Animations.Fade(_Coins[i],0,1f,0.1f,0.5f);
		}
	}
	
	public void ExitAnimation()
	{
		Animations.Fade(_Wheel,1f,0f,0.1f,0.5f,_EaseType.linear,0,false,false,fun=>DestroyWheel());
		Animations.Fade(_Arrow,1f,0f,0.1f,0.5f);
		for(int i=0;i<_Coins.Length;i++)
		{
			Animations.Fade(_Coins[i],1f,0f,0.1f,0.5f);
		}
	}
	
	void DestroyWheel()
	{
		Destroy(_Wheel);
		Destroy(_Arrow);
		Destroy(gameObject);
	}
	
}
