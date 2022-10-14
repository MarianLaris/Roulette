//using UnityEngine;
//using System.Collections;
//
//[ExecuteInEditMode]
//public class EditMode : MonoBehaviour {
//	
//	public bool _SaveStartCondition = false;
//	public bool _SaveEndCondition = false;
//	public bool _ShowStartConditions = false;
//	public bool _ShowEndConditions = false;
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		if(_SaveStartCondition == true)
//		{
//			saveStartCondition();
//			_SaveStartCondition = false;
//		}
//		
//		else if(_SaveEndCondition == true)
//		{
//			saveEndCondition();
//			_SaveEndCondition = false;
//		}
//		
//		else if(_ShowStartConditions == true)
//		{
//			restoreStartCondition();
//			_ShowStartConditions = false;
//		}
//		
//		else if(_ShowEndConditions == true)
//		{
//			restoreEndCondition();
//			_ShowEndConditions = false;
//		}
//	}
//	
//	void saveStartCondition()
//	{	
//		MenuItemEntryAnimation animEntry = GetComponent<MenuItemEntryAnimation>();
//		if(animEntry != null)
//		{
//			animEntry._StartPosition = transform.position;
//			animEntry._StartScale =transform.localScale;
//			animEntry._StartRotation=transform.eulerAngles;
//		}
//		
////		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
////		if(animExit != null)
////		{
////			animExit._EndPos = gameObject.transform.position;
////			animExit._EndScale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
////		}
//	}
//	
//	void saveEndCondition()
//	{
//		MenuItemEntryAnimation animEntry = GetComponent<MenuItemEntryAnimation>();
//		if(animEntry != null)
//		{
//			animEntry._EndPosition = transform.position;
//			animEntry._EndScale =transform.localScale;
//			animEntry._EndRotation=transform.eulerAngles;
//		}
//		
////		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
////		if(animExit != null)
////		{
////			animExit._StartScale = new Vector2(gameObject.transform.localScale.x,gameObject.transform.localScale.y);
////		}
//	}
//	
//	void restoreStartCondition()
//	{
//		MenuItemEntryAnimation animEntry = GetComponent<MenuItemEntryAnimation>();
//		if(animEntry != null)
//		{
//			transform.position=animEntry._StartPosition;
//			transform.localScale=animEntry._StartScale;
//			transform.eulerAngles=animEntry._StartRotation;
//		}
////		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
////		if(animExit != null)
////		{
////			gameObject.transform.position = animExit._EndPos;
////			gameObject.transform.localScale = new Vector3(animExit._StartScale.x,animExit._StartScale.y,gameObject.transform.localScale.z);
////		}
//	}
////	
//	void restoreEndCondition()
//	{
//		MenuItemEntryAnimation animEntry = GetComponent<MenuItemEntryAnimation>();
//		if(animEntry != null)
//		{
//			transform.position=animEntry._EndPosition;
//			transform.localScale=animEntry._EndScale;
//			transform.eulerAngles=animEntry._EndRotation;
//		}
//		
////		MenuItemExitAnim animExit = GetComponent<MenuItemExitAnim>();
////		if(animExit != null)
////		{
////			gameObject.transform.localScale = new Vector3(animExit._EndScale.x,animExit._EndScale.y,gameObject.transform.localScale.z);
////		}
//	}
//}
