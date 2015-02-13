using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class TileCheck : MonoBehaviour {

	public bool occupied;

	// Use this for initialization
	void Start () {
		occupied = false;
	}

	void OnTriggerEnter(Collider other){
		occupied = true;
	}

	void OnTriggerExit(Collider other){
		occupied = false;
	}

	// Update is called once per frame
	void Update () {

	}
}
