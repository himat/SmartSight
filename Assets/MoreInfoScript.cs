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

	private Rect lTD = new Rect(2, 300, 300, 100);
	private Rect lRE = new Rect(2, 400, 300, 100);
	private Rect lTA = new Rect(2, 500, 300, 100);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin.font = MyFont;
		Texty.fontSize = 50;
		Texty.normal.textColor = Color.white;

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
		GUI.Label (lTD, "Total Debt: " + totalDebt, Texty);
		GUI.Label (lRE, "Retained Earnings: " + retainedEarnings, Texty);
		GUI.Label (lTA, "Total Assets: " + totalAssets, Texty);

		Title.fontSize = 200;
		Title.normal.textColor = Color.white;
		GUI.Label(new Rect(Screen.width/2 - Screen.width/16, Screen.height/3 - Screen.height/4, 200, 100), stockTicker, Title);
	}
}
