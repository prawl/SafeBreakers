using UnityEngine;
using System.Collections;

public class SB_EnemyFlashlight : MonoBehaviour {

    private SB_EnemyController enemyCon;
    public GameObject flashlight;

	// Use this for initialization
	void Start () {
        enemyCon = gameObject.GetComponent<SB_EnemyController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (enemyCon.faceUp)
        {
            Quaternion tempRot = new Quaternion();
            tempRot.x = 0;tempRot.y = 90;tempRot.z = -80;
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
