using UnityEngine;
using System.Collections;

[System.Serializable]
public class CellData
{
	public GameObject _CellPrefab;
	public float _CellSpacingTop;
	public float _CellSpacingBottom;
	public Vector2 _CellSize;
	public Vector3 _Offset;
}

public class CTableView : CScrollView 
{
	public CellData[] _CellStructures;
	public Vector3 _ContentOffset;
	
	Vector3 mvAddCellPosition;
	ArrayList _CellArray = new ArrayList();
	
	// Use this for initialization
	void Start () 
	{
		//CREATE THE TABLE USING ITS TABLEVIEW DELEGATE SCRIPT
		
	}
	
	public void InitTableView()
	{
		_Bounds = new Rect(_Bounds.x,_Bounds.y,0,0);
		mvAddCellPosition = _ContentOffset;
		base.InitScroll();
	}
	
	public void addCellOfType(int pType,string pCellData)
	{
		if(_CellStructures.Length > pType)
		{
			//CREATE CELL OF THIS TYPE
			GameObject cell = Instantiate(_CellStructures[pType]._CellPrefab) as GameObject;
			cell.transform.parent = gameObject.transform;
			
			mvAddCellPosition += new Vector3(0,-1 * _CellStructures[pType]._CellSpacingTop,0);
			_Bounds = new Rect(_Bounds.x,_Bounds.y,_Bounds.width,_Bounds.height + _CellStructures[pType]._CellSpacingTop);
			
			cell.transform.localPosition = mvAddCellPosition;
			mvAddCellPosition += new Vector3(0,-1 * _CellStructures[pType]._CellSpacingBottom,0);

			float boundsWidth = mvAddCellPosition.x - _ContentOffset.x;
			float boundsHeight = (-1*mvAddCellPosition.y) - _ContentOffset.y;
			mvAddCellPosition += new Vector3(_CellStructures[pType]._CellSize.x/2.0f,-1 * (_CellStructures[pType]._CellSize.y/2.0f) ,0);
			
			if(boundsWidth < 0)
				boundsWidth = 0;
			if(boundsHeight < 0)
				boundsHeight = 0;
			
			_Bounds = new Rect(_Bounds.x,_Bounds.y,boundsWidth,boundsHeight);
			
			CCell tCellScr = cell.GetComponent<CCell>();
			if(tCellScr != null)
				tCellScr.initializeWithJSONString(pCellData);
			
			_CellArray.Add(cell);
		}
	}
	
	public void refresh()
	{
		foreach(GameObject obj in _CellArray)
			Destroy(obj);
		
		_CellArray.RemoveRange(0,_CellArray.Count);
		_CellArray = new ArrayList();
		
		_Bounds = new Rect(_Bounds.x,_Bounds.y,0,0);
		mvAddCellPosition = _ContentOffset;		
	}
}
