using UnityEngine;
using System.Collections;
using Rotorz.Tile;

public class NpcController : MonoBehaviour {

	public GameObject enemy;
	public TileSystem tileSystem;
	private TileIndex current;
	private Vector3 temp;
	private float speed;
	private bool move;
	private int north, south, east, west;
	public float secondsBetweenMoving = 3;
	private float nextMove = 0;



	// Use this for initialization
	void Start () {
		// gameObject defaults to the current gameObject the scripts attached to...
		gameObject.renderer.material.color = Color.red;
		current = tileSystem.ClosestTileIndexFromWorld (enemy.transform.position);
		Vector3 temp = tileSystem.WorldPositionFromTileIndex(current, true);
		enemy.transform.position = temp;
	}
	
	// Update is called once per frame
	void Update () {

		// Check to see if x amount of seconds has passed
		if (Time.time > nextMove) {
			MoveToLocation ();
			nextMove = Time.time + secondsBetweenMoving;
		}
				
		if(move){
			enemy.transform.position = temp;
			UpdateLocation ();
		}
	}

	void MoveToLocation(){
		temp = new Vector3 ();
		// Get the current Coordinate in the format row: 1, column: 1
		current = tileSystem.ClosestTileIndexFromWorld (enemy.transform.position);
		// Randomly generate the next move
		TileIndex next = GenerateNextMove(current);
		// Determine if we can make a valid move... 
		move = CanMove (current, next, move);
		if (move == true) {
			temp = tileSystem.WorldPositionFromTileIndex (next, true);
			temp.z = -1;
		}
	}

	TileIndex GenerateNextMove(TileIndex current){
		// Get the surrounding 4 tiles, or the possible places to move
		west = current.row + 1;
		east = current.row - 1;
		north = current.column + 1;
		south = current.column - 1;

		float randomValue = Random.value;
		if (randomValue > .75) {
			current.row = west;
		}
		if ((randomValue <= .75) && (randomValue > .50)){
			current.row = east;
		}
		if ((randomValue <= .50) && (randomValue > .25)){
			current.column = north;
		}
		if ((randomValue < .25)){
			current.column = south;
		}
		return current;
    }

	void UpdateLocation(){
		if(enemy.transform.position == temp){
			move = false;
		}
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
