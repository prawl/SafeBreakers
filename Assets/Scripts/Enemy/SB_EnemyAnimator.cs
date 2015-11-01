using UnityEngine;
using System.Collections;

public class SB_EnemyAnimator : MonoBehaviour {

	private SB_EnemyController enemyCon;
    private SB_EnemyAlarmed alarmed;
	public Animator animator;

	// Use this for initialization
	void Start () {
		enemyCon = gameObject.GetComponent<SB_EnemyController> ();
        alarmed = gameObject.GetComponent<SB_EnemyAlarmed>();
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (!enemyCon.isAlarmed)
        {
            if (!GameObject.FindGameObjectWithTag("Player").GetComponent<SB_GameController>().isLevelPaused && !enemyCon.moved)
            {
                if (enemyCon.faceUp)
                {
                    animator.SetBool("Walk_Up", true);
                }
                if (enemyCon.faceDown)
                {
                    animator.SetBool("Walk_Down", true);
                }
                if (enemyCon.faceRight)
                {
                    animator.SetBool("Walk_Right", true);
                }
                if (enemyCon.faceLeft)
                {
                    animator.SetBool("Walk_Left", true);
                }
            }
        }
        if(enemyCon.isAlarmed && enemyCon.gameCon.playerCount > enemyCon.gameCon.enemyCount)
        {
            if (!alarmed.isClosest)
            {
                if (alarmed.checkingUp)
                {
                    animator.SetBool("Turn_Up", true);
                }
                if (alarmed.checkingDown)
                {
                    animator.SetBool("Turn_Down", true);
                }
                if (alarmed.checkingRight)
                {
                    animator.SetBool("Turn_Right", true);
                }
                if (alarmed.checkingLeft)
                {
                    animator.SetBool("Turn_Left", true);
                }
            }
        }
		if(enemyCon.gameCon.playerCount == enemyCon.gameCon.enemyCount) {
			animator.SetBool ("Walk_Up", false);
			animator.SetBool ("Walk_Down", false);
			animator.SetBool ("Walk_Right", false);
			animator.SetBool ("Walk_Left", false);
            animator.SetBool("Turn_Up", false);
            animator.SetBool("Turn_Down", false);
            animator.SetBool("Turn_Right", false);
            animator.SetBool("Turn_Left", false);
        }
	}
}
