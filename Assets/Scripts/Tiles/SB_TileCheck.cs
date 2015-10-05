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

	}
}
