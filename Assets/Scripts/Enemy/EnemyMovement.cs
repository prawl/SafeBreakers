using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class EnemyMovement : MonoBehaviour {

	public EnemyController enemyContrl;
	public bool left, right, up, down, to, from, goUp, goDown, goRight, goLeft;
	public TileIndex startTile, endTile, currentTile, nextTile;
	public TileSystem tileSystem;
	public Vector3 startPos, endPos;

	// Use this for initialization
	void Start () {

		GetDirections ();
		to = true;
		from = false;

		if (tileSystem.GetTile (startTile).gameObject != null) {
			startPos = tileSystem.GetTile(startTile).gameObject.transform.position;
		}
		if (tileSystem.GetTile (endTile).gameObject.transform.position != null) {
			endPos = tileSystem.GetTile(endTile).gameObject.transform.position;
		}
		startPos.z = -1.5f;
		endPos.z = -1.5f;
		currentTile = startTile;
		GetPathDir ();
	}

	void GetPathDir(){
		if(up){
			if(right){
				if(Mathf.Abs(int.Parse(startTile.column.ToString ()) - int.Parse (endTile.column.ToString ())) < Mathf.Abs(int.Parse(startTile.row.ToString ()) - int.Parse (endTile.row.ToString ()))){
					goUp = true;
					goDown = false;
					goRight = false;
					goLeft = false;
				}
				else{
					goUp = false;
					goDown = false;
					goRight = true;
					goLeft = false;
				}
			}
			else if(left){
				if(Mathf.Abs(int.Parse(startTile.column.ToString ()) - int.Parse (endTile.column.ToString ())) < Mathf.Abs(int.Parse(startTile.row.ToString ()) - int.Parse (endTile.row.ToString ()))){
					goUp = true;
					goDown = false;
					goRight = false;
					goLeft = false;
				}
				else{
					goUp = false;
					goDown = false;
					goRight = true;
					goLeft = false;
				}
			}
			else{
				goUp = true;
				goDown = false;
				goRight = false;
				goLeft = false;
			}
		}
		else if(down){
			if(right){
				if(Mathf.Abs(int.Parse(startTile.column.ToString ()) - int.Parse (endTile.column.ToString ())) < Mathf.Abs(int.Parse(startTile.row.ToString ()) - int.Parse (endTile.row.ToString ()))){
					goUp = false;
					goDown = true;
					goRight = false;
					goLeft = false;
				}
				else{
					goUp = false;
					goDown = false;
					goRight = true;
					goLeft = false;
				}
			}
			else if(left){
				if(Mathf.Abs(int.Parse(startTile.column.ToString ()) - int.Parse (endTile.column.ToString ())) < Mathf.Abs(int.Parse(startTile.row.ToString ()) - int.Parse (endTile.row.ToString ()))){
					goUp = false;
					goDown = true;
					goRight = false;
					goLeft = false;
				}
				else{
					goUp = false;
					goDown = false;
					goRight = false;
					goLeft = true;
				}
			}
			else{
				goUp = false;
				goDown = true;
				goRight = false;
				goLeft = false;
			}
		}
		else{
			if(right){
				goUp = false;
				goDown = false;
				goRight = true;
				goLeft = false;
			}
			else if(left){
				goUp = false;
				goDown = false;
				goRight = false;
				goLeft = true;
			}
			else{
				
			}
		}
	}

	void GetDirections(){
		if(startTile.row < endTile.row){
			left = false;
			right = true;
		}
		if(startTile.row > endTile.row){
			left = true;
			right = false;
		}
		if(startTile.row == endTile.row){
			left = false;
			right = false;
		}
		if(startTile.column > endTile.column){
			up = false;
			down = true;
		}
		if(startTile.column < endTile.column){
			up = true;
			down = false;
		}
		if(startTile.column == endTile.column){
			up = false;
			down = false;
		}
	}
}
