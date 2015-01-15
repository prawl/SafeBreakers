using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class EnemyGuard : MonoBehaviour {
	
	public GameObject enemy;
	public TileSystem tileSystem;
	public TileIndex start;
	public TileIndex currentTile;
	public Vector3 currentLoc;
	public TileIndex end;
	public float speed;
	public bool to;
	public bool from;
	public bool up;
	public bool down;
	public bool left;
	public bool right;
	public bool horizontal;
	public bool vertical;
	private Animator enemyAnimator = null;
	private bool firstMove;
	public  bool moving;
	
	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponent<Animator>();
		start = tileSystem.ClosestTileIndexFromWorld (enemy.transform.position);
		currentTile = start;
		currentLoc = enemy.transform.position;
		speed = 1.0f;
		StartDirection ();
		to = true;
		from = false;
		firstMove = true;
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.enemyCount < GameController.gameCount){
			if(GameController.doneMoving < GameController.numEnemies){
				GameController.doneMoving++;
			}
			print ("1");
			CheckDirection ();
			GetNextTile();
			firstMove = false;
			/*else{
				print ("2");
				if(GameController.doneMoving < GameController.numEnemies){
					GameController.doneMoving++;
				}
				CheckDirection ();
				GetNextTile ();
			}*/
		}
		if (enemy.transform.position != currentLoc) {
			MoveToLocation (currentTile, end);
		}

		if (enemy.transform.position == currentLoc) {
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Front", false);
			enemyAnimator.SetBool ("Back", false);
		}
	}

	void CheckDirection(){
		if (currentTile == end){
			if(vertical==true && horizontal == false){
				if(up){
					down = true;
					up = false;
					to = false;
					from = true;
				}
				else{
					down = false;
					up = true;
					to = false;
					from = true;
				}
			}
			if(horizontal ==true && vertical == false){
				if(right){
					right = false;
					left = true;
					to = false;
					from = true;
				}
				else{
					right = true;
					left = false;
					to = false;
					from = true;
				}
			}
		}

		if (currentTile == start){
			if(vertical==true && horizontal == false){
				if(up){
					down = true;
					up = false;
					to = true;
					from = false;
				}
				else{
					down = false;
					up = true;
					to = true;
					from = false;
				}
			}
			if(horizontal ==true && vertical == false){
				if(right){
					right = false;
					left = true;
					to = true;
					from = false;
				}
				else{
					right = true;
					left = false;
					to = true;
					from = false;
				}
			}
		}
	}

	void GetNextTile(){
		if(currentTile.column == end.column && currentTile.row < end.row && down && to){
			currentTile.row = currentTile.row + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
		if(currentTile.column == end.column && currentTile.row > end.row && up && to){
			currentTile.row = currentTile.row - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
		if(currentTile.row == end.row && currentTile.column < end.column && right && to){
			currentTile.column = currentTile.column + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
		if(currentTile.row == end.row && currentTile.column > end.column && left && to){
			currentTile.column = currentTile.column - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
		if(currentTile.column == start.column && currentTile.row < start.row && down && from){
			currentTile.row = currentTile.row + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
		if(currentTile.column == start.column && currentTile.row > start.row && up && from){
			currentTile.row = currentTile.row - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
		if(currentTile.row == start.row && currentTile.column < start.column && right && from){
			currentTile.column = currentTile.column + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
		if(currentTile.row == start.row && currentTile.column > start.column && left && from){
			currentTile.column = currentTile.column - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
		}
	}
	
	void StartDirection(){
		if(start.column == end.column && start.row < end.row){
			vertical = true;
			horizontal = false;
			up = false;
			down = true;
			left = false;
			right = false;
			enemyAnimator.SetBool ("Front", true);
			enemyAnimator.SetBool ("Front", false);
		}
		if(start.column == end.column && start.row > end.row){
			vertical = true;
			horizontal = false;
			up = true;
			down = false;
			left = false;
			right = false;
			enemyAnimator.SetBool ("Back", true);
			enemyAnimator.SetBool ("Back", false);
		}
		if(start.row == end.row && start.column < end.column){
			vertical = false;
			horizontal = true;
			up = false;
			down = false;
			left = false;
			right = true;
			enemyAnimator.SetBool ("Right", true);
			enemyAnimator.SetBool ("Right", false);
		}
		if(start.row == end.row && start.column > end.column){
			vertical = false;
			horizontal = true;
			up = false;
			down = false;
			left = true;
			right = false;
			enemyAnimator.SetBool ("Left", true);
			enemyAnimator.SetBool ("Left", false);
		}
	}
	
	void MoveToLocation(TileIndex currentTile, TileIndex end){
		if(up){
			enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, currentLoc, Time.deltaTime*speed);
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Front", false);
			enemyAnimator.SetBool ("Back", true);
		}
		if(down){
			enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, currentLoc, Time.deltaTime*speed);
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Front", true);
			enemyAnimator.SetBool ("Back", false);
		}
		if(left){
			enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, currentLoc, Time.deltaTime*speed);
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", true);
			enemyAnimator.SetBool ("Front", false);
			enemyAnimator.SetBool ("Back", false);
		}
		if(right){
			enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, currentLoc, Time.deltaTime*speed);
			enemyAnimator.SetBool ("Right", true);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Front", false);
			enemyAnimator.SetBool ("Back", false);
		}
	}
}
