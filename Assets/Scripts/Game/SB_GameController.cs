/*
Script Name: SB_PlayerController.cs
Author: Bradley M. Butts
Last Modified: 9-24-2015
Description: This script handles all game functions for a playable level.
             This includes the gameCount, playerCount, and enemyCount.
             The script also checks to see if the player has paused, won, or lost the current level; prompting the GUI window.
             The script also starts a playable mini-game
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_GameController : MonoBehaviour {

	public int playerCount, enemyCount, gameCount, enemyDone;
	public GameObject[] enemies;
    public GameObject player;
    public bool isLevelWon, isLevelLost, isLevelPaused, canInteract, alarmMode;
    public bool minigameOn;
	public int numOfEnemies;
	public TileIndex startTile, endTile;
    public Button pauseButton, actionButton;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCount = 0;enemyCount = 0;gameCount = 1;
		isLevelWon = false;isLevelPaused = false;isLevelLost = false;minigameOn = false;canInteract = false;alarmMode = false;
		numOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
        GetComponent<SB_AlarmMode>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (minigameOn)
        {
            pauseButton.interactable = false;
        }
        else
        {
            pauseButton.interactable = true;
        }
        if (canInteract)
        {
            actionButton.interactable = true;   
        }
        else
        {
            actionButton.interactable = false;
        }
		if (enemyDone == numOfEnemies && enemyCount < playerCount) {
			enemyDone = 0;
			enemyCount++;
		}
		if(enemyCount == playerCount && playerCount == gameCount){
			gameCount++;
		}
        if (alarmMode)
        {
            GetComponent<SB_AlarmMode>().enabled = true;
        }
		CheckGameLost ();
		CheckGameWon ();
	}

    //Checks every frame to see if the player has lost the level
	void CheckGameLost(){
		if (isLevelLost) {
			isLevelPaused = true;
		}
	}

    //Checks every frame to see if the player has won the level
	void CheckGameWon(){
		if (isLevelWon) {
			isLevelPaused = true;
		}
	}

    //Public function that pauses the level the player is playing. This function also fires the GUI window.
	public void PauseGame(){
		if (!isLevelPaused && !minigameOn) {
			isLevelPaused = true;
		}
		else{
			isLevelPaused = false;
		}
	}

    //Public function that begins the mini-game the player has selected. The function also fires the mini-game GUI window.
    public void startMinigame()
    {
        if (canInteract && !isLevelPaused)
        {
            minigameOn = true;
        }
    }
}
