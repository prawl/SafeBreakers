using UnityEngine;
using System.Collections;

public class Door_ButtonController : MonoBehaviour {

	public bool valid;
	public bool wrong;
	public int buttonNum;

	// Use this for initialization
	void Start () {
		selected = false;
		valid = false;
		wrong = false;
		buttonNum = 0;
	}

	public void SetButtonNum(int i){
		buttonNum = i;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
