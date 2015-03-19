using UnityEngine;
using System.Collections;

public class Door_GUIController : Door_MiniGameController {
	
	public Rect windowRect;
	public string stringToPrint;

	public Door_MiniGameController controller;

	
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

	Color GetColor(int buttonNum){
		Color buttonColor;
		if(controller.buttons[buttonNum-1].valid){
			buttonColor = Color.blue;
			return buttonColor;
		}
		if(controller.buttons[buttonNum-1].selected){
			buttonColor = Color.green;
			return buttonColor;
		}
		if(controller.buttons[buttonNum-1].wrong){
			buttonColor = Color.red;
			return buttonColor;
		}
		else{
			buttonColor = Color.gray;
			return buttonColor;
		}
	}
	
	void DoMyWindow(int windowID){
		GUI.color = GetColor (1);
		if(GUI.Button (new Rect(10, 120, 50, 50), "")){
			if(controller.buttons[0].valid){
				controller.buttons[0].selected = true;
				controller.buttons[0].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[0].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		GUI.color = GetColor (2);
		if(GUI.Button (new Rect(75, 120, 50, 50), "")){
			if(controller.buttons[1].valid){
				controller.buttons[1].selected = true;
				controller.buttons[1].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[1].wrong = true;
				controller.alarmTriggered = true;				
			}
		}
		GUI.color = GetColor (3);
		if(GUI.Button (new Rect(140, 120, 50, 50), "")){
			if(controller.buttons[2].valid){
				controller.buttons[2].selected = true;
				controller.buttons[2].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[2].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		GUI.color = GetColor (4);
		if(GUI.Button (new Rect(10, 180, 50, 50), "")){
			if(controller.buttons[3].valid){
				controller.buttons[3].selected = true;
				controller.buttons[3].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[3].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		GUI.color = GetColor (5);
		if(GUI.Button (new Rect(75, 180, 50, 50), "")){
			if(controller.buttons[4].valid){
				controller.buttons[4].selected = true;
				controller.buttons[4].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[4].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		GUI.color = GetColor (6);
		if(GUI.Button (new Rect(140, 180, 50, 50), "")){
			if(controller.buttons[5].valid){
				controller.buttons[5].selected = true;
				controller.buttons[5].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[5].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		GUI.color = GetColor (7);
		if(GUI.Button (new Rect(10, 240, 50, 50), "")){
			if(controller.buttons[6].valid){
				controller.buttons[6].selected = true;
				controller.buttons[6].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[6].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		GUI.color = GetColor (8);
		if(GUI.Button (new Rect(75, 240, 50, 50), "")){
			if(controller.buttons[7].valid){
				controller.buttons[7].selected = true;
				controller.buttons[7].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[7].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		GUI.color = GetColor (9);
		if(GUI.Button (new Rect(140, 240, 50, 50), "")){
			if(controller.buttons[8].valid){
				controller.buttons[8].selected = true;
				controller.buttons[8].valid = false;
				controller.arrayProgression++;
			}
			else{
				controller.buttons[8].wrong = true;
				controller.alarmTriggered = true;
			}
		}
		stringToPrint = GUI.TextField (new Rect(10, 10, 180, 100), stringToPrint, 25);
	}
}
