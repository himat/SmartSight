using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Net;
using SimpleJSON;

public class LoginMenu : MonoBehaviour {
	//actually about menu
	private GUIStyle Texty = new GUIStyle();
	public Font MyFont;

	string name = "";

	//Person's bank info
	private string jsonAccountInput = null;
	private JSONNode accountParser = null;
	private string accountName = null;

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

		name = GUI.TextField (new Rect (Screen.width / 80 + 100, Screen.height / 50, 400, 200), "First and last name");


		if (GUI.Button(new Rect (Screen.width / 80 +500, Screen.height/350, 300, 200), "Confirm")) {
			Application.LoadLevel ("menu");
		}
		if (GUI.Button(new Rect (Screen.width / 2 - Screen.width/12,Screen.height/2 + Screen.height/4, 300, 200), "")) {
			Application.LoadLevel ("menu");
		}

		GUI.Label (new Rect (Screen.width / 2 - Screen.width/50 - Screen.width/30, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), "Back", Texty);
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