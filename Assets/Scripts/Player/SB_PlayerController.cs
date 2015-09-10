using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_PlayerController : MonoBehaviour {

	public TileSystem tileSystem;
	public TileIndex currentTile, nextTile;
	private Vector3 currentLoc, nextLoc;
	private CharacterController controller;
	private float speed = 1;
	public bool moving;
	public Button upButton, downButton, rightButton, leftButton;
	private TileData upTile, downTile, rightTile, leftTile;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Player";
		GetComponent<Renderer> ().castShadows = true;
		GetComponent<Renderer> ().receiveShadows = true;
		controller = gameObject.GetComponent<CharacterController> ();
		moving = false;
		currentTile = tileSystem.ClosestTileIndexFromWorld (transform.position);
		currentLoc = tileSystem.GetTile (currentTile).gameObject.transform.position;
		currentLoc.y = .75f;
		transform.position = currentLoc;
		//Set occupied for tile to true
	}
	
	// Update is called once per frame
	void Update () {
		UpdateButtons ();
		//LockYAxis ();
		if(!moving){
			GetValidTiles ();
		}
		if (moving) {
			MoveToNextTile();
		}
	}

	void LockYAxis(){
		Vector3 temp = transform.position;
		temp.y = .75f;
		transform.position = temp;
	}

	void UpdateButtons(){
		if (moving) {
			upButton.interactable = false;
			downButton.interactable = false;
			rightButton.interactable = false;
			leftButton.interactable = false;
		}
		if (!moving) {
			try{
				if(upTile.gameObject.GetComponent<SB_TileCheck>().valid){
					upButton.interactable = true;
				}
				if(!upTile.gameObject.GetComponent<SB_TileCheck>().valid || currentTile.row == 0){
					upButton.interactable = false;
				}
				if(downTile.gameObject.GetComponent<SB_TileCheck>().valid){
					downButton.interactable = true;
				}
				if(!downTile.gameObject.GetComponent<SB_TileCheck>().valid || currentTile.row == tileSystem.RowCount-1){
					downButton.interactable = false;
				}
				if(rightTile.gameObject.GetComponent<SB_TileCheck>().valid){
					rightButton.interactable = true;
				}
				if(!rightTile.gameObject.GetComponent<SB_TileCheck>().valid || currentTile.column == tileSystem.ColumnCount-1){
					rightButton.interactable = false;
				}
				if(leftTile.gameObject.GetComponent<SB_TileCheck>().valid){
					leftButton.interactable = true;
				}
				if(!leftTile.gameObject.GetComponent<SB_TileCheck>().valid || currentTile.column == 0){
					leftButton.interactable = false;
				}
				else{
					upButton.interactable = true;
					downButton.interactable = true;
					rightButton.interactable = true;
					leftButton.interactable = true;
				}
			}
			catch{}
		}
	}

	public void GoUp(){
		GetNextTile (tileSystem.ClosestTileIndexFromWorld (upTile.gameObject.transform.position));
	}

	public void GoDown(){
		GetNextTile (tileSystem.ClosestTileIndexFromWorld (downTile.gameObject.transform.position));
	}

	public void GoRight(){
		GetNextTile (tileSystem.ClosestTileIndexFromWorld (rightTile.gameObject.transform.position));
	}

	public void GoLeft(){
		GetNextTile (tileSystem.ClosestTileIndexFromWorld (leftTile.gameObject.transform.position));
	}


	void GetNextTile(TileIndex next){
		nextTile = next;
		nextLoc = tileSystem.GetTile (nextTile).gameObject.transform.position;
		moving = true;
	}

	void GetValidTiles(){
		try{
			upTile = tileSystem.GetTile (currentTile.row - 1, currentTile.column);
		}
		catch{}
		try{
			downTile = tileSystem.GetTile (currentTile.row + 1, currentTile.column);
		}
		catch{}
		try{
			rightTile = tileSystem.GetTile (currentTile.row, currentTile.column + 1);
		}
		catch{}
		try{
			leftTile = tileSystem.GetTile (currentTile.row, currentTile.column - 1);
		}
		catch{}

		try{
			if (upTile.gameObject.GetComponent<SB_TileCheck> ().occupied && upTile.gameObject.GetComponent<SB_TileCheck> ().occupier != "Player") {
				upTile.gameObject.GetComponent<SB_TileCheck>().valid = false;
			}
			else if (downTile.gameObject.GetComponent<SB_TileCheck> ().occupied && downTile.gameObject.GetComponent<SB_TileCheck> ().occupier != "Player") {
				downTile.gameObject.GetComponent<SB_TileCheck>().valid = false;
			}
			else if (rightTile.gameObject.GetComponent<SB_TileCheck> ().occupied && rightTile.gameObject.GetComponent<SB_TileCheck> ().occupier != "Player") {
				rightTile.gameObject.GetComponent<SB_TileCheck>().valid = false;
			}
			else if (leftTile.gameObject.GetComponent<SB_TileCheck> ().occupied && leftTile.gameObject.GetComponent<SB_TileCheck> ().occupier != "Player") {
				leftTile.gameObject.GetComponent<SB_TileCheck>().valid = false;
			}
			else{
				upTile.gameObject.GetComponent<SB_TileCheck>().valid = true;
				downTile.gameObject.GetComponent<SB_TileCheck>().valid = true;
				rightTile.gameObject.GetComponent<SB_TileCheck>().valid = true;
				leftTile.gameObject.GetComponent<SB_TileCheck>().valid = true;
			}
		}
		catch{}
	}

	public bool V3Equal(Vector3 a, Vector3 b){
		return Vector3.SqrMagnitude(a-b) < 0.001;
	}

	void MoveToNextTile(){
		if (V3Equal(transform.position, nextLoc)) {
			transform.position = nextLoc;
			currentTile = nextTile;
			moving = false;
		}
		nextLoc.y = .75f;
		Vector3 moveDiff = nextLoc - transform.position;
		Vector3 moveDir = moveDiff.normalized * speed * Time.deltaTime;
		if(moveDir.sqrMagnitude < moveDiff.sqrMagnitude){
			controller.Move (moveDir);
		}
		else{
			controller.Move (moveDiff);
		}
	}
}
