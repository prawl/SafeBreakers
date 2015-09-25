using UnityEngine;
using System.Collections;
using Rotorz.Tile;

public class DistractionItem : MonoBehaviour {

  public GameObject playerClickedTile;
  public Vector3 currentTileLocation;
  public float itemThrowArc = 1.5f;
  public float timeToTarget = 0.18f;
  private GameObject player;
  private Vector3 itemPosition;
  private Vector3 throwSpeed;
  private Vector3 mouseClickWorldPosition;
  private GameObject[] deployItems;
  private int numberOfItems;
  private GameObject instantiatedItem;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
  void Update () {
    CountNumberOfItems("Item");
    CaptureMouseClick();

    if (ItemsInLevel() < 2 && ItemsInLevel() > 0) {
      if (Input.GetMouseButtonDown(0) && playerClickedTile != null) {
        CreateItem();
        // Invoke("DestroyAllItems", 5f);
      }
    }

    if(Input.GetKeyDown("space") ){
      DestroyAllItems();
    }

  }

  public int ItemsInLevel() {
    return numberOfItems;
  }

  // This function will not work properly unless a camera has the tag MainCamera
  private void CaptureMouseClick(){
    if(Input.GetMouseButtonDown(0) ) {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit)) {
        if (hit.transform.gameObject.tag == "Ground") {
          playerClickedTile = hit.transform.gameObject.transform.parent.gameObject;
          currentTileLocation = hit.transform.gameObject.transform.position;
        } else {
          playerClickedTile = null;
        }
      }
    }
  }

  private void DestroyAllItems() {
    if(ItemsInLevel() > 0){
      deployItems = GameObject.FindGameObjectsWithTag("Item");
      for(int i=1; i < deployItems.Length; i++) {
        Destroy(deployItems[i]);
      }
    }
  }

  private void OnCollisionEnter(Collision col) {
    if(col != null && col.gameObject.transform.parent != null && col.gameObject.transform.parent.transform.parent != null){
      playerClickedTile = col.gameObject;
    }
  }

  private void CountNumberOfItems(string tag) {
   numberOfItems = GameObject.FindGameObjectsWithTag(tag).Length;
  }

  private void CreateItem () {
    itemPosition = player.transform.position;
    itemPosition.y += .75f;
    instantiatedItem = (GameObject) Instantiate(PrefabManager.Instance.deployItem, itemPosition, player.transform.rotation);
    throwSpeed = calculateBestThrowSpeed(player.transform.position, currentTileLocation, timeToTarget);
    instantiatedItem.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.VelocityChange);
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
    float v0y = y / t + itemThrowArc * Physics.gravity.magnitude * t;
    float v0xz = xz / t;
    // create result vector for calculated starting speeds
    Vector3 result = toTargetXZ.normalized;        // get direction of xz but with magnitude 1
    result *= v0xz;                                // set magnitude of xz to v0xz (starting speed in xz plane)
    result.y = v0y;                                // set y to v0y (starting speed of y plane)
    return result;
  }
}
