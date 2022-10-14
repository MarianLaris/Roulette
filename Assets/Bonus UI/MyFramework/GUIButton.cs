using UnityEngine;
using System.Collections;

public class GUIButton : GUIItemsHandler 
{
	//[HideInInspector]
	public GUIItemsHandler _gUIItemsHandler;
	//RaycastHit mhit;
	//GameObject SelectedButton;
	
	public bool _UsingSpriteSheet;
	
	public Texture2D _NormalStateTex;
	public Texture2D _SelectedStateTex;
	public Texture2D _HoverStateTex;
	public Texture2D _DisableStateTex;
	
	public Mesh _NormalStateMesh;
	public Mesh _SelectedStateMesh;
	public Mesh _HoverStateMesh;
	public Mesh _DisableStateMesh;
	
	bool mbButtonIsSelected;
	bool mbButtonIsDisable;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	void OnMouseDown() 
	{
		if(!mbButtonIsDisable)
		{
			mbButtonIsSelected=true;
			ChangeSelectedStateImage(_UsingSpriteSheet);
		}
    }
	
	void OnMouseUp()
	{
		mbButtonIsSelected=false;
	}
	
	void OnMouseEnter() 
	{
		if(!mbButtonIsDisable)
		{
			if(mbButtonIsSelected&&Input.GetMouseButton(0))
				ChangeSelectedStateImage(_UsingSpriteSheet);
			else if(!Input.GetMouseButton(0))
				ChangeHoverStateImage(_UsingSpriteSheet);
		}
	}
	
	void OnMouseExit() 
	{
		if(!mbButtonIsDisable)
		{
			ChangeNormaStateImage(_UsingSpriteSheet);
		}
	}
	
	void OnMouseUpAsButton() 
	{
		if(!mbButtonIsDisable)
		{
			mbButtonIsSelected=false;
			_gUIItemsHandler.OnSelectedEvent(gameObject);
			ChangeNormaStateImage(_UsingSpriteSheet);
		}
    }
	
	public void SetDisable(bool status)
	{
		mbButtonIsDisable=status;
		if(status)
		{
			ChangeDisableStateImage(_UsingSpriteSheet);
		}
		else
		{
			ChangeNormaStateImage(_UsingSpriteSheet);
		}
	}
	public bool GetButtonEnableStatus()
	{
		return(!mbButtonIsDisable);
	}
	
	void ChangeNormaStateImage(bool status)
	{
		if(!status)
				renderer.material.mainTexture=_NormalStateTex;
			else
				gameObject.GetComponent<MeshFilter>().mesh = _NormalStateMesh;
	}
	void ChangeSelectedStateImage(bool status)
	{
		if(!status)
				renderer.material.mainTexture=_SelectedStateTex;
			else
				 gameObject.GetComponent<MeshFilter>().mesh = _SelectedStateMesh;
	}
	void ChangeHoverStateImage(bool status)
	{
		if(!status)
			renderer.material.mainTexture=_HoverStateTex;
		else
			 gameObject.GetComponent<MeshFilter>().mesh = _HoverStateMesh;
	}
	void ChangeDisableStateImage(bool status)
	{
		if(!status)
			renderer.material.mainTexture=_DisableStateTex;
		else
			gameObject.GetComponent<MeshFilter>().mesh = _DisableStateMesh;
	}
	
}
