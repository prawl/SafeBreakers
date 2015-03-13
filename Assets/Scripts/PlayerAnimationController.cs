using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	public GameObject player;
	public PlayerController playerController;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateAnimation ();
	}

	void UpdateAnimation(){
		if (Mathf.Abs(playerController.temp.x) > Mathf.Abs(player.transform.position.x) && PlayerController.move) {
			playerController.playerAnimator.SetBool ("Walk_Right", true);
			playerController.playerAnimator.SetBool ("Walk_Left", false);
			playerController.playerAnimator.SetBool ("Walk_Forward", false);
			playerController.playerAnimator.SetBool ("Walk_Back", false);
		}
		if (Mathf.Abs(playerController.temp.x) < Mathf.Abs(player.transform.position.x) && PlayerController.move) {
			playerController.playerAnimator.SetBool ("Walk_Right", false);
			playerController.playerAnimator.SetBool ("Walk_Left", true);
			playerController.playerAnimator.SetBool ("Walk_Forward", false);
			playerController.playerAnimator.SetBool ("Walk_Back", false);
		}
		if (Mathf.Abs(playerController.temp.y) > Mathf.Abs(player.transform.position.y) && PlayerController.move) {
			playerController.playerAnimator.SetBool ("Walk_Right", false);
			playerController.playerAnimator.SetBool ("Walk_Left", false);
			playerController.playerAnimator.SetBool ("Walk_Forward", true);
			playerController.playerAnimator.SetBool ("Walk_Back", false);
		}
		if (Mathf.Abs(playerController.temp.y) < Mathf.Abs(player.transform.position.y) && PlayerController.move) {
			playerController.playerAnimator.SetBool ("Walk_Right", false);
			playerController.playerAnimator.SetBool ("Walk_Left", false);
			playerController.playerAnimator.SetBool ("Walk_Forward", false);
			playerController.playerAnimator.SetBool ("Walk_Back", true);
		}
		if (!PlayerController.move) {
			playerController.playerAnimator.SetBool ("Walk_Right", false);
			playerController.playerAnimator.SetBool ("Walk_Left", false);
			playerController.playerAnimator.SetBool ("Walk_Forward", false);
			playerController.playerAnimator.SetBool ("Walk_Back", false);
		}
		
	}
}
