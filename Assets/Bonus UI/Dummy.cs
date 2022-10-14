using UnityEngine;
using System.Collections;

public class Dummy : MonoBehaviour {

	// Use this for initialization
	public GameObject prefabRef;
	public int[] arr;
	public int availableCoins;
	void Start () {
		
	
	}
	
	void OnGUI()
	{
		if(GUILayout.Button("StartBonus",GUILayout.Width(100),GUILayout.Height(100)))
		{
			//bool status = BonusHandler.Instance.StartBonus(prefabRef,arr,CallBack);
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void CallBack(int receivedBonus)
	{
		Debug.Log("Call back Received with Bonus : "+receivedBonus);
	}
}
