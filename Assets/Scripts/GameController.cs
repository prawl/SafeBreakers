using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class GameController : MonoBehaviour {

	public static bool playerReady;
	public static bool interactiveWindowOn = false;
	public Brush endTile;
	public static GameObject[] guards;
	public GameObject player;
	public static int gameCount = 0;
	public static int enemyCount = 0;
	public static int doneMoving = 0;
	public static int nextTurn = 0;
	public static int numEnemies;
	public string scene;
	public TileIndex start;
	public TileIndex end;
	public TileSystem tileSystem;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		guards = GameObject.FindGameObjectsWithTag ("Enemy");
    GetNumberOfGuards();
    InventoryController.ResetCurrency();
    CameraController.SetCameraFocus("Player");
	}

	void FixedUpdate() {
		PaintEnd ();
		if(PlayerController.Spotted() && !PlayerController.GodMode()){
			RestartGame ();
		}

		if (gameCount > enemyCount) {
			if(doneMoving == numEnemies){
        // Count enemy movement once they're done moving
				enemyCount++;
				doneMoving = 0;
			}
		}
		if (CameraController.AbleToMoveCamera()) {
      EnablePlayerReady();
			CameraController.PanCamera();
		}
		if (nextTurn == numEnemies) {
			nextTurn = 0;
		}
		CheckIfMoving ();
	}

  // Paint the end tile
	void PaintEnd(){
		for(int row = 0; row < tileSystem.RowCount; row++){
			for(int column = 0; column<tileSystem.ColumnCount; column++){
				if(int.Parse (end.row.ToString ()) == row && int.Parse (end.column.ToString ()) == column){
					endTile.Paint (tileSystem, row, column);
				}
			}
		}
	}

	void CheckIfMoving(){
    EnablePlayerReady();
		for(int i = 0; i < numEnemies; i++){
			GameObject temp = guards[i];
      if (temp != null) {
        EnemyGuard tempScript = temp.GetComponent<EnemyGuard>();
        if(tempScript.moving){
          DisablePlayerReady();
        }
      }
		}
	}

	void RestartGame(){
		if (gameCount == enemyCount) {
			Application.LoadLevel (scene);
		}
	}

  public static void ShowNPCInteractions(){
    GUIController.CreatePopUpMenu(guards);
  }

  public static void EnablePlayerReady(){
    playerReady = true;
  }

  public static void DisablePlayerReady(){
    playerReady = false;
  }

  public static bool PlayerReady(){
    return playerReady;
  }

  void GetNumberOfGuards(){
		numEnemies = guards.Length;
  }
}
