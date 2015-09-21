using UnityEngine;
using System.Collections;

public class LevelLost : MonoBehaviour {

	public Rect windowRect;
	private New_GameController gameCon;

	// Use this for initialization
	void Start () {
		gameCon = gameObject.GetComponent<New_GameController> ();
		windowRect = new Rect(Screen.width/2.25f, Screen.height/2.5f, 200, 300);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameCon.levelLost) {
			gameCon.levelPaused = true;
		}
	}

	void OnGUI(){
		if(gameCon.levelLost){
			windowRect = GUI.Window (0, windowRect, DoMyWindow, "");
		}
	}

	void DoMyWindow(int windowID){
		if (GUI.Button (new Rect (10, 120, 50, 50), "Retry?")) {
			gameCon.levelLost = false;
			gameCon.levelPaused = false;
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}
