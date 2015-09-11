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
		if (!enemyCon.moved) {
			if (enemyCon.up) {
				animator.SetBool ("Up", true);
			}
			if (enemyCon.down) {
				animator.SetBool ("Down", true);
			}
			if (enemyCon.right) {
				animator.SetBool ("Right", true);
			}
			if (enemyCon.left) {
				animator.SetBool ("Left", true);
			}
		}
		else {
			animator.SetBool ("Up", false);
			animator.SetBool ("Down", false);
			animator.SetBool ("Right", false);
			animator.SetBool ("Left", false);
		}
	}
}
