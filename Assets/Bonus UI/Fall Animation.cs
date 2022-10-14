using UnityEngine;
using System.Collections;

public class FallAnimation : MonoBehaviour {

	Vector3 finalPosition;
	float speed;
	// Use this for initialization
	void Start () {
		finalPosition = new Vector3(1,transform.position.y + .3f , transform.position.z);
		speed = 1;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position , finalPosition , speed * Time.deltaTime);
		if(transform.position.Equals(finalPosition))
		{
			if(transform.position.y < 0)
				Destroy(this);
			finalPosition = new Vector3(1,-.1f , transform.position.z);
		}
		
	
	}
}
