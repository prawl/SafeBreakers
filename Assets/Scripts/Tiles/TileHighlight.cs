using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class TileHighlight : MonoBehaviour {

	public Brush possibleLoc; 
	public Brush defaultLoc; 
	public int tempGameCount;
	public GameObject player; 
	public PlayerController playerController; 
	public TileSystem tileSystem; 
	public TileIndex current; 

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		HighlightMoves ();
	}
	
	void HighlightMoves(){
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		if(!GameController.playerReady || GameController.EnemiesReadyToMove() == true){
			for(int row = 0; row < tileSystem.RowCount; row++){
				for(int column = 0; column<tileSystem.ColumnCount; column++){
					defaultLoc.Paint (tileSystem, row, column);
				}
			}
		}
		
		if(GameController.PlayerReady() && GameController.ActorsDoneMoving()){
			if(!PlayerController.CanMove()){
				for(int row = 0; row < tileSystem.RowCount; row++){
					for(int column = 0; column < tileSystem.ColumnCount; column++){
						if((int.Parse (current.row.ToString ()) + 1 == row) && (int.Parse (current.column.ToString ()) == column)
						   || (int.Parse (current.row.ToString ()) - 1 == row) && (int.Parse (current.column.ToString()) == column)
						   || (int.Parse (current.column.ToString ()) + 1 == column) && (int.Parse (current.row.ToString()) == row)
						   ||  (int.Parse (current.column.ToString ()) - 1 == column) && (int.Parse (current.row.ToString()) == row)){
							
							TileIndex temp = tileSystem.ClosestTileIndexFromWorld (tileSystem.GetTile (row, column).gameObject.transform.position);
							bool end = CheckIfEnd(temp);
							bool occupied = CheckIfOccupied(temp);
							
							if(end == false && occupied == false){
								UpdateValid (temp, true);
								possibleLoc.Paint (tileSystem, row, column);
							}
							if(occupied == true){
								UpdateValid (temp, false);
								defaultLoc.Paint (tileSystem, row, column);
							}
						}
					}
				}
			}
			else{
				for(int row = 0; row < tileSystem.RowCount; row++){
					for(int column = 0; column < tileSystem.ColumnCount; column++){
						defaultLoc.Paint (tileSystem, row, column);
					}
				}
			}
		}
	}
	
	public bool CheckIfEnd(TileIndex next){
		TileData tile = tileSystem.GetTile (next.row, next.column);
		GameObject tileObject = tile.gameObject;
		TileCheck validTile = tileObject.GetComponent<TileCheck>();
		return validTile.end;
	}

	public bool CheckIfValid(TileIndex next){
		TileData tile = tileSystem.GetTile (next.row, next.column);
		GameObject tileObject = tile.gameObject;
		TileCheck validTile = tileObject.GetComponent<TileCheck>();
		return validTile.isValid;
	}

	public bool CheckIfOccupied(TileIndex next){
		TileData tile = tileSystem.GetTile (next.row, next.column);
		GameObject tileObject = tile.gameObject;
		TileCheck check = tileObject.GetComponent<TileCheck> ();
		bool status = check.occupied;
		return status;
	}

	void UpdateValid(TileIndex next, bool valid){
		if (valid) {
			TileData tile = tileSystem.GetTile (next.row, next.column);
			GameObject tileObject = tile.gameObject;
			TileCheck validTile = tileObject.GetComponent<TileCheck>();
			validTile.isValid = true;
		}
		else{
			TileData tile = tileSystem.GetTile (next.row, next.column);
			GameObject tileObject = tile.gameObject;
			TileCheck validTile = tileObject.GetComponent<TileCheck>();
			validTile.isValid = false;
		}
	}
}
