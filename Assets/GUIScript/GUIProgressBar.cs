using UnityEngine;
using System.Collections;
//using GameHelper;

public class GUIProgressBar : MonoBehaviour
{
	public Texture2D foregroundTexture;
	public GameObject _Filler;
	public Rect _SounndFillRect;
	public Rect _MusicFillRect;
	public Rect _SpeedFillRect;
	//public Rect Rect;
	//public Rect frameRect;
	public Rect _SoundSliderRect;
	public Rect _MusicSliderRect;
	public Rect _SpeedSliderRect;
	
	public Rect _SoundBeginGroupRect;
	public Rect _MusicBeginGroupRect;
	
	public GUIStyle thumbGUIStyle;
	float mfSHRatio;
	float mfSWRatio;
	
	GameObject mSoundFiller;
	Camera mCamera;
	GameObject mSwitcher;
	
	// Use this for initialization
	void Start ()
	{	
		mfSWRatio=Screen.width/1024f;
		mfSHRatio=Screen.height/768f;
		thumbGUIStyle.fixedWidth = 62*mfSWRatio;
		thumbGUIStyle.fixedHeight = 48*mfSHRatio;
		
		 mCamera=Camera.main;
		 mSwitcher=GameObject.Find("SoundManager");
		
		_SoundBeginGroupRect=new Rect(262*mfSWRatio,285*mfSHRatio,515*mfSWRatio,21*mfSHRatio);
		_SounndFillRect=new Rect(0,0,515*mfSWRatio,21*mfSHRatio);
		
		_MusicBeginGroupRect=new Rect(262*mfSWRatio,406*mfSHRatio,515*mfSWRatio,21*mfSHRatio);
		_MusicFillRect=new Rect(0,0,515*mfSWRatio,21*mfSHRatio);
		
		
		_SoundSliderRect=new Rect(252*mfSWRatio,274*mfSHRatio,540*mfSWRatio,50*mfSHRatio);
		_MusicSliderRect=new Rect(252*mfSWRatio,395*mfSHRatio,540*mfSWRatio,50*mfSHRatio);
	
		
	}
	
	void Update()
	{
		mSwitcher.audio.volume=RT_DataHandler.GetSoundValue();
		mCamera.audio.volume=RT_DataHandler.GetMusicValue();
	}
	// Draw Slider
    void OnGUI()
	{
		GUI.BeginGroup(new Rect(_SoundBeginGroupRect.x,_SoundBeginGroupRect.y,(515f*mfSWRatio)*(RT_DataHandler.GetSoundValue()) , _SoundBeginGroupRect.height));
		GUI.DrawTexture(_SounndFillRect, foregroundTexture);
		GUI.EndGroup();
		
		RT_DataHandler.SetSoundValue(GUI.HorizontalSlider(_SoundSliderRect,RT_DataHandler.GetSoundValue(),0.0f,1.0f,GUIStyle.none,thumbGUIStyle));
		
		GUI.BeginGroup(new Rect(_MusicBeginGroupRect.x,_MusicBeginGroupRect.y,(515f*mfSWRatio)*(RT_DataHandler.GetMusicValue()) , _MusicBeginGroupRect.height));
		GUI.DrawTexture(_MusicFillRect, foregroundTexture);
		GUI.EndGroup();
		
		RT_DataHandler.SetMusicValue(GUI.HorizontalSlider(_MusicSliderRect,RT_DataHandler.GetMusicValue(),0.0f,1.0f,GUIStyle.none,thumbGUIStyle));
	}
}
