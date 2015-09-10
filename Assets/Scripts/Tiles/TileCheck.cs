using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;
using HighlightingSystem;

public class TileCheck : MonoBehaviour {

	private TileSystem tileSystem;
	public bool occupied, valid, end, selected;
	public string occupier;
	public Renderer render;
	public TileHighlighter tileHighlighter;
	private GameObject player;
	private New_GameController gameCon;
	private TileIndex playerLocation, tileLocation;

	// Use this for initialization
	void Start() {
		//BoxCollider collider = gameObject.AddComponent<BoxCollider> ();
		//collider.isTrigger = true;
		player = GameObject.FindGameObjectWithTag ("Player");
		gameCon = player.GetComponent<New_GameController> ();
		tileSystem = player.GetComponent<NewPlayerController> ().tileSystem;
		render = gameObject.GetComponent<Renderer> ();
		tileHighlighter = gameObject.GetComponent<TileHighlighter> ();
		if(tileHighlighter == null){
			tileHighlighter = gameObject.AddComponent<TileHighlighter>();
		}
		tileLocation = tileSystem.ClosestTileIndexFromWorld (gameObject.transform.position);
		valid = false;
		occupied = false;
		end = false;
		selected = false;
		occupier = "";
	}

	void OnCollisionEnter(Collision other){
		occupier = other.gameObject.tag.ToString();
		occupied = true;
	}

	void OnCollisionExit(Collision other){
		occupier = "";
		occupied = false;
		valid = false;
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
		playerLocation = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		if(valid && !occupied && player.GetComponent<NewPlayerController>().moving == false && !gameCon.levelPaused){
			render.material.color = Color.green;
			tileHighlighter.enabled = true;
		}
		else{
			render.material.color = Color.white;
			tileHighlighter.enabled = false;
		}
		if ((occupied && occupier != "Player")) {
			valid = false;
		}
		if(occupier == "Player"){
			valid = true;
		}
		if(occupied && occupier == ""){
			occupied = false;
		}
		if (occupied && tileLocation == playerLocation) {
			occupier = "Player";
		}
		if (!occupied && tileLocation != playerLocation) {
			occupier = "";
		}
	}
}
