using UnityEngine;
using System.Collections;

public class DistractionItem : MonoBehaviour {

  private GameObject player;
  private Vector3 itemPosition;
  public GameObject deployItem;
  public GameObject itemIsTouchingTile;

	void Initialize() {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Start () {
    Initialize();
    CreateItem();
	}
	
	void Update () {
	}

  void OnTriggerEnter(Collider col) {
    CaptureTileTouchingItem(col);
  }

  void CaptureTileTouchingItem(Collider col){
    if(col.gameObject.transform.parent.gameObject.name != null){
      itemIsTouchingTile = col.gameObject.transform.parent.gameObject;
    }
  }

  void CreateItem () {
    itemPosition = player.transform.position;
    itemPosition.x += .25f;
    Instantiate(deployItem, itemPosition, player.transform.rotation);
  }
}
