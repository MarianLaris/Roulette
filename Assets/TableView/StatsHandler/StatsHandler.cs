using UnityEngine;
using System.Collections;
using FBKit;
using System;

public class StatsHandler 
{
	static StatsHandler _Instance;
	static JSONObject _JsonObject;
	public static int _MaxNumberOfStats = 20;
	
	static StatsHandler GetInstance()
	{
		if(_Instance == null)
		{
			_Instance = new StatsHandler();
			_JsonObject = new JSONObject();
			_JsonObject.type = JSONObject.Type.OBJECT;
			
			retrieveStats();
		}	
		return _Instance;
	}
	
	static void retrieveStats()
	{
		//GETTINGING THE JSON OBJECT AS STRING FROM PLAYER PREFS
		string tJSONString = null;
		if(PlayerPrefs.HasKey("StatsHadlerStats"))
			tJSONString = PlayerPrefs.GetString("StatsHadlerStats",_JsonObject.print());
		
		if(tJSONString != null && tJSONString != "") 
		{
			_JsonObject = new JSONObject(tJSONString);		
			_JsonObject.type = JSONObject.Type.OBJECT;
		}
		else
			_JsonObject = new JSONObject();		
			_JsonObject.type = JSONObject.Type.OBJECT;
			
	}
	
	public static void saveStats()
	{
		if(_Instance == null)
			GetInstance();
		
		//SAVING THE JSON OBJECT AS STRING IN PLAYER PREFS
		PlayerPrefs.SetString("StatsHadlerStats",_JsonObject.print());
	}
	
	public static void clearStats()
	{
		if(_Instance == null)
			GetInstance();
		
		PlayerPrefs.DeleteKey("StatsHadlerStats");
		retrieveStats();
	}
	
	public static void addJSONStringToStats(string pValue)
	{
		//MAKE SURE THE KEY IS OF THIS FORMAT Stats_#
		if(_Instance == null)
			GetInstance();
				
		if(_JsonObject.HasField("Stats_1"))
		{
			addStatsFromIndexToIndex(1,2);								//SHIFT TO NEXT STATS
			JSONObject tJSONobj = new JSONObject(pValue);
			tJSONobj.type = JSONObject.Type.OBJECT;
			_JsonObject.SetField("Stats_1",tJSONobj);					//REPLACE
		}
		else
		{
			JSONObject tJSONobj = new JSONObject(pValue);				//CREATE NEW (ONLY 1st TIME)
			tJSONobj.type = JSONObject.Type.OBJECT;
			_JsonObject.SetField("Stats_1",tJSONobj);	
		}
	}
	
	public static JSONObject getCurrentStatus()
	{
		if(_Instance == null)
			GetInstance();
		if(_JsonObject.HasField("Stats_1"))
			return(_JsonObject.GetField("Stats_1"));
		return null;
	}
	
	public static JSONObject getPreviousStatus()
	{
		if(_JsonObject.HasField("Stats_2"))
			return(_JsonObject.GetField("Stats_2"));
		return null;
	}
					
	static void addStatsFromIndexToIndex(int index1,int index2)
	{
		string tKey1 = "Stats_"+index1;
		string tKey2 = "Stats_"+index2;
		
		if(index2 <= _MaxNumberOfStats)
		{
			//ITS DEFINATE THAT OBJECT FOR KEY 1 EXISTS
			if(_JsonObject.HasField(tKey2) && _JsonObject.HasField(tKey1))
			{
				if(index2 != _MaxNumberOfStats)
					addStatsFromIndexToIndex(index2,index2+1);				//SHIFT TO NEXT STATS
				_JsonObject.SetField(tKey2,_JsonObject.GetField(tKey1));	//REPLACE
			}
			else if(_JsonObject.HasField(tKey1))
				_JsonObject.AddField(tKey2,_JsonObject.GetField(tKey1));	//ADD NEW
		}
	}
	
	public static JSONObject getStats()
	{
		if(_Instance == null)
			GetInstance();
		return _JsonObject;
	}
	
