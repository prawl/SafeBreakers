/*
Script Name: SB_EnemyFlashlight.cs
Author: Bradley M. Butts
Last Modified: 11-03-2015
Description: This script handles the direction and length of the enemies flashlight based on the direction and
             line of sight that the enemy has.

             *RESOLVED* CURRENT BUG: When the enemy reaches the end of their path, the flashlight automatically switches to the next direction before the enemy does.
*/

using UnityEngine;
using System.Collections;

public class SB_EnemyFlashlight : MonoBehaviour {

    private SB_EnemyController enemyCon;
    public GameObject flashlight;

	// Use this for initialization
	void Start () {
        enemyCon = gameObject.GetComponent<SB_EnemyController>();
	}
	
	// Update is called once per frame. Based on the direction the player is looking, the flashlight is rotated and transformed to the correct direction
	void Update () {
        if (enemyCon.gameCon.enemyCount < enemyCon.gameCon.playerCount)
        {
            if (enemyCon.faceUp)
            {
                Quaternion tempRot = new Quaternion();
                tempRot.x = 0; tempRot.y = 90; tempRot.z = -80;
                flashlight.gameObject.transform.rotation = Quaternion.Euler(tempRot.x, tempRot.y, tempRot.z);
            }
            if (enemyCon.faceDown)
            {
                Quaternion tempRot = new Quaternion();
                tempRot.x = 0; tempRot.y = 270; tempRot.z = -80;
                flashlight.gameObject.transform.rotation = Quaternion.Euler(tempRot.x, tempRot.y, tempRot.z);
            }
            if (enemyCon.faceRight)
            {
                Quaternion tempRot = new Quaternion();
                tempRot.x = 0; tempRot.y = 180; tempRot.z = -80;
                flashlight.gameObject.transform.rotation = Quaternion.Euler(tempRot.x, tempRot.y, tempRot.z);
            }
            if (enemyCon.faceLeft)
            {
                Quaternion tempRot = new Quaternion();
                tempRot.x = 0; tempRot.y = 0; tempRot.z = -80;
                flashlight.gameObject.transform.rotation = Quaternion.Euler(tempRot.x, tempRot.y, tempRot.z);
            }
        } 
	}
}
