using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

[RequireComponent(typeof(CameraController))]
[RequireComponent(typeof(Animator))]
public class TileMovement : MonoBehaviour {

	public GameObject player;
	public TileSystem tileSystem;
	public Brush possibleLoc;
	public Brush defaultLoc;
	public TileIndex current;
	public float distance;
	public Vector3 temp;
	public Rigidbody rigid;
	public float speed;
	public bool move;
	private Animator playerAnimator = null;
	public GameObject highlightMouse;
	
	// Use this for initialization
	void Start () {
		playerAnimator = GetComponent<Animator>();
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		Vector3 temp = tileSystem.WorldPositionFromTileIndex(current, true);
		temp.z = -1;
		player.transform.position = temp;
		speed = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		CameraController.EnableCameraMovement ();
		HighlightMoves ();
		if (Input.GetMouseButtonDown(0)) {
			MoveToLocation ();
		}
		if(move){
			CameraController.DisableCameraMovement();
			player.transform.position = Vector3.MoveTowards (player.transform.position, temp, Time.deltaTime*speed);
			checkLoc ();
			CameraController.SetCameraFocus("Player");
		}
		highlightMouse.transform.position = highlightMouse .transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
		UpdateAnimation ();
	
	}

	void UpdateAnimation(){
		if (temp.x > player.transform.position.x) {
			playerAnimator.SetBool ("Walk_Right", true);
			playerAnimator.SetBool ("Walk_Left", false);
			playerAnimator.SetBool ("Walk_Forward", false);
			playerAnimator.SetBool ("Walk_Back", false);
		}
		if (temp.x < player.transform.position.x) {
			playerAnimator.SetBool ("Walk_Right", false);
			playerAnimator.SetBool ("Walk_Left", true);
			playerAnimator.SetBool ("Walk_Forward", false);
			playerAnimator.SetBool ("Walk_Back", false);
		}
		if (temp.y < player.transform.position.y) {
			playerAnimator.SetBool ("Walk_Right", false);
			playerAnimator.SetBool ("Walk_Left", false);
			playerAnimator.SetBool ("Walk_Forward", true);
			playerAnimator.SetBool ("Walk_Back", false);
		}
		if (temp.y > player.transform.position.y) {
			playerAnimator.SetBool ("Walk_Right", false);
			playerAnimator.SetBool ("Walk_Left", false);
			playerAnimator.SetBool ("Walk_Forward", false);
			playerAnimator.SetBool ("Walk_Back", true);
		}
		if (!move) {
			playerAnimator.SetBool ("Walk_Right", false);
			playerAnimator.SetBool ("Walk_Left", false);
			playerAnimator.SetBool ("Walk_Forward", false);
			playerAnimator.SetBool ("Walk_Back", false);
		}

	}

	void HighlightMoves(){
		current = tileSystem.ClosestTileIndexFromWorld (player.transform.position);
		
		for(int row = 0; row < tileSystem.RowCount; row++){
			for(int column = 0; column<tileSystem.ColumnCount; column++){
				if((int.Parse (current.row.ToString ()) + 1 == row) && (int.Parse (current.column.ToString ()) == column)
				   || (int.Parse (current.row.ToString ()) - 1 == row) && (int.Parse (current.column.ToString()) == column)
				   || (int.Parse (current.column.ToString ()) + 1 == column) && (int.Parse (current.row.ToString()) == row)
				   ||  (int.Parse (current.column.ToString ()) - 1 == column) && (int.Parse (current.row.ToString()) == row)){
					
					possibleLoc.Paint (tileSystem, row, column);
					
				}

				else{
					defaultLoc.Paint (tileSystem, row, column);
				}
			}
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
		move = CanMove (current, next, move);
		if (move == true) {
				temp = tileSystem.WorldPositionFromTileIndex (next, true);
				temp.z = -1;
				GameController.gameCount++;
		}
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(1f);			
	}
	
	bool CanMove(TileIndex current, TileIndex next, bool status){
		bool canMove = false;
		for(int row = 0; row < tileSystem.RowCount; row++){
			for(int column = 0; column<tileSystem.ColumnCount; column++){
				if(((int.Parse (current.row.ToString ()) + 1) == (int.Parse(next.row.ToString ())) && ((int.Parse (current.column.ToString ())) == (int.Parse(next.column.ToString ()))))
				   || ((int.Parse (current.row.ToString ()) - 1) == (int.Parse(next.row.ToString ())) && ((int.Parse (current.column.ToString ())) == (int.Parse(next.column.ToString ()))))
				   || ((int.Parse (current.column.ToString ()) + 1) == (int.Parse(next.column.ToString ())) && ((int.Parse (current.row.ToString ())) == (int.Parse(next.row.ToString ()))))
				   || ((int.Parse (current.column.ToString ()) - 1) == (int.Parse(next.column.ToString ())) && ((int.Parse (current.row.ToString ())) == (int.Parse(next.row.ToString ())))))
				{
					canMove = true;
				}
			}
		}
		
		return canMove;
	}
}
