using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	private static bool isPaused = false;
	private static bool displayInventory = false;
	private static bool showMenu = false;
	private static float boxWidth = 250f;
	private static float boxHeight = 300f;
	private static float buttonWidth = 100f;
	private static float buttonHeight = 50f;
	private static float yItemPos = 150f;
	private static float buttonXSpacing = 65f; // To center the buttons horizontally in the pause menu we need X spacing
	private static float buttonYSpacing = 70f; // To center the buttons vertically in the pause menu we need Y spacing

	public static void PauseGame(){
		isPaused = true;
	}

	public static void ResumeGame(){
		isPaused = false;
	}

	public static bool GameIsPaused(){
		return isPaused;
	}

	public static void ShowInventory(){
	  displayInventory = true;
	}

	public static void HideInventory(){
	  displayInventory = false;
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

  public static void DisplayInventory(){
	  GUI.Box (new Rect(0, 1, boxWidth/2, Screen.height), "");
		if (GUI.Button (new Rect (0, yItemPos, 100, 50), "Smoke Bomb")) {
			// itemHandler.DeploySmokeBomb();
		}
		if (GUI.Button (new Rect (0, yItemPos + 60, 100, 50), "Tranq Gun")) {
			// itemHandler.DeployTraqGun();
	  }
  }
				
	public static void DisplayPauseMenu(){
		GUI.Box (new Rect((Screen.width - boxWidth)/2, (Screen.height - boxHeight)/2, boxWidth, boxHeight), "");
		if (GUI.Button (new Rect( ((Screen.width - boxWidth)/2)+buttonYSpacing, ((Screen.height - boxHeight)/2)+buttonXSpacing, buttonWidth, buttonHeight), "Resume") || Input.GetKeyDown(KeyCode.Escape)){
			DeactivePauseMenu();
			ResumeGame();
			ResumeTime();
		}
		if (GUI.Button (new Rect( ((Screen.width - boxWidth)/2)+buttonYSpacing, ((Screen.height - boxHeight)/2)+buttonXSpacing*2, buttonWidth, buttonHeight), "Quit")) {
		}
	}
}
