﻿using UnityEngine;
using System.Collections;

public class CokeStockPopup : MonoBehaviour, ITrackableEventHandler {
	
	private TrackableBehaviour mTrackableBehaviour;
	private bool mShowGUIButton = false;
	private Rect lTitle = new Rect(50,50,400,100);
	private Rect lText = new Rect(50,100,300,100);
	private GUIStyle Title = new GUIStyle();
	
	
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
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
				
			Title.fontSize = 100;
			Title.font = (Font)Resources.Load("Fonts/Freshman");
			Title.normal.textColor = Color.white;
			GUI.Label(lTitle, "34.5",Title);
		}
	}
}