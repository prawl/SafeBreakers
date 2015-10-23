/*
Script Name: SB_LevelLost.cs
Author: Bradley M. Butts
Last Modified: 9-12-2015
Description: This script handles the UI functions that occur if the player loses the level
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SB_LevelPaused : MonoBehaviour {

	private SB_GameController gameCon;
	public Image pauseWindow;

	// Use this for initialization
	void Start () {
		gameCon = gameObject.GetComponent<SB_GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameCon.isLevelPaused && !gameCon.isLevelLost) {
			pauseWindow.gameObject.SetActive(true);
		}
		else{
			pauseWindow.gameObject.SetActive(false);
		}
	}

    //Public function assigned to the Replay button attached to the UI window
	public void ResetLevel(){
		Application.LoadLevel (Application.loadedLevelName);
	}
}
