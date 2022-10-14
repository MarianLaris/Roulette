using UnityEngine;
using System.Collections;

public class PagingDelegate : MonoBehaviour {

	// Use this for initialization
	public GameObject _IndicatorPrefab;
	public Vector3 _PositionDiff;
	public Vector3 _BasePosition;

	GameObject[] _IndicatorArray;
	int miNumberOfPages = 1;
	void Start () 
	{

	}
	
	void Awake()
	{
		
	}
	
	public void OnPageChanged(int pageNumber)
	{
		Debug.Log("Page number = " + pageNumber);
		GameObject tObject;
		if(_IndicatorArray != null)
		{
			if((pageNumber-1) > _IndicatorArray.Length || (pageNumber-1) < 0 || _IndicatorArray.Length == 0)
			{
				Debug.Log("Paging index out of range");
				return;
			}
			
			for(int i = 0; i< _IndicatorArray.Length;i++)
			{
				tObject = _IndicatorArray[i];
//				if(i == (pageNumber-1))
//					tObject.GetComponent<GUITexture>().color = new Color(0.5f,0.5f,0.5f,0.5f);
//				else
//					tObject.GetComponent<GUITexture>().color = new Color(1,1,1,1);
			}
		}
	}
	
	public void OnSetNumberOfPages(int numberOfPages)
	{
		Debug.Log("Set number of pages "+numberOfPages);
		miNumberOfPages = numberOfPages;
		GameObject tObject = null;
		if(_IndicatorArray != null)
		{
			for(int i = 0; i< _IndicatorArray.Length;i++)
			{
				tObject = _IndicatorArray[i];
				Destroy(tObject);
			}
		}
		_IndicatorArray = new GameObject[miNumberOfPages];
		createIndicators();
	}
	
	void createIndicators()
	{
		if(_IndicatorPrefab == null)
			return;
		
		GameObject tObject = null;
		int tVal = miNumberOfPages;
		if(tVal % 2 == 1)
			tVal -= 1;
		
		Vector3 currentPosition = _BasePosition - _PositionDiff*(tVal/2.0f);
		for(int i = 0; i< _IndicatorArray.Length;i++)
		{
			tObject = Instantiate(_IndicatorPrefab,currentPosition,Quaternion.identity) as GameObject;
			currentPosition += _PositionDiff;
			_IndicatorArray[i] = tObject;
		}
	}
	
	public void cleanUp()
	{
		GameObject tObject = null;
		if(_IndicatorArray != null)
		{
			for(int i = 0; i< _IndicatorArray.Length;i++)
			{
				tObject = _IndicatorArray[i];
				Destroy(tObject);
			}
		}
		_IndicatorArray = null;
	}
	
	void OnEnable()
	{
	}
	
	void OnDisable()
	{
		Debug.Log("Disabled");
	}
	
	void OnDestroy() 
	{
		cleanUp();
		Debug.Log("Destroyed");
	}
	// Update is called once per frame
	void Update () 
	{
	
	}
}
