using UnityEngine;
using System.Collections;

public class EnemyAnimationController : MonoBehaviour {

	public EnemyController enemyCon;
	private Animator enemyAnimator = null;
	
	// Use this for initialization
	void Start () {
		enemyAnimator = GetComponent<Animator>();
		enemyCon = GetComponent<EnemyController> ();
		StartDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAnimation ();
	}

	void StartDirection(){
		if(enemyCon.down){
			enemyAnimator.SetBool ("Down", true);
			enemyAnimator.SetBool ("Down", false);
		}
		if(enemyCon.up){
			enemyAnimator.SetBool ("Up", true);
			enemyAnimator.SetBool ("Up", false);
		}
		if(enemyCon.right){
			enemyAnimator.SetBool ("Right", true);
			enemyAnimator.SetBool ("Right", false);
		}
		if(enemyCon.left){
			enemyAnimator.SetBool ("Left", true);
			enemyAnimator.SetBool ("Left", false);
		}
	}

	void UpdateAnimation(){
		if(enemyCon.up){
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Down", false);
			enemyAnimator.SetBool ("Up", true);
		}
		if(enemyCon.down){
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Down", true);
			enemyAnimator.SetBool ("Up", false);
		}
		if(enemyCon.left){
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", true);
			enemyAnimator.SetBool ("Down", false);
			enemyAnimator.SetBool ("Up", false);
		}
		if(enemyCon.right){
			enemyAnimator.SetBool ("Right", true);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Down", false);
			enemyAnimator.SetBool ("Up", false);
		}
		else{
			enemyAnimator.SetBool ("Right", false);
			enemyAnimator.SetBool ("Left", false);
			enemyAnimator.SetBool ("Down", false);
			enemyAnimator.SetBool ("Up", false);
		}
	}
}
