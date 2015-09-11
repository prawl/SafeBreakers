using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class SB_GameController : MonoBehaviour {

	public int playerCount, enemyCount, gameCount, enemyDone;
	public bool levelWon, levelLost, levelPaused;
	public int numOfEnemies;
	public TileIndex startTile, endTile;

	// Use this for initialization
	void Start () {
		playerCount = 0;
		enemyCount = 0;
		gameCount = 1;
		levelWon = false;
		levelPaused = false;
		levelLost = false;
		numOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
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
	}
}