	public static void enterStats(string newStatsJSON)
	{
		//ENTER STATS FROM GAME
		JSONObject tNewStats = new JSONObject(newStatsJSON);
		string date = tNewStats.GetField("textField1").str;
		//Debug.Log("Date = "+date);
		
		//RETRIEVE CURRENT AND PREVIOUS STATS
		JSONObject tcurrentStatus = getCurrentStatus();
		if(tcurrentStatus != null)
		{
			//IF THE STATS ENTERED IS TO BE ADDED WITH CURRENT ENTRY
			if(date == tcurrentStatus.GetField("textField1").str)
			{
				double originalFund = tcurrentStatus.GetField("OriginalFund").n;
				
				//UPDATE CURRENT STATS
				tcurrentStatus.GetField("Rounds").n = tcurrentStatus.GetField("Rounds").n + 1;
				tcurrentStatus.GetField("textField2").str = ""+tcurrentStatus.GetField("Rounds").n;
				int scoreValue = (int)tcurrentStatus.GetField("Scored").n + (int)tNewStats.GetField("Scored").n;
				tcurrentStatus.GetField("textField4").str = tNewStats.GetField("textField4").str;
				tcurrentStatus.GetField("Scored").n = scoreValue;
				tcurrentStatus.GetField("textField3").str = "100";
				
				//COMPARE WITH CURRENT STATS AND NEW STATS PASSED (SAME DAY)
				//COMPARE SCORES AND GET RELATIVE INCREASE OR DECREASE IN FUNDS
				int curScore = (int)tcurrentStatus.GetField("Scored").n;
				float increaseInFundPercentage = (curScore/(float)originalFund) * 100.0f;										
				string increaseInFundsString = (increaseInFundPercentage.ToString("0.00"));
				if(increaseInFundPercentage > 100)
					increaseInFundsString = (increaseInFundPercentage.ToString("0"));
						
				tcurrentStatus.GetField("textField3").str = ""+increaseInFundsString;
				if(increaseInFundPercentage > 0)
					tcurrentStatus.GetField("Increased").b = true;
				else 
					tcurrentStatus.GetField("Increased").b = false;
			}
			else //IF NEW ENTRY IS REQUIRED
			{
				JSONObject tJSONObj = new JSONObject();
				tJSONObj.type = JSONObject.Type.OBJECT;
				tJSONObj.AddField("textField1",tNewStats.GetField("textField1").str);
				tJSONObj.AddField("Rounds",1);
				tJSONObj.AddField("textField2","1");
				tJSONObj.AddField("textField4",""+ tNewStats.GetField("textField4").str);	
				tJSONObj.AddField("Scored",(int)tNewStats.GetField("Scored").n);
				tJSONObj.AddField("textField3","100");
				tJSONObj.AddField("Increased",true);

				//IF CURRENTLY STATS EXISTS TAKE ORIGNIAL FUNDS VALUE FROM IT
				int curScore = (int)tJSONObj.GetField("Scored").n;
				tJSONObj.AddField("OriginalFund",int.Parse(tNewStats.GetField("textField4").str) - curScore);
				int originalFund = (int)tcurrentStatus.GetField("OriginalFund").n;				
				float increaseInFundPercentage = (curScore/(float)originalFund) * 100.0f;
										
				string increaseInFundsString = (increaseInFundPercentage.ToString("0.00"));
				if(increaseInFundPercentage > 100)
					increaseInFundsString = (increaseInFundPercentage.ToString("0"));					
				tJSONObj.GetField("textField3").str = ""+increaseInFundsString;
				if(increaseInFundPercentage > 0)
					tJSONObj.GetField("Increased").b = true;
				else 
					tJSONObj.GetField("Increased").b = false;
				StatsHandler.addJSONStringToStats(tJSONObj.print());
			}
		}
		else
		{
			//FIRST ENTRY
			JSONObject tJSONObj = new JSONObject();
			tJSONObj.type = JSONObject.Type.OBJECT;
			tJSONObj.AddField("textField1",tNewStats.GetField("textField1").str);
			tJSONObj.AddField("Rounds",1);
			tJSONObj.AddField("textField2","1");
			tJSONObj.AddField("textField4",""+ tNewStats.GetField("textField4").str);
			tJSONObj.AddField("Scored",(int)tNewStats.GetField("Scored").n);
			
			//FUNDS IN HAND ORIGINALLY BECOMES STANDARD FOR COMPARISION IN PERCENTAGE WIN
			int curScore = (int)tJSONObj.GetField("Scored").n;
			tJSONObj.AddField("OriginalFund",int.Parse(tNewStats.GetField("textField4").str) - curScore);
			
			//COMPARE SCORES AND GET RELATIVE INCREASE OR DECREASE IN FUNDS
			double originalFund = tJSONObj.GetField("OriginalFund").n;
				
			float increaseInFundPercentage = (curScore/(float)originalFund) * 100.0f;										
			string increaseInFundsString = (increaseInFundPercentage.ToString("0.00"));
			if(increaseInFundPercentage > 100)
				increaseInFundsString = (increaseInFundPercentage.ToString("0"));
					
			tJSONObj.AddField("textField3",""+increaseInFundsString);
			if(increaseInFundPercentage > 0)
				tJSONObj.AddField("Increased",true);
			else 
				tJSONObj.AddField("Increased",false);
						
			StatsHandler.addJSONStringToStats(tJSONObj.print());
		}
	}
}
