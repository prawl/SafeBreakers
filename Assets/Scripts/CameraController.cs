using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject mainCamera;
	private GameObject mainCharacter;
	private Vector3 curPosition;
	private bool canMove = true;
	public float cameraSensitivity = 0.1f;
	// To prevent the player from panning the camera past where the level exist, we set a limit
	// to how far they can go in each direction
	public float cameraLimitLeft = 1.0f;
	public float cameraLimitDown = -15.0f;
	public float cameraLimitRight = 27.0f;
	public float cameraLimitUp = -12.0f;


	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per framedadawdawdsa
	void Update () {
		curPosition = mainCamera.transform.position;
		if (canMove) {
			MoveCamera ();
		}
	}

	private void MoveCamera() {
		DisableCameraMovement ();
		if (Input.GetKeyDown(KeyCode.A) && cameraLimitLeft < curPosition.x){
			mainCamera.transform.Translate(-curPosition.x * cameraSensitivity, 0, 0);
		}
		if (Input.GetKeyDown(KeyCode.W) && cameraLimitUp > curPosition.y){
			mainCamera.transform.Translate(0, -curPosition.y * cameraSensitivity, 0);
		}
		if (Input.GetKeyDown(KeyCode.S) && cameraLimitDown < curPosition.y){
			mainCamera.transform.Translate(0, curPosition.y * cameraSensitivity, 0);
		}
		if (Input.GetKeyDown(KeyCode.D) && cameraLimitRight > curPosition.x){
			mainCamera.transform.Translate(curPosition.x * cameraSensitivity, 0, 0);
		}
		EnableCameraMovement ();
	}

	public void CenterCameraOnPlayer(){

	}

	public void DisableCameraMovement() {
		canMove = false;
	}

	public void EnableCameraMovement(){
		canMove = true;
	}
}
