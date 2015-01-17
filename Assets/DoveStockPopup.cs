using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class DoveStockPopup : MonoBehaviour, ITrackableEventHandler {
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
			var stocks = "Stock Price : $41.78";
			
			Title.fontSize = 70;
			//Title.font = (Font)Resources.Load("Fonts/Freshman.ttf");
			Title.normal.textColor = Color.white;
			Texty.fontSize = 50;
			//Texty.font = (Font)Resources.Load("Fonts/FineCollege.ttf");
			Texty.normal.textColor = Color.white;
			GUI.Label(lTitle, "Unilever(UL)",Title);
			//if(GUI.Button(
			GUI.skin.font = MyFont;
			GUI.Label(lText, stocks, Texty);
			GUI.Box(backgroundy,"", Backy);
			
			
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
			
			
		}
	}
}