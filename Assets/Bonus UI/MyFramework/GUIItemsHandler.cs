using UnityEngine;
using System.Collections;

public class GUIItemsHandler : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	public virtual void OnSelectedEvent(GameObject item)
	{
		//ON CALL BACK FROM THE BUTTONS
		Debug.Log("------- IN base Class.. GUI item selected : " + item.name);
	}
}
