using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public static float cameraLimitLeft = 1.5f;
	public static float cameraLimitDown = -2.5f;
	public static float cameraLimitRight = 27.0f;
	public static float cameraLimitUp = -5.5f;	
	public static float dragSpeed = 30.0f;
	private static float yDistanceFromThePlayer = 5.0f; // The camera needs to be moved back so we can see the player, because target.position.y is the player position not the camera.
	private static GameObject mainCamera, defaultFocus;
	private static Transform target;
	private static Vector3 curPosition;
	private static bool canMove = true;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Moves the main camera's focus onto an object using its tag
	public static void SetCameraFocus(string Tag){
		defaultFocus = GameObject.FindGameObjectWithTag (Tag);
		target = defaultFocus.transform;
		mainCamera.transform.position = new Vector3 (target.position.x, target.position.y - yDistanceFromThePlayer, -4.7f);
	}

	public static bool AbleToMoveCamera() {
		return canMove;
	}
	
	public static void DisableCameraMovement() {
		canMove = false;
	}
	
	public static void EnableCameraMovement(){
		canMove = true;
	}

	public static void PanCamera() {
		//Right mouse btn is held down
		if (Input.GetMouseButton(1)){
			OnMouseDrag();
		}
		//Right mouse btn is let go
		if (Input.GetMouseButtonUp(1)) {
			SetCameraFocus("Player");
		}
	}

	private static void OnMouseDrag(){
		mainCamera.transform.position = new Vector3(Mathf.Clamp(mainCamera.transform.position.x, cameraLimitLeft, cameraLimitRight), mainCamera.transform.position.y, Mathf.Clamp(mainCamera.transform.position.z, cameraLimitUp, cameraLimitDown));
		mainCamera.transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, 0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed);
	}
}
