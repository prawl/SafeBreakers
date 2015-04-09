using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using Rotorz.Tile.Internal;

public class EnemyController : MonoBehaviour {

	public GameObject enemy, player;
	public float speed;
	private Animator enemyAnimator = null;
	public EnemyMovement movementScript;

	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponent<Animator>();
		enemy.transform.position = movementScript.startPos;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void MoveToLocation(){

	}
}
