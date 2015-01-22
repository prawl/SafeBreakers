using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public float cameraLimitLeft = 1.5f;
	public float cameraLimitDown = -2.0f;
	public float cameraLimitRight = 27.0f;
	public float cameraLimitUp = -5.0f;	
	public float dragSpeed = 30.0f;
	private GameObject mainCamera, defaultFocus;
	private Transform target;
	private Vector3 curPosition;
	private bool canMove = true;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		curPosition = mainCamera.transform.position;
		if (AbleToMoveCamera()) {
			PanCamera ();
		}
	}
	
	// Moves the main camera's focus onto an object using it's tag that exist in the scene
	public void SetCameraFocus(string Tag){
		defaultFocus = GameObject.FindGameObjectWithTag (Tag);
		target = defaultFocus.transform;
		mainCamera.transform.position = new Vector3 (target.position.x, -11.5f, -4.7f);
	}

	public bool AbleToMoveCamera() {
		return canMove;
	}
	
	public void DisableCameraMovement() {
		canMove = false;
	}
	
	public void EnableCameraMovement(){
		canMove = true;
	}

	private void PanCamera() {
		//Right mouse btn is held down
		if (Input.GetMouseButton(1)){
			OnMouseDrag();
		}
		//Right mouse btn is let go
		if (Input.GetMouseButtonUp(1)) {
			SetCameraFocus("Player");
		}
	}

	private void OnMouseDrag(){
		mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, cameraLimitLeft, cameraLimitRight), mainCamera.transform.position.y, Mathf.Clamp(mainCamera.transform.position.z, cameraLimitUp, cameraLimitDown));
		mainCamera.transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, 0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed);
	}
}
