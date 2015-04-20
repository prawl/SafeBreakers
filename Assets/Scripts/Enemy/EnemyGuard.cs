using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class EnemyGuard : MonoBehaviour {
	
	public GameObject enemy;
	public GameObject player;
	public TileIndex playerLoc;
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
	public bool moving;
	public int individualMove;
	public Vector3 startingPos;
	public Vector3 curPos;
	public Vector3 lastPos;
	
	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag ("Player");
		start = tileSystem.ClosestTileIndexFromWorld (enemy.transform.position);
		currentTile = start;
		currentLoc = enemy.transform.position;
		startingPos = currentLoc;
		speed = 1.0f;
		StartDirection ();
		GetComponent<Renderer>().castShadows = true;
		GetComponent<Renderer>().receiveShadows = true;
		to = true;
		from = false;
		firstMove = true;
		moving = false;
	}

	// Update is called once per frame
	void Update () {
		curPos = transform.position;
		if (curPos != lastPos) {
			moving = true;
		}
		else{
			moving = false;
		}
		lastPos = curPos;

		GetPlayerPos ();
		if(GameController.enemyCount < GameController.gameCount && individualMove == 0 && PlayerController.move == false){
			if(firstMove){
				GetNextTile();
				firstMove = false;
			}
			else{
				CheckDirection ();
				GetNextTile ();
			}
			if(GameController.doneMoving < GameController.numEnemies){
				GameController.doneMoving++;
				individualMove++;
			}
		}
		
		if (GameController.enemyCount == GameController.gameCount) {
			individualMove = 0;
		}
		
		if (enemy.transform.position != currentLoc) {
			MoveToLocation (currentTile, end);
		}
		
		if (enemy.transform.position == currentLoc) {
			if(individualMove == 1){
				individualMove++;
			}
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Front", false);
			enemyAnimator.SetBool ("Back", false);
		}

		CheckLineOfSight ();

	}

	public void GetPlayerPos(){
		playerLoc = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
	}

	bool CheckIfOccupied(TileIndex next){
		TileData tile = tileSystem.GetTile (next.row, next.column);
		GameObject tileObject = tile.gameObject;
		TileCheck check = tileObject.GetComponent<TileCheck> ();
		bool status = check.occupied;
		return status;
	}

	void CheckLineOfSight(){
		if(up){
			for(int i = 0; i < 3; i++){
				if(currentTile.row-i == playerLoc.row && playerLoc.column == currentTile.column){
					PlayerController.spotted = true;
				}
			}
		}
		if(down){
			for(int i = 0; i < 3; i++){
				if(currentTile.row+i == playerLoc.row && playerLoc.column == currentTile.column){
					PlayerController.spotted = true;
				}
			}
		}
		if(left){
			for(int i = 0; i < 3; i++){
				if(currentTile.column-i == playerLoc.column && playerLoc.row == currentTile.row){
					PlayerController.spotted = true;
				}
			}
		}
		if(right){
			for(int i = 0; i < 3; i++){
				if(currentTile.column+i == playerLoc.column && playerLoc.row == currentTile.row){
					PlayerController.spotted = true;
				}
			}
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
		if(vertical && down && to){
			currentTile.row = currentTile.row + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
		}
		if(vertical && up && to){
			currentTile.row = currentTile.row - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
		}
		if(horizontal && right && to){
			currentTile.column = currentTile.column + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
		}
		if(horizontal && left && to){
			currentTile.column = currentTile.column - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
		}
		if(vertical && down && from){
			currentTile.row = currentTile.row + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
		}
		if(vertical && up && from){
			currentTile.row = currentTile.row - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
		}
		if(horizontal && right && from){
			currentTile.column = currentTile.column + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
		}
		if(horizontal && left && from){
			currentTile.column = currentTile.column - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1.1f;
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
