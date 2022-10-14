using UnityEngine;
using System.Collections;

public enum _EaseType
{
	linear,easeInQuad,easeInCubic,easeInQuart,easeInQuint,easeInSine,easeInExpo,easeInCirc,easeInBounce,easeInBack,easeInElastic,
	easeOutQuad,easeOutCubic,easeOutQuart,easeOutQuint,easeOutSine,easeOutExpo,easeOutCirc,easeOutBounce,easeOutBack,easeOutElastic,
	easeInOutQuad,easeInOutCubic,easeInOutQuart,easeInOutQuint,easeInOutSine, easeInOutExpo,easeInOutCirc,easeInOutBounce,easeInOutBack, easeInOutElastic,clerp,spring,Mod_easeInBack,Mod_easeOutBack,Mod_easeInOutBack,Mod_easeOutInBack
}

public class AnimationEffect  
{
	public delegate float EaseTypeDelegate(float x1,float x2,float t);
	public  EaseTypeDelegate easeTypeDelegate;
	public  float mval;
	
	public void  Mover(GameObject isObject,Vector3 strat,Vector3 end,float val)
	{
		isObject.transform.position=new Vector3(easeTypeDelegate(strat.x,end.x,val),easeTypeDelegate(strat.y,end.y,val),easeTypeDelegate(strat.z,end.z,val));
	}
	
	public void  Scaler(GameObject isObject,Vector3 strat,Vector3 end,float val)
	{
		isObject.transform.localScale=new Vector3(easeTypeDelegate(strat.x,end.x,val),easeTypeDelegate(strat.y,end.y,val),easeTypeDelegate(strat.z,end.z,val));
	}
	
	public void  Rotater(GameObject isObject,Vector3 strat,Vector3 end,float val)
	{
		isObject.transform.eulerAngles=new Vector3(easeTypeDelegate(strat.x,end.x,val),easeTypeDelegate(strat.y,end.y,val),easeTypeDelegate(strat.z,end.z,val));
	}
	
	public void  Fader(GameObject isObject,float strat,float end,float val)
	{
		isObject.renderer.material.color=new Color(isObject.renderer.material.color.r,isObject.renderer.material.color.g,isObject.renderer.material.color.b,easeTypeDelegate(strat,end,val));
	}
	
	public void  GuiTextureFader(GameObject isObject,float strat,float end,float val)
	{
		isObject.guiTexture.color=new Color(isObject.guiTexture.color.r,isObject.guiTexture.color.g,isObject.guiTexture.color.b,easeTypeDelegate(strat,end,val));
	}
	
	public void  GuiTextFader(GameObject isObject,float strat,float end,float val)
	{
		isObject.guiText.material.color=new Color(isObject.guiText.material.color.r,isObject.guiText.material.color.g,isObject.guiText.material.color.b,easeTypeDelegate(strat,end,val));
	}
	
	public void  ColorChanger(GameObject isObject,Color strat,Color end,float val)
	{
		isObject.renderer.material.color=new Color(easeTypeDelegate(strat.r,end.r,val),easeTypeDelegate(strat.g,end.g,val),easeTypeDelegate(strat.b,end.b,val),easeTypeDelegate(strat.a,end.a,val));
	}
	
	public void  GuiTextureColorChanger(GameObject isObject,Color strat,Color end,float val)
	{
		isObject.guiTexture.color=new Color(easeTypeDelegate(strat.r,end.r,val),easeTypeDelegate(strat.g,end.g,val),easeTypeDelegate(strat.b,end.b,val),easeTypeDelegate(strat.a,end.a,val));
	}
	
	public void  GuiTextColorChanger(GameObject isObject,Color strat,Color end,float val)
	{
		isObject.guiText.material.color=new Color(easeTypeDelegate(strat.r,end.r,val),easeTypeDelegate(strat.g,end.g,val),easeTypeDelegate(strat.b,end.b,val),easeTypeDelegate(strat.a,end.a,val));
	}
	
