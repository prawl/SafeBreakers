using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DistractionItem : MonoBehaviour {

  public GameObject playerClickedTile;
  public Vector3 currentTileLocation;
  public int numberOfItems;
  public AudioClip impact;
  public Button rockButton;
  private float itemThrowArc = 1.5f;
  private float timeToTarget = 0.18f;
  private GameObject player;

  private Text guiCounter;

  private Vector3 itemPosition;
  private Vector3 throwSpeed;
  private GameObject[] deployItems;
  private GameObject instantiatedItem;
  private int readyToThrow = 0;
  private int maxNumberOfDeployItems = 5;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		// guiCounter = GameObject.Find ("GUI_Rock_Counter");
    guiCounter = GameObject.Find("GUI_Rock_Counter").GetComponent<Text>();
	}
	

  private void NumberOfRocksLeft(){
    int rocksActive = (maxNumberOfDeployItems + 1) - numberOfItems;
    guiCounter.text = rocksActive.ToString();
  }

  void Update () {
    CountNumberOfItems("Item");
    NumberOfRocksLeft();
    ToggleButton();

    if (Input.GetMouseButtonDown(0)) {
      CaptureMouseClick();
      if (ItemsInLevel() <= maxNumberOfDeployItems && ItemsInLevel() > 0) {
        if (currentTileLocation != Vector3.zero && DeployItem() == 1) {
          CreateItem();
          DisableDeployItem();
        }
        // Invoke("DestroyAllItems", 5f);
      }
    }

    if(Input.GetKeyDown("space") ){
      DestroyAllItems();
      ResetTileLocation();
    }
  }

  public void ToggleButton(){
    if(ItemsInLevel() > maxNumberOfDeployItems){
      DisableButton();
    } else {
      EnableButton();
    }
  }

  public void OnClick(){
    EnableDeployItem();
  }

  public int DeployItem(){
    return readyToThrow;
  }

  public void EnableDeployItem(){
    readyToThrow = 1;
  }

  public void DisableDeployItem(){
    readyToThrow = 0;
  }

  public void OnBecameInvisible(){
    DestroyAllItems();
  }

  public int ItemsInLevel() {
    return numberOfItems;
  }

  public void CountNumberOfItems(string tag) {
   numberOfItems = GameObject.FindGameObjectsWithTag(tag).Length;
  }

  // This function will not work properly unless a camera has the tag MainCamera
  private void CaptureMouseClick(){
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out hit)) {
      if (hit.transform.gameObject.tag == "Ground") {
        playerClickedTile = hit.transform.gameObject.transform.parent.gameObject;
        currentTileLocation = hit.transform.gameObject.transform.position;
      }
    }
  }

  private void DestroyAllItems() {
    if(ItemsInLevel() > 0){
      GetDeployItems();
      for(int i=1; i < deployItems.Length; i++) {
        Destroy(deployItems[i]);
      }
    }
  }

  private void DisableButton(){
    rockButton.interactable = false;
  }

  private void EnableButton(){
    rockButton.interactable = true;
  }

  private void GetDeployItems() {
    deployItems = GameObject.FindGameObjectsWithTag("Item");
  }

  private void OnCollisionEnter(Collision col) {
    GetDeployItems();
    int size = new int();
    size = deployItems.Length;
    instantiatedItem = deployItems[size-1];
    if(size > 1){
      if(col.transform.gameObject.name == "Grass_Floor"){
        instantiatedItem.GetComponent<AudioSource>().Play();
      }else if (col.transform.gameObject.name == "Tree") {
        print("Hit tree!");
      }
    }
  }

  private void CreateItem () {
    itemPosition = player.transform.position;
    itemPosition.y += .75f;
    instantiatedItem = (GameObject) Instantiate(PrefabManager.Instance.deployItem, itemPosition, player.transform.rotation);
    throwSpeed = calculateBestThrowSpeed(player.transform.position, currentTileLocation, timeToTarget);
    instantiatedItem.GetComponent<Rigidbody>().AddForce(throwSpeed, ForceMode.VelocityChange);
  }

  // When we don't want the player to throw an item unless they click a tile.  In order to enforce this
  // we set the currentTileLocation to 0,0,0 to disable throwing when you click on nothing.
  private void ResetTileLocation(){
    currentTileLocation = Vector3.zero;
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
