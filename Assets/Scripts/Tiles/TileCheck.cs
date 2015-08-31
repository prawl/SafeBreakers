using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;
using HighlightingSystem;

public class TileCheck : MonoBehaviour {

	public bool occupied, valid, end;
	public string occupier;
  public GameObject occupierObject;
	public Renderer render;
	public TileHighlighter tileHighlighter;

	void Start () {
		render = gameObject.GetComponent<Renderer> ();
		tileHighlighter = gameObject.GetComponent<TileHighlighter> ();
		if(tileHighlighter == null){
			tileHighlighter = gameObject.AddComponent<TileHighlighter>();
		}
    TileNotValid();
    TileNotOccupied();
		end = false;
	}

	void Update () {
		if(TileValid()){
			render.material.color = Color.green;
			tileHighlighter.enabled = true;
		}
		else{
			render.material.color = Color.white;
			tileHighlighter.enabled = false;
		}
		if (TileOccupied()) {
      TileNotValid();
		}
	}
	
	void OnTriggerEnter(Collider other){
		occupier = other.gameObject.tag;
		occupierObject = other.gameObject;
    TileIsOccupied();
    TileNotValid();
	}

	void OnTriggerExit(Collider other){
    TileNotOccupied();
	}

  public bool TileOccupied() {
    return occupied;
  }

  public void TileIsOccupied (){
    occupied = true;
  }

  public void TileNotOccupied() {
    occupied = false;
  }

  public bool TileValid(){
    return valid;
  }

  public void TileIsValid(){
    valid = true;
  }

  public void TileNotValid(){
    valid = false;
  }
}
