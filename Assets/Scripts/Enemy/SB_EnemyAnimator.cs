using UnityEngine;
using System.Collections;

public class SB_EnemyAnimator : MonoBehaviour {

	private SB_EnemyController enemyCon;
	private Animator animator;

	// Use this for initialization
	void Start () {
		enemyCon = gameObject.GetComponent<SB_EnemyController> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameObject.FindGameObjectWithTag("Player").GetComponent<SB_GameController>().isLevelPaused && !enemyCon.moved) {
            if (enemyCon.faceUp) {
				animator.SetBool ("Up", true);
			}
            if (enemyCon.faceDown) {
				animator.SetBool ("Down", true);
			}
            if (enemyCon.faceRight) {
				animator.SetBool ("Right", true);
			}
            if (enemyCon.faceLeft) {
				animator.SetBool ("Left", true);
			}
		}
		if(enemyCon.moved) {
			animator.SetBool ("Up", false);
			animator.SetBool ("Down", false);
			animator.SetBool ("Right", false);
			animator.SetBool ("Left", false);
		}
	}
}
