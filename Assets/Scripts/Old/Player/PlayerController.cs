using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

//[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
	
	public GameObject player;
	public TileSystem tileSystem;
	public TileIndex current;
	public float distance;
	public Vector3 temp;
	public Rigidbody rigid;
	public float speed;
	public static bool move;
	public bool occupied;
	public static bool spotted;
	public GameController controllerScript;
	//public TileHighlight highlighter;
	public Animator playerAnimator;
	
	// Use this for initialization
	void Start () {
		spotted = false;
		playerAnimator = player.GetComponent<Animator> ();
		//highlighter = player.GetComponent<TileHighlight> ();
		controllerScript = player.GetComponent<GameController> ();
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		controllerScript.start = current;
		Vector3 temp = tileSystem.WorldPositionFromTileIndex(current, true);
		temp.y = 1.1f;
		player.transform.position = temp;
		speed = 1.0f;
		GetComponent<Renderer>().castShadows = true;
		GetComponent<Renderer>().receiveShadows = true;
	}
	
	// Update is called once per frame
	void Update () {
		Restart ();
		//CameraController.EnableCameraMovement ();	
		if(GameController.playerReady){
			if (Input.GetMouseButtonDown(0)) {
				if(GameController.gameCount == GameController.enemyCount && GameController.nextTurn == 0){
					MoveToLocation ();
				}
			}
		}
		if(move){
			//CameraController.DisableCameraMovement();
			player.transform.position = Vector3.MoveTowards (player.transform.position, temp, Time.deltaTime*speed);
			checkLoc ();
			//CameraController.SetCameraFocus("Player");
		}		
	}

	void Restart(){
		if(GameController.restart){
			Start ();
		}
	}

	void checkLoc(){
		if(player.transform.position == temp){
			move = false;
		}
	}

	void MoveToLocation(){
		move = false;
		temp = new Vector3 ();
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit);
		TileIndex next = tileSystem.ClosestTileIndexFromWorld (hit.point);
		//move = highlighter.CheckIfValid (next);
		//occupied = highlighter.CheckIfOccupied (next);
		if (move == true && occupied == false) {
			temp = tileSystem.WorldPositionFromTileIndex (next, true);
			temp.y = 1.1f;
			GameController.gameCount++;
		}
		else{
			temp = player.transform.position;
		}
	}
	
	IEnumerator wait(){
		yield return new WaitForSeconds(1f);			
	}
}
