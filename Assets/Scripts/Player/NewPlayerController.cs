using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

[RequireComponent(typeof(Animator))]
public class NewPlayerController : MonoBehaviour {

	public GameObject player;
	public TileSystem tileSystem;
	public TileIndex current, next;
	public Camera camera;
	private Vector3 temp;
	private CharacterController controller;
	private float speed = 1;
	private bool moving;
	private bool mouseInput;

	// Use this for initialization
	void Start () {
    EnableMouseInput();
		player = GameObject.FindGameObjectWithTag ("Player");
		GetComponent<Renderer>().castShadows = true;
		GetComponent<Renderer>().receiveShadows = true;
		controller = gameObject.GetComponent<CharacterController> ();
		moving = false;
		current = tileSystem.ClosestTileIndexFromWorld (transform.position);
		temp = tileSystem.GetTile (current).gameObject.transform.position;
		temp.y = 1;
		transform.position = temp;
		tileSystem.GetTile (current).gameObject.GetComponent<TileCheck>().occupied = true;
	}

	void Update() {
		GetValidTiles();
		GetNextTile();
		DefaultTiles();
	}

  public bool Moving(){
    return moving;
  }

  public void DisableMovement(){
    moving = false;
  }

  public void EnableMovement(){
    moving = true;
  }
	
  public bool AllowMouseInput(){
    return mouseInput;
  }

  public void DisableMouseInput(){
    mouseInput = false;
  }

  public void EnableMouseInput(){
    mouseInput = true;
  }

	void GetNextTile(){
    if(Input.GetMouseButtonDown(0) && AllowMouseInput()) {
      Ray ray = camera.ScreenPointToRay (Input.mousePosition);
      RaycastHit hit;
      Physics.Raycast(ray, out hit);
      next = tileSystem.ClosestTileIndexFromWorld(hit.point);
    }
    if(tileSystem.GetTile(next).gameObject.GetComponent<TileCheck>().valid){
      MoveToNextTile();
    }
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude (a - b) < 0.001;
	}

	void MoveToNextTile(){
    DisableMouseInput();
		Vector3 nextLoc = tileSystem.GetTile (next).gameObject.transform.position;
		nextLoc.y = 1;
		if(!V3Equal (nextLoc, transform.position)){
      EnableMovement();
			Vector3 dir = (nextLoc - transform.position).normalized;
			dir *= Time.fixedDeltaTime * speed;
			controller.Move (dir);
		}
		if(V3Equal(nextLoc, transform.position)){
			tileSystem.GetTile (current).gameObject.GetComponent<TileCheck>().occupied = false;
			current = next;
			tileSystem.GetTile (current).gameObject.GetComponent<TileCheck>().occupied = true;
			transform.position = nextLoc;
      DisableMovement();
      EnableMouseInput();
		}
	}

	void DefaultTiles(){
		if(moving){
			for(int i = 0; i < tileSystem.RowCount; i++){
				for(int j = 0; j < tileSystem.ColumnCount; j ++){
					if(tileSystem.GetTile (i, j) != tileSystem.GetTile(next)){
						tileSystem.GetTile (i, j).gameObject.GetComponent<TileCheck>().valid = false;
					}
				} 
			}
		}
	}

	void GetValidTiles(){
		if(!moving){
			try{
				if(!tileSystem.GetTile (current.row + 1, current.column).gameObject.GetComponent<TileCheck>().occupied){
					tileSystem.GetTile (current.row + 1, current.column).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
			try{
				if(!tileSystem.GetTile (current.row - 1, current.column).gameObject.GetComponent<TileCheck>().occupied){
					tileSystem.GetTile (current.row - 1, current.column).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
			try{
				if(!tileSystem.GetTile (current.row, current.column + 1).gameObject.GetComponent<TileCheck>().occupied){
					tileSystem.GetTile (current.row, current.column + 1).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
			try{
				if(!tileSystem.GetTile (current.row, current.column - 1).gameObject.GetComponent<TileCheck>().occupied){
					tileSystem.GetTile (current.row, current.column - 1).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
		}
	}
}
