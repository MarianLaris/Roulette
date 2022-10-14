using UnityEngine;
using System.Collections;

public class LoadingBar : MonoBehaviour {

	public bool _bStartProgess = false;
	Vector3 mvFullScale,mvScaleTo;
    float mfScale;
    float mfProgressRate;
    // Use this for initialization
    
	void Awake()
	{
		mvFullScale = gameObject.transform.localScale;
		mvScaleTo   = mvFullScale;
		mfProgressRate = mvFullScale.x/100.0f;
		mfScale = mvFullScale.x*0;
		gameObject.transform.localScale = new Vector3(mfScale,mvFullScale.y,mvFullScale.z);
		
		
		
		setProgressBarBegin(1.0f);
	}
	
    public  void setProgressBarBegin(float percent)
	{
		
		mvScaleTo.x      = mvFullScale.x*percent;
		_bStartProgess = true;
		
	}
    
    void setCompletion()
    {
        if(mfScale<mvScaleTo.x )
		{
			
			mfScale = mfScale+mfProgressRate;
			gameObject.transform.localScale = new Vector3(mfScale,mvFullScale.y,mvFullScale.z);
		}
		else
		{
			_bStartProgess = false;
		}
       
    }
	// Update is called once per frame
    void Update () 
    {
		if(_bStartProgess)
		{
			setCompletion();
		}
    }
}
