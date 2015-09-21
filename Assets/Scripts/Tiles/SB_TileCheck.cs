using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_TileCheck : MonoBehaviour {

	public TileSystem tileSystem;
	public TileIndex currentTile;
	public GameObject player, occupier;
	public GameObject[] enemies, obstacles;
	public bool occupied, valid, end, selected;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("SB_Player");
		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        tileSystem = player.GetComponent<SB_PlayerController> ().tileSystem;
		currentTile = tileSystem.ClosestTileIndexFromWorld (transform.position);
        occupier = gameObject;
	}
	
    void OnTriggerEnter(Collider collider)
    {
        occupier = collider.gameObject;
        occupied = true;
    }

    void OnTriggerExit(Collider collider)
    {
        occupier = gameObject;
        occupied = false;
    }

	// Update is called once per frame
	void Update () {

	}
}
