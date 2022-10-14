using UnityEngine;
using System.Collections;
using System;

public class Animations 
{
	public static void Move(GameObject gameObject,Vector3 Start,Vector3 End,float _Time,float delay=0f,_EaseType easeType=_EaseType.linear,int Repeat=0,bool RepeatWithDelay=false,bool PingPong=false,RTS.CallBack callbakfun=null)
	{
		GameObject isObject= CreateRTSObject();
		AnimationEffect isanimationEffect=new AnimationEffect();
		isanimationEffect.EaseAction(easeType);
		RTS isRTS=isObject.GetComponent<RTS>();
		isRTS.SetInitialValue(gameObject,Start,End,_Time,delay,Repeat,RepeatWithDelay,PingPong,callbakfun);
		isRTS._DelegateFunction=isanimationEffect.Mover;
	}
	
	public static void Scale(GameObject gameObject,Vector3 Start,Vector3 End,float _Time,float delay=0f,_EaseType easeType=_EaseType.linear,int Repeat=0,bool RepeatWithDelay=false,bool PingPong=false,RTS.CallBack callbakfun =null)
	{
		GameObject isObject= CreateRTSObject();
		AnimationEffect isanimationEffect=new AnimationEffect();
		isanimationEffect.EaseAction(easeType);
		RTS isRTS=isObject.GetComponent<RTS>();
		isRTS.SetInitialValue(gameObject,Start,End,_Time,delay,Repeat,RepeatWithDelay,PingPong,callbakfun);
		isRTS._DelegateFunction=isanimationEffect.Scaler;
	}

	public static void Rotate(GameObject gameObject,Vector3 Start,Vector3 End,float _Time,float delay=0f,_EaseType easeType=_EaseType.linear,int Repeat=0,bool RepeatWithDelay=false,bool PingPong=false,RTS.CallBack callbakfun =null)
	{
		GameObject isObject= CreateRTSObject();
		AnimationEffect isanimationEffect=new AnimationEffect();
		isanimationEffect.EaseAction(easeType);
		RTS isRTS=isObject.GetComponent<RTS>();
		isRTS.SetInitialValue(gameObject,Start,End,_Time,delay,Repeat,RepeatWithDelay,PingPong,callbakfun);
		isRTS._DelegateFunction=isanimationEffect.Rotater;
	}
	public static void Fade(GameObject gameObject,float Start,float End,float _Time,float delay=0f,_EaseType easeType=_EaseType.linear,int Repeat=0,bool RepeatWithDelay=false,bool PingPong=false,FADER.CallBack callbakfun =null)
	{
		GameObject isObject= CreateFaderObject();
		AnimationEffect isanimationEffect=new AnimationEffect();
		isanimationEffect.EaseAction(easeType);
		FADER isFader=isObject.GetComponent<FADER>();
		isFader.SetInitialValue(gameObject,Start,End,_Time,delay,Repeat,RepeatWithDelay,PingPong,callbakfun);
		if(gameObject.guiTexture)
			isFader._DelegateFunction=isanimationEffect.GuiTextureFader;
		else if(gameObject.guiText)
		{
			isFader._DelegateFunction=isanimationEffect.GuiTextFader;
		}
		else
			isFader._DelegateFunction=isanimationEffect.Fader;
	}
	
	public static void Colour(GameObject gameObject,Color Start,Color End,float _Time,float delay=0f,_EaseType easeType=_EaseType.linear,int Repeat=0,bool RepeatWithDelay=false,bool PingPong=false,CHANGECOLOR.CallBack callbakfun =null)
	{
		GameObject isObject= CreateColorObject();
		AnimationEffect isanimationEffect=new AnimationEffect();
		isanimationEffect.EaseAction(easeType);
		CHANGECOLOR iscHANGECOLOR=isObject.GetComponent<CHANGECOLOR>();
		iscHANGECOLOR.SetInitialValue(gameObject,Start,End,_Time,delay,Repeat,RepeatWithDelay,PingPong,callbakfun);
		
		if(gameObject.guiTexture)
			iscHANGECOLOR._DelegateFunction=isanimationEffect.GuiTextureColorChanger;
		else if(gameObject.guiText)
		{
			iscHANGECOLOR._DelegateFunction=isanimationEffect.GuiTextColorChanger;
		}
		else
			iscHANGECOLOR._DelegateFunction=isanimationEffect.ColorChanger;
	}

	static GameObject CreateRTSObject()
	{
		GameObject isObject=new GameObject("Animation");
		isObject.AddComponent<RTS>();
		return isObject;
	}
	static GameObject CreateFaderObject()
	{
		GameObject isObject=new GameObject("Animation");
		isObject.AddComponent<FADER>();
		return isObject;
	}
	
	static GameObject CreateColorObject()
	{
		GameObject isObject=new GameObject("Animation");
		isObject.AddComponent<CHANGECOLOR>();
		return isObject;
	}
}
