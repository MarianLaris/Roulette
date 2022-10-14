using UnityEngine;
using System.Collections;

public class RouletteRotation : MonoBehaviour {
	
	public Vector3 _AngularVel = new Vector3(0,8,0);
	//public Vector3 _Tourque=new Vector3(0,50000000,0);
	//public Vector3 _Velocity = new Vector3(0,0,5);
	public Transform _Collider;
	bool mbOnce;
	float mfMinVelocty;
	float mfMaxAngularDrag;
	Switcher mSwitcher;
	// Use this for initialization
	void Start () 
	{
		mfMinVelocty=5f;//Random.Range(5f,6f);
		float isYcom=Random.Range(7f,9f);
		mfMaxAngularDrag=0.6f;
		rigidbody.angularDrag=0.05f;
		rigidbody.angularVelocity = new Vector3(0,isYcom,0);//_AngularVel;
		mSwitcher=GameObject.Find("Switcher").GetComponent<Switcher>();
	}
	// Update is called once per frame
	void Update()
	{
		
		if(rigidbody.angularVelocity.y<mfMinVelocty&&!mbOnce)
		{
			mbOnce=true;
			rigidbody.angularDrag=mfMaxAngularDrag;
		}
     	_Collider.rotation=this.transform.rotation;
		if(rigidbody.angularVelocity.y<=0.25f)
		{
			   mSwitcher._IsSleep=true;
		}
	}

}




//float sliceAmount = 0;
//
//int slice;
//
// 
//
//while (angle > sliceAmount)
//
//{
//
// 
//
//   sliceAmount += 9.4f;
//
//   slice ++;
//
//}
//
// 
//
//if (slice == 36) { sliceNumber = "0" }
//
//else if (slice == 37) { sliceNumber = "00" }
//
//else { sliceNumber = slice };