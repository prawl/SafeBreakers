/*
Script Name: SB_tILeCheck.cs
Author: Bradley M. Butts
Last Modified: 10-19-2015
Description: This script checks if any gameobjects are colliding with a tile. If so, the tile is listed as occupied
             and the variable occupier is the gameobjects tag. This script also adds a basic visual effect if the 
             respective tile is the end level tile
*/
using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_TileCheck : MonoBehaviour {

	public GameObject occupier;
    public bool occupied, valid;
    public TileIndex currentTile;
    private TileSystem tileSystem;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        tileSystem = player.GetComponent<SB_PlayerController>().tileSystem;
        currentTile = tileSystem.ClosestTileIndexFromWorld(gameObject.transform.position);
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
        //Looking for visual to assign to the end tile. Add another wall with a door? 
        /*if(currentTile == player.GetComponent<SB_GameController>().endTile)
        {

        }*/
	}
}
