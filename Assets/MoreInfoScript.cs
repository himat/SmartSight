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

	private Rect lPrompt = new Rect(2, 650, 300, 100);
	private Rect lInput = new Rect (2, 750, 600, 150);
	private string pickupAddress = "";
	private string dropoffAddress = "220 South 33rd Street, Philadelphia, PA";
	private bool displayQuote = false;
	private string responseString2 = null;
	private JSONNode parser2 = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("myscene");
		}

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

		string prompt = "Would you like to be delivered ";
		if (stockTicker == "KO") {
			pickupAddress = "1617 John F Kennedy Boulevard, Philadelphia, PA";
			prompt += "Coke?";
		}
		GUI.Label (lPrompt, prompt, Texty);
		dropoffAddress = GUI.TextField(lInput, dropoffAddress);
		
		Buttony.fontSize = 65;
		Buttony.normal.textColor = Color.white;
		if (GUI.Button(new Rect (Screen.width * 3/4, 650, 400, 80), "Get Quote")) {
			string username = "7cb30674-b406-474d-a453-3cc59587d89c";
			string password = "";
			string encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
			HttpWebRequest req = (HttpWebRequest) HttpWebRequest.Create ("https://api.postmates.com/v1/customers/cus_KAbLacuUuuIaTV/deliveries");
//			string[] names = new string[] {"pickup_address", "dropoff_address"};
//			string[] data = new string[] {pickupAddress, dropoffAddress};
//			byte[] postData = GetBytes(GeneratePostData(names, data));
			req.Headers.Add("Authorization", "Basic " + encoded);
//			req.ContentType = "application/x-www-form-urlencoded";
//			req.ContentLength = postData.Length;			
//			req.Method = "POST";
//			Stream dataStream = req.GetRequestStream();
//			dataStream.Write(postData, 0, postData.Length);
//			dataStream.Close ();

			HttpWebResponse res = (HttpWebResponse) req.GetResponse ();
			using (Stream stream = res.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream, new System.Text.UTF8Encoding()); 
				responseString2 = reader.ReadToEnd();
			}
			res.Close();
			parser2 = JSON.Parse(responseString2);

			displayQuote = true;
		}
		if (displayQuote) {
			GUI.Label (new Rect(Screen.width * 3/4, 450, 400, 100), "ETA: " + responseString2, Texty);
			GUI.Label (new Rect(Screen.width * 3/4, 550, 400, 100), "Cost: ", Texty);
			
			if (GUI.Button(new Rect (Screen.width * 3/4, 750, 400, 80), "Buy")) {
				
			}
		}

		Title.fontSize = 200;
		Title.normal.textColor = Color.white;
		GUI.Label(new Rect(Screen.width/2 - Screen.width/16, Screen.height/3 - Screen.height/4, 200, 100), stockTicker, Title);
	}

	static string GeneratePostData(string[] names, string[] values){
		string postData = "";
		
		for (int idx = 0; idx < names.Length; idx++) {
			postData += names[idx] + "=" + System.Text.Encoding.UTF8.GetString(System.Text.Encoding.Default.GetBytes(values[idx]));
			if (idx < names.Length - 1) {
				postData += "&";
			}
		}
		
		return postData;
	}

	static byte[] GetBytes(string str) {
		byte[] bytes = new byte[str.Length * sizeof(char)];
		System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
		return bytes;
	}
}
