using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Net;
using SimpleJSON;

public class LoginMenu : MonoBehaviour {
	//actually about menu
	private GUIStyle Title = new GUIStyle();
	private GUIStyle Texty = new GUIStyle();
	public Font MyFont;


	

	//Person's bank account info

	private string jsonAccountInput = null;
	private JSONNode accountParser = null;
	private string accountName = null;

	//Person's bank info
	private string jsonBankInfoInput = null;
	private JSONNode bankInfoParser = null;

	public static string enteredName = "";
	public static int bankBalance = -1;
	public static bool isLoggedIn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("menu");
				}
	}
	void OnGUI () {

		if (jsonAccountInput == null) {
			jsonAccountInput = new WebClient().DownloadString("http://api.reimaginebanking.com/customers/54b604dfa520e02948a0f45d?key=CUST993aa30727255ae56bf9447b45dbfc39");
			accountParser = JSON.Parse (jsonAccountInput);
			
			accountName = accountParser["first name"].Value + " " + accountParser["last name"].Value;
			}

		GUI.skin.font = MyFont;
		Texty.fontSize = 65;
		Texty.normal.textColor = Color.white;
		Title.fontSize = 170;
		Title.normal.textColor = Color.white;

		GUI.Label (new Rect (Screen.width / 80, Screen.height / 80 , 100, 200), "Capital One Login", Title);
		GUI.Label (new Rect (Screen.width / 80, Screen.height / 5 + Screen.height / 20, 100, 200), "Please Enter Your  \nFirst and Last Name:", Texty);
		enteredName = GUI.TextField(new Rect (Screen.width / 80, Screen.height / 4 + Screen.height / 8, 600, 150), enteredName, 30);

		if (GUI.Button(new Rect (0 , Screen.height/3 *2, 300, 200), "1")) {
			
			if(enteredName == accountName)
				isLoggedIn = true;

			if(isLoggedIn)	
			{
				jsonBankInfoInput = new WebClient().DownloadString("http://api.reimaginebanking.com/customers/54b604dfa520e02948a0f45d/accounts?key=CUST993aa30727255ae56bf9447b45dbfc39");
				bankInfoParser = JSON.Parse (jsonBankInfoInput);
				
				bankBalance = bankInfoParser[0]["balance"].AsInt;
				Application.LoadLevel ("menu");
			}
			else
				Debug.Log (accountName + " " + enteredName);
				GUI.Label (new Rect (Screen.width / 2 - Screen.width/50 - Screen.width/30, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), accountName + "::" + enteredName, Texty);

		}
		if (GUI.Button(new Rect (Screen.width/2 - Screen.width /3,Screen.height/3 *2, 300, 200), "2")) {
			Application.LoadLevel ("menu");
		}

		GUI.Label (new Rect (Screen.width/2 - Screen.width /3 + Screen.width/50, Screen.height/3 *2 + Screen.height/15, 200, 100), "Back", Texty);
		GUI.Label (new Rect (Screen.width/90, Screen.height/3 *2 + Screen.height/15, 200, 100), "Confirm", Texty);
	}
}

/*using UnityEngine;
using System.Collections;

public class LoginScript : MonoBehaviour {
	private GUIStyle Texty = new GUIStyle();
	public Font MyFont;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () { 
		GUI.skin.font = MyFont;
		Texty.fontSize = 65;
		Texty.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 80, Screen.height / 80 , 100, 200), "Capital One account first and last name:", Texty);
		if (GUI.Button(new Rect (Screen.width / 2 - Screen.width/12,Screen.height/2 + Screen.height/4, 300, 200), "")) {
			Application.LoadLevel ("menu");
		}
		//GUI.TextField (new Rect (Screen.width / 80 + 100, Screen.height / 80, 400, 200), "First and last name");
	}
}
*/