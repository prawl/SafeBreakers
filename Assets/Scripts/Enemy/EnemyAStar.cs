using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class EnemyAStar : MonoBehaviour {
	
	public EnemyMovement moveScript;
	public LinkedList<TileIndex> open = new LinkedList<TileIndex>();
	public LinkedList<TileIndex> closed = new LinkedList<TileIndex>();
	public TileIndex currentTile;
	public bool test;

	void Start(){
		currentTile = moveScript.startTile;
		open.AddLast (currentTile);
		Pathfinding (currentTile);
	}

	void Update(){

	}

	void Pathfinding(TileIndex tile){

		TileIndex rightTile, leftTile, upTile, downTile;
		TileIndex badTile = moveScript.tileSystem.ClosestTileIndexFromWorld (moveScript.tileSystem.GetTile (0, 0).gameObject.transform.position);

		try{
			leftTile = moveScript.tileSystem.ClosestTileIndexFromWorld(moveScript.tileSystem.GetTile (tile.row - 1, tile.column).gameObject.transform.position);
		}
		catch(Exception left){
			leftTile = badTile;
		}

		try{
			upTile = moveScript.tileSystem.ClosestTileIndexFromWorld(moveScript.tileSystem.GetTile (tile.row, tile.column - 1).gameObject.transform.position);
		}
		catch(Exception up){
			upTile = badTile;
		}

		try{
			rightTile = moveScript.tileSystem.ClosestTileIndexFromWorld(moveScript.tileSystem.GetTile (tile.row + 1, tile.column).gameObject.transform.position);
		}
		catch(Exception right){
			rightTile = badTile;
		}

		try{
			downTile = moveScript.tileSystem.ClosestTileIndexFromWorld(moveScript.tileSystem.GetTile (tile.row, tile.column + 1).gameObject.transform.position);
		}
		catch(Exception down){
			downTile = badTile;
		}

		if(leftTile != badTile && moveScript.tileSystem.GetTile (leftTile).gameObject.GetComponent<TileCheck>().isOccupied != true){
			open.AddAfter(open.Find(tile), leftTile);
		}
		if(upTile != badTile && moveScript.tileSystem.GetTile (upTile).gameObject.GetComponent<TileCheck>().isOccupied != true){

		}
		if(rightTile != badTile && moveScript.tileSystem.GetTile (rightTile).gameObject.GetComponent<TileCheck>().isOccupied != true){

		}
		if(downTile != badTile && moveScript.tileSystem.GetTile (downTile).gameObject.GetComponent<TileCheck>().isOccupied != true){

		}

	}

	/*TileIndex lowestFCost(TileIndex current, TileIndex end){
		
	}

	int calculateHScore(TileIndex current, TileIndex end){

	}

    TileIndex tieBreaker(TileIndex one, TileIndex two, TileIndex oneSource, TileIndex twoSource){

    }*/

}
