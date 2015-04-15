using UnityEngine;
using System;
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
		moveScript = gameObject.GetComponent<EnemyMovement> ();
		Pathfinding (moveScript.startTile, moveScript.endTile);
	}

	void Update(){
		if (moveScript.to) {
			if(temp != moveScript.endTile){
				temp = lowestFCost(temp, moveScript.endTile);
				closed.Add (temp);
			}
		}
		if (moveScript.from) {
			if(temp != moveScript.startTile){
				temp = lowestFCost(temp, moveScript.startTile);
				closed.Add (temp);
			}
		}
	}

	void Pathfinding(TileIndex start, TileIndex end){
		closed.Add (start);
		temp = start;
	}

	TileIndex lowestFCost(TileIndex current, TileIndex end){
		int G = 1;
		int F = 10;
		int tempInt;
		TileIndex rightTile, leftTile, upTile, downTile;
		TileIndex lowestFTile = current;
		TileIndex badTile = moveScript.tileSystem.ClosestTileIndexFromWorld (moveScript.tileSystem.GetTile (0, 0).gameObject.transform.position);

		try{
			leftTile = moveScript.tileSystem.ClosestTileIndexFromWorld (moveScript.tileSystem.GetTile (current.row - 1, current.column).gameObject.transform.position);
		}
		catch(Exception left){
			leftTile = badTile;
		}
		try{
			upTile = moveScript.tileSystem.ClosestTileIndexFromWorld (moveScript.tileSystem.GetTile (current.row, current.column - 1).gameObject.transform.position);
		}
		catch(Exception up){
			upTile = badTile;
		}
		try{
			rightTile = moveScript.tileSystem.ClosestTileIndexFromWorld (moveScript.tileSystem.GetTile (current.row + 1, current.column).gameObject.transform.position);
		}
		catch(Exception right){
			rightTile = badTile;
		}
		try{
			downTile = moveScript.tileSystem.ClosestTileIndexFromWorld (moveScript.tileSystem.GetTile (current.row, current.column + 1).gameObject.transform.position);
		}
		catch(Exception down){
			downTile = badTile;
		}

		if (leftTile != badTile && moveScript.tileSystem.GetTile (leftTile).gameObject.GetComponent<TileCheck> ().occupied == false) {
			tempInt = G + calculateHScore(leftTile, end);
			if(tempInt < F){
				F = tempInt;
				lowestFTile = leftTile;
			}
			if(tempInt == F){
				lowestFTile = tieBreaker(leftTile, lowestFTile, leftTile, lowestFTile);
			}
		}
		if (upTile != badTile && moveScript.tileSystem.GetTile (upTile).gameObject.GetComponent<TileCheck> ().occupied == false) {
			tempInt = G + calculateHScore(upTile, end);
			if(tempInt < F){
				F = tempInt;
				lowestFTile = upTile;
			}
			if(tempInt == F){
				lowestFTile = tieBreaker(upTile, lowestFTile, upTile, lowestFTile);
			}
		}
		if (rightTile != badTile && moveScript.tileSystem.GetTile (rightTile).gameObject.GetComponent<TileCheck> ().occupied == false) {
			tempInt = G + calculateHScore(rightTile, end);
			if(tempInt < F){
				F = tempInt;
				lowestFTile = rightTile;
			}
			if(tempInt == F){
				lowestFTile = tieBreaker(rightTile, lowestFTile, rightTile, lowestFTile);
			}
		}
		if (downTile != badTile && moveScript.tileSystem.GetTile (downTile).gameObject.GetComponent<TileCheck> ().occupied == false) {
			tempInt = G + calculateHScore(downTile, end);
			if(tempInt < F){
				F = tempInt;
				lowestFTile = downTile;
			}
			if(tempInt == F){
				lowestFTile = tieBreaker(downTile, lowestFTile, downTile, lowestFTile);
			}
		}

		return lowestFTile;
	}

	int calculateHScore(TileIndex current, TileIndex end){
		int H = 0;
		if (current.row > end.row) {
			H = H + int.Parse (current.row.ToString()) - int.Parse (end.row.ToString());
		}
		if (current.row < end.row) {
			H = H + int.Parse (end.row.ToString()) - int.Parse (current.row.ToString());
		}
		if (current.column > end.column) {
			H = H + int.Parse (current.column.ToString()) - int.Parse (end.column.ToString());
		}
		if (current.column < end.column) {
			H = H + int.Parse (end.column.ToString()) - int.Parse (current.column.ToString());
		}
		return H;
	}

	TileIndex tieBreaker(TileIndex one, TileIndex two, TileIndex oneSource, TileIndex twoSource){
		int oneF = calculateHScore (one, moveScript.endTile) + 1;
		int twoF = calculateHScore (two, moveScript.endTile) + 1;
		if(oneF > twoF){
			return one;
		}
		if(twoF > oneF){
			return two;
		}
		if (one == moveScript.endTile) {
			return one;
		}
		if (two == moveScript.endTile) {
			return two;
		}
		else {
			return one;
		}
	}
}
