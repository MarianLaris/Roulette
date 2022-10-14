using UnityEngine;
using System.Collections;

public class MenuItemExitAnim : MonoBehaviour 
{
	
	GameObject mAnimObj = null;
	
	public delegate void callBack(GameObject obj);
	public callBack onCallBack;
	
	// Use this for initialization
	public Vector3 _EndPos = new Vector3(0,0,0);
	public Vector2 _StartScale = new Vector2(1.0f,1.0f);
	public Vector2 _EndScale = new Vector2(1.0f,1.0f);
	public bool _FadeOut = true;
	public Color _StartColor = new Color(0.5f,0.5f,0.5f,0.5f);
	public Color _EndColor = new Color(0.5f,0.5f,0.5f,0.5f);
	
	public float _InitialStartDelay = 1.0f;
	public float _duration = 1.0f;
	public bool _VisibleOnStart = true;
	public bool _alignCenterAccordingToScreen = true;
	public bool _adjustSizeAccordingToScreen = true;
	
	public EaseActionType _actionType = EaseActionType.EaseNormal;
	public EaseType _easeType = EaseType.EaseIn;
	
	float mfDefaultResolution = 4.0f/3.0f;
	float mfCurrentResolution;
	float mfResMultiplier = 1;
	//GameObject mBetCamera;
		
		
	
	void Start () 
	{
	//mBetCamera=GameObject.Find("Camera");	
	}
	
	public void beginAnimationWithCallBack(callBack pCallBack)
	{
		mfCurrentResolution = (float)Screen.width/(float)Screen.height;
		mfResMultiplier = mfDefaultResolution/mfCurrentResolution;
		
		if(_alignCenterAccordingToScreen)
		{
//			Vector3 startPos = gameObject.transform.position;
//			startPos.x -= 0.5f;
//			startPos.x *= mfResMultiplier;
//			startPos.x += 0.5f;
//			gameObject.transform.position = startPos;
			
			_EndPos.x -= 0.5f;
			_EndPos.x *= mfResMultiplier;
			_EndPos.x += 0.5f;			
		}
	
		
		if(_adjustSizeAccordingToScreen)
		{
			_StartScale.x *= mfResMultiplier;
			_EndScale.x *= mfResMultiplier;
		}
		
		stopAllActions();
		if(_FadeOut == true)
		{
			_StartColor = new Color(0.5f,0.5f,0.5f,1.0f);
			_EndColor = new Color(0.0f,0.0f,0.0f,0.0f);
		}
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
		onCallBack = pCallBack;
		
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
		GUITexture texture = GetComponent<GUITexture>();
		if(texture != null)
		{
			texture.enabled = true;
			texture.color = _StartColor;
		}
		
		GUIText text = GetComponent<GUIText>();
		if(text != null)
		{
			text.enabled = true;
			text.material.color = _StartColor;
		}
		
		if(_actionType != EaseActionType.EaseNone)
		{
			mAnimObj = new GameObject();
			mAnimObj.name = "BeginAnimation "+ gameObject.name;
			
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
			scale.actionWith(gameObject,new Vector2(_EndScale.x,_EndScale.y),_duration);
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
		stopAllActions();
		if(onCallBack != null)
		onCallBack(gameObject);
		//if(mBetCamera.camera.enabled==false)
		//mBetCamera.camera.enabled=true;
		
	}
	
	public void stopAllActions()
	{
		if(mAnimObj != null)
		{
			Destroy(mAnimObj);
			mAnimObj = null;
		}
	}
}