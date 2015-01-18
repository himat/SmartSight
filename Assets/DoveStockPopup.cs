using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Net;
using SimpleJSON;

public class DoveStockPopup : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;
	private bool mShowGUIButton = false;
	private Rect lTitle = new Rect(2,50,400,100);
	private Rect lText = new Rect(2,150,300,100);
	private Rect lDailyChange = new Rect (2, 250, 300, 100);
	private Rect lYearlyChange = new Rect (2, 350, 300, 100);
	private Rect backgroundy = new Rect (50,200,300,500);
	private GUIStyle Title = new GUIStyle();
	private GUIStyle Texty = new GUIStyle();
	private GUIStyle Backy = new GUIStyle();
	private GUIStyle Buttony = new GUIStyle();
	public Font MyFont;
	
	//Stock calculations via Bloomberg
	private string jsonInput = null;
	private JSONNode parser = null;
	private float lastYearPrice = 0;
	private JSONNode thisYearPrices = null;
	private float yesterdayPrice =  0;
	private float todayPrice =  0;	
	private float dailyChange = 0;
	private float yearlyChange = 0;
	
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
		bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}
	
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED)
		{
			mShowGUIButton = true;
		}
		else
		{
			mShowGUIButton = false;
		}
	}
	
	void OnGUI() {
		if (mShowGUIButton) {
			// draw the GUI button
			Title.fontSize = 70;
			//Title.font = (Font)Resources.Load("Fonts/Freshman.ttf");
			Title.normal.textColor = Color.white;
			Texty.fontSize = 50;
			//Texty.font = (Font)Resources.Load("Fonts/FineCollege.ttf");
			Texty.normal.textColor = Color.white;
			GUI.Label(lTitle, "Unilever (UL)",Title);
			//if(GUI.Button(
			GUI.skin.font = MyFont;
			
			GUI.Box(backgroundy,"", Backy);
			
			if(jsonInput == null)
			{
				jsonInput = new WebClient().DownloadString("http://104.131.94.146:8080/UL");
				parser = JSON.Parse (jsonInput);
				
				lastYearPrice = parser["data"] [0] ["securityData"] ["fieldData"] [0] ["PX_LAST"].AsFloat;
				thisYearPrices = parser["data"] [0] ["securityData"] ["fieldData"];
				yesterdayPrice =  thisYearPrices[thisYearPrices.Count-2] ["PX_LAST"].AsFloat;
				todayPrice =  thisYearPrices[thisYearPrices.Count-1] ["PX_LAST"].AsFloat;
				
				dailyChange = todayPrice-yesterdayPrice;
				yearlyChange = todayPrice-lastYearPrice;
			}
			/*RootObject wrapper = ser.Deserialize<RootObject> (jsonInput);
			Datum d = wrapper.data;
			SecurityData s = d.securityData;
			FieldData fieldData = s.fieldData;
			var theDate = fieldData.date[fieldData.date.Length-1];*/
			//Dictionary dict = ser.Deserialize<Dictionary<string,object>>(jsonInput);
			//var postalCode = dict["fieldData"];
			
			var stocks = "Stock Price : " + todayPrice;
			GUI.Label (lText, stocks, Texty);
			GUI.Label(lDailyChange, "Daily Change: " + (dailyChange>0 ? System.String.Format("+{0}", dailyChange.ToString("F2")) : dailyChange.ToString("F2")), Texty);
			GUI.Label (lYearlyChange, "Yearly Change: "+ (yearlyChange>0 ? System.String.Format("+{0}", yearlyChange.ToString("F2")) : yearlyChange.ToString ("F2")), Texty);
			
			Buttony.fontSize = 65;
			Buttony.normal.textColor = Color.white;
			if (GUI.Button(new Rect (32, 450, 400, 80), "")) {
				Application.LoadLevel ("moreinfo");	
			}
			GUI.Label (new Rect (72, 450, 400, 80), "More Info", Buttony);
			
		};
		
		//GUI.Label(lTitle, totalDebt, Title);
		
		/*
			var totalDebt = "";
			var retainedEarnings = "";
			var totalAssets = "";
			XmlDocument doc = new XmlDocument();
			doc.Load("C:\\Users\\Hima\\Documents\\Visual Studio 2012\\Projects\\XMLTest\\XMLTest\\StockInfo\\Coke.xml");
			
			XmlNode node = doc.DocumentElement.SelectSingleNode("/corefinancials/result/rowset/row/groups/group/rowset");
			foreach (XmlNode valueNode in node)
			{
				if(valueNode.Attributes["field"].InnerText=="TotalDebt")
					totalDebt = valueNode.InnerText;
				if (valueNode.Attributes["field"].InnerText == "RetainedEarnings")
					retainedEarnings = valueNode.InnerText;
				if (valueNode.Attributes["field"].InnerText == "TotalAssets")
					totalAssets = valueNode.InnerText;
			}
			
			/*Console.WriteLine(totalDebt);
			Console.WriteLine(retainedEarnings);
			Console.WriteLine(totalAssets);
			Console.ReadLine();*/
		//Debug.Log (totalDebt);
		//Debug.Log (retainedEarnings);
		
		//Debug.Log ("if this outputs, that is good");
		//GUI.Label(lTitle, "hi", Title);
		
		

	}
}