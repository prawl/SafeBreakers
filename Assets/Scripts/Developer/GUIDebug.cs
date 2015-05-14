using UnityEngine;
using System.Collections;

public class GUIDebug : MonoBehaviour {

  private bool visible = false;
	private float buttonHeightSpacing = 35f;
	private float buttonWidth = 400f;
	private float buttonHeight = 35f;
	private float boxWidth = 400f;
	private float boxHeight = 200f;
	private float boxHeightSpacing = 200f;
	private float xPos;
	private float yPos;
  private float debugBoxHeight = 165f;
  private float[] coords;
	private static int roundedRestSeconds;
	private static int displaySeconds;
	private static int displayMinutes;
	private static string debugText;
  private Texture2D godModeImage;
  private Texture2D skipTurnImage;

  void Start(){
    godModeImage = Resources.Load("god_mode_image") as Texture2D; //Looks in Resources folder to images to load
    skipTurnImage = Resources.Load("skip_turn_image") as Texture2D; //Looks in Resources folder to images to load
  }

  void OnGUI(){
    if(DebugMenuVisibile()){
      DisplayDebugMenu();
      GameController.ShowNPCInteractions();
    }

    coords = SetCoords(0, Screen.height - buttonHeightSpacing);
    if (GUI.Button(new Rect(coords[0], coords[1], buttonWidth, buttonHeight), "Debug")){
      ToggleDebugMenu();
    }
  }

  public void DisplayDebugMenu(){
    coords = SetCoords(0, Screen.height - boxHeightSpacing);
    GUI.Box(new Rect(coords[0], coords[1], boxWidth, boxHeight), "");
		GUI.TextField (new Rect(coords[0], coords[1], boxWidth, debugBoxHeight), DebugText(), 250);
    GUI.backgroundColor = new Color(0,0,0,0);
    coords = SetCoords( Screen.width - (Screen.width /3), 0);
    if(GUI.Button(new Rect(coords[0], coords[1], 35, 35), godModeImage)){
      PlayerController.ToggleGodMode();
    }
    coords = SetCoords(Screen.width - (Screen.width /3) - 50, 0);
    if(GUI.Button(new Rect(coords[0], coords[1], 35, 35), skipTurnImage)){
      PlayerController.SkipTurn();
    }
  }

  public bool DebugMenuVisibile(){
    return visible;
  }

  public void ToggleDebugMenu(){
    if(DebugMenuVisibile()){
      visible = false;
    } else {
      visible = true;
    }
  }

  private string DebugText(){
		roundedRestSeconds = Mathf.CeilToInt(Time.time);
		displaySeconds = roundedRestSeconds % 60;
		displayMinutes = roundedRestSeconds / 60;
		debugText = string.Format ("Timer: {0:00}:{1:00}", displayMinutes, displaySeconds);	
		debugText += "\n\n";
		debugText += "Steps Taken: " + PlayerController.StepsTaken();	
		debugText += "\n\n";
		debugText += "Currency: " + InventoryController.DisplayCurrency();	
		debugText += "\n\n";
		debugText += "God Mode: " + PlayerController.GodMode();	
    return debugText;
  }

  // Makes setting GUI objects x and y coordinates more efficient
  private float[] SetCoords(float xPos, float yPos){
    float[] coords = new float[] { xPos, yPos };
    return coords;
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
