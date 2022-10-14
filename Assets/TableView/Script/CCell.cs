using UnityEngine;
using System.Collections;
using FBKit;

[System.Serializable]
public class CellTextFieldData
{
	public GameObject _TextFieldPrefab;
	public Vector3 _LocalPosition;
	public Vector3 _LocalScale;
	public Color _TextFieldColor;
	public string _TextFieldString;
	public TextAlignment _TextFieldAlignment;
	public TextAnchor _TextFieldAnchor;
}

[System.Serializable]
public class CellImageData
{
	public GameObject _CellImagePrefab;
	public string _DownloadUrl;
	public Texture _PredefinedTexture;
	public bool _IsButton;
}

public class CCell : MonoBehaviour
{	
	public CellTextFieldData[] _CellTextFields;
	public CellImageData[] _CellImageFields;

	public TextMesh _Textfield1;
	public Color _TextField1Color;
	
	public TextMesh _Textfield2;
	public Color _TextField2Color;
	
	public TextMesh _Textfield3;
	public Color _TextField3Color;
	public Color _TextField3AlternateColor;

	public TextMesh _Textfield4;
	public Color _TextField4Color;
	
	public GameObject _ImageField1;
	public Texture[] _ImageFieldTexture1;
	
	public GameObject _ImageField2;
	public Texture _ImageFieldTexture2;

	public string _UserData;
	
	bool _CellIsSelectable = false;
	JSONObject _jsonObject;
	// Use this for initialization
	void Start () 
	{
		if(_Textfield1 != null)
			_Textfield1.renderer.material.color = _TextField1Color;
		
		if(_Textfield2 != null)
			_Textfield2.renderer.material.color = _TextField2Color;
		
		if(_Textfield3 != null)
			_Textfield3.renderer.material.color = _TextField3Color;
		
		if(_Textfield4 != null)
			_Textfield4.renderer.material.color = _TextField4Color;
				
		if(_ImageField2 != null && _ImageFieldTexture2 != null)
			_ImageField2.renderer.material.mainTexture = _ImageFieldTexture2;
		
		refreshData();
	}
	
	public virtual void initializeWithJSONString(string pJSONString)
	{
		_jsonObject = new JSONObject(pJSONString);
		refreshData();
	}
	
	void refreshData()
	{
		if(_jsonObject != null)
		{
			if(_jsonObject.HasField("textField1"))
				_Textfield1.text = _jsonObject.GetField("textField1").str;
			
			if(_jsonObject.HasField("textField2"))
				_Textfield2.text = _jsonObject.GetField("textField2").str;
			
			if(_jsonObject.HasField("textField3"))
			{
				_Textfield3.text = _jsonObject.GetField("textField3").str;
				if(_jsonObject.HasField("Increased"))
				{
					if(_jsonObject.GetField("Increased").b == true)
						_Textfield3.renderer.material.color = _TextField3AlternateColor;
					else
						_Textfield3.renderer.material.color = _TextField3Color;
				}
			}
			
			if(_jsonObject.HasField("textField4"))
				_Textfield4.text = _jsonObject.GetField("textField4").str;
			
			if(_jsonObject.HasField("Increased"))
			{
				if(_jsonObject.GetField("Increased").b == true)
					_ImageField1.renderer.material.mainTexture = _ImageFieldTexture1[1];
				else
					_ImageField1.renderer.material.mainTexture = _ImageFieldTexture1[0];
			}
			else
				_ImageField1.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
