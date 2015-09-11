using UnityEngine;
using System.Collections;

public class SB_PlayerAnimator : MonoBehaviour {

	private SB_PlayerController playerCon;
	private Animator animator;

	// Use this for initialization
	void Start () {
		playerCon = gameObject.GetComponent<SB_PlayerController> ();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCon.up) {
			animator.SetBool ("Up", true);
		}
		if (playerCon.down) {
			animator.SetBool ("Down", true);
		}
		if (playerCon.right) {
			animator.SetBool ("Right", true);
		}
		if (playerCon.left) {
			animator.SetBool ("Left", true);
		}
		if(!playerCon.moving) {
			animator.SetBool ("Up", false);
			animator.SetBool ("Down", false);
			animator.SetBool ("Right", false);
			animator.SetBool ("Left", false);
		}
	}
}