 	public void EaseAction(_EaseType isEasetype)
	{
		switch(isEasetype)
		{
		case _EaseType.linear:
			easeTypeDelegate=EaseTypeFunction.linear;
			break;
		case _EaseType.easeInQuad:
			easeTypeDelegate=EaseTypeFunction.easeInQuad;
			break;
		case _EaseType.easeInCubic:
			easeTypeDelegate=EaseTypeFunction.easeInCubic;
			break;	
		case _EaseType.easeInQuart:
			easeTypeDelegate=EaseTypeFunction.easeInQuart;
			break;
		case _EaseType.easeInQuint:
			easeTypeDelegate=EaseTypeFunction.easeInQuint;
			break;
		case _EaseType.easeInSine:
			easeTypeDelegate=EaseTypeFunction.easeInSine;
			break;	
		case _EaseType.easeInExpo:
			easeTypeDelegate=EaseTypeFunction.easeInExpo;
			break;
		case _EaseType.easeInCirc:
			easeTypeDelegate=EaseTypeFunction.easeInCirc;
			break;	
		case _EaseType.easeInBounce:
			easeTypeDelegate=EaseTypeFunction.easeInBounce;
			break;
		case _EaseType.easeInBack:
			easeTypeDelegate=EaseTypeFunction.easeInBack;
			break;
		case _EaseType.easeInElastic:
			easeTypeDelegate=EaseTypeFunction.easeInElastic;
			break;		
		case _EaseType.easeOutQuad:
			easeTypeDelegate=EaseTypeFunction.easeOutQuad;
			break;
		case _EaseType.easeOutCubic:
			easeTypeDelegate=EaseTypeFunction.easeOutCubic;
			break;	
		case _EaseType.easeOutQuart:
			easeTypeDelegate=EaseTypeFunction.easeOutQuart;
			break;
		case _EaseType.easeOutQuint:
			easeTypeDelegate=EaseTypeFunction.easeOutQuint;
			break;
		case _EaseType.easeOutSine:
			easeTypeDelegate=EaseTypeFunction.easeOutSine;
			break;	
		case _EaseType.easeOutExpo:
			easeTypeDelegate=EaseTypeFunction.easeOutExpo;
			break;
		case _EaseType.easeOutCirc:
			easeTypeDelegate=EaseTypeFunction.easeOutCirc;
			break;	
		case _EaseType.easeOutBounce:
			easeTypeDelegate=EaseTypeFunction.easeOutBounce;
			break;
		case _EaseType.easeOutBack:
			easeTypeDelegate=EaseTypeFunction.easeOutBack;
			break;
		case _EaseType.easeOutElastic:
			easeTypeDelegate=EaseTypeFunction.easeOutElastic;
			break;			
		case _EaseType.easeInOutQuad:
			easeTypeDelegate=EaseTypeFunction.easeInOutQuad;
			break;
		case _EaseType.easeInOutCubic:
			easeTypeDelegate=EaseTypeFunction.easeInOutCubic;
			break;	
		case _EaseType.easeInOutQuart:
			easeTypeDelegate=EaseTypeFunction.easeInOutQuart;
			break;
		case _EaseType.easeInOutQuint:
			easeTypeDelegate=EaseTypeFunction.easeInOutQuint;
			break;
		case _EaseType.easeInOutSine:
			easeTypeDelegate=EaseTypeFunction.easeInOutSine;
			break;	
		case _EaseType.easeInOutExpo:
			easeTypeDelegate=EaseTypeFunction.easeInOutExpo;
			break;
		case _EaseType.easeInOutCirc:
			easeTypeDelegate=EaseTypeFunction.easeInOutCirc;
			break;	
		case _EaseType.easeInOutBounce:
			easeTypeDelegate=EaseTypeFunction.easeInOutBounce;
			break;
		case _EaseType.easeInOutBack:
			easeTypeDelegate=EaseTypeFunction.easeInOutBack;
			break;
		case _EaseType.easeInOutElastic:
			easeTypeDelegate=EaseTypeFunction.easeInOutElastic;
			break;	
		case _EaseType.clerp:
			easeTypeDelegate=EaseTypeFunction.clerp;
			break;
		case _EaseType.spring:
			easeTypeDelegate=EaseTypeFunction.spring;
			break;
//		case _EaseType.Mod_easeInBack:
//			easeTypeDelegate=EaseTypeFunction.Mod_easeInBack;
//			break;
//		case _EaseType.Mod_easeOutBack:
//			easeTypeDelegate=EaseTypeFunction.Mod_easeOutBack;
//			break;
//		case _EaseType.Mod_easeInOutBack:
//			easeTypeDelegate=EaseTypeFunction.Mod_easeInOutBack;
//			break;	
//		case _EaseType.Mod_easeOutInBack:
//			easeTypeDelegate=EaseTypeFunction.Mod_easeOutInBack;
//			break;	
		}
	}
	
}
