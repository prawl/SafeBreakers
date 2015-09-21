using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;
using HighlightingSystem;

public class TileCheck : MonoBehaviour {

	public string occupier;
	public bool occupied;
	public bool valid;
	public bool end;

	void Start(){
		occupier = null;
	}
}
