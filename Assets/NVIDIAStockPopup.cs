using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Net;
using SimpleJSON;

public class NVIDIAStockPopup : MonoBehaviour, ITrackableEventHandler {
	private TrackableBehaviour mTrackableBehaviour;
	private bool mShowGUIButton = false;
	private Rect lTitle = new Rect(2,50,400,100);
	private Rect lText = new Rect(2,150,300,100);
	private Rect lDailyChange = new Rect (2, 250, 300, 100);
	private Rect lYearlyChange = new Rect (2, 350, 300, 100);
	private Rect lStockAmount = new Rect (2, 600, 300, 100);	
	private Rect backgroundy = new Rect (50,200,300,500);
	private GUIStyle Title = new GUIStyle();
	private GUIStyle Texty = new GUIStyle();
	private GUIStyle Backy = new GUIStyle();
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
			GUI.Label(lTitle, "NVIDIA(NVDA)",Title);
			//if(GUI.Button(
			GUI.skin.font = MyFont;

			var stocks = "Stock Price : " + todayPrice;	//var stocks = "Logged in?: " + LoginMenu.isLoggedIn;
			var pricey = System.String.Format ("Can buy {0} stocks", ""+(LoginMenu.bankBalance/todayPrice).ToString("F2"));
			GUI.Label (lText, stocks, Texty);
			GUI.Label(lDailyChange, "Daily Change: " + (dailyChange>0 ? System.String.Format("+{0}", dailyChange.ToString("F2")) : dailyChange.ToString("F2")), Texty);
			GUI.Label (lYearlyChange, "Yearly Change: "+ (yearlyChange>0 ? System.String.Format("+{0}", yearlyChange.ToString("F2")) : yearlyChange.ToString ("F2")), Texty);

			GUI.Box(backgroundy,"", Backy);

			if(jsonBloombergInput == null)
			{
				jsonBloombergInput = new WebClient().DownloadString("http://104.131.94.146:8080/NVDA");
				bloombergParser = JSON.Parse (jsonBloombergInput);
				
				lastYearPrice = bloombergParser["data"] [0] ["securityData"] ["fieldData"] [0] ["PX_LAST"].AsFloat;
				thisYearPrices = bloombergParser["data"] [0] ["securityData"] ["fieldData"];
				yesterdayPrice =  thisYearPrices[thisYearPrices.Count-2] ["PX_LAST"].AsFloat;
				todayPrice =  thisYearPrices[thisYearPrices.Count-1] ["PX_LAST"].AsFloat;
				
				dailyChange = todayPrice-yesterdayPrice;
				yearlyChange = todayPrice-lastYearPrice;
			}

			if(LoginMenu.isLoggedIn)
				GUI.Label(lStockAmount, pricey, Texty));
			}
		/*
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("StockInfo/Coke.xml");
			XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/corefinancials/result/rowset/row/groups/group/rowset");
			string totalDebt = "", retainedEarnings = "", totalAssets="";
			foreach (XmlNode node in nodeList)
			{
				if(node.Attributes["field"].Value == "TotalDebt"){
					Debug.Log ("hi" + totalDebt);
					totalDebt = node.InnerText;
					
				}
				Debug.Log ("not a right node");
				
			};
			
			GUI.Label(lTitle, totalDebt, Title);
			*/
			
		}

}