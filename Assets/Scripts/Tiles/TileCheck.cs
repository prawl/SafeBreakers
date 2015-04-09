using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class TileCheck : TileHighlight {

	public bool occupied, valid, end, isOccupied, isValid;
	public string occupier;

	// Use this for initialization
	void Start () {
		valid = false;
		occupied = false;
		end = false;
	}
	
	void OnTriggerEnter(Collider other){
		occupier = other.gameObject.tag;
		occupied = true;
		valid = false;
	}

	void OnTriggerExit(Collider other){
		occupied = false;
	}

	// Update is called once per frame
	void Update () {

	}
}
