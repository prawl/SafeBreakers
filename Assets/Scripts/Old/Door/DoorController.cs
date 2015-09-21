using UnityEngine;
using System.Collections;

public class DoorControllerScript : MonoBehaviour {

	public static bool doorToggle = true;
	public GameObject entryWay;

	// Use this for initialization
	void Start () {
	/* 	IgnoreEntryWay (); */
	}
	
	// Update is called once per frame
	void Update () {
		ToggleDoor();
	}

	void IgnoreEntryWay(){
		Physics.IgnoreCollision (entryWay.GetComponent<Collider>(), GetComponent<Collider>());
	}

	void ToggleDoor(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if(hit.collider.name == "Door") {
					float zPos = new float();
					zPos = hit.collider.gameObject.transform.position.z;
					if (doorToggle){
						hit.collider.gameObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, zPos - 2);
						doorToggle = false;
					}else {
						hit.collider.gameObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, zPos + 2);
						doorToggle = true;
					}
				}
			}
		}	
	}
}
