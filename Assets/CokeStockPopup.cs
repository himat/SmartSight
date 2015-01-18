using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Net;
using SimpleJSON;

public class CokeStockPopup : MonoBehaviour, ITrackableEventHandler {
	
	private TrackableBehaviour mTrackableBehaviour;
	private bool mShowGUIButton = false;
	private Rect lTitle = new Rect(2,50,400,100);
	private Rect lText = new Rect(2,150,300,100);
	private Rect lDailyChange = new Rect (2, 250, 300, 100);
	private Rect lYearlyChange = new Rect (2, 350, 300, 100);
	private Rect lStockAmount = new Rect (400, 900, 300, 100);
	private Rect backgroundy = new Rect (50,200,300,500);

	private GUIStyle Title = new GUIStyle();
	private GUIStyle Texty = new GUIStyle();
	private GUIStyle Backy = new GUIStyle();
	private GUIStyle Buttony = new GUIStyle();
	public Font MyFont;

	//Stock calculations via Bloomberg
	private string jsonBloombergInput = null;
	private JSONNode bloombergParser = null;
	private float lastYearPrice = 0;
	private JSONNode thisYearPrices = null;
	private float yesterdayPrice =  0;
	private float todayPrice =  0;	
	private float dailyChange = 0;
	private float yearlyChange = 0;

	//Stock data via Edgar Online
	XmlDocument edgarXmlDoc = null;
	private string totalDebt = "null";
	private string retainedEarnings = "null";
	private string totalAssets = "null";

	//Person's bank info
	private string jsonBankInfoInput = null;
	private JSONNode bankInfoParser = null;
	private float bankBalance = 0;

	/*
	public class FieldData
	{
		public string date { get; set; }
		public double PX_LAST { get; set; }
	}
	
	public class SecurityData
	{
		public string security { get; set; }
		public List<object> eidData { get; set; }
		public int sequenceNumber { get; set; }
		public List<object> fieldExceptions { get; set; }
		public List<FieldData> fieldData { get; set; }
	}
	
	public class Datum
	{
		public SecurityData securityData { get; set; }
	}
	
	public class RootObject
	{
		public List<Datum> data { get; set; }
		public int status { get; set; }
		public string message { get; set; }
	}*/

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
			GUI.Label(lTitle, "The Coca-Cola Co (KO)",Title);
			//if(GUI.Button(
			GUI.skin.font = MyFont;

			GUI.Box(backgroundy,"", Backy);

			if(jsonBloombergInput == null)
			{
				jsonBloombergInput = new WebClient().DownloadString("http://104.131.94.146:8080/KO");
				bloombergParser = JSON.Parse (jsonBloombergInput);

				lastYearPrice = bloombergParser["data"] [0] ["securityData"] ["fieldData"] [0] ["PX_LAST"].AsFloat;
				thisYearPrices = bloombergParser["data"] [0] ["securityData"] ["fieldData"];
				yesterdayPrice =  thisYearPrices[thisYearPrices.Count-2] ["PX_LAST"].AsFloat;
				todayPrice =  thisYearPrices[thisYearPrices.Count-1] ["PX_LAST"].AsFloat;

				dailyChange = todayPrice-yesterdayPrice;
				yearlyChange = todayPrice-lastYearPrice;
			}

			/************************************************\
			/*if(edgarXmlDoc == null)
			{
				edgarXmlDoc.Load("http://edgaronline.api.mashery.com/v1/corefinancials?primarysymbols=KO&conceptGroups=BalanceSheetConsolidated&sortby=primarysymbol+asc&debug=false&appkey=x5dx58wc6mqkn6j668fnedqh");
				GUI.Label(lDailyChange, ""+edgarXmlDoc.Name, Texty);
				totalDebt = edgarXmlDoc.DocumentElement.ChildNodes[0].ChildNodes[4].ChildNodes[0].ChildNodes[2].ChildNodes[0].ChildNodes[3].ChildNodes[0].Value;

			}*/
			/************************************************\

				/*RootObject wrapper = ser.Deserialize<RootObject> (jsonInput);
			Datum d = wrapper.data;
			SecurityData s = d.securityData;
			FieldData fieldData = s.fieldData;
			var theDate = fieldData.date[fieldData.date.Length-1];*/
			//Dictionary dict = ser.Deserialize<Dictionary<string,object>>(jsonInput);
			//var postalCode = dict["fieldData"];

			//var stocks = "Stock Price : " + todayPrice;
			var stocks = "Logged in?: " + LoginMenu.isLoggedIn;
			GUI.Label (lText, stocks, Texty);
			GUI.Label(lDailyChange, "Daily Change: " + (dailyChange>0 ? System.String.Format("+{0}", dailyChange.ToString("F2")) : dailyChange.ToString("F2")), Texty);
			GUI.Label (lYearlyChange, "Yearly Change: "+ (yearlyChange>0 ? System.String.Format("+{0}", yearlyChange.ToString("F2")) : yearlyChange.ToString ("F2")), Texty);

			Buttony.fontSize = 65;
			Buttony.normal.textColor = Color.white;
			if (GUI.Button(new Rect (32, 450, 400, 80), "")) {
				Application.LoadLevel ("moreinfo");	
			}
			GUI.Label (new Rect (72, 450, 400, 80), "More Info", Buttony);

			GUI.Label(lStockAmount, System.String.Format ("Can buy {0} stocks", ""+bankBalance/todayPrice), Texty);
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