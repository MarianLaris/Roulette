using UnityEngine;
using System.Collections;

public class ScrollMenuItemManager : ScrollAndPaging
{
	
	void Start () 
	{
		base.InitScroll();
	}
	
	public override void OnSelectedEvent(GUIItem item)
	{
		//ON CALL BACK FROM THE BUTTONS
		Debug.Log("------- IN derived Class.. GUI item selected : " + item.name);
		if(item.name == "BtnBuySuperKirara")
		{
			
		}			
		else if(item.name == "BtnBuyElf")
		{
		}
		else if(item.name == "BtnBuyMermaid")
		{
		}
		else if(item.name == "BtnBuyNinja")
		{
		}
		else if(item.name == "BtnBuyGenie")
		{
		}
	}
	
}
