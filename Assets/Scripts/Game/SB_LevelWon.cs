/*
Script Name: SB_LevelWon.cs
Author: Bradley M. Butts
Last Modified: 9-17-2015
Description: This script handles the UI functions that occur if the player wins the level.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SB_LevelWon : MonoBehaviour {
	
	private SB_GameController gameCon;
	public Image winWindow;
	public Text winCount;
	
	// Use this for initialization
	void Start () {
		gameCon = gameObject.GetComponent<SB_GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameCon.isLevelPaused && gameCon.isLevelWon) {
			winWindow.gameObject.SetActive(true);
			winCount.text = "You won in " + gameCon.playerCount + " turns!";
		}
		else{
			winWindow.gameObject.SetActive(false);
		}
	}
	
    //Public functions that's assigned to the Replay button in the UI window.
	public void ResetLevel(){
		Application.LoadLevel (Application.loadedLevelName);
	}
}
