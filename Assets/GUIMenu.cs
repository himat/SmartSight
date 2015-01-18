using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour {
	private GUIStyle Title = new GUIStyle();
	private GUIStyle Buttony = new GUIStyle();
	public Font MyFont;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.skin.font = MyFont;
		Buttony.fontSize = 65;
		Buttony.normal.textColor = Color.white;

		if (GUI.Button(new Rect (Screen.width/6,Screen.height/2 + Screen.height/4, 300, 200), "")) {
					Application.LoadLevel ("help");	
				}

		if (GUI.Button(new Rect (Screen.width / 2 - Screen.width/12,Screen.height/2 + Screen.height/4, 300, 200), "")) {
			Application.LoadLevel ("myscene");
		}


		if (GUI.Button(new Rect (Screen.width - Screen.width/3,Screen.height/2 + Screen.height/4, 300, 200), "")) {
			Application.LoadLevel ("about");
		}

		GUI.Label (new Rect (Screen.width / 5 - Screen.width/80, Screen.height / 2 + Screen.height/4 + Screen.height/15, 300, 200), "About", Buttony);
		GUI.Label (new Rect (Screen.width / 2 - Screen.width/50 - Screen.width/30, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), "Start", Buttony);
		GUI.Label (new Rect (Screen.width - Screen.width/3 + Screen.width/24 - Screen.width/80, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), "Help", Buttony);
		Title.fontSize = 200;
		//Title.font = (Font)Resources.Load("Fonts/Freshman.ttf");
		Title.normal.textColor = Color.white;
		GUI.Label(new Rect(Screen.width/2 - Screen.width/4 - Screen.width/25, Screen.height/2 - Screen.height/4, 200, 100), "Smart Eye",Title);
	}
}
