using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class GameController : MonoBehaviour {

	public static int gameCount;
	public static int enemyCount;
	public bool enemiesDone;
	public GameObject[] guards;
	public static int doneMoving;
	public static int numEnemies;

	// Use this for initialization
	void Start () {
		gameCount = 0;
		enemyCount = gameCount;
		guards = GameObject.FindGameObjectsWithTag ("Enemy");
		enemiesDone = true;
		numEnemies = guards.Length;
		doneMoving = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameCount > enemyCount) {
			if(doneMoving == numEnemies){
				enemyCount++;
				doneMoving = 0;
			}
		}
	}
}
