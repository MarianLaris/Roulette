using UnityEngine;
using System.Collections;

public class ImageLoader : MonoBehaviour {

	public void LoadFromURL(string url)
	{
		StartCoroutine(LoadImageFromURL(url)); 
	}
	
	IEnumerator LoadImageFromURL(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		gameObject.guiTexture.texture = www.texture;
	}
}
