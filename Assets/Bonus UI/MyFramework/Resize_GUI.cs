using UnityEngine;
using System.Collections;

public class Resize_GUI : MonoBehaviour 
{
	float mfScreenWidthRatioForScale=1.0f;
	float mfScreenHeightRatioForScale=1.0f;
	float mfScreenWidthRatioForPos=1.0f;
	float mfScreenHeightRatioForPos=1.0f;
	float mfDummyScreenWidth;
	float mfDummyScreenHeight;
	
	public eScreenModeType _ScreenModeType = eScreenModeType.Landscape;
	public bool _alignCenterAccordingToScreen = true;
	public bool _adjustSizeAccordingToScreen = true;
	
	MenuItemEntryAnimation mmenuItemEntryAnimation=null;
	// Use this for initialization
	void Start () 
	{
		mmenuItemEntryAnimation=GetComponent<MenuItemEntryAnimation>();
		ScaleObjectAccordingToScreen();
		PositionObjectAccodingToScreen();
		mmenuItemEntryAnimation._StartPosition=new Vector3(mmenuItemEntryAnimation._StartPosition.x*mfScreenWidthRatioForPos,mmenuItemEntryAnimation._StartPosition.y*mfScreenHeightRatioForPos,mmenuItemEntryAnimation._StartPosition.z);
		mmenuItemEntryAnimation._EndPosition=new Vector3(mmenuItemEntryAnimation._EndPosition.x*mfScreenWidthRatioForPos,mmenuItemEntryAnimation._EndPosition.y*mfScreenHeightRatioForPos,mmenuItemEntryAnimation._EndPosition.z);
		
	}
	
	void ScaleObjectAccordingToScreen()
	{
		if(_adjustSizeAccordingToScreen)
		{
			mfScreenWidthRatioForScale=Screen.width/1024f;
			mfScreenHeightRatioForScale=Screen.height/768f;
		}
		else
		{
			if(_ScreenModeType==eScreenModeType.Landscape)
			{
				mfDummyScreenWidth=(4f/3f)*Screen.height;
				mfScreenWidthRatioForScale=mfDummyScreenWidth/1024;
				mfScreenHeightRatioForScale=Screen.height/768f;
			}
			else
			{
				mfDummyScreenHeight=(3f/4f)*Screen.width;
				mfScreenWidthRatioForScale=Screen.width/1024f;
				mfScreenHeightRatioForScale=mfDummyScreenHeight/768f;
			}
		}
		transform.localScale=new Vector3(transform.localScale.x*mfScreenWidthRatioForScale,transform.localScale.y*mfScreenHeightRatioForScale,transform.localScale.z);
	}
	
	void PositionObjectAccodingToScreen()
	{
		if(_alignCenterAccordingToScreen)
		{
			mfScreenWidthRatioForPos=Screen.width/1024f;
			mfScreenHeightRatioForPos=Screen.height/768f;
		}
		else
		{
			if(_ScreenModeType==eScreenModeType.Landscape)
			{
				mfDummyScreenWidth=(4f/3f)*Screen.height;
				mfScreenWidthRatioForPos=mfDummyScreenWidth/1024;
				mfScreenHeightRatioForPos=Screen.height/768f;
			}
			else
			{
				mfDummyScreenHeight=(3f/4f)*Screen.width;
				mfScreenWidthRatioForPos=Screen.width/1024f;
				mfScreenHeightRatioForPos=mfDummyScreenHeight/768f;
			}
		}
		transform.position=new Vector3(transform.position.x*mfScreenWidthRatioForPos,transform.position.y*mfScreenHeightRatioForPos,transform.position.z);
	}
}
