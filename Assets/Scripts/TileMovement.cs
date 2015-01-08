using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class TileMovement : MonoBehaviour {

	public GameObject player;
	public TileSystem tileSystem;
	public Brush possibleLoc;
	public Brush defaultLoc;
	public TileIndex current;
	public float distance;
	public Vector3 temp;
	public Rigidbody rigid;
	public float speed;
	public bool move;
	
	// Use this for initialization
	void Start () {
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		Vector3 temp = tileSystem.WorldPositionFromTileIndex(current, true);
		player.transform.position = temp;
		speed = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
		HighlightMoves ();
		if (Input.GetMouseButtonDown(0)) {
			MoveToLocation ();
		}
		if(move){
			player.transform.position = Vector3.Lerp (player.transform.position, temp, Time.deltaTime*speed);
			checkLoc ();
		}

	}
	
	void HighlightMoves(){
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		
		for(int row = 0; row < tileSystem.RowCount; row++){
			for(int column = 0; column<tileSystem.ColumnCount; column++){
				if((int.Parse (current.row.ToString ()) + 1 == row) && (int.Parse (current.column.ToString ()) == column)
				   || (int.Parse (current.row.ToString ()) - 1 == row) && (int.Parse (current.column.ToString()) == column)
				   || (int.Parse (current.column.ToString ()) + 1 == column) && (int.Parse (current.row.ToString()) == row)
				   ||  (int.Parse (current.column.ToString ()) - 1 == column) && (int.Parse (current.row.ToString()) == row)){
					
					possibleLoc.Paint (tileSystem, row, column);
					
				}

				else{
					defaultLoc.Paint (tileSystem, row, column);
				}
			}
		}
	}

	void checkLoc(){
		if(player.transform.position == temp){
			move = false;
		}
	}

	void MoveToLocation(){
		move = false;
		temp = new Vector3 ();
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		TileIndex next = tileSystem.ClosestTileIndexFromRay (ray);
		move = CanMove (current, next, move);
		if (move == true) {
						temp = tileSystem.WorldPositionFromTileIndex (next, true);
						temp.z = -1;
		}
			//player.transform.position = temp;
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(1f);			
	}
	
	bool CanMove(TileIndex current, TileIndex next, bool status){
		bool canMove = false;
		for(int row = 0; row < tileSystem.RowCount; row++){
			for(int column = 0; column<tileSystem.ColumnCount; column++){
				if(((int.Parse (current.row.ToString ()) + 1) == (int.Parse(next.row.ToString ())) && ((int.Parse (current.column.ToString ())) == (int.Parse(next.column.ToString ()))))
				   || ((int.Parse (current.row.ToString ()) - 1) == (int.Parse(next.row.ToString ())) && ((int.Parse (current.column.ToString ())) == (int.Parse(next.column.ToString ()))))
				   || ((int.Parse (current.column.ToString ()) + 1) == (int.Parse(next.column.ToString ())) && ((int.Parse (current.row.ToString ())) == (int.Parse(next.row.ToString ()))))
				   || ((int.Parse (current.column.ToString ()) - 1) == (int.Parse(next.column.ToString ())) && ((int.Parse (current.row.ToString ())) == (int.Parse(next.row.ToString ())))))
				{
					canMove = true;
				}
			}
		}
		
		return canMove;
	}
}
