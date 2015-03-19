using UnityEngine;
using System.Collections;

public class MiniGame_Door : MonoBehaviour {
	
	public Rect windowRect;
	public string stringToPrint;

	// Use this for initialization
	void Start () {
		windowRect = new Rect(Screen.width/2.25f, Screen.height/2.5f, 200, 300);
		stringToPrint = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		windowRect = GUI.Window (0, windowRect, DoMyWindow, "");
	}

	void DoMyWindow(int windowID){
		if(GUI.Button (new Rect(10, 120, 50, 50), "")){
			stringToPrint = stringToPrint + "1";
		}
		if(GUI.Button (new Rect(75, 120, 50, 50), "")){
			stringToPrint = stringToPrint + "2";
		}
		if(GUI.Button (new Rect(140, 120, 50, 50), "")){
			stringToPrint = stringToPrint + "3";
		}
		if(GUI.Button (new Rect(10, 180, 50, 50), "")){
			stringToPrint = stringToPrint + "4";
		}
		if(GUI.Button (new Rect(75, 180, 50, 50), "")){
			stringToPrint = stringToPrint + "5";
		}
		if(GUI.Button (new Rect(140, 180, 50, 50), "")){
			stringToPrint = stringToPrint + "6";
		}
		if(GUI.Button (new Rect(10, 240, 50, 50), "")){
			stringToPrint = stringToPrint + "7";
		}
		if(GUI.Button (new Rect(75, 240, 50, 50), "")){
			stringToPrint = stringToPrint + "8";
		}
		if(GUI.Button (new Rect(140, 240, 50, 50), "")){
			stringToPrint = stringToPrint + "9";
		}
		stringToPrint = GUI.TextField (new Rect(10, 10, 180, 100), stringToPrint, 25);
	}
}
