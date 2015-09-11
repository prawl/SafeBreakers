using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_TileCheck : MonoBehaviour {

	public TileSystem tileSystem;
	public TileIndex currentTile;
	public GameObject player;
	public GameObject[] enemies, obstacles;
	public string occupier;
	public bool occupied, valid, end, selected;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("SB_Player");
		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		tileSystem = player.GetComponent<SB_PlayerController> ().tileSystem;
		currentTile = tileSystem.ClosestTileIndexFromWorld (transform.position);
		occupier = "";
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfOccupied ();
	}

	void CheckIfOccupied(){
		if (player.GetComponent<SB_PlayerController>().currentTile == currentTile) {
			occupied = true;
			occupier = "Player";
		}
		if (player.GetComponent<SB_PlayerController>().currentTile != currentTile) {
			occupied = false;
			occupier = "";
		}
		for (int i = 0; i < obstacles.Length; i++) {
			if(tileSystem.ClosestTileIndexFromWorld(obstacles[i].transform.position) == currentTile){
				occupied = true;
				occupier = "Obstacle";
			}
		}
	}
}
