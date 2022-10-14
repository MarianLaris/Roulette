using UnityEngine;
using System.Collections;

public class BallSpeed : MonoBehaviour {

	public Vector3 _Velocity;
	// Use this for initialization
	void Start () 
	{
		rigidbody.velocity = _Velocity;
	}
}
