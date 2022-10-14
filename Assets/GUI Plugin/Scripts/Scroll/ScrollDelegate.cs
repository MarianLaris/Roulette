using UnityEngine;
using System.Collections;

public class ScrollDelegate : MonoBehaviour  {

	// Use this for initialization
	public GameObject _IndicatorPrefab;
	public Vector3 _StartPosition;
	public Vector3 _EndPosition;

	GameObject mIndicator = null;
	Vector3 mDirectionVector = new Vector3(0,0,0);
	float mfPercentageScroll = 0.0f;
	float mfDefaultResolution = 4.0f/3.0f;
	float mfCurrentResolution;
	float mfResMultiplier = 1.0f;
	
	public bool _alignCenterAccordingToScreen = true;
    public bool _adjustSizeAccordingToScreen = true;
	
	void Start () 
	{
		mfCurrentResolution = (float)Screen.width/(float)Screen.height;
		mfResMultiplier = mfDefaultResolution/mfCurrentResolution;
		
		if(_alignCenterAccordingToScreen)
        {
			_StartPosition.x -= 0.5f;
			_StartPosition.x *= mfResMultiplier;
			_StartPosition.x += 0.5f;
			_EndPosition.x -= 0.5f;
			_EndPosition.x *= mfResMultiplier;
			_EndPosition.x += 0.5f;
		}
		
		if(_adjustSizeAccordingToScreen)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= mfResMultiplier;
            transform.localScale = localScale;
        }
		
		mDirectionVector = _EndPosition - _StartPosition;
		mIndicator = Instantiate(_IndicatorPrefab,_StartPosition,_IndicatorPrefab.transform.rotation) as GameObject;
	}
	
	public void OnScrollPercentage(float Percentage)
	{
		if(mIndicator != null)
		{
			float actualPercentage = Percentage;
			if(actualPercentage > 1.0f)
				actualPercentage = 1.0f;
			else if(actualPercentage < 0.0f)
				actualPercentage = 0.0f;
			
			mIndicator.transform.position = _StartPosition + mDirectionVector*actualPercentage;
		}
	}	
	
	public void cleanUp()
	{
		GameObject tObject = null;
		if(mIndicator != null)
		{
			Destroy(mIndicator);
			mIndicator = null;
		}
	}
	
	void OnDestroy() 
	{
		cleanUp();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
