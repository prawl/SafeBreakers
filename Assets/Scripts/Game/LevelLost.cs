using UnityEngine;
using System.Collections;

public class LevelLost : MonoBehaviour {

	public Rect windowRect;

	// Use this for initialization
	void Start () {
		windowRect = new Rect(Screen.width/2.25f, Screen.height/2.5f, 200, 300);
	}
	
	// Update is called once per frame
	void Update () {
		if (New_GameController.levelLost) {
			Time.timeScale = 0.0f;
			print ("Gotcha bitch");
		}
	}

	void OnGUI(){
		if(New_GameController.levelLost){
			windowRect = GUI.Window (0, windowRect, DoMyWindow, "");
		}
	}

	void DoMyWindow(int windowID){
		if (GUI.Button (new Rect (10, 120, 50, 50), "Retry?")) {
			New_GameController.Restart ();
		}
	}
}
