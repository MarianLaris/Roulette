using UnityEngine;
using System.Collections;


public enum eFadeConditions
{
	NoFade = 0,
	FadeInOnly,
	FadeOutOnly,
	FadeInAndOut,
}

[ExecuteInEditMode]
public class GUIEditMode : MonoBehaviour {

	// Use this for initialization
	public Vector2 _ReferenceScreenRatio = new Vector2(4,3);
	public bool _CorrectWidth = false;
	public bool _CorrectHeight = false;
	public bool _SaveStartCondition = false;
	public bool _SaveEndCondition = false;
	public bool _ShowStartConditions = false;
	public bool _ShowEndConditions = false;
	public eFadeConditions _fadeCondition = eFadeConditions.NoFade;
	
	private eFadeConditions _CurFadeCondition = eFadeConditions.NoFade;
	private bool _fadeInOnEntry = false;
	private bool _fadeOutOnExit = false;
	float mfRefScreenRatio = 3.0f/4.0f;
	
	void Start () 
	{
		//SETUP FADE OPTION
		mfRefScreenRatio = (float)_ReferenceScreenRatio.y/(float)_ReferenceScreenRatio.x;

		MenuItemEntryAnim animEntry = GetComponent<MenuItemEntryAnim>();
		if(animEntry != null)
			_fadeInOnEntry = animEntry._FadeIn;
				
		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
		if(animExit != null)
			_fadeOutOnExit = animExit._FadeOut;
		
		if(_fadeInOnEntry && _fadeOutOnExit)
			_fadeCondition = eFadeConditions.FadeInAndOut;
		else if(_fadeInOnEntry)
			_fadeCondition = eFadeConditions.FadeInOnly;
		else if(_fadeOutOnExit)
			_fadeCondition = eFadeConditions.FadeOutOnly;
		else
			_fadeCondition = eFadeConditions.NoFade;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_CorrectHeight == true)
		{
			correctAspectRatioX();
			_CorrectHeight = false;	
		}
		
		else if(_CorrectWidth == true)
		{
			correctAspectRatioY();
			_CorrectWidth = false;	
		}
		
		else if(_SaveStartCondition == true)
		{
			saveStartCondition();
			_SaveStartCondition = false;
		}
		
		else if(_SaveEndCondition == true)
		{
			saveEndCondition();
			_SaveEndCondition = false;
		}
		
		else if(_ShowStartConditions == true)
		{
			restoreStartCondition();
			_ShowStartConditions = false;
		}
		
		else if(_ShowEndConditions == true)
		{
			restoreEndCondition();
			_ShowEndConditions = false;
		}
		
		if(_fadeCondition != _CurFadeCondition)
		{
			
			_CurFadeCondition = _fadeCondition;
			switch(_CurFadeCondition)
			{
				case eFadeConditions.NoFade:
					_fadeInOnEntry = false;
					_fadeOutOnExit = false;
					break;
				case eFadeConditions.FadeInOnly:
					_fadeInOnEntry = true;
					_fadeOutOnExit = false;
					break;
				case eFadeConditions.FadeOutOnly:
					_fadeInOnEntry = false;
					_fadeOutOnExit = true;
					break;
				case eFadeConditions.FadeInAndOut:
					_fadeInOnEntry = true;
					_fadeOutOnExit = true;
					break;
				default:
					break;
			}
			FadeOnEntry(_fadeInOnEntry);
			FadeOnExit(_fadeOutOnExit);
		}
	}
	
	void FadeOnEntry(bool bShouldFade)
	{
		MenuItemEntryAnim animEntry = GetComponent<MenuItemEntryAnim>();
		if(animEntry != null)
		{
			animEntry._FadeIn = bShouldFade;
			animEntry._VisibleOnStart = !bShouldFade;
		}
	}
	
	void FadeOnExit(bool bShouldFade)
	{
		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
		if(animExit != null)
		{
			animExit._VisibleOnStart = true;
			animExit._FadeOut = bShouldFade;
		}
	}
	
	
	void correctAspectRatioX()
	{
		GUITexture tGUITexture = GetComponent<GUITexture>();
		Texture tTexture = tGUITexture.texture;
		if(tTexture != null)
		{
			float correctAspectRatio = (float)tTexture.width/(float)tTexture.height;
			Debug.Log("AspectRatio = "+correctAspectRatio);
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x,gameObject.transform.localScale.x/(correctAspectRatio*mfRefScreenRatio),gameObject.transform.localScale.z);
		}
	}
	
	void correctAspectRatioY()
	{
		GUITexture tGUITexture = GetComponent<GUITexture>();
		Texture tTexture = tGUITexture.texture;
		if(tTexture != null)
		{
			float correctAspectRatio = (float)tTexture.width/(float)tTexture.height;
			Debug.Log("AspectRatio = "+correctAspectRatio);
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.y*(correctAspectRatio*mfRefScreenRatio),gameObject.transform.localScale.y,gameObject.transform.localScale.z);
		}
	}
	
	void saveStartCondition()
	{
		MenuItemEntryAnim animEntry = GetComponent<MenuItemEntryAnim>();
		if(animEntry != null)
		{
			animEntry._StartScale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
		}
		
		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
		if(animExit != null)
		{
			animExit._EndPos = gameObject.transform.position;
			animExit._EndScale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
		}
	}
	
	void saveEndCondition()
	{
		MenuItemEntryAnim animEntry = GetComponent<MenuItemEntryAnim>();
		if(animEntry != null)
		{
			animEntry._EndPos = gameObject.transform.position;
			animEntry._EndScale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
		}
		
		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
		if(animExit != null)
		{
			animExit._StartScale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
		}
	}
	
	void restoreStartCondition()
	{
		MenuItemEntryAnim animEntry = GetComponent<MenuItemEntryAnim>();
		if(animEntry != null)
		{
			gameObject.transform.localScale = new Vector3(animEntry._StartScale.x,animEntry._StartScale.y,gameObject.transform.localScale.z);
		}
		
		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
		if(animExit != null)
		{
			gameObject.transform.position = animExit._EndPos;
			gameObject.transform.localScale = new Vector3(animExit._StartScale.x,animExit._StartScale.y,gameObject.transform.localScale.z);
		}
	}
	
	void restoreEndCondition()
	{
		MenuItemEntryAnim animEntry = GetComponent<MenuItemEntryAnim>();
		if(animEntry != null)
		{
			gameObject.transform.position = animEntry._EndPos;
			gameObject.transform.localScale = new Vector3(animEntry._EndScale.x,animEntry._EndScale.y,gameObject.transform.localScale.z);
		}
		
		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
		if(animExit != null)
		{
			gameObject.transform.localScale = new Vector3(animExit._EndScale.x,animExit._EndScale.y,gameObject.transform.localScale.z);
		}
	}
}
