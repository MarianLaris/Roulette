using UnityEngine;
using System.Collections;

public class BetCamera : MonoBehaviour {
	
	GameObject mBetCamera;
	// Use this for initialization
	void Start () {
		
		mBetCamera=GameObject.Find("Camera");
		mBetCamera.camera.enabled=false;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
	
	}
}
