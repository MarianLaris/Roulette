using UnityEngine;
using System;
using System.Collections;
using FBKit;

public class CTableViewDelegate : MonoBehaviour {
	
	public CTableView _TableView;
	int day = 1;
	// Use this for initialization
	void Start ()
	{
		_TableView.InitTableView();
		JSONObject stats = StatsHandler.getStats();
		for(int i = 1; i <= StatsHandler._MaxNumberOfStats ; i++)
		{
			if(stats.HasField("Stats_"+i))
			{
				_TableView.addCellOfType(0,stats.GetField("Stats_"+i).print());
			}
			else if(i == 1)
			{
				JSONObject tJSONObj = new JSONObject();
				tJSONObj.type = JSONObject.Type.OBJECT;
				tJSONObj.AddField("textField1","NO STATS TODAY");
				tJSONObj.AddField("Rounds",0);
				tJSONObj.AddField("textField2","");
				tJSONObj.AddField("textField4","");	
				tJSONObj.AddField("Scored",0);
				tJSONObj.AddField("textField3","");
				_TableView.addCellOfType(0,tJSONObj.print());
			}
		}
		
	}
	
	public virtual void SetDataForCells(string pJSONString)
	{
		//DEPENDING ON JSON STRING ASSIGN TO EACH FIELD OF CELL
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(Input.GetKeyDown("1"))
//		{
//			day++;
//			day %= 31;
//		}
//		if(Input.GetKeyDown("2"))
//		{
//			string date = DateTime.Now.ToString("MM/dd/yyyy");
//			date = (day >= 10 ? "":"0")+day+"/05/2011";
//			Debug.Log("Date = "+date);
//			
//			
//			JSONObject tJSONObj3 = new JSONObject();
//			tJSONObj3.type = JSONObject.Type.OBJECT;
//			tJSONObj3.AddField("textField1",date);
//			int scoreValue = UnityEngine.Random.Range(500,1000);
//			tJSONObj3.AddField("textField4",""+ scoreValue);	
//			tJSONObj3.AddField("Scored",scoreValue);
//			StatsHandler.enterStats(tJSONObj3.print());
//			
//			
//			JSONObject stats = StatsHandler.getStats();
//			_TableView.refresh();
//			for(int i = 1; i <= StatsHandler._MaxNumberOfStats ; i++)
//			{
//				if(stats.HasField("Stats_"+i))
//				{
//					_TableView.addCellOfType(0,stats.GetField("Stats_"+i).print());
//				}
//			}
//			StatsHandler.saveStats();
//		}
//		if(Input.GetKeyDown("3"))
//		{
//			StatsHandler.clearStats();
//			JSONObject stats = StatsHandler.getStats();
//			_TableView.refresh();
//			for(int i = 1; i <= StatsHandler._MaxNumberOfStats ; i++)
//			{
//				if(stats.HasField("Stats_"+i))
//				{
//					_TableView.addCellOfType(0,stats.GetField("Stats_"+i).print());
//				}
//				else if(i == 1)
//				{
//					JSONObject tJSONObj = new JSONObject();
//					tJSONObj.type = JSONObject.Type.OBJECT;
//					tJSONObj.AddField("textField1","NO STATS TODAY");
//					tJSONObj.AddField("Rounds",0);
//					tJSONObj.AddField("textField2","");
//					tJSONObj.AddField("textField4","");	
//					tJSONObj.AddField("Scored",0);
//					tJSONObj.AddField("textField3","");
//					_TableView.addCellOfType(0,tJSONObj.print());
//				}
//			}
//		}
	}
}
