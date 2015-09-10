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
	public New_GameController gameCon;
	public Camera camera;
	private Vector3 temp, nextLoc;
	private CharacterController controller;
	private float speed = 1;
	public bool moving;

	// Use this for initialization
	void Start() {
		AddRigidBody ();
		gameObject.tag = "Player";
		player = GameObject.FindGameObjectWithTag ("Player");
		gameCon = gameObject.GetComponent<New_GameController> ();
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
	
	// Update is called once per frame
	void Update () {
		GetValidTiles ();
		GetNextTile ();
		DefaultTiles ();
	}

	void AddRigidBody(){
		Rigidbody rigid = gameObject.AddComponent<Rigidbody> ();
		rigid.isKinematic = true;
		rigid.useGravity = false;
	}

	void GetNextTile(){
		if(Input.GetMouseButtonDown (0) && !moving && gameCon.gameCount > gameCon.playerCount && !gameCon.levelPaused){
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			next = tileSystem.ClosestTileIndexFromWorld(hit.point);
			if(tileSystem.GetTile (next).gameObject.GetComponent<TileCheck>().valid && tileSystem.GetTile (next).gameObject.GetComponent<TileCheck>().occupier != "Player"){
				moving = true;
			}
		}
		if(tileSystem.GetTile (next).gameObject.GetComponent<TileCheck>().valid && moving){
			nextLoc = tileSystem.GetTile (next).gameObject.transform.position;
			MoveToNextTile ();
		}
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude (a - b) < 0.001;
	}

	void MoveToNextTile(){
		nextLoc.y = 1;
		if(!V3Equal (nextLoc, transform.position)){
			Vector3 dir = (nextLoc - transform.position).normalized;
			dir *= Time.fixedDeltaTime * speed;
			controller.Move (dir);
		}
		if(V3Equal(nextLoc, transform.position)){
			current = next;
			transform.position = nextLoc;
			moving = false;
			gameCon.playerCount++;
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
				if(tileSystem.GetTile (current.row + 1, current.column).gameObject.GetComponent<TileCheck>().occupied && tileSystem.GetTile (current.row + 1, current.column).gameObject.GetComponent<TileCheck>().occupier == "Player"){
					tileSystem.GetTile (current.row + 1, current.column).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
			try{
				if(!tileSystem.GetTile (current.row - 1, current.column).gameObject.GetComponent<TileCheck>().occupied){
					tileSystem.GetTile (current.row - 1, current.column).gameObject.GetComponent<TileCheck>().valid = true;
				}
				if(tileSystem.GetTile (current.row - 1, current.column).gameObject.GetComponent<TileCheck>().occupied && tileSystem.GetTile (current.row - 1, current.column).gameObject.GetComponent<TileCheck>().occupier == "Player"){
					tileSystem.GetTile (current.row - 1, current.column).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
			try{
				if(!tileSystem.GetTile (current.row, current.column + 1).gameObject.GetComponent<TileCheck>().occupied){
					tileSystem.GetTile (current.row, current.column + 1).gameObject.GetComponent<TileCheck>().valid = true;
				}
				if(tileSystem.GetTile (current.row, current.column + 1).gameObject.GetComponent<TileCheck>().occupied && tileSystem.GetTile (current.row, current.column + 1).gameObject.GetComponent<TileCheck>().occupier == "Player"){
					tileSystem.GetTile (current.row, current.column + 1).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
			try{
				if(!tileSystem.GetTile (current.row, current.column - 1).gameObject.GetComponent<TileCheck>().occupied){
					tileSystem.GetTile (current.row, current.column - 1).gameObject.GetComponent<TileCheck>().valid = true;
				}
				if(tileSystem.GetTile (current.row, current.column - 1).gameObject.GetComponent<TileCheck>().occupied && tileSystem.GetTile (current.row, current.column - 1).gameObject.GetComponent<TileCheck>().occupier == "Player"){
					tileSystem.GetTile (current.row, current.column - 1).gameObject.GetComponent<TileCheck>().valid = true;
				}
			}
			catch{}
		}
	}
}
