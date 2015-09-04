using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;
using Pathfinding;

public class EnemyController : MonoBehaviour {

	public GameObject enemy, player;
	public TileSystem tileSystem;
	public TileIndex startTile, endTile;
	public int lineOfSight;
	//The points and tiles to move to
	private Vector3 startPosition, endPosition;
	private Seeker seeker;
	private CharacterController controller;
	//The calculated path
	private Path path;
	//The AI's speed per second
	private float speed = 2;
	//Array of all Vector3 data in A* path
	private Vector3[] pathArray;
	private int currentPos;
	public bool up, down, right, left, to, from, moved, updatedPos, faceDown, faceUp, faceRight, faceLeft;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().castShadows = true;
		GetComponent<Renderer>().receiveShadows = true;
		startPosition = tileSystem.GetTile (startTile).gameObject.transform.position;
		startPosition.y = 1f;
		transform.position = startPosition;
		endPosition = tileSystem.GetTile (endTile).gameObject.transform.position;
		endPosition.y = 1f;
		player = GameObject.FindGameObjectWithTag ("Player");
		seeker = GetComponent<Seeker> ();
		currentPos = 0;
		controller = GetComponent<CharacterController> ();
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (startPosition, endPosition, OnPathComplete);
		to = true;
		from = false;
		updatedPos = false;
	}

	public void GetStartDirection(Vector3 currentPos, Vector3 targetPos){
		if((currentPos.x > targetPos.x) && (currentPos.z == targetPos.z)){
			left = true;
			right = false;
			down = false;
			up = false;
			faceLeft = true;
			faceRight = false;
			faceDown = false;
			faceUp = false;
		}
		if((currentPos.x < targetPos.x) && (currentPos.z == targetPos.z)){
			left = false;
			right = true;
			down = false;
			up = false;
			faceLeft = false;
			faceRight = true;
			faceDown = false;
			faceUp = false;
		}
		if((currentPos.z < targetPos.z) && (currentPos.x == targetPos.x)){
			left = false;
			right = false;
			down = false;
			up = true;
			faceLeft = false;
			faceRight = false;
			faceDown = false;
			faceUp = true;
		}
		else{
			left = false;
			right = false;
			down = true;
			up = false;
			faceLeft = false;
			faceRight = false;
			faceDown = true;
			faceUp = false;
		}
	}

	public void OnPathComplete(Path p){
		if(!p.error){
			path = p;
			pathArray = path.vectorPath.ToArray ();
			ChangeYvalue (pathArray);
			GetStartDirection (startPosition , pathArray[currentPos]);
		}
	}

	public void ChangeYvalue(Vector3[] path){
		for(int i = 0; i < path.Length; i++){
			Vector3 temp = path[i];
			temp.y = 1f;
			path[i] = temp;
		}
	}
	public void GetDirection(Vector3 currentPos, Vector3 targetPos){
		if((currentPos.x > targetPos.x) && (currentPos.z == targetPos.z) && !V3Equal (currentPos, targetPos)){
			left = true;
			right = false;
			down = false;
			up = false;
			faceLeft = true;
			faceRight = false;
			faceDown = false;
			faceUp = false;
		}
		if((currentPos.x < targetPos.x) && (currentPos.z == targetPos.z) && !V3Equal (currentPos, targetPos)){
			left = false;
			right = true;
			down = false;
			up = false;
			faceLeft = false;
			faceRight = true;
			faceDown = false;
			faceUp = false;
		}
		if((currentPos.z > targetPos.z) && (currentPos.x == targetPos.x) && !V3Equal (currentPos, targetPos)){
			left = false;
			right = false;
			down = true;
			up = false;
			faceLeft = false;
			faceRight = false;
			faceDown = true;
			faceUp = false;
		}
		if((currentPos.z < targetPos.z) && (currentPos.x == targetPos.x) && !V3Equal (currentPos, targetPos)){
			left = false;
			right = false;
			down = false;
			up = true;
			faceLeft = false;
			faceRight = false;
			faceDown = false;
			faceUp = true;
		}
		if(V3Equal (currentPos, targetPos)){
			left = false;
			right = false;
			down = false;
			up = false;
		}
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude (a - b) < 0.0000001;
	}

	public void Update(){
		if (New_GameController.gameCount > New_GameController.enemyCount && New_GameController.playerCount > New_GameController.enemyCount && !moved) {
			MoveToNextLoc();
		}
		if (New_GameController.enemyCount == New_GameController.playerCount) {
			moved = false;
		}
	}

	public void LookForPlayer(){
		if (faceUp) {
			try{
				for(int i = 0; i < lineOfSight; i++){
					if(tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).row - i, tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).column) == tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(player.transform.position))){
						New_GameController.levelLost = true;
					}
				}
			}
			catch(UnityException e){

			}
		}
		if(faceDown){
			try{
				for(int i = 0; i < lineOfSight; i++){
					if(tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).row + i, tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).column) == tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(player.transform.position))){
						New_GameController.levelLost = true;
					}
				}
			}
			catch(UnityException e){
				
			}
		}
		if(faceRight){
			try{
				for(int i = 0; i < lineOfSight; i++){
					if(tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).row , tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).column + i) == tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(player.transform.position))){
						New_GameController.levelLost = true;
					}
				}
			}
			catch(UnityException e){
				
			}
		}
		if(faceLeft){
			try{
				for(int i = 0; i < lineOfSight; i++){
					if(tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).row , tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position).column - i) == tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(player.transform.position))){
						New_GameController.levelLost = true;
					}
				}
			}
			catch(UnityException e){
				
			}
		}
	}

	// Update is called once per frame
	public void MoveToNextLoc () {
		if (to) {
			if(!updatedPos){
				currentPos++;
				updatedPos = true;
			}
			if(path == null){
				//We have no path to move after yet
				return;
			}
			if (!V3Equal(pathArray[currentPos], transform.position) && (currentPos < pathArray.Length-1)) {
				Vector3 dir = (pathArray[currentPos]-transform.position).normalized;
				dir *= Time.fixedDeltaTime * speed;
				controller.Move (dir);
			}
			if(V3Equal(pathArray[currentPos], transform.position)){
				updatedPos = false;
				moved = true;
				New_GameController.enemyDone++;
			}
			
			if (currentPos == pathArray.Length-1) {
				from = true;
				to = false;
			}
		}
		if (from) {
			if(!updatedPos){
				currentPos--;
				updatedPos = true;
			}
			if(path == null){
				//We have no path to move after yet
				return;
			}
			if (!V3Equal(pathArray[currentPos], transform.position) && (currentPos > 0)) {
				Vector3 dir = (pathArray[currentPos]-transform.position).normalized;
				dir *= Time.fixedDeltaTime * speed;
				controller.Move (dir);
			}
			if(V3Equal(pathArray[currentPos], transform.position)){
				updatedPos = false;
				moved = true;
				New_GameController.enemyDone++;
			}
			
			if (currentPos == 0) {
				to = true;
				from = false;
			}
		}
		GetDirection (transform.position, pathArray[currentPos]);
	}
}
