using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class GameController : MonoBehaviour {

	public static int gameCount, enemyCount, doneMoving, nextTurn, numEnemies = 0;
	public bool enemiesDone = true;
	public static GameObject[] guards;
	public TileIndex start, end;
	public Brush endTile;
	public TileSystem tileSystem;
	public GameObject player;
	public static bool restart, playerReady;
	public string scene;
	public static bool interactiveWindowOn;

	void Start () {
		restart = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		guards = GameObject.FindGameObjectsWithTag ("Enemy");
		numEnemies = guards.Length;
		interactiveWindowOn = false;
    	InventoryController.ResetCurrency();
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
