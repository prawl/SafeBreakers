using UnityEngine;
using System.Collections;

public class Wall_Fade : MonoBehaviour {

	public GameObject wall;
	public Material faded;
	public Material normal;
	public Renderer rend;
	public bool insideRoom;

	// Use this for initialization
	void Start () {
		rend = wall.GetComponent<Renderer> ();
		rend.material = normal;
	}

	void OnTriggerEnter(Collider collider){
		if(insideRoom){
			insideRoom = false;
			rend.material = normal;
		}
		else{
			insideRoom = true;
			rend.material = faded;
		}
	}

	void OnTriggerExit(Collider collider){

	}

	// Update is called once per frame
	void Update () {

	}
}
