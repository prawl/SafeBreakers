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
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy.gameObject.transform.position = movementScript.startPos;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void MoveToLocation(){

	}
}
