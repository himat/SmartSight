using UnityEngine;
using System.Collections;

public class AboutMenu : MonoBehaviour {

	//actually help menu
	private GUIStyle Texty = new GUIStyle();
	public Font MyFont;
	
	// Use this for initialization
	void Start () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("menu");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel("menu");
		}
	}
	void OnGUI () {
		GUI.skin.font = MyFont;
		Texty.fontSize = 65;
		Texty.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 80, Screen.height / 80 , 300, 200), "Point Camera at Target and View Information \n...", Texty);
		if (GUI.Button(new Rect (Screen.width / 2 - Screen.width/12,Screen.height/2 + Screen.height/4, 300, 200), "")) {
			Application.LoadLevel ("menu");
		}
		GUI.Label (new Rect (Screen.width / 2 - Screen.width/50 - Screen.width/30, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), "Back", Texty);
	}
}

