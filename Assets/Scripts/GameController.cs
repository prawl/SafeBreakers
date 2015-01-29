using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class GameController : MonoBehaviour {

	public static int gameCount = 0;
	public static int enemyCount = 0;
	public bool enemiesDone = true;
	public GameObject[] guards;
	public static int doneMoving = 0;
	public static int numEnemies;

	// Use this for initialization
	void Start () {
		guards = GameObject.FindGameObjectsWithTag ("Enemy");
		numEnemies = guards.Length;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if (gameCount > enemyCount) {
			if(doneMoving == numEnemies){
				enemyCount++;
				doneMoving = 0;
			}
		}
		if (CameraController.AbleToMoveCamera()) {
			CameraController.PanCamera();
		}

	}

	void OnGUI(){
		if (GUI.Button (new Rect(0, 0, 100, 50), "Pause")){	
			GUIController.PauseGame();
		}
		if (GUIController.GameIsPaused()) {
			GUIController.FreezeTime();
			GUIController.ActivatePauseMenu();
		}
		if (GUIController.PauseActive()) {
			GUIController.DisplayPauseMenu();
		}

	}
}
