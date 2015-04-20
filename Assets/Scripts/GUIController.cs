using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	private static bool timer =  false;
	private static bool purchase = false;
	private static bool isPaused = false;
	private static bool displayInventory = false;
	private static bool showMenu = false;
	private static float buttonWidth = 250;
	private static float buttonHeight = 300f;
	private static float purchaseBoxWidth = 500;
	private static float purchaseBoxHeight = 300;
	private static float xPos;
	private static float yPos;
	private static float yItemPos = 150;
	private static float x, y;
	private static int timerWidth = 100;
	private static int timerHeight = 50;
	private static string textCurrency;

  void OnGUI(){
		if (GUI.Button (new Rect(0, 0, 100, 50), "Pause")){
			PauseGame();
			HideInventory();
			HidePurchase();
		}
		if (PauseActive()){
			GUI.enabled = false; // When paused, disable Backpack and Shop GUI buttons (grayed-out) 
		}
		if (GUI.Button (new Rect(0, 55, 100, 50), "Backpack")){	
			ToggleInventory();
		}
		if (GUI.Button (new Rect(Screen.width-100, 0, 100, 50), "Shop")){	
			TogglePurchaseWindow();
		}
		GUI.enabled = true;
    if (GameIsPaused()) {
			FreezeTime();
			ActivatePauseMenu();
		}
		if (PauseActive()) {
			DisplayPauseMenu();
		}
		if (InventoryActive()){
		  DisplayInventory();
		}
    if (PurchaseActive()){
      DisplayPurchaseWindow();
    }
  }

  public static void TogglePurchaseWindow(){
    if (PurchaseActive()){
			HidePurchase();
			ResumeGame();
			ResumeTime();
    } else {
			ActivatePurchase();
			FreezeTime();
    }
  }

	public static void ToggleInventory(){
	  if (InventoryActive()){
			HideInventory();
		} else {
			ShowInventory();
		}
	}

	public static bool GameIsPaused(){
		return isPaused;
	}

	public static void PauseGame(){
		isPaused = true;
	}

	public static void ResumeGame(){
		isPaused = false;
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

	public static void HidePurchase(){
		 purchase = false;
	}

	public static void ActivatePurchase(){
		 purchase = true;
	}

	public static bool PurchaseActive(){
		return purchase;
	}

  public static void DisplayInventory(){
	  GUI.Box (new Rect(0, 1, buttonWidth/2, Screen.height/2), "");
		if (GUI.Button (new Rect (0, yItemPos, 100, 50), "Smoke Bomb")) {
		}
		if (GUI.Button (new Rect (0, yItemPos + 60, 100, 50), "Tranq Gun")) {
	  }
  }

  public static void CreatePopUpMenu(GameObject[] enemies){
			GUI.backgroundColor = Color.red; // GUI set to red so you can actually see it
			if (enemies.Length > 0){
				foreach(GameObject enemy in enemies){
          if (enemy != null){
            Vector3 screenPos = Camera.main.WorldToScreenPoint(enemy.transform.position);
            xPos = screenPos.x - 250 / 2; // Center button horizontally over targets head
            yPos = -screenPos.y + Screen.height / 1.25f;
            float xSpacing = new float();
            xSpacing = (250 / 2) - (100 / 2);
            if (GUI.Button (new Rect((xPos + xSpacing), (yPos), 100, 50), "Knockout")) {
              Destroy(enemy);
            }
          }
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

  public static void DisplayPurchaseWindow(){
    FreezeTime();
		xPos = (Screen.width - purchaseBoxWidth)/2;
		yPos = (Screen.height - purchaseBoxHeight)/2;

    textCurrency = string.Format("Currency: {0}", InventoryController.DisplayCurrency());
    GUI.Box(new Rect(xPos, yPos, purchaseBoxWidth, purchaseBoxHeight), "");
    GUI.Label(new Rect(xPos + 70, yPos + 15, 100, 50), textCurrency);
    GUI.Label(new Rect(xPos + 230, yPos + 15, 100, 50), "Purchase ");
    GUI.Button(new Rect(xPos + 70, yPos + 50, 100, 50), "Item 1");
    GUI.Button(new Rect(xPos + 210, yPos + 50, 100, 50), "Item 2");
    GUI.Button(new Rect(xPos + 360, yPos + 50, 100, 50), "Item 3");
    GUI.Button(new Rect(xPos + 70, yPos + 125, 100, 50), "Item 4");
    GUI.Button(new Rect(xPos + 210, yPos + 125, 100, 50), "Item 5");
    GUI.Button(new Rect(xPos + 360, yPos + 125, 100, 50), "Item 6");
    GUI.Button(new Rect(xPos + 70, yPos + 200, 100, 50), "Item 7");
    GUI.Button(new Rect(xPos + 210, yPos + 200, 100, 50), "Item 8");
    GUI.Button(new Rect(xPos + 360, yPos + 200, 100, 50), "Item 9");
  }
  // Use this in FixedUpdate method to see more detailed info about what you're currently clicking on
	void ClickInfoDebug(){
	 if (Input.GetMouseButtonDown (0)) {
		 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		 RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					 Debug.Log ("Name = " + hit.collider.name);
					 Debug.Log ("Tag = " + hit.collider.tag);
					 Debug.Log ("Hit Point = " + hit.point);
					 Debug.Log ("Object position = " + hit.collider.gameObject.transform.position);
					 Debug.Log ("--------------");
				}
		 }	
	}
}
