/*
Script Name: SB_PlayerAnimator.cs
Author: Bradley M. Butts
Last Modified: 9-12-2015
Description: This script handles the animations for the player's character in the game.
             Using a series of bool items inside the Animator attached to the player's gameObject,
             it'll calculate which animations to use for which direction the player is moving, and whether
             or not the player is moving.
*/

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
	
	// Based on the SB_PlayerController's bool values for "Up", "Down", "Right", and "Left"...
    // We are turning on the corresponding bool inside of the animator to trigger the respective animation
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
