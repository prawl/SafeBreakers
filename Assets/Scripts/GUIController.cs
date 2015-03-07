using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	private static bool isPaused = false;
	private static bool displayInventory = false;
	private static bool showMenu = false;
	private static float buttonWidth = 250;
	private static float buttonHeight = 300f;
	private static float xPos;
	private static float yPos;
	private static float yItemPos = 150;
	private static bool timer =  false;
	private static string textTimer;
	private static int restSeconds;
	private static int displaySeconds;
	private static int displayMinutes;
	private static int roundedRestSeconds;
	private static int timerWidth = 100;
	private static int timerHeight = 50;

	private static float x, y;

	public static void PauseGame(){
		isPaused = true;
	}

	public static void ResumeGame(){
		isPaused = false;
	}

	public static bool GameIsPaused(){
		return isPaused;
	}

	public static void ToggleInventory(){
	  if (InventoryActive()){
		  displayInventory = false;
		}
		else{
		  displayInventory = true;
		}
	}

	public static bool InventoryActive(){
		return displayInventory;
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

	public static bool TimerActive(){
		return timer;
	}

	public static void ActivateTimer(){
		 timer = true;
	}

	public static void HideTimer(){
		 timer = false;
	}

  public static void DisplayTimer(){
    GUIStyle style = new GUIStyle();
		style.normal.textColor = Color.white;
	  style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 25;
		roundedRestSeconds = Mathf.CeilToInt(Time.time);
		displaySeconds = roundedRestSeconds % 60;
		displayMinutes = roundedRestSeconds / 60;
		textTimer = string.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds);	
	  GUI.Label (new Rect((Screen.width-timerWidth)/2, 0, timerWidth, timerHeight), textTimer, style);
  }

  public static void DisplayInventory(){
	  GUI.Box (new Rect(0, 1, buttonWidth/2, Screen.height), "");
		if (GUI.Button (new Rect (0, yItemPos, 100, 50), "Smoke Bomb")) {
			// itemHandler.DeploySmokeBomb();
		}
		if (GUI.Button (new Rect (0, yItemPos + 60, 100, 50), "Tranq Gun")) {
			// itemHandler.DeployTraqGun();
	  }
  }

  public static void CreatePopUpMenu(GameObject[] enemies){
			GUI.backgroundColor = Color.red; // GUI set to red so you can actually see it
			if (enemies.Length > 0){
				foreach(GameObject enemy in enemies){
					Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
					xPos = screenPos.x - 250 / 2; // Center button horizontally over targets head
					yPos = -screenPos.y + Screen.height / 1.25f;
					float xSpacing = new float(); 
					xSpacing = (250 / 2) - (100 / 2);
					GUI.Button (new Rect((xPos + xSpacing), (yPos), 100, 50), "Knockout");
				}
		 }
	}				

	public static void DisplayPauseMenu(){
		xPos = (Screen.width - buttonWidth)/2;
		yPos = (Screen.height - buttonHeight)/2;
		
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
