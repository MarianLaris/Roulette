using UnityEngine;
using System.Collections;
using FBKit;
using System;

public class AchieveFundHandler : MonoBehaviour {

   public	int mTotalLastBetFund;
   public	int mTotalReducedFund;
   public	int TotalFund;
   public   int mTotaDebedetAmpont;
   public   int mTotalWinnigAmount;	
   public bool updateTableView;
   int mDifferenceofBettingAndWinningAmount;	
	int count;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(updateTableView)
		{
			UpdateTableView();	
			//print(updateTableView);
			updateTableView=false;
			//print(updateTableView);
			mDifferenceofBettingAndWinningAmount=mTotalWinnigAmount-mTotaDebedetAmpont;	
		//print("mTotalWinnigAmount"+mTotalWinnigAmount+"mTotaDebedetAmpont"+mTotaDebedetAmpont+"TotalFund"+TotalFund);
			
		}
	
	}
	
	
    public	void Funddata(int lastBetFund,int lastReducedFund,int totalfund)
	{
	  mTotalLastBetFund=lastBetFund;	
	  mTotalReducedFund=lastReducedFund;
	  TotalFund=totalfund;
	 // print("bet"+mTotalLastBetFund+"Reduce"+mTotalReducedFund+"Totalfund"+TotalFund);	
	}
	
	public void TotalWinningAmount()
	{
		
		
	}
	
	 void  UpdateTableView()
		
	    {
		
		  //	Debug.Log("_TotalWinningAmount"+_TotalWinningAmount+"_TotalbettingAmount"+_TotalbettingAmount);
		
		
		
		     	//TableView
			   string date = DateTime.Now.ToString("MM/dd/yyyy");
		        //date = ( day >= 10 ? "":"0")+day+"/05/2011";
              // Debug.Log("Date = "+date);
	           JSONObject tJSONObj3 = new JSONObject();
	           tJSONObj3.type = JSONObject.Type.OBJECT;
	           tJSONObj3.AddField("textField1",date);
	          // int scoreValue = UnityEngine.Random.Range(500,1000);
		
		    //   Debug.Log("TotalFund"+TotalFund);
	           tJSONObj3.AddField("textField4",""+ PlayerPrefs.GetInt("TotalFund"));    
	           tJSONObj3.AddField("Scored",mDifferenceofBettingAndWinningAmount);
	           StatsHandler.enterStats(tJSONObj3.print());
		       StatsHandler.saveStats();
		
			//Tableview
		
	    }
	
}
