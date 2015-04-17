using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class EnemyGetPath : MonoBehaviour {
	
	public EnemyMovement moveScript;
	public List<TileIndex> open;
	public List<TileIndex> closed;
	public TileIndex temp;

	void Start(){
		if(moveScript.to){
			Pathfinding (moveScript.startTile, moveScript.endTile);
		}
		if (moveScript.from) {
			Pathfinding (moveScript.endTile, moveScript.startTile);
		}
	}

	void Pathfinding(TileIndex start, TileIndex end){

		open.Add (start);
		temp = start;

		lowestFCost (start);
	}

	TileIndex lowestFCost(TileIndex current){

		int G = 0;
		int H = 0;

		if(moveScript.tileSystem.GetTile(current.row + 1, current.column).gameObject.GetComponent<TileCheck>().occupied == false){
			print ("1");
		}
		if(moveScript.tileSystem.GetTile(current.row -1, current.column).gameObject.GetComponent<TileCheck>().occupied == false){
			print ("2");
		}
		if(moveScript.tileSystem.GetTile(current.row, current.column+1).gameObject.GetComponent<TileCheck>().occupied == false){
			print ("3");
		}
		if(moveScript.tileSystem.GetTile(current.row, current.column-1).gameObject.GetComponent<TileCheck>().occupied == false){
			print ("4");
		}
		return current;
	}
}
