using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

//[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
	
	public Animator playerAnimator;
  public static bool godMode = false;
	public static bool move;
	public static bool spotted = false;
	public static bool occupied;
	public float distance;
	public float speed;
	public GameObject player;
	public GameController controllerScript;
	public Rigidbody rigid;
  public static int steps = 0;
	public TileSystem tileSystem;
	public TileIndex current;
	public TileHighlight highlighter;
	public Vector3 temp;
	
	void Start () {
    ResetSpotted();
		playerAnimator = player.GetComponent<Animator> ();
		highlighter = player.GetComponent<TileHighlight> ();
		controllerScript = player.GetComponent<GameController> ();
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		controllerScript.start = current;
		Vector3 temp = tileSystem.WorldPositionFromTileIndex(current, true);
		temp.y = 1.1f;
		player.transform.position = temp;
		speed = 1.0f;
    EnableShadowRender();
	}
	
	void Update () {
		if(GameController.playerReady){
			if (Input.GetMouseButtonDown(0)) {
				if(GameController.gameCount == GameController.enemyCount && GameController.nextTurn == 0){
					MoveToLocation ();
				}
			}
		}
		if(move){
			player.transform.position = Vector3.MoveTowards (player.transform.position, temp, Time.deltaTime*speed);
			checkLoc ();
		}		
	}

	void checkLoc(){
		if(player.transform.position == temp){
      DisableMovement();
		}
	}
  
  // Skips the players turn and lets all enemies advance once
  public static void SkipTurn(){
    if (GameController.ActorsDoneMoving() && GameController.PlayerReady()){
      GameController.gameCount++;
    }
  }

	void MoveToLocation(){
    DisableMovement();
		temp = new Vector3 ();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		TileIndex next = tileSystem.ClosestTileIndexFromWorld (hit.point);
		move = highlighter.CheckIfValid (next);
		occupied = highlighter.CheckIfOccupied (next);
		if (CanMove() && !TileOccupied()) {
			temp = tileSystem.WorldPositionFromTileIndex (next, true);
			temp.y = 1.1f;
			GameController.gameCount++;
		}
		else{
			temp = player.transform.position;
		}
	}
	
	IEnumerator wait(){
		yield return new WaitForSeconds(1f);			
	}

  public static bool TileOccupied(){
    return occupied;
  }

  public static string StepsTaken(){
    steps = GameController.gameCount;
    return steps.ToString();
  }

  public static bool GodMode(){
    return godMode;
  }

  public static void EnableGodMode(){
    godMode = true;
  }

  public static void DisableGodMode(){
    godMode = false;
  }

  public static void ToggleGodMode(){
    if (GodMode()){
      DisableGodMode();
    } else {
      EnableGodMode();
    }
  }

  public static bool Spotted(){
    return spotted;
  }

  // The player has been seen
  public static void PlayerSpotted(){
    spotted = true;
  }

  public static void ResetSpotted(){
    spotted = false;
  }

  public static bool CanMove(){
    return move;
  }

  public static void EnableMovement(){
    move = true;
  }

  public static void DisableMovement(){
    move = false;
  }

  private void EnableShadowRender(){
		GetComponent<Renderer>().castShadows = true;
		GetComponent<Renderer>().receiveShadows = true;
  }
}
