using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;
using HighlightingSystem;

public class TileCheck : MonoBehaviour {

	public bool occupied, valid, end, selected;
	public string occupier;
	public Renderer render;
	public TileHighlighter tileHighlighter;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		render = gameObject.GetComponent<Renderer> ();
		tileHighlighter = gameObject.GetComponent<TileHighlighter> ();
		if(tileHighlighter == null){
			tileHighlighter = gameObject.AddComponent<TileHighlighter>();
		}
		valid = false;
		occupied = false;
		end = false;
		selected = false;
	}
	
	void OnTriggerEnter(Collider other){
		occupier = other.gameObject.tag.ToString();
		occupied = true;
	}

	void OnTriggerExit(Collider other){
		occupier = "";
		occupied = false;
		valid = false;
	}

	// Update is called once per frame
	void Update () {
		if(valid && !occupied && player.GetComponent<NewPlayerController>().moving == false){
			render.material.color = Color.green;
			tileHighlighter.enabled = true;
		}
		else{
			render.material.color = Color.white;
			tileHighlighter.enabled = false;
		}
		if (occupied && occupier != "Player") {
			valid = false;
		}
		if(occupier == "Player"){
			valid = true;
		}
	}
}
