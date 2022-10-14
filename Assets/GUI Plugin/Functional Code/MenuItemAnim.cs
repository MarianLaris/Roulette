using UnityEngine;
using System.Collections;

public enum EaseActionType
{
	EaseNone,
	EaseNormal,
	EaseCubic,
	EaseExponential,
	EaseElastic,
}


public class MenuItemAnim : MonoBehaviour {
	
	GameObject mAnimObj = null;
	
	public delegate void callBack();
	public callBack onEnterCallBack;
	public callBack onExitCallBack;

	
	// Use this for initialization
	public Vector3 _StartPos = new Vector3(0,0,0);
	public Vector3 _EndPos = new Vector3(0,0,0);
	public Vector2 _StartScale = new Vector2(1.0f,1.0f);
	public Vector2 _EndScale = new Vector2(1.0f,1.0f);
	public bool _FadeIn = true;
	Color _StartColor = new Color(0.5f,0.5f,0.5f,0.5f);
	Color _EndColor = new Color(0.5f,0.5f,0.5f,0.5f);
	
	public float _InitialStartDelay = 1.0f;
	public float _InitialExitDelay = 1.0f;
	public float _duration = 1.0f;
	public float _exitDuration = 1.0f;
	public bool _VisibleOnStart = true;

	public EaseActionType _actionType = EaseActionType.EaseNormal;
	public EaseType _easeType = EaseType.EaseIn;
	
	void Start () 
	{
		
	}
	
	public void beginAnimationWithCallBack(callBack pCallBack)
	{
		stopAllActions();
		GUITexture texture = GetComponent<GUITexture>();
		if(texture != null)
		{
			if(_VisibleOnStart)
				texture.enabled = true;
			else
				texture.enabled = false;
		}
		
		GUIText text = GetComponent<GUIText>();
		if(text != null)
		{
			if(_VisibleOnStart)
				text.enabled = true;
			else
				text.enabled = false;
		}
		
		gameObject.transform.localScale = new Vector3(_StartScale.x,_StartScale.y,gameObject.transform.localScale.z);
		
		onEnterCallBack = pCallBack;
		stopAllActions();
		
		mAnimObj = new GameObject();
		mAnimObj.name = "BeginAnim Delay";
		
		CDelayTime delay = mAnimObj.AddComponent<CDelayTime>();
		delay.actionWithDuration(_InitialStartDelay);
		
		CCallFunc call = mAnimObj.AddComponent<CCallFunc>();
		call.actionWithCallBack(beginEntryAnimation);
		
		CSequence seq = mAnimObj.AddComponent<CSequence>();
		seq.actionWithActions(delay,call);
		seq.runAction();
	}
	
