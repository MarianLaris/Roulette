using UnityEngine;
using System.Collections;

// used to adjust the viewport rectangle of the main camera to force an aspect _ratio

public class ResizingViewport : MonoBehaviour 
{
    public static ResizingViewport _instance;
  	public Rect _tempRect;
    public float _ratio;
    private bool mbTooWide = false;

	void Awake () 
    {
        _instance = this;
        _ratio = Screen.width / ResizingDefaultSizes._instance.DefaultResolutionWidth;
        if (_ratio > Screen.height / ResizingDefaultSizes._instance.DefaultResolutionHeight)
        {
            _ratio = Screen.height / ResizingDefaultSizes._instance.DefaultResolutionHeight;
            mbTooWide = true;
        }
	}
	
	void Update () 
    {	
        _ratio = Screen.width / ResizingDefaultSizes._instance.DefaultResolutionWidth;
        if (_ratio > Screen.height / ResizingDefaultSizes._instance.DefaultResolutionHeight)
        {
            _ratio = Screen.height / ResizingDefaultSizes._instance.DefaultResolutionHeight;
            mbTooWide = true;
        }
        else
        {
            mbTooWide = false;
        }


        if (mbTooWide && _tempRect.width != ResizingDefaultSizes._instance.DefaultResolutionWidth * _ratio / Screen.width)
        {
            _ratio = Screen.height / ResizingDefaultSizes._instance.DefaultResolutionHeight;
			_tempRect.width = ResizingDefaultSizes._instance.DefaultResolutionWidth * _ratio / Screen.width;
            
			_tempRect.height = 1.0f;
            _tempRect.x = (1.0f - _tempRect.width) / 2.0f;
            _tempRect.y = 0.0f;
			
			/*_ratio = Screen.width / ResizingDefaultSizes._instance.DefaultResolutionWidth;
			_tempRect.height = ResizingDefaultSizes._instance.DefaultResolutionWidth * _ratio / Screen.height;
			_tempRect.width = 1.0f;
			_tempRect.y = (1.0f - _tempRect.height)/2.0f;
			_tempRect.x = 0.0f;*/
			
        }
        

        else if (!mbTooWide && _tempRect.height != ResizingDefaultSizes._instance.DefaultResolutionHeight * (_ratio / Screen.height))
        {
            _ratio = Screen.width / ResizingDefaultSizes._instance.DefaultResolutionWidth;

            _tempRect.width = 1.0f;
            _tempRect.height = ResizingDefaultSizes._instance.DefaultResolutionHeight * _ratio / Screen.height;
            _tempRect.x = 0.0f;
            _tempRect.y = (1.0f - _tempRect.height) / 2.0f;
        }

        if (camera.rect != _tempRect)
        {
            camera.rect = _tempRect;
			Debug.Log(_tempRect.xMax);
			//GUICompnent._instance.SetParam ();
        }
	}
}