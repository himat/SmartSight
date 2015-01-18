using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class BeerStockPopup : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour mTrackableBehaviour;
	private bool mShowGUIButton = false;
	private Rect lTitle = new Rect(2,50,400,100);
	private Rect lText = new Rect(2,150,300,100);
	private Rect backgroundy = new Rect (50,200,300,500);
	private GUIStyle Title = new GUIStyle();
	private GUIStyle Texty = new GUIStyle();
	private GUIStyle Backy = new GUIStyle();
	public Font MyFont;
	
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
			var stocks = "Stock Price : $76.46";
			
			Title.fontSize = 70;
			//Title.font = (Font)Resources.Load("Fonts/Freshman.ttf");
			Title.normal.textColor = Color.white;
			Texty.fontSize = 50;
			//Texty.font = (Font)Resources.Load("Fonts/FineCollege.ttf");
			Texty.normal.textColor = Color.white;
			GUI.Label(lTitle, "Dr Pepper Snapple (DPS)",Title);
			//if(GUI.Button(
			GUI.skin.font = MyFont;
			GUI.Label(lText, stocks, Texty);
			GUI.Box(backgroundy,"", Backy);
			
			
			
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
		Debug.Log ("if this outputs, that is good");
		
		
	}
	
}