	public void beginEntryAnimation()
	{
		//STOP ALL ACTIONS FIRST
		stopAllActions();
		if(_FadeIn == true)
		{
			_StartColor = new Color(0,0,0,0);
			_EndColor = new Color(0.5f,0.5f,0.5f,1.0f);
		}
		gameObject.transform.position = _StartPos;
		
		GUITexture texture = GetComponent<GUITexture>();
		if(texture != null)
		{
			texture.enabled = true;
			texture.color = _StartColor;
		}
		
		GUIText text = GetComponent<GUIText>();
		if(text != null)
		{
			_EndColor = new Color(1,1,1,1);
			text.enabled = true;
			text.material.color = _StartColor;
		}
		
		if(_actionType != EaseActionType.EaseNone)
		{
			mAnimObj = new GameObject();
			mAnimObj.name = "BeginAnimation "+gameObject.name;
			
			CMoveTo move = mAnimObj.AddComponent<CMoveTo>();
			move.actionWith(gameObject,_EndPos,_duration);
			
			CAction ease;
			CEaseNone ease0;
			CEaseCubic ease1;
			CEaseExponential ease2;
			CEaseElastic ease3;
			switch(_actionType)
			{
				case EaseActionType.EaseNormal:
					ease0 = mAnimObj.AddComponent<CEaseNone>();
					ease0.actionWithAction(move);
					ease = ease0;
					break;
				case EaseActionType.EaseCubic:
					ease1 = mAnimObj.AddComponent<CEaseCubic>();
					ease1.actionWithAction(move,_easeType);
					ease = ease1;
					break;
				case EaseActionType.EaseExponential:
					ease2 = mAnimObj.AddComponent<CEaseExponential>();
					ease2.actionWithAction(move,_easeType);
					ease = ease2;
					break;
				case EaseActionType.EaseElastic:
					ease3 = mAnimObj.AddComponent<CEaseElastic>();
					ease3.actionWithAction(move,_easeType);
					ease = ease3;
					break;
				default:
					ease0 = mAnimObj.AddComponent<CEaseNone>();
					ease0.actionWithAction(move);
					ease = ease0;
					break;
			}
			
			CScaleTo scale = mAnimObj.AddComponent<CScaleTo>();
			scale.actionWith(gameObject,_EndScale,_duration);
			CAction easeScale;
			CEaseNone easeScale0;
			CEaseCubic easeScale1;
			CEaseExponential easeScale2;
			CEaseElastic easeScale3;
			switch(_actionType)
			{
				case EaseActionType.EaseNormal:
					easeScale0 = mAnimObj.AddComponent<CEaseNone>();
					easeScale0.actionWithAction(scale);
					easeScale = easeScale0;
					break;
				case EaseActionType.EaseCubic:
					easeScale1 = mAnimObj.AddComponent<CEaseCubic>();
					easeScale1.actionWithAction(scale,_easeType);
					easeScale = easeScale1;
					break;
				case EaseActionType.EaseExponential:
					easeScale2 = mAnimObj.AddComponent<CEaseExponential>();
					easeScale2.actionWithAction(scale,_easeType);
					easeScale = easeScale2;
					break;
				case EaseActionType.EaseElastic:
					easeScale3 = mAnimObj.AddComponent<CEaseElastic>();
					easeScale3.actionWithAction(scale,_easeType);
					easeScale = easeScale3;
					break;
				default:
					easeScale0 = mAnimObj.AddComponent<CEaseNone>();
					easeScale0.actionWithAction(scale);
					easeScale = easeScale0;
					break;
			}
			
			CAction colorChange;
			if(GetComponent<GUITexture>() != null)
			{
				CChangeColorGUITexture color = mAnimObj.AddComponent<CChangeColorGUITexture>();
				color.actionWith(GetComponent<GUITexture>(),_EndColor,_duration);
				colorChange = color;
			}
			else
			{
				CChangeColorMat color = mAnimObj.AddComponent<CChangeColorMat>();
				color.actionWith(GetComponent<GUIText>().material,_EndColor,_duration);
				colorChange = color;
			}
				
			CSpawnAction spw = mAnimObj.AddComponent<CSpawnAction>();
			spw.actionWithActions(ease,easeScale,colorChange);
			spw.runAction();
			
			CCallFunc call = mAnimObj.AddComponent<CCallFunc>();
			call.actionWithCallBack(beginAnimationFinished);
			
			CSequence seq = mAnimObj.AddComponent<CSequence>();
			seq.actionWithActions(spw,call);
			seq.runAction();
		}
		else
		{
			gameObject.transform.position = _EndPos;
			gameObject.transform.localScale = new Vector3(_EndScale.x,_EndScale.y,gameObject.transform.localScale.z);
			if(texture != null)
				texture.color = _EndColor;
			if(text != null)
				text.material.color = _EndColor;
			
			beginAnimationFinished();
		}
	}
			
	public void beginAnimationFinished()
	{
		if(onEnterCallBack != null)
			onEnterCallBack();
		stopAllActions();
	}
	
	public void beginReverseAnimationWithCallBack(callBack pCallBack)
	{
		stopAllActions();
		onExitCallBack = pCallBack;
		
		gameObject.transform.localScale = new Vector3(_EndScale.x,_EndScale.y,gameObject.transform.localScale.z);
		
		mAnimObj = new GameObject();
		mAnimObj.name = "ReverseAnim Delay";
		
		CDelayTime delay = mAnimObj.AddComponent<CDelayTime>();
		delay.actionWithDuration(_InitialExitDelay);
		
		CCallFunc call = mAnimObj.AddComponent<CCallFunc>();
		call.actionWithCallBack(reverseAnimation);
		
		CSequence seq = mAnimObj.AddComponent<CSequence>();
		seq.actionWithActions(delay,call);
		seq.runAction();
	}
	
