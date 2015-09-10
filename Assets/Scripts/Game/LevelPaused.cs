using UnityEngine;
using System.Collections;

public class LevelPaused : MonoBehaviour {

	New_GameController gameCon;

	// Use this for initialization
	void Start () {
		gameCon = gameObject.GetComponent<New_GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameCon.levelLost || gameCon.levelPaused || gameCon.levelWon) {
			Time.timeScale = 0.0f;
		}
	}
}
