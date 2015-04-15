using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;
using HighlightingSystem;

public class TileCheck : MonoBehaviour {

	public bool occupied, valid, end;
	public string occupier;
	public Renderer render;
	public TileHighlighter tileHighlighter;

	// Use this for initialization
	void Start () {
		render = gameObject.GetComponent<Renderer> ();
		tileHighlighter = gameObject.GetComponent<TileHighlighter> ();
		if(tileHighlighter == null){
			tileHighlighter = gameObject.AddComponent<TileHighlighter>();
		}
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
		if(valid){
			render.material.color = Color.green;
			tileHighlighter.enabled = true;
		}
		else{
			render.material.color = Color.white;
			tileHighlighter.enabled = false;
		}
		if (occupied) {
			valid = false;
		}
	}
}
