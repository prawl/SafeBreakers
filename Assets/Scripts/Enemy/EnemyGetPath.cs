﻿using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class EnemyGetPath : MonoBehaviour {

	public int numSteps;
	public Stack toPath, fromPath, shortestPath;
	public TileCheck tileCheckScript;
	public EnemyMovement enemyMoveScript;

	// Use this for initialization
	void Start () {
		numSteps = 0;
		GetShortestPath_To (enemyMoveScript.startTile, enemyMoveScript.endTile);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GetShortestPath_To(TileIndex to, TileIndex from){
		if(enemyMoveScript.goUp){
			TileIndex tempTile = enemyMoveScript.tileSystem.ClosestTileIndexFromWorld(enemyMoveScript.tileSystem.GetTile(enemyMoveScript.currentTile.row+1, enemyMoveScript.currentTile.column).gameObject.transform.position);
			tileCheckScript = enemyMoveScript.tileSystem.GetTile (tempTile).gameObject.GetComponent<TileCheck>();
			if(tileCheckScript.isValid && !tileCheckScript.occupied){
				toPath.Push (tempTile);
				enemyMoveScript.nextTile = tempTile;
				numSteps++;
			}
			else{
				if(enemyMoveScript.left){
					toPath.Pop ();
					enemyMoveScript.goLeft = true;
					enemyMoveScript.goUp = false;
				}
				if(enemyMoveScript.right){
					toPath.Pop ();
					enemyMoveScript.goRight = true;
					enemyMoveScript.goUp = false;
				}
				else{
					toPath.Pop ();
					enemyMoveScript.goDown = true;
					enemyMoveScript.goUp = false;
				}
			}
		}
		else if(enemyMoveScript.goDown){
			TileIndex tempTile = enemyMoveScript.tileSystem.ClosestTileIndexFromWorld(enemyMoveScript.tileSystem.GetTile(enemyMoveScript.currentTile.row-1, enemyMoveScript.currentTile.column).gameObject.transform.position);
			tileCheckScript = enemyMoveScript.tileSystem.GetTile (tempTile).gameObject.GetComponent<TileCheck>();
			if(tileCheckScript.isValid && !tileCheckScript.occupied){
				toPath.Push (tempTile);
				numSteps++;
			}
			else{
				if(enemyMoveScript.left){
					toPath.Pop ();
					enemyMoveScript.goLeft = true;
					enemyMoveScript.goUp = false;
				}
				if(enemyMoveScript.right){
					toPath.Pop ();
					enemyMoveScript.goRight = true;
					enemyMoveScript.goUp = false;
				}
				else{
					toPath.Pop ();
					enemyMoveScript.goUp = true;
					enemyMoveScript.goDown = false;
				}
			}
		}
		else if(enemyMoveScript.goRight){
			TileIndex tempTile = enemyMoveScript.tileSystem.ClosestTileIndexFromWorld(enemyMoveScript.tileSystem.GetTile(enemyMoveScript.currentTile.row, enemyMoveScript.currentTile.column+1).gameObject.transform.position);
			tileCheckScript = enemyMoveScript.tileSystem.GetTile (tempTile).gameObject.GetComponent<TileCheck>();
			if(tileCheckScript.isValid && !tileCheckScript.occupied){
				toPath.Push (tempTile);
				numSteps++;
			}
			else{
				if(enemyMoveScript.up){
					toPath.Pop ();
					enemyMoveScript.goUp = true;
					enemyMoveScript.goRight = false;
				}
				if(enemyMoveScript.down){
					toPath.Pop ();
					enemyMoveScript.goDown = true;
					enemyMoveScript.goRight = false;
				}
				else{
					toPath.Pop ();
					enemyMoveScript.goLeft = true;
					enemyMoveScript.goRight = false;
				}
			}
		}
		else if(enemyMoveScript.goLeft){
			TileIndex tempTile = enemyMoveScript.tileSystem.ClosestTileIndexFromWorld(enemyMoveScript.tileSystem.GetTile(enemyMoveScript.currentTile.row, enemyMoveScript.currentTile.column-1).gameObject.transform.position);
			tileCheckScript = enemyMoveScript.tileSystem.GetTile (tempTile).gameObject.GetComponent<TileCheck>();
			if(tileCheckScript.isValid && !tileCheckScript.occupied){
				toPath.Push (tempTile);
				numSteps++;
			}
			else{
				if(enemyMoveScript.left){
					toPath.Pop ();
					enemyMoveScript.goLeft = true;
					enemyMoveScript.goUp = false;
				}
				if(enemyMoveScript.right){
					toPath.Pop ();
					enemyMoveScript.goRight = true;
					enemyMoveScript.goUp = false;
				}
				else{
					toPath.Pop ();
					enemyMoveScript.goDown = true;
					enemyMoveScript.goUp = false;
				}
			}
		}
	}
}
