﻿using UnityEngine;
using System.Collections;

public class HelpMenu : MonoBehaviour {
	//actually about menu
	private GUIStyle Texty = new GUIStyle();
	public Font MyFont;

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
		GUI.skin.font = MyFont;
		Texty.fontSize = 65;
		Texty.normal.textColor = Color.white;
		GUI.Label (new Rect (Screen.width / 80, Screen.height / 80 , 300, 200), "This is a app devolped during the Winter PennApps \n2015. The app reads the real-time life feed from \nthe android camera and analyzes it \nto recongize which company the product was \nmanufactured by. The app then lists various \ninformation such as stock prices, financial \ninformation, ordering details, etc. \n\nThe a Smart Sight app utilizes varius APIs such as \n     -Bloomberg API\n     -Capital One API\n     -Mashery(Edgar API)\n     -PostMates API\n     -Digital Ocean API", Texty);
		if (GUI.Button(new Rect (Screen.width / 2 - Screen.width/12,Screen.height/2 + Screen.height/4, 300, 200), "")) {
			Application.LoadLevel ("menu");
		}
		GUI.Label (new Rect (Screen.width / 2 - Screen.width/50 - Screen.width/30, Screen.height / 2 + Screen.height/4 + Screen.height/15, 200, 100), "Back", Texty);
	}
}
