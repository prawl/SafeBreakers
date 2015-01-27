using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	private static bool isPaused = false;
	private static bool showMenu = false;
	private static float screenHeight;
	private static float screenWidth;
	private static float buttonWidth = 250;
	private static float buttonHeight = 300f;
	// Determines horizonatally where the box will start 
	private static float xPos = (Screen.width - buttonWidth) / 2;  
	// Determines vertically where the box will start
	private static float yPos  = (Screen.height - buttonWidth) / 2;  

	public static void PauseGame(){
		isPaused = true;
	}

	public static void ResumeGame(){
		isPaused = false;
	}

	public static bool GameIsPaused(){
		return isPaused;
	}

	public static void FreezeTime(){
		Time.timeScale = 0;
	}

	public static void ResumeTime(){
		Time.timeScale = 1;
	}

	public static void ActivatePauseMenu(){
		showMenu = true;
	}

	public static void DeactivePauseMenu(){
		showMenu = false;
	}

	public static bool PauseActive(){
		return showMenu;
	}

	public static void DisplayPauseMenu(){
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
