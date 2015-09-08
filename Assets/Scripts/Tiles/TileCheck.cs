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

  void Initialize() {
		player = GameObject.FindGameObjectWithTag ("Player");
		render = gameObject.GetComponent<Renderer> ();
		tileHighlighter = gameObject.GetComponent<TileHighlighter> ();
		if(tileHighlighter == null){
			tileHighlighter = gameObject.AddComponent<TileHighlighter>();
		}
  }

	void Start () {
    Initialize();
    ClearTile();
	}

	void Update () {
    HighlightTile();
    ValidateTile();
	}
	
	void OnTriggerEnter(Collider other){
    OccupyTile(other);
	}

	void OnTriggerExit(){
    ClearTile();
	}

  void OccupyTile(Collider other){
		occupier = other.gameObject.tag.ToString();
		occupied = true;
  }

  void ClearTile(){
		occupier = "";
		occupied = false;
		valid = false;
    end = false;
    selected = false;
  }

  void ValidateTile() {
		if (occupied && occupier != "Player") {
			valid = false;
		}
		if(occupier == "Player"){
			valid = true;
		}
  }

  void HighlightTile() {
		if(valid && !occupied && player.GetComponent<NewPlayerController>().moving == false){
			render.material.color = Color.green;
			tileHighlighter.enabled = true;
		}
		else{
			render.material.color = Color.white;
			tileHighlighter.enabled = false;
		}
  }
}
