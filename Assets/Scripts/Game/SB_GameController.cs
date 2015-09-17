using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_GameController : MonoBehaviour {

	public int playerCount, enemyCount, gameCount, enemyDone;
	public GameObject[] enemies;
	public bool isLevelWon, isLevelLost, isLevelPaused;
	public int numOfEnemies;
	public TileIndex startTile, endTile;

	// Use this for initialization
	void Start () {
		playerCount = 0;
		enemyCount = 0;
		gameCount = 1;
		isLevelWon = false;
		isLevelPaused = false;
		isLevelLost = false;
		numOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyDone == numOfEnemies && enemyCount < playerCount) {
			enemyDone = 0;
			enemyCount++;
		}
		if(enemyCount == playerCount && playerCount == gameCount){
			gameCount++;
		}
		CheckGameLost ();
		CheckGameWon ();
	}

	void CheckGameLost(){
		if (isLevelLost) {
			isLevelPaused = true;
		}
	}

	void CheckGameWon(){
		if (isLevelWon) {
			isLevelPaused = true;
		}
	}

	public void PauseGame(){
		if (!isLevelPaused) {
			isLevelPaused = true;
		}
		else{
			isLevelPaused = false;
		}
	}
}
