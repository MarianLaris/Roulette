using UnityEngine;
using System.Collections;

public class ResizingGUITexture : MonoBehaviour 
{	
	GUITexture myTexture;

    public float pi_X;
    public float pi_Y;
    public float pi_Width;
    public float pi_Height;
//	public Texture image;
    private Rect tempRect;

	// Use this for initialization
	void Start () 
    {
		
        myTexture = gameObject.guiTexture;
		pi_X = myTexture.pixelInset.x;
		pi_Y = myTexture.pixelInset.y;
		pi_Width = myTexture.pixelInset.width;
		pi_Height = myTexture.pixelInset.height;
	}
	
	// Update is called once per frame
	void Update () 
    {
       	tempRect.x = pi_X * ResizingViewport._instance._ratio;
        tempRect.y = pi_Y * ResizingViewport._instance._ratio;
        tempRect.width = pi_Width * ResizingViewport._instance._ratio;
        tempRect.height = pi_Height * ResizingViewport._instance._ratio;
		//Debug.Log(ResizingViewport.instance.ratio);
        if (myTexture.pixelInset != tempRect)
        {
            myTexture.pixelInset = tempRect;
		}
	}
}
