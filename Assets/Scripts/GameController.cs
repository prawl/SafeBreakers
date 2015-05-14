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
	public static int numEnemies;
	public string scene;
	public TileIndex start;
	public TileIndex end;
	public TileSystem tileSystem;

	void Start () {
    player = GameObject.FindGameObjectWithTag ("Player");
    InventoryController.ResetCurrency();
    CameraController.SetCameraFocus("Player");
	}

	void FixedUpdate() {
    GetNumberOfGuards();
		PaintEnd ();
		if(PlayerController.Spotted() && !PlayerController.GodMode()){
			RestartGame ();
		}
		if (EnemiesReadyToMove()) {
      if (doneMoving == numEnemies){
        MoveEnemies();
      }
		}
		if (CameraController.AbleToMoveCamera()) {
      EnablePlayerReady();
			CameraController.PanCamera();
		}
		CheckIfMoving ();
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

  void MoveEnemies(){
    enemyCount++;
    doneMoving = 0;
  }

  public static bool EnemiesReadyToMove(){
    if (gameCount > enemyCount){
      return true;
    } else {
      return false;
    }
  }

  public static bool ActorsDoneMoving(){
    if (gameCount == enemyCount){
      return true;
    } else {
      return false;
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
		guards = GameObject.FindGameObjectsWithTag ("Enemy");
		numEnemies = guards.Length;
  }

  public static int GetGameCount(){
    return gameCount;
  }
}
