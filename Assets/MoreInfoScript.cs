using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using SimpleJSON;

public class MoreInfoScript : MonoBehaviour {
	private GUIStyle Title = new GUIStyle();
	private GUIStyle Texty = new GUIStyle();
	private GUIStyle Buttony = new GUIStyle();
	public Font MyFont;

	public static string stockTicker = "";
	public static JSONNode stockPrices = null;

	private string responseString = null;
	private JSONNode parser = null;
	private string totalDebt = "null";
	private string retainedEarnings = "null";
	private string totalAssets = "null";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin.font = MyFont;

		if (responseString == null) {
			HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create ("http://edgaronline.api.mashery.com/v1/corefinancials?primarysymbols="+stockTicker+"&conceptGroups=BalanceSheetConsolidated&sortby=primarysymbol+asc&debug=false&appkey=x5dx58wc6mqkn6j668fnedqh");
			req.Accept = "application/json";
			HttpWebResponse res = (HttpWebResponse) req.GetResponse ();
			using (Stream stream = res.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream, new System.Text.UTF8Encoding()); 
				responseString = reader.ReadToEnd();
			}
			res.Close();
			parser = JSON.Parse(responseString);

			JSONNode groups = parser["result"]["rowset"][0]["groups"];
			JSONNode tD = groups[0]["rowset"][0];
			JSONNode rE = groups[0]["rowset"][22];
			JSONNode tA = groups[0]["rowset"][23];

			totalDebt = tD["value"].AsDouble.ToString();
			retainedEarnings = rE["value"].AsDouble.ToString();
			totalAssets = tA["value"].AsDouble.ToString();
		}

//		Buttony.fontSize = 65;
//		Buttony.normal.textColor = Color.white;
//		
//		if (GUI.Button(new Rect (Screen.width/6,Screen.height/2 + Screen.height/4, 300, 200), "")) {
//			Application.LoadLevel ("help");	
//		}
//		
//		if (GUI.Button(new Rect (Screen.width / 2 - Screen.width/12,Screen.height/2 + Screen.height/4, 300, 200), "")) {
//			Application.LoadLevel ("myscene");
//		}
//		
//		
//		if (GUI.Button(new Rect (Screen.width - Screen.width/3,Screen.height/2 + Screen.height/4, 300, 200), "")) {
//			Application.LoadLevel ("about");
//		}
//		
//		GUI.Label (new Rect (Screen.width / 5 - Screen.width/80, Screen.height / 2 + Screen.height/4 + Screen.height/15, 300, 200), "About", Buttony);
//		GUI.Label (new Rect (Screen.width / 2 - Screen.width/50 - Screen.width/30, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), "Start", Buttony);
//		GUI.Label (new Rect (Screen.width - Screen.width/3 + Screen.width/24 - Screen.width/80, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), "Help", Buttony);
		Title.fontSize = 200;
		//Title.font = (Font)Resources.Load("Fonts/Freshman.ttf");
		Title.normal.textColor = Color.white;
		GUI.Label(new Rect(Screen.width/2 - Screen.width/4, Screen.height/2 - Screen.height/4, 200, 100), stockTicker, Title);
	}
}
