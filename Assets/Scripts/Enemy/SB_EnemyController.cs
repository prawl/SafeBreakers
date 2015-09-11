using UnityEngine;
using System.Collections;
using Pathfinding;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_EnemyController : MonoBehaviour {

	public GameObject player, enemy;
	private SB_GameController gameCon;
	public TileSystem tileSystem;
	public TileIndex startTile, endTile;
	public int lineOfSight;
	private Vector3 startPos, endPos;
	private Seeker seeker;
	private CharacterController controller;
	private Path path;
	private float speed = 2;
	public Vector3[] pathArray;
	public int currentPos;
	public bool up, down, right, left, to, from, moved, updatedPos, faceUp, faceDown, faceRight, faceLeft;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().castShadows = true;
		GetComponent<Renderer> ().receiveShadows = true;
		startPos = tileSystem.GetTile (startTile).gameObject.transform.position;
		startPos.y = .75f;
		transform.position = startPos;
		endPos = tileSystem.GetTile (endTile).gameObject.transform.position;
		endPos.y = .75f;
		player = GameObject.Find ("SB_Player");
		gameCon = player.GetComponent<SB_GameController> ();
		seeker = GetComponent<Seeker> ();
		currentPos = 0;
		controller = GetComponent<CharacterController> ();
		seeker.StartPath (startPos, endPos, OnPathComplete);
		to = true;
		from = false;
		updatedPos = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameCon.levelPaused) {
			LookForPlayer();
			if(gameCon.playerCount > gameCon.enemyCount){
				MoveToNextLoc ();
			}
			if(gameCon.gameCount == gameCon.enemyCount){
				moved = false;
			}
		}
	}

	void GetDirection(Vector3 start, Vector3 end){
		if ((start.x > end.x)  && (Mathf.Approximately(start.z, end.z))) {
			up = false; down = false; right = false; left = true;
			faceUp = false; faceDown = false; faceRight = false; faceLeft = true;
		}
		if ((start.x < end.x)  && (Mathf.Approximately(start.z, end.z))) {
			up = false; down = false; right = true; left = false;
			faceUp = false; faceDown = false; faceRight = true; faceLeft = false;
		}
		if ((start.z > end.z) && (Mathf.Approximately(start.x, end.x))) {
			up = false; down = true; right = false; left = false;
			faceUp = false; faceDown = true; faceRight = false; faceLeft = false;
		}
		if ((start.z < end.z) && (Mathf.Approximately(start.x, end.x))) {
			up = true; down = false; right = false; left = false;
			faceUp = true; faceDown = false; faceRight = false; faceLeft = false;
		}
	}

	void ChangeYValue(Vector3[] path){
		for (int i = 0; i < path.Length; i++) {
			Vector3 temp = path[i];
			temp.y = .75f;
			path[i] = temp;
		}
	}

	void OnPathComplete(Path p){
		if (!p.error) {
			path = p;
			pathArray = path.vectorPath.ToArray ();
			ChangeYValue(pathArray);
			GetDirection(pathArray[currentPos], pathArray[currentPos + 1]);
		}
	}

	public void LookForPlayer(){
		if (faceUp) {
			try{
				for(int i = 0; i < lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(enemy.transform.position);
					if(tempTile.row - i >= 0){
						SB_TileCheck tempTileCheck = tileSystem.GetTile (tempTile.row - i, tempTile.column).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
							if(tempTileCheck.occupier == "Player"){
								gameCon.levelLost = true;
							}
							else{
								i = lineOfSight;
							}
						}						
					}
					else{
						i = lineOfSight;
					}
				}
			}
			catch{}
		}
		if (faceDown) {
			try{
				for(int i = 0; i < lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(enemy.transform.position);
					if(tempTile.row + i <= tileSystem.RowCount-1){
						SB_TileCheck tempTileCheck = tileSystem.GetTile (tempTile.row + i, tempTile.column).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
							if(tempTileCheck.occupier == "Player"){
								gameCon.levelLost = true;
							}
							else{
								i = lineOfSight;
							}
						}						
					}
					else{
						i = lineOfSight;
					}
				}
			}
			catch{}
		}
		if (faceRight) {
			try{
				for(int i = 0; i < lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(enemy.transform.position);
					if(tempTile.column + i <= tileSystem.ColumnCount-1){
						SB_TileCheck tempTileCheck = tileSystem.GetTile (tempTile.row, tempTile.column + i).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
							if(tempTileCheck.occupier == "Player"){
								gameCon.levelLost = true;
							}
							else{
								i = lineOfSight;
							}
						}						
					}
					else{
						i = lineOfSight;
					}
				}
			}
			catch{}
		}
		if (faceLeft) {
			try{
				for(int i = 0; i < lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(enemy.transform.position);
					if(tempTile.column - i <= 0){
						SB_TileCheck tempTileCheck = tileSystem.GetTile (tempTile.row, tempTile.column - i).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
							if(tempTileCheck.occupier == "Player"){
								gameCon.levelLost = true;
							}
							else{
								i = lineOfSight;
							}
						}						
					}
					else{
						i = lineOfSight;
					}
				}	
			}
			catch{}
		}
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude (a - b) < 0.000001;
	}

	public void MoveToNextLoc(){
		if (!moved) {
			if(to){
				if(tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(pathArray[currentPos + 1])).gameObject.GetComponent<SB_TileCheck>().occupier == "Enemy"){
					moved = true;
					updatedPos = false;
					gameCon.enemyDone++;
				}
				else{
					if(!updatedPos && currentPos != pathArray.Length-1){
						currentPos++;
						updatedPos = true;
					}
					if((!V3Equal(pathArray[currentPos], transform.position)) && (currentPos <= pathArray.Length-1)){
						Vector3 dir = (pathArray[currentPos]-transform.position).normalized;
						dir *= Time.fixedDeltaTime * speed;
						controller.Move (dir);
					}
					if(V3Equal(pathArray[currentPos], transform.position)){
						updatedPos = false;
						moved = true;
						gameCon.enemyDone++;
					}
					if(currentPos == pathArray.Length-1){
						to = false;
						from = true;
					}
				}
			}
			if(from){
				if(tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(pathArray[currentPos - 1])).gameObject.GetComponent<SB_TileCheck>().occupier == "Enemy"){
					moved = true;
					updatedPos = false;
					gameCon.enemyDone++;
				}
				else{
					if(!updatedPos && currentPos != 0){
						currentPos--;
						updatedPos = true;
					}
					if((!V3Equal(pathArray[currentPos], transform.position)) && (currentPos >= 0)){
						Vector3 dir = (pathArray[currentPos]-transform.position).normalized;
						dir *= Time.fixedDeltaTime * speed;
						controller.Move (dir);
					}
					if(V3Equal(pathArray[currentPos], transform.position)){
						updatedPos = false;
						moved = true;
						gameCon.enemyDone++;
					}
					if(currentPos == 0){
						to = false;
						from = true;
					}
				}
			}
		}
	}
}
