using UnityEngine;
//using System.Collections;

public class PopupTexAnimation : MonoBehaviour {
	
	int mPopUpTime;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
			mPopUpTime++;
		 	if(mPopUpTime>=30)
			{
				Destroy(this.gameObject);
			}
	}
}
