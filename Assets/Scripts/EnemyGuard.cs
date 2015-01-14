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
	public bool doneMoving;
	public bool to;
	public bool from;
	public bool up;
	public bool down;
	public bool left;
	public bool right;
	private Animator enemyAnimator = null;
	private Vector3 velocity;
	
	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponent<Animator>();
		start = tileSystem.ClosestTileIndexFromWorld (enemy.transform.position);
		currentTile = tileSystem.ClosestTileIndexFromWorld (enemy.transform.position);
		currentLoc = enemy.transform.position;
		speed = 1.0f;
		doneMoving = true;
		StartDirection ();
		to = true;
		from = false;
		velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.enemyCount < GameController.gameCount){
			doneMoving = false;
			MoveToLocation (currentTile, end);
		}
		if (doneMoving) {
			currentTile = tileSystem.ClosestTileIndexFromWorld(currentLoc);
		}
	}
	
	void StartDirection(){
		if(start.column == end.column && start.row < end.row){
			up = false;
			down = true;
			left = false;
			right = false;
		}
		if(start.column == end.column && start.row > end.row){
			up = true;
			down = false;
			left = false;
			right = false;
		}
		if(start.row == end.row && start.column < end.column){
			up = false;
			down = false;
			left = false;
			right = true;
		}
		if(start.row == end.row && start.column < end.column){
			up = false;
			down = false;
			left = true;
			right = false;
		}
	}
	
	void MoveToLocation(TileIndex currentTile, TileIndex end){
		if(currentTile.column == end.column && currentTile.row < end.row && down && to){
			print ("1");
			currentTile.row = currentTile.row + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(currentTile.column == end.column && currentTile.row > end.row && up && to){
			print ("2");
			currentTile.row = currentTile.row - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(currentTile.row == end.row && currentTile.column < end.column && right && to){
			print ("3");
			currentTile.column = currentTile.column + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(currentTile.row == end.row && currentTile.column > end.column && left && to){
			print ("4");
			currentTile.column = currentTile.column - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(currentTile.column == start.column && currentTile.row < start.row && down && from){
			print ("5");
			currentTile.row = currentTile.row + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(currentTile.column == start.column && currentTile.row > start.row && up && from){
			print ("6");
			currentTile.row = currentTile.row - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(currentTile.row == start.row && currentTile.column < start.column && right && from){
			print ("7");
			currentTile.column = currentTile.column + 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(currentTile.row == start.row && currentTile.column > start.column && left && from){
			print ("8");
			currentTile.column = currentTile.column - 1;
			currentLoc = tileSystem.WorldPositionFromTileIndex (currentTile, true);
			currentLoc.z = -1;
			while(enemy.transform.position != currentLoc){
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, currentLoc, Time.deltaTime*speed);
			}
		}
		if(enemy.transform.position == currentLoc){
			print ("9");
			GameController.enemyCount++;
		}
		if (currentTile == end) {
			print ("10");
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
		if (currentTile == start) {
			print ("11");
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
		doneMoving = true;
	}
}
