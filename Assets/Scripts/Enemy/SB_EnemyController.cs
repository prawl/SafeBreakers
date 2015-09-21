﻿using UnityEngine;
using System.Collections;
using Pathfinding;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_EnemyController : MonoBehaviour {

	private GameObject player;
	private SB_GameController gameCon;
	private TileSystem tileSystem;
	public TileIndex startTile, endTile;
	public int lineOfSight;
	private Vector3 startPos, endPos;
	private Seeker seeker;
	private CharacterController controller;
	private Path path;
	private float speed = 1;
	private Vector3[] pathArray;
	private int currentPos;
	public bool up, down, right, left, moved, faceUp, faceDown, faceRight, faceLeft;
    public bool to, from, updatedPos;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().castShadows = true;
		GetComponent<Renderer> ().receiveShadows = true;
		player = GameObject.Find ("SB_Player");
		tileSystem = player.GetComponent<SB_PlayerController> ().tileSystem;
		startPos = tileSystem.GetTile (startTile).gameObject.transform.position;
		startPos.y = .74f;
		transform.position = startPos;
		endPos = tileSystem.GetTile (endTile).gameObject.transform.position;
		endPos.y = .74f;
		gameCon = player.GetComponent<SB_GameController> ();
		seeker = GetComponent<Seeker> ();
		currentPos = 0;
		controller = GetComponent<CharacterController> ();
		seeker.StartPath (startPos, endPos, OnPathComplete);
		to = true;from = false;updatedPos = false;moved = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (!gameCon.isLevelPaused) {
            if (!player.GetComponent<SB_PlayerController>().moving){
                LookForPlayer();
            }
			if(gameCon.playerCount > gameCon.enemyCount){
				moved = false;
				MoveToNextLoc ();
			}
			if(gameCon.playerCount == gameCon.enemyCount){
				moved = true;
			}
		}
	}

	void GetDirection(Vector3 start, Vector3 end){
		if ((start.x > end.x)  && (Mathf.Approximately(start.z, end.z)) && !V3Equal(start, end)) {
			up = false; down = false; right = false; left = true;
			faceUp = false; faceDown = false; faceRight = false; faceLeft = true;
		}
		else if ((start.x < end.x)  && (Mathf.Approximately(start.z, end.z)) && !V3Equal(start, end)) {
			up = false; down = false; right = true; left = false;
			faceUp = false; faceDown = false; faceRight = true; faceLeft = false;
		}
		else if ((start.z > end.z) && (Mathf.Approximately(start.x, end.x)) && !V3Equal(start, end)) {
			up = false; down = true; right = false; left = false;
			faceUp = false; faceDown = true; faceRight = false; faceLeft = false;
		}
		else if ((start.z < end.z) && (Mathf.Approximately(start.x, end.x)) && !V3Equal(start, end)) {
			up = true; down = false; right = false; left = false;
			faceUp = true; faceDown = false; faceRight = false; faceLeft = false;
		}
	}

	void ChangeYValue(Vector3[] path){
		for (int i = 0; i < path.Length; i++) {
			Vector3 temp = path[i];
			temp.y = .74f;
			path[i] = temp;
		}
	}

	void OnPathComplete(Path p){
		if (!p.error) {
			path = p;
			pathArray = path.vectorPath.ToArray ();
			ChangeYValue(pathArray);
		}
	}

	public void LookForPlayer(){
		if (faceUp) {
			try{
				for(int i = 1; i <= lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(transform.position);
					if(tempTile.row - i >= 0){
						SB_TileCheck tempTileCheck = tileSystem.GetTile ((tempTile.row - i), tempTile.column).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
							if(tempTileCheck.occupier.tag == "Player"){
								gameCon.isLevelLost = true;
							}
							else{
								i = lineOfSight + 1;
							}
						}						
					}
				}
			}
			catch{}
		}
		if (faceDown) {
			try{
				for(int i = 1; i <= lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(transform.position);
					if(tempTile.row + i <= tileSystem.RowCount-1){
						SB_TileCheck tempTileCheck = tileSystem.GetTile ((tempTile.row + i), tempTile.column).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
                            if (tempTileCheck.occupier.tag == "Player"){
                                gameCon.isLevelLost = true;
							}
							else{
								i = lineOfSight + 1;
							}
						}						
					}
				}
			}
			catch{}
		}
		if (faceRight) {
			try{
				for(int i = 1; i <= lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(transform.position);
					if(tempTile.column + i <= tileSystem.ColumnCount-1){
						SB_TileCheck tempTileCheck = tileSystem.GetTile (tempTile.row, (tempTile.column + i)).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
							if(tempTileCheck.occupier.tag == "Player"){
								gameCon.isLevelLost = true;
							}
							else{
								i = lineOfSight + 1;
							}
						}						
					}
				}
			}
			catch{}
		}
		if (faceLeft) {
			try{
				for(int i = 1; i <= lineOfSight; i++){
					TileIndex tempTile = tileSystem.ClosestTileIndexFromWorld(transform.position);
					if(tempTile.column - i <= 0){
						SB_TileCheck tempTileCheck = tileSystem.GetTile (tempTile.row, (tempTile.column - i)).gameObject.GetComponent<SB_TileCheck>();
						if(tempTileCheck.occupied == true){
							if(tempTileCheck.occupier.tag == "Player"){
								gameCon.isLevelLost = true;
							}
							else{
								i = lineOfSight + 1;
							}
						}						
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
				if((currentPos != pathArray.Length-1) && (tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(pathArray[currentPos + 1])).gameObject.GetComponent<SB_TileCheck>().occupier.tag == "Enemy") && (tileSystem.GetTile(tileSystem.ClosestTileIndexFromWorld(pathArray[currentPos + 1])).gameObject.GetComponent<SB_TileCheck>().occupier != gameObject))
                {
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
                        GetDirection(transform.position, pathArray[currentPos]);
					}
					if(V3Equal(pathArray[currentPos], transform.position)){
						updatedPos = false;
						moved = true;
						gameCon.enemyDone++;
					}
					if(currentPos == pathArray.Length-1){
						moved = true;
						to = false;
						from = true;
					}
				}
			}
			if(from){
				if((currentPos != 0) && (tileSystem.GetTile (tileSystem.ClosestTileIndexFromWorld(pathArray[currentPos - 1])).gameObject.GetComponent<SB_TileCheck>().occupier.tag == "Enemy") && (tileSystem.GetTile(tileSystem.ClosestTileIndexFromWorld(pathArray[currentPos - 1])).gameObject.GetComponent<SB_TileCheck>().occupier != gameObject))
                {
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
                        GetDirection(transform.position, pathArray[currentPos]);
                    }
					if(V3Equal(pathArray[currentPos], transform.position)){
						updatedPos = false;
						moved = true;
						gameCon.enemyDone++;
					}
					if(currentPos == 0){
						moved = true;
						to = true;
						from = false;
					}
				}
			}
		}
	}
}