	public void reverseAnimation()
	{
		stopAllActions();
		if(_actionType != EaseActionType.EaseNone)
		{
			mAnimObj = new GameObject();
			mAnimObj.name = "ReverseAnim";
			
			CMoveTo move = mAnimObj.AddComponent<CMoveTo>();
			move.actionWith(gameObject,_StartPos,_duration);
			CAction ease;
			CEaseNone ease0;
			CEaseCubic ease1;
			CEaseExponential ease2;
			CEaseElastic ease3;
			switch(_actionType)
			{
				case EaseActionType.EaseNormal:
					ease0 = mAnimObj.AddComponent<CEaseNone>();
					ease0.actionWithAction(move);
					ease = ease0;
					break;
				case EaseActionType.EaseCubic:
					ease1 = mAnimObj.AddComponent<CEaseCubic>();
					ease1.actionWithAction(move,_easeType);
					ease = ease1;
					break;
				case EaseActionType.EaseExponential:
					ease2 = mAnimObj.AddComponent<CEaseExponential>();
					ease2.actionWithAction(move,_easeType);
					ease = ease2;
					break;
				case EaseActionType.EaseElastic:
					ease3 = mAnimObj.AddComponent<CEaseElastic>();
					ease3.actionWithAction(move,_easeType);
					ease = ease3;
					break;
				default:
					ease0 = mAnimObj.AddComponent<CEaseNone>();
					ease0.actionWithAction(move);
					ease = ease0;
					break;
			}
			
			CScaleTo scale = mAnimObj.AddComponent<CScaleTo>();
			scale.actionWith(gameObject,_StartScale,_duration);
			
			CAction easeScale;
			CEaseNone easeScale0;
			CEaseCubic easeScale1;
			CEaseExponential easeScale2;
			CEaseElastic easeScale3;
			switch(_actionType)
			{
				case EaseActionType.EaseNormal:
					easeScale0 = mAnimObj.AddComponent<CEaseNone>();
					easeScale0.actionWithAction(scale);
					easeScale = easeScale0;
					break;
				case EaseActionType.EaseCubic:
					easeScale1 = mAnimObj.AddComponent<CEaseCubic>();
					easeScale1.actionWithAction(scale,_easeType);
					easeScale = easeScale1;
					break;
				case EaseActionType.EaseExponential:
					easeScale2 = mAnimObj.AddComponent<CEaseExponential>();
					easeScale2.actionWithAction(scale,_easeType);
					easeScale = easeScale2;
					break;
				case EaseActionType.EaseElastic:
					easeScale3 = mAnimObj.AddComponent<CEaseElastic>();
					easeScale3.actionWithAction(scale,_easeType);
					easeScale = easeScale3;
					break;
				default:
					easeScale0 = mAnimObj.AddComponent<CEaseNone>();
					easeScale0.actionWithAction(scale);
					easeScale = easeScale0;
					break;
			}
			
			CAction colorChange;
			if(GetComponent<GUITexture>() != null)
			{
				CChangeColorGUITexture color = mAnimObj.AddComponent<CChangeColorGUITexture>();
				color.actionWith(GetComponent<GUITexture>(),_StartColor,_duration);
				colorChange = color;
			}
			else
			{
				CChangeColorMat color = mAnimObj.AddComponent<CChangeColorMat>();
				color.actionWith(GetComponent<GUIText>().material,_StartColor,_duration);
				colorChange = color;
			}
				
			CSpawnAction spw = mAnimObj.AddComponent<CSpawnAction>();
			spw.actionWithActions(ease,easeScale,colorChange);
						
			CCallFunc call = mAnimObj.AddComponent<CCallFunc>();
			call.actionWithCallBack(reverseAnimationFinished);
			
			CSequence seq = mAnimObj.AddComponent<CSequence>();
			seq.actionWithActions(spw,call);
			seq.runAction();
		}
		else
		{
			//gameObject.transform.position = _StartPos;
			GUITexture texture = GetComponent<GUITexture>();
			if(texture != null)
			{
				texture.color = _StartColor;
				if(_VisibleOnStart)
					GetComponent<GUITexture>().enabled = false;
			}
			
			reverseAnimationFinished();
		}
	}
			
	public void reverseAnimationFinished()
	{
		stopAllActions();
		//Debug.Log("Reverse anim finished "+gameObject.name);
		
		GUITexture texture = GetComponent<GUITexture>();
		if(texture != null)
		{
			if(_VisibleOnStart)
				texture.enabled = true;
		}
		
		if(onExitCallBack != null)
			onExitCallBack();
	}
	
	public void stopAllActions()
	{
		if(mAnimObj != null)
		{
			Destroy(mAnimObj);
			mAnimObj = null;
		}
	}
	
	void OnDestroy()
	{
		stopAllActions();
	}
	
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
