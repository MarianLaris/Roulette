using UnityEngine;
using System.Collections;

public class Zooming : MonoBehaviour 
{
	public float _startTimeDelay;
	public float _btwAnimationDelay;
	Vector3 targetValue;
	Vector3 currentValue;
	float speed = .1f;
	bool _bAnimating;
	int counter;
	
	// Use this for initialization
	IEnumerator Start () 
	{
		currentValue = transform.localScale;
		yield return new WaitForSeconds(_startTimeDelay);
		ZoomIn();
		_bAnimating = true;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_bAnimating)
		{
			transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x , targetValue.x , speed * Time.deltaTime) , Mathf.MoveTowards(transform.localScale.y , targetValue.y , speed * Time.deltaTime) , transform.localScale.z);
			
			if(transform.localScale.x == targetValue.x)
			{
				if(targetValue.x > currentValue.x)
				{
					
					ZoomOut();
				}
				
				else
				{			
					if(counter == 3)
					{
						this.enabled = false;
						StartCoroutine(StartAction(_btwAnimationDelay));
					}
					else
					ZoomIn();
					
				}
			}
		}
	}
	
	void ZoomIn()
	{
		counter++;
		if(counter == 1)
		targetValue = currentValue + new Vector3(currentValue.x/(3.0f * 2.5f) ,currentValue.y/(1.5f * 2.5f), currentValue.z);
		else if(counter == 3)
			targetValue = currentValue;

	}
	
	void ZoomOut()
	{
		targetValue = currentValue - new Vector3(currentValue.x/(3.0f * 2.5f) ,currentValue.y/(1.5f * 2.5f) , currentValue.z);
		counter++;
	}
	
	IEnumerator StartAction(float time)
	{
		yield return new WaitForSeconds(time);
		this.enabled = true;
		counter = 0;
		ZoomIn();
	}
	
	public void SetDefaultScale()
	{
		transform.localScale = currentValue;
	}
}
