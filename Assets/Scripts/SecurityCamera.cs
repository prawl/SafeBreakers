using UnityEngine;
using System.Collections;

public class SecurityCamera : MonoBehaviour {

	public GameObject securityCamera;
	private Animator cameraAnimator = null;
	public bool animate;
	public int moving;
	public bool start;

	// Use this for initialization
	void Start () {
		animate = false;
		cameraAnimator = GetComponent<Animator>();
		start = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.enemyCount < GameController.gameCount && moving == 0) {
			if(GameController.doneMoving < GameController.numEnemies){
				GameController.doneMoving++;
				moving++;
				animate = true;
			}
		}

		if (GameController.enemyCount == GameController.gameCount) {
			moving = 0;
		}
		AnimationController ();
	}

	IEnumerator left(){
		cameraAnimator.SetBool ("MoveLeft", true);
		yield return new WaitForSeconds (1.0f);
		cameraAnimator.SetBool ("MoveLeft", false);
		start = false;
		animate = false;
	}

	IEnumerator right(){
		cameraAnimator.SetBool ("MoveRight", true);
		yield return new WaitForSeconds (1.0f);
		cameraAnimator.SetBool ("MoveRight", false);
		start = true;
		animate = false;
	}

	void AnimationController(){
		if(animate){
			if(start){
				StartCoroutine (left ());
			}
			else{
				StartCoroutine (right ());
			}
		}
	}
}
