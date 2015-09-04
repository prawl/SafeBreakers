using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class New_GameController : MonoBehaviour {

	public static int playerCount, enemyCount, gameCount, enemyDone;
	public static bool levelWon, levelLost, levelPaused;
	public int numOfEnemies;
	public TileIndex startTile, endTile;
	public GameObject player;

	// Use this for initialization
	public void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerCount = 0;
		numOfEnemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		enemyCount = 0;
		gameCount = 1;
	}

	public static void Restart(){
		//player.gameObject.GetComponent<New_GameController> ().Start ();
		//Start ();
		print ("You lose");
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyDone == numOfEnemies) {
			enemyDone = 0;
			enemyCount++;
		}
		if (playerCount == gameCount && enemyCount < gameCount) {

		}
		if(enemyCount == gameCount && playerCount == gameCount){
			gameCount++;
		}
	}
}
