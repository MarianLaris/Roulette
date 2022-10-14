using UnityEngine;
using System.Collections;

public class GUIItemImage : GUIItem 
{
	// Use this for initialization
	void Start() 
	{
		meGUIItemType = eGUI_ITEM_TYPE.Image;
		if(_guiItemsManager == null)
		{
			//MenuItemEntryAnim tMenuEntryAnim = gameObject.GetComponent<MenuItemEntryAnim>();
			//if(tMenuEntryAnim != null)
			//	tMenuEntryAnim.beginAnimationWithCallBack(null);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateSizeWithViewPort();
	}
}
