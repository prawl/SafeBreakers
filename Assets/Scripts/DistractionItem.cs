using UnityEngine;
using System.Collections;

public class DistractionItem : MonoBehaviour {

  private GameObject player;
  private Vector3 itemPosition;
  private Vector3 throwSpeed;
  private Vector3 mouseClickWorldPosition;
  private float startTime;
  public GameObject deployItem;
  public GameObject itemIsTouchingTile;
  public bool itemExistInGame;
  private GameObject[] deployItems;
  private float waitUntilThisTime;
  private float numberOfSeconds = 3f;


  public float temp = 0.5f;
  public float temp2 = 2f;

	void Initialize() {
		player = GameObject.FindGameObjectWithTag ("Player");
    itemExistInGame = false;
    waitUntilThisTime = 0f;
	}

	void Start () {
    Initialize();
	}
	
	void Update () {
    if (SecondsHavePassed() == true) {
      print("true");
      if (Input.GetMouseButtonDown(0)) {
         CreateItem();
        waitUntilThisTime = Time.time + numberOfSeconds;
        CaptureMouseClick();
        throwSpeed = calculateBestThrowSpeed(player.transform.position, mouseClickWorldPosition, temp2);
        GameObject.Find("deploy_item(Clone)").GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.VelocityChange);
      }
    } else {
      print("false");
    }
    // print(itemIsTouchingTile);
	}

  bool SecondsHavePassed(){
    if(waitUntilThisTime < Time.time){
      return true;
    } else {
      return false;
    }
  }

  void OnCollisionEnter(Collision col) {
    if(col != null && col.gameObject.transform.parent != null && col.gameObject.transform.parent.transform.parent != null){
      itemIsTouchingTile = col.gameObject;
    }
  }

  // Borrowed from http://answers.unity3d.com/questions/248788/calculating-ball-trajectory-in-full-3d-world.html
  private Vector3 calculateBestThrowSpeed(Vector3 origin, Vector3 target, float timeToTarget) {
    // calculate vectors
    Vector3 toTarget = target - origin;
    Vector3 toTargetXZ = toTarget;
    toTargetXZ.y = 0;

    // calculate xz and y
    float y = toTarget.y;
    float xz = toTargetXZ.magnitude;

    // calculate starting speeds for xz and y. Physics forumulase deltaX = v0 * t + 1/2 * a * t * t
    // where a is "-gravity" but only on the y plane, and a is 0 in xz plane.
    // so xz = v0xz * t => v0xz = xz / t
    // and y = v0y * t - 1/2 * gravity * t * t => v0y * t = y + 1/2 * gravity * t * t => v0y = y / t + 1/2 * gravity * t
    float t = timeToTarget;
    float v0y = y / t + temp * Physics.gravity.magnitude * t;
    float v0xz = xz / t;
    // create result vector for calculated starting speeds
    Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
    result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
    result.y = v0y;                                // set y to v0y (starting speed of y plane)
    return result;
  }

  // This function will not work properly unless a camera has the tag MainCamera
  void CaptureMouseClick(){
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit)) {
      mouseClickWorldPosition = hit.point;
    }
  }

  void CreateItem () {
    itemPosition = player.transform.position;
    itemPosition.y += .75f;
    Instantiate(deployItem, itemPosition, player.transform.rotation);
    ActivateItem();
    // Invoke("DestroyItem", 5f);
  }

  void DestroyItem() {
    if(ItemActive()){
      deployItems = GameObject.FindGameObjectsWithTag("Item");
      for(int i=1; i < deployItems.Length; i++) {
        Destroy(deployItems[i]);
      }
      DeactivateItem();
    }
  }

  bool ItemActive(){
    return itemExistInGame;
  }

  void ActivateItem(){
    itemExistInGame = true;
  }

  void DeactivateItem(){
    itemExistInGame = false;
  }
}
