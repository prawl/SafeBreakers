using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class Node{
	public Node[] next;
	public TileIndex data;
	public int position;
	public int numOfChildren;
	public bool closed;
}

public class TileLinkedList{
	public Node head;
	public int size;
}

public class EnemyAStar : MonoBehaviour {
	
	public EnemyMovement moveScript;
	public TileLinkedList path;
	public TileIndex currentTile, startTile, endTile, badTile;

	void Start(){
		currentTile = startTile;
	}

	void Update(){

	}

	void pathFinder(TileIndex current){
		int lowestFScore = 100;

		if (moveScript.tileSystem.GetTile (current.row + 1, current.column).gameObject.GetComponent<TileCheck> ().valid == true) {

		}
	}

	int calculateFScore(TileIndex tile){
		return 1 + calculateHScore(tile);
	}

	int calculateHScore(TileIndex tile){
		return Mathf.Abs (tile.column - endTile.column) + Mathf.Abs (tile.row - endTile.row);
	}

}
