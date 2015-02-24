using UnityEngine;
using UnityEditor;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class GameController : MonoBehaviour {

	public static int gameCount = 0;
	public static int enemyCount = 0;
	public bool enemiesDone = true;
	public GameObject[] guards;
	public static int doneMoving = 0;
	public static int nextTurn = 0;
	public static int numEnemies;
	public TileIndex start;
	public TileIndex end;
	public Brush endTile;
	public TileSystem tileSystem;
	public GameObject player;
	public static bool restart;
	public string scene;
	public static bool playerReady;

	// Use this for initialization
	void Start () {
		restart = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		guards = GameObject.FindGameObjectsWithTag ("Enemy");
		numEnemies = guards.Length;
	}

	void PaintEnd(){
		for(int row = 0; row < tileSystem.RowCount; row++){
			for(int column = 0; column<tileSystem.ColumnCount; column++){
				if(int.Parse (end.row.ToString ()) == row && int.Parse (end.column.ToString ()) == column){
					endTile.Paint (tileSystem, row, column);
				}
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate() {
		PaintEnd ();
		bool spotted = CheckIfSpotted ();
		if(spotted){
			RestartGame ();
		}
		if (gameCount > enemyCount) {
			if(doneMoving == numEnemies){
				enemyCount++;
				doneMoving = 0;
			}
		}
		if (CameraController.AbleToMoveCamera()) {
			CameraController.PanCamera();
		}

		if (nextTurn == numEnemies) {
			nextTurn = 0;
		}
		CheckIfMoving ();
	}

	void CheckIfMoving(){
		playerReady = true;
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = guards[i];
			EnemyGuard tempScript = temp.GetComponent<EnemyGuard>();
			if(tempScript.moving){
				playerReady = false;
			}
		}
	}

	bool CheckIfSpotted(){
		return PlayerController.spotted;
	}

	void RestartGame(){
		if (gameCount == enemyCount) {
			Application.LoadLevel (scene);
		}
	}

	void OnGUI(){
	  GUIController.DisplayTimer();

		if (GUI.Button (new Rect(0, 0, 100, 50), "Pause")){
			GUIController.PauseGame();
		}
    if (GUI.Button (new Rect(0, 55, 100, 50), "Backpack")){	
	  	GUIController.ToggleInventory();
		}
  	if (GUIController.GameIsPaused()) {
			GUIController.FreezeTime();
			GUIController.ActivatePauseMenu();
		}
		if (GUIController.PauseActive()) {
			GUIController.DisplayPauseMenu();
		}
		if (GUIController.InventoryActive()){
		  GUIController.DisplayInventory();
			GUIController.CreatePopUpMenu(guards);
		}
	}
}
