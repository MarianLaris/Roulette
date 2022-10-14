using UnityEngine;
using System.Collections;

public class ChipProperty : MonoBehaviour 
{
    public bool _IsWinningChip; 
	public GameObject _bettedObject;
	public Vector3 _intialPosition;
	public int _AmountOnthis;
	public GameObject _ParentChip;
	
	public ArrayList _ChipRelatedtoRaceTrackChip=new ArrayList();
	public void DestroyRelatedChip()
	{
		foreach(GameObject Obj in _ChipRelatedtoRaceTrackChip)
		{
			Destroy(Obj);
		}
		_ChipRelatedtoRaceTrackChip.Clear();
	}
}
