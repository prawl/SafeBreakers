using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	private bool isPaused = false;
	private bool showMenu = false;
	private float screenHeight;
	private float screenWidth;
	private float buttonWidth = 250;
	private float buttonHeight = 300f;
	public float xPos;  // Determines horizonatally where the box will start 
	public float yPos;  // Determines vertically where the box will start
	
	// Use this for initialization
	void Start () {
		xPos = (Screen.width - buttonWidth) / 2;
		yPos = (Screen.height - buttonHeight) / 2;
	}

	void OnGUI(){
		if (GUI.Button (new Rect(0, 0, 100, 50), "Pause")){	
			PauseGame();
		}
		if (GameIsPaused()) {
			FreezeTime();
			ActivatePauseMenu();
		}
		if (PauseActive()) {
			DisplayPauseMenu();
		}
	}

	public void PauseGame(){
		isPaused = true;
	}

	public void ResumeGame(){
		isPaused = false;
	}

	public bool GameIsPaused(){
		return isPaused;
	}

	public void FreezeTime(){
		Time.timeScale = 0;
	}

	public void ResumeTime(){
		Time.timeScale = 1;
	}

	public void ActivatePauseMenu(){
		showMenu = true;
	}

	public void DeactivePauseMenu(){
		showMenu = false;
	}

	public bool PauseActive(){
		return showMenu;
	}

	private void DisplayPauseMenu(){
		GUI.Box (new Rect(xPos, yPos, buttonWidth, buttonHeight), "");
		if (GUI.Button (new Rect( (xPos + 70) , (yPos + 65), 100, 50), "Resume") || Input.GetKeyDown(KeyCode.Escape)){
			DeactivePauseMenu();
			ResumeGame();
			ResumeTime();
		}
		if (GUI.Button (new Rect ((xPos + 70), (yPos + 130), 100, 50), "Quit")) {
		}
	}
}